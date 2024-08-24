using System.Text.Json;
using System.Text.Json.Serialization;

using Confluent.Kafka;

namespace ConsumerQueue;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;
    ConsumerConfig _consumerConfig;
    public double Min = 0;
    public double Max = 0;
    public long Count = 0;

    public List<double> DataSet = new List<double>();

    public Worker(ILogger<Worker> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    private double CalculateDelay(Timestamp ts)
    {
        var delay = DateTimeOffset.UtcNow - DateTimeOffset.FromUnixTimeMilliseconds(ts.UnixTimestampMs);
        return delay.TotalMilliseconds;
    }

    private void AddToTrack(double delay)
    {
        DataSet.Add(delay);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            var appSettings =  _config.GetSection("AppSettings");
            var brokerList = appSettings["BrokerList"];
            var topic = appSettings["Topic"];

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SocketTimeoutMs = 60000,                //this corresponds to the Consumer config `request.timeout.ms`
                SessionTimeoutMs = 30000,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = _config.GetConnectionString("EventHub"),
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = true,
                MaxPartitionFetchBytes = int.Parse(appSettings["MaxPartitionFetchBytes"]),
                FetchWaitMaxMs = int.Parse(appSettings["FetchWaitMaxMs"]),
                FetchMaxBytes = int.Parse(appSettings["FetchMaxBytes"]),
                BrokerVersionFallback = "1.0.0",        //Event Hubs for Kafka Ecosystems supports Kafka v1.0+, a fallback to an older API will fail           //Debug = "security,broker,protocol"    //Uncomment for librdkafka debugging information
            };
            using var consumer = new ConsumerBuilder<int, string>(_consumerConfig).SetKeyDeserializer(Deserializers.Int32).SetValueDeserializer(Deserializers.Utf8).Build();

            consumer.Subscribe(topic);
            _logger.LogInformation("Consuming messages from topic: {topic}, broker(s): {brokerList}", topic, brokerList);

            _logger.LogInformation("count, cur, min, max, avg");

            double min = double.MaxValue;
            double max = double.MinValue;
            double cur = 0;
            double avg = 0;

            ConsumeResult<int, string>? msg = null;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    msg = consumer.Consume(10);
                    if (msg == null) continue;

                    cur = CalculateDelay(msg.Message.Timestamp);
                    AddToTrack(cur);

                    Count++;

                    if (cur < min) min = cur;
                    if (cur > max) max = cur;

                    if (min < Min) Min = min;
                    if (max > Max) Max = max;

                    avg = ((Count - 1) * avg + cur) / Count;

                    if (Count % 1000 == 0)
                    {
                        _logger.LogInformation("{Count}, {cur}, {min}, {max}, {avg}", Count, cur, min, max, avg);
                        min = double.MaxValue;
                        max = double.MinValue;
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError(e, "Consume error");
                }
                catch (OperationCanceledException)
                {
                }
            }
        }, stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        double[] percentiles = CalculatePercentile(DataSet, 0.75, 0.95, 0.99);

        _logger.LogInformation("Finishing Program");
        _logger.LogInformation(@"Count, Min, Max, Media, 75th, 95th, 99th");
        _logger.LogInformation($"{Count}, {Min}, {Max}, {percentiles[0]}, {percentiles[1]}, {percentiles[2]} ");

        string Json = JsonSerializer.Serialize(_consumerConfig);

        _logger.LogInformation("Configurações: ");
        _logger.LogInformation(Json);

        return Task.CompletedTask;
    }

    // Should be sorted
    private double[] CalculatePercentile(IEnumerable<double> entry, params double[] percentiles)
    {
        var elements = entry.OrderBy(x => x).ToArray();
        int N = elements.Length;

        for (int i = 0; i < percentiles.Length; i++)
        {
            double excelPercentile = percentiles[i];


            double realIndex = excelPercentile * (elements.Length - 1);
            int index = (int)realIndex;
            double frac = realIndex - index;

            if (index + 1 < elements.Length)
                percentiles[i] = elements[index] * (1 - frac) + elements[index + 1] * frac;
            else
                percentiles[i] = elements[index];

        }

        return percentiles;
    }
}
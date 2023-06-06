using Confluent.Kafka;

namespace ConsumerQueue;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;

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

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            var appSettings = _config.GetSection("AppSettings");
            var brokerList = appSettings["BrokerList"];
            var topic = appSettings["Topic"];
            var config = new ConsumerConfig
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
                MaxPartitionFetchBytes = 1048576,
                FetchWaitMaxMs = 10,
                BrokerVersionFallback = "1.0.0",        //Event Hubs for Kafka Ecosystems supports Kafka v1.0+, a fallback to an older API will fail
                                                        //Debug = "security,broker,protocol"    //Uncomment for librdkafka debugging information
            };
            using var consumer = new ConsumerBuilder<int, string>(config).SetKeyDeserializer(Deserializers.Int32).SetValueDeserializer(Deserializers.Utf8).Build();

            consumer.Subscribe(topic);
            _logger.LogInformation("Consuming messages from topic: {topic}, broker(s): {brokerList}", topic, brokerList);

            _logger.LogInformation("count, cur, min, max, avg");
            long count = 0;
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
                    count++;
                    if (cur < min) min = cur;
                    if (cur > max) max = cur;
                    avg = ((count - 1) * avg + cur) / count;
                    if (count % 1000 == 0)
                    {
                        _logger.LogInformation("{count}, {cur}, {min}, {max}, {avg}", count, cur, min, max, avg);
                        min = 0;
                        max = 0;
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
}
using System.Diagnostics;
using System.Text.Json;

using Confluent.Kafka;

namespace KafkaProducer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;

    public Worker(ILogger<Worker> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            var appSettings = _config.GetSection("AppSettings");

            var config = new ProducerConfig
            {
                ConnectionsMaxIdleMs = 180000,
                MetadataMaxAgeMs = 180000,

                BootstrapServers = appSettings["BrokerList"],
                SaslUsername = "$ConnectionString",
                SaslPassword = _config.GetConnectionString("EventHub"),

                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,

                CompressionType = CompressionType.None,
                Acks = 0,
                LingerMs = Double.Parse(appSettings["LingerMS"]),
                BatchSize = int.Parse(appSettings["BatchZize"]),
                SocketNagleDisable = bool.Parse(appSettings["SocketNagleDisable"]),
            };

            using var producer = new ProducerBuilder<int, string>(config).SetKeySerializer(Serializers.Int32).SetValueSerializer(Serializers.Utf8).Build();
            int counter = 0;
            var message = new Message<int, string>
            {
                Key = counter,
                Value = new string('a', 1024),
            };

            int limit = string.IsNullOrEmpty(appSettings["NumMessages"]) ? 100000 : Convert.ToInt32(appSettings["NumMessages"]);
            var topic = appSettings["Topic"];
            int mps = string.IsNullOrEmpty(appSettings["Mps"]) ? 1000 : Convert.ToInt32(appSettings["Mps"]);


            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    producer.Produce(topic, message);
                    counter++;

                    if (counter % mps == 0)
                    {
                        producer.Flush();
                        sw.Stop();
                        int remaining = 1000 - (int)sw.ElapsedMilliseconds;

                        _logger.LogInformation($"Produced {counter} messages, sleeping {remaining}ms");

                        if (remaining > 0)
                        {
                            await Task.Delay(remaining);
                        }
                        sw.Restart();

                        if (counter >= limit) stoppingToken.ThrowIfCancellationRequested();
                    }
                }
                catch (ProduceException<int, string> ex)
                {
                    _logger.LogError(ex, null);
                    producer.Flush();
                }
                catch (OperationCanceledException)
                {
                }
            }

            string Json = JsonSerializer.Serialize(config);

            _logger.LogInformation("Configurações: ");
            _logger.LogInformation(Json);

        }, stoppingToken);
    }
}
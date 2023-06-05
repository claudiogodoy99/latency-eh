using System.Diagnostics;

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
                BootstrapServers = appSettings["BrokerList"],
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "$ConnectionString",
                SaslPassword = _config.GetConnectionString("EventHub"),
                // EnableIdempotence = true,
                CompressionType = CompressionType.None,
                Acks = Acks.All,
                LingerMs = 1,
                BatchSize = 131072,
            };
            using var producer = new ProducerBuilder<int, string>(config).SetKeySerializer(Serializers.Int32).SetValueSerializer(Serializers.Utf8).Build();
            int counter = 0;
            var topic = appSettings["Topic"];
            var message = new Message<int, string>
            {
                Key = counter,
                Value = new string('a', 1024),
            };
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    producer.Produce(topic, message);
                    counter++;
                    if (counter % 1000 == 0)
                    {
                        producer.Flush();
                        sw.Stop();
                        int remaining = 1000 - (int)sw.ElapsedMilliseconds;
                        _logger.LogInformation("Produced {counter} messages, sleeping {remaining}ms", counter, remaining);
                        if (remaining > 0) {
                            await Task.Delay(remaining);
                        }
                        sw.Restart();
                    }
                }
                catch (ProduceException<int, string> ex)
                {
                    _logger.LogError(ex, null);
                    producer.Flush();
                }
                catch (OperationCanceledException) {
                }
            }
        }, stoppingToken);
    }
}
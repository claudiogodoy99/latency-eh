# Teste de latencia

O objeto deste projeto é testar a latencia de consumo de eventos do Event Hubs. Visando evidênciar diferentes configurações, e também estratégias de pre-fetching da biblioteca Confluent Kafka.

> Teste com duas VM's e 1 PU Event Hub Premium

 

## Configs:

```dotnet
new ConsumerConfig
{
    BootstrapServers = brokerList,
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SocketTimeoutMs = 60000,
    SessionTimeoutMs = 30000,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "$ConnectionString",
    SaslPassword = _config.GetConnectionString("EventHub"),
    GroupId = Guid.NewGuid().ToString(),
    AutoOffsetReset = AutoOffsetReset.Latest,
    EnableAutoCommit = false,
    MaxPartitionFetchBytes = 1048576,
    FetchWaitMaxMs = 10,
    BrokerVersionFallback = "1.0.0"
};

new ProducerConfig
{
    BootstrapServers = appSettings["BrokerList"],
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "$ConnectionString",
    SaslPassword = _config.GetConnectionString("EventHub"),
    EnableIdempotence = true,
    CompressionType = CompressionType.None,
    Acks = Acks.All,
    LingerMs = 1,
    BatchSize = 131072,
};  
```
### Fecthing Nativo

```
  count, cur, min, max, avg
info: ConsumerQueue.Worker[0]
      1000, 67.3703, 9.4792, 190.2476, 47.11578150000002
info: ConsumerQueue.Worker[0]
      2000, 21.7011, 9.4792, 190.2476, 32.04222959999999
info: ConsumerQueue.Worker[0]
      3000, 8.9188, 8.4299, 190.2476, 26.264371933333294
info: ConsumerQueue.Worker[0]
      4000, 14.3735, 8.4299, 190.2476, 24.839196875000013
info: ConsumerQueue.Worker[0]
      5000, 16.9443, 8.4299, 190.2476, 24.11480006000005
info: ConsumerQueue.Worker[0]
      6000, 11.1306, 8.4299, 190.2476, 23.064369316666703
info: ConsumerQueue.Worker[0]
      7000, 15.2766, 8.4299, 190.2476, 22.363327442857184
info: ConsumerQueue.Worker[0]
      8000, 17.9098, 8.4299, 190.2476, 21.63542925000003
info: ConsumerQueue.Worker[0]
      9000, 10.8259, 8.4299, 190.2476, 21.055779322222236
info: ConsumerQueue.Worker[0]
      10000, 24.1784, 8.4299, 190.2476, 20.67933485999996
```

### Fecthing Queue

```
 count, cur, min, max, avg
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/claudio/latency-eh/consumerQueue
info: consumerQueue.Worker[0]
      Consuming messages from topic: evh-eventhub, broker(s): eh-eastus-godoy.servicebus.windows.net:9093
info: consumerQueue.Worker[0]
      1000, 16.8385, 12.9123, 39.6056, 22.70578330000003
info: consumerQueue.Worker[0]
      2000, 11.4724, 8.6915, 39.6056, 20.028684050000024
info: consumerQueue.Worker[0]
      3000, 20.5556, 8.6915, 39.6056, 18.90219653333335
info: consumerQueue.Worker[0]
      4000, 16.1921, 8.6915, 39.6056, 18.473444024999992
info: consumerQueue.Worker[0]
      5000, 11.9242, 8.6915, 39.6056, 17.936997800000032
info: consumerQueue.Worker[0]
      6000, 10.9101, 8.6045, 39.6056, 17.384108050000012
info: consumerQueue.Worker[0]
      7000, 18.139, 8.6045, 39.6056, 17.35734808571434
info: consumerQueue.Worker[0]
      8000, 10.2768, 7.9328, 39.6056, 17.058727512500056
info: consumerQueue.Worker[0]
      9000, 18.9272, 7.9328, 39.6056, 16.98987662222224
info: consumerQueue.Worker[0]
      10000, 15.7751, 7.9328, 39.6056, 16.99898540000009
```
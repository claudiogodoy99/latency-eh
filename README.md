# Teste de latencia

O objeto deste projeto é testar a latencia de consumo de eventos do Event Hubs. Visando evidênciar diferentes configurações, e também estratégias de pre-fetching da biblioteca Confluent Kafka.

## Teste Pre-Fetching Nativo - Enable Idepontence False

Consumer Configs:

```csharp
var config = new ConsumerConfig
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
    BrokerVersionFallback = "1.0.0",
};
```

Producer Configs:

```csharp
var config = new ProducerConfig
{
    BootstrapServers = appSettings["BrokerList"],
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "$ConnectionString",
    SaslPassword = _config.GetConnectionString("EventHub"),
    CompressionType = CompressionType.None,
    Acks = Acks.All,
    LingerMs = 1,
    BatchSize = 131072,
};
```

Resultado:

```
|      count |      cur |      min |      max |          avg |
|------------|----------|----------|----------|--------------|
|       1000 |  86.9065 |  48.6305 |  86.9065 |  78.0661058  |
|       2000 |  38.6433 |   14.499 |  86.9065 |  56.90940015 |
|       3000 |  39.6201 |   14.499 |  86.9065 |  48.9328928  |
|       4000 |  48.8747 |   14.499 |  86.9065 |  46.9121137  |
|       5000 |  92.4052 |   14.499 |  92.4052 |  51.11330526 |
|       6000 | 116.8166 |   14.499 | 116.8166 |  49.4159396  |
|       7000 |  27.6042 |   14.499 | 116.8166 |  46.7571021  |
|       8000 |  26.4324 |  14.0269 | 116.8166 |  49.01877265 |
|       9000 |  700.4539|  14.0269 |  730.5911|  88.64428973 |
|      10000 |  293.1356|  14.0269 |  730.5911|  115.0475085 |
|      11000 |  97.1239 |  14.0269 |  730.5911|  108.9649369 |
|      12000 |  14.8134 |  14.0269 |  730.5911|  101.8096759 |
|      13000 | 117.4587 |  10.8167 |  730.5911|  100.3120682 |
|      14000 |  37.0538 |  10.8167 |  730.5911|  95.63937292 |
|      15000 |  68.1744 |  10.8167 |  730.5911|  92.81201111 |
|      16000 |  81.8744 |  10.8167 |  730.5911|  89.80777061 |
|      17000 |  41.6822 |  10.8167 |  730.5911|  86.79923282 |
|      18000 |  36.9559 |  10.8167 |  730.5911|  83.74412224 |
|      19000 |  24.2858 |  10.8167 |  730.5911|  80.70249038 |
|      20000 |  32.2946 |  10.8167 |  730.5911|  78.16215818 |
|      21000 |  49.1812 |  10.8167 |  730.5911|  75.58628079 |
|      22000 |  41.274  |  10.8167 |  730.5911|  73.38582745 |
|      23000 |  33.9611 |  10.8167 |  730.5911|  71.27361727 |
|      24000 |  20.8247 |  10.8167 |  730.5911|  69.22819962 |
|      25000 |  35.2167 |  10.8167 |  730.5911|  67.43868941 |
|      26000 |  14.869  |  10.8167 |  730.5911|  65.63362508 |
|      27000 |  39.0086 |  10.8167 |  730.5911|  63.98675197 |
|      28000 |  25.1739 |  10.8167 |  730.5911|  62.44155065 |
|      29000 |  14.6659 |  10.8167 |  730.5911|  60.99547309 |
|      30000 |  24.8215 |  10.8167 |  730.5911|  59.65743773 |
|      31000 |  14.9062 |  10.8167 |  730.5911|  58.39331901 |
|      32000 |  16.2281 |  10.8167 |  730.5911|  57.19512614 |
|      33000 |  22.7062 |  10.8167 |  730.5911|  56.06388814 |
|      34000 |  14.7507 |  10.8167 |  730.5911|  54.99743262 |
|      35000 |  14.7401 |  10.8167 |  730.5911|  53.98627252 |
|      36000 |  15.6995 |  10.8167 |  730.5911|  53.02692745 |
|      37000 |  14.7686 |  10.8167 |  730.5911|  52.11470084 |
|      38000 |  14.6823 |  10.8167 |  730.5911|  51.24559088 |
|      39000 |  14.683  |  10.8167 |  730.5911|  50.41631197 |
|      40000 |  14.7103 |  10.8167 |  730.5911|  49.62379298 |
```


## Teste Pre-Fetching Nativo - EnableIdempotence True

Consumer Configs:

```csharp
var config = new ConsumerConfig
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
    BrokerVersionFallback = "1.0.0",
};
```

Producer Configs:

```csharp
var config = new ProducerConfig
{
    BootstrapServers = appSettings["BrokerList"],
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "$ConnectionString",
    SaslPassword = _config.GetConnectionString("EventHub"),
    CompressionType = CompressionType.None,
    Acks = Acks.All,
    EnableIdempotence = true,
    LingerMs = 1,
    BatchSize = 131072,
};
```


# Teste de latencia

O objeto deste projeto é testar a latencia de consumo de eventos do Event Hubs. Visando evidênciar diferentes configurações, e também estratégias de pre-fetching da biblioteca Confluent Kafka.

## Teste Pre-Fetching Nativo 

### EnableIdempotence True, LingerMs 1

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
|      count |      cur    |      min    |      max    |      avg         |
|------------|-------------|-------------|-------------|------------------|
|       1000 |    71.5187  |    32.9539  |    71.5187  |    62.16285179999996  |
|       2000 |    44.7293  |    20.5354  |    71.5187  |    51.580909100000035 |
|       3000 |    40.3122  |    19.7464  |    71.5187  |    45.76172279999993  |
|       4000 |    44.4673  |    19.7464  |    71.5187  |    45.779878324999906 |
|       5000 |    70.4773  |    19.7464  |    83.9481  |    49.844075499999974 |
|       6000 |    45.4727  |    19.7464  |    83.9481  |    48.09561808333348  |
|       7000 |    44.1893  |    19.7464  |    85.7207  |    47.72768691428587  |
|       8000 |    49.9203  |    19.7464  |    85.7207  |    47.15460048750012  |
|       9000 |    135.2892 |     19.7464 |    148.495  |    56.54325260000002  |
|      10000 |    150.5828 |    15.7371  |    181.4296 |    63.10018442000006  |
|      11000 |    137.8548 |    15.7371  |    181.4296 |    66.09596422727293  |
|      12000 |    34.6468  |    15.7371  |    181.4296 |    63.208481716666945 |
|      13000 |    59.3979  |    15.7371  |    181.4296 |    62.0147007307695   |
|      14000 |    49.0416  |    15.1555  |    181.4296 |    60.39573131428587  |
|      15000 |    36.2832  |    15.1555  |    181.4296 |    58.323502853333544 |
|      16000 |    99.7642  |    15.1555  |    181.4296 |    58.47256050625022  |
|      17000 |    53.6978  |    15.1555  |    181.4296 |    57.30862266470604  |
|      18000 |    64.6777  |    15.1555  |    181.4296 |    57.03411240000019  |
|      19000 |    45.8715  |    12.1128  |    181.4296 |    55.945757026316045 |
|      20000 |    36.5127  |    12.1128  |    181.4296 |    55.201009970000094 |
```


## Teste Pre-Fetching Nativo 

## EnableIdempotence True, LingerMs 1

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

Resultado:

```
|      count |      cur    |      min    |      max    |      avg         |
|------------|-------------|-------------|-------------|------------------|
|       1000 |    26.8112  |    25.1347  |    43.6171  |    32.05275479999999  |
|       2000 |    16.4523  |    15.7497  |    43.6171  |    30.761519450000026 |
|       3000 |    14.4161  |    13.9432  |    43.6171  |    27.59697556666674  |
|       4000 |    24.7569  |    13.9432  |    43.6171  |    27.171592850000046 |
|       5000 |    18.8774  |    13.9432  |    43.6171  |    25.345020020000025 |
|       6000 |    19.165   |    13.9432  |    43.6171  |    24.49164380000001  |
|       7000 |    17.9784  |    13.9432  |    43.6171  |    23.83623087142861  |
|       8000 |    23.8748  |    12.3034  |    43.6171  |    23.099576500000023 |
|       9000 |    23.548   |    12.3034  |    43.6171  |    23.05972862222226  |
|      10000 |    17.3004  |    12.3034  |    43.6171  |    22.76429269000003  |
|      11000 |    21.244   |    12.3034  |    44.2575  |    23.011244299999912 |
|      12000 |    16.7752  |    12.3034  |    44.2575  |    23.01723736666657  |
|      13000 |    17.1607  |    12.3034  |    44.2575  |    22.814839738461462 |
|      14000 |    22.8277  |    12.3034  |    44.2575  |    22.7635067142856   |
|      15000 |    13.0688  |    12.3034  |    44.2575  |    22.69228235999994  |
|      16000 |    13.642   |    12.3034  |    44.2575  |    22.527335631249905 |
|      17000 |    83.3495  |    12.3034  |    83.3495  |    22.792094688235196 |
|      18000 |    19.933   |    12.3034  |    83.3495  |    22.756799083333174 |
|      19000 |    14.6939  |    12.3034  |    83.3495  |    22.571136784210356 |
|      20000 |    19.7274  |    12.3034  |    83.3495  |    22.48857513499982  |
```


## Teste Pre-Fetching Nativo 

### EnableIdempotence false, Linger 0

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
    EnableIdempotence = false,
    LingerMs = 0,
    BatchSize = 131072,
};
```

Resultado:

```
|      count |      cur    |      min    |      max    |      avg         |
|------------|-------------|-------------|-------------|------------------|
|       1000 |    27.7159  |    16.0701  |    119.7041 |    57.147566699999906 |
|       2000 |    53.2047  |    5.6615   |    119.7041 |    49.10860169999995  |
|       3000 |    30.3894  |    5.6615   |    119.7041 |    42.406298933333275 |
|       4000 |    89.483   |    5.6615   |    119.7041 |    53.06367329999991  |
|       5000 |    41.7745  |    5.6615   |    119.7041 |    48.85158859999999  |
|       6000 |    34.4756  |    5.6615   |    119.7041 |    46.87219281666685  |
|       7000 |    43.4622  |    5.6615   |    119.7041 |    45.10512885714297  |
|       8000 |    47.2423  |    5.6615   |    119.7041 |    44.41377241250003  |
|       9000 |    50.5845  |    5.6615   |    119.7041 |    43.91929911111111  |
|      10000 |    35.1164  |    5.6615   |    119.7041 |    42.75542222999999  |
|      11000 |    43.487   |    5.6615   |    119.7041 |    42.56280716363656  |
|      12000 |    43.0444  |    5.6615   |    119.7041 |    42.073403725000084 |
|      13000 |    48.457   |    5.6615   |    119.7041 |    41.920108246153944 |
|      14000 |    41.3665  |    5.6615   |    119.7041 |    41.63618609999993  |
|      15000 |    49.35    |    5.6615   |    119.7041 |    41.35666950666667  |
|      16000 |    34.4108  |    5.6615   |    119.7041 |    40.88190292499998  |
|      17000 |    28.6727  |    5.4017   |    119.7041 |    40.96260993529408  |
|      18000 |    39.4527  |    5.4017   |    119.7041 |    41.194863372222116 |
|      19000 |    56.2958  |    5.4017   |    119.7041 |    41.48010537368412  |
|      20000 |    89.7213  |    5.4017   |    119.7041 |    43.236499839999944 |

```



## Teste Pre-Fetching Nativo 

### EnableIdempotence true, Linger 0

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
    LingerMs = 0,
    BatchSize = 131072,
};
```

Resultado:

```

|      count |      cur    |      min    |      max    |      avg         |
|------------|-------------|-------------|-------------|------------------|
|       1000 |    51.637   |    50.7633  |    193.1216 |    117.64103290000001 |
|       2000 |    69.2341  |    26.7921  |    379.6486 |    127.01567355       |
|       3000 |    123.2214 |    19.7931  |    379.6486 |    100.83395739999972 |
|       4000 |    25.9402  |    8.434    |    379.6486 |    83.42861020000001  |
|       5000 |    22.186   |    8.434    |    379.6486 |    71.41534996        |
|       6000 |    34.3377  |    8.434    |    379.6486 |    75.40020936666676  |
|       7000 |    13.7905  |    6.1484   |    379.6486 |    67.18835838571417  |
|       8000 |    22.8356  |    6.1484   |    379.6486 |    62.56891819999981  |
|       9000 |    25.9577  |    6.1484   |    379.6486 |    57.87014922222215  |
|      10000 |    16.5234  |    6.1484   |    379.6486 |    54.12644954000001  |
|      11000 |    13.5963  |    6.1484   |    379.6486 |    52.79950531818183  |
|      12000 |    21.3274  |    6.1484   |    379.6486 |    50.421418841666686 |
|      13000 |    16.0929  |    5.5936   |    379.6486 |    48.06975314615389  |
|      14000 |    25.0338  |    5.5936   |    379.6486 |    45.798253121428644 |
|      15000 |    23.7108  |    5.5936   |    379.6486 |    43.90998506000012  |
|      16000 |    26.0722  |    5.5936   |    379.6486 |    42.641317743750136 |
|      17000 |    22.7893  |    5.5936   |    379.6486 |    41.42887307647072  |
|      18000 |    20.5659  |    5.5936   |    379.6486 |    40.52300253888896  |
|      19000 |    21.241   |    5.5936   |    379.6486 |    39.404445026315884 |
|      20000 |    20.0181  |    5.5936   |    379.6486 |    38.91819341500008  |

```

## Teste Pre-Fetching Nativo 

### EnableIdempotence False, LingerMs 1

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
|   count |    cur   |    min   |    max   |    avg         |
|---------|----------|----------|----------|----------------|
| 1000    | 23.5857  | 17.1579  | 43.0279  | 23.452590900000022 |
| 2000    | 22.7763  | 14.2456  | 43.0279  | 21.92374280000005 |
| 3000    | 19.7699  | 14.2456  | 43.0279  | 21.725089633333294 |
| 4000    | 23.6834  | 9.092    | 43.0279  | 20.73951274999995 |
| 5000    | 19.9441  | 9.092    | 43.0279  | 20.367689799999997 |
| 6000    | 15.9654  | 8.0321   | 43.0279  | 19.40517085000001 |
| 7000    | 22.1787  | 8.0321   | 43.0279  | 19.040912742857177 |
| 8000    | 24.4819  | 8.0321   | 43.0279  | 19.47797257500009 |
| 9000    | 25.1449  | 8.0321   | 43.0279  | 19.44358694444451 |
| 10000   | 14.9993  | 8.0321   | 43.0279  | 19.11660083000013 |
| 11000   | 10.4448  | 8.0321   | 43.0279  | 19.06056921818194 |
| 12000   | 13.6566  | 8.0321   | 43.0279  | 19.050216500000143 |
| 13000   | 23.6591  | 8.0321   | 43.0279  | 18.78705468461546 |
| 14000   | 21.968   | 8.0321   | 43.0279  | 19.027429414285834 |
| 15000   | 19.8918  | 8.0321   | 43.0279  | 19.04022982000012 |
| 16000   | 34.4323  | 8.0321   | 43.0279  | 19.37037049375012 |
| 17000   | 14.0024  | 8.0321   | 43.0279  | 19.339056476470738 |
| 18000   | 25.1665  | 8.0321   | 106.4807 | 22.143862838889078 |
```


## Teste Pre-Fetching com a ConcurrentQueue 

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
    EnableAutoCommit = true,
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
|   count |    cur   |    min   |    max   |    avg         |
|---------|----------|----------|----------|----------------|
| 1000    | 16.4875  | 15.475   | 46.3687  | 21.09626930000001 |
| 2000    | 32.7825  | 15.475   | 46.3687  | 23.492655299999967 |
| 3000    | 26.7388  | 15.475   | 56.4568  | 26.435036833333292 |
| 4000    | 28.8492  | 15.475   | 56.4568  | 26.22350244999996 |
| 5000    | 18.2482  | 15.475   | 56.4568  | 25.142988619999986 |
| 6000    | 13.1466  | 12.8796  | 56.4568  | 24.61926116666659 |
| 7000    | 69.5478  | 12.8796  | 69.5478  | 24.802168585714206 |
| 8000    | 23.9734  | 12.8796  | 69.5478  | 24.81583108749995 |
| 9000    | 36.7378  | 12.8796  | 69.5478  | 24.975749422222183 |
| 10000   | 21.1654  | 12.8796  | 69.5478  | 24.377350109999952 |
| 11000   | 27.4563  | 12.8796  | 69.5478  | 24.433798136363617 |
| 12000   | 26.4137  | 11.843   | 69.5478  | 24.628250233333393 |
| 13000   | 11.5976  | 11.3232  | 69.5478  | 24.398978623076964 |
| 14000   | 23.398   | 11.3232  | 69.5478  | 24.918957564285716 |
| 15000   | 25.7603  | 11.3232  | 69.5478  | 24.96178114 |
| 16000   | 19.6007  | 11.3232  | 69.5478  | 24.835343843749982 |
| 17000   | 27.3735  | 11.3232  | 69.5478  | 24.619428188235176 |
| 18000   | 20.7059  | 11.3232  | 69.5478  | 24.501546538888793 |
| 19000   | 15.9668  | 11.3232  | 69.5478  | 24.55451266842095 |
| 20000   | 13.5862  | 11.3232  | 69.5478  | 24.473754669999934 |
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
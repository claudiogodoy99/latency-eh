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


## Consumer Queue AutoCommit

```  
count, cur, min, max, avg
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/claudio/latency-eh/consumerQueue
info: consumerQueue.Worker[0]
      Consuming messages from topic: evh-eventhub, broker(s): eh-eastus-godoy.servicebus.windows.net:9093
info: consumerQueue.Worker[0]
      1000, 18.6382, 8.7121, 26.3424, 18.13298769999997
info: consumerQueue.Worker[0]
      2000, 11.5215, 8.7554, 22.9009, 16.71262145
info: consumerQueue.Worker[0]
      3000, 7.6411, 7.2681, 66.014, 18.307199999999956
info: consumerQueue.Worker[0]
      4000, 19.5901, 7.6956, 22.5012, 17.478843449999946
info: consumerQueue.Worker[0]
      5000, 16.6334, 8.9311, 22.9392, 17.086011980000002
info: consumerQueue.Worker[0]
      6000, 12.915, 9.8747, 27.0823, 16.99158418333334
info: consumerQueue.Worker[0]
      7000, 22.1716, 8.0616, 22.2509, 16.870065885714336
info: consumerQueue.Worker[0]
      8000, 9.3875, 9.1931, 44.6679, 17.311462187500016
info: consumerQueue.Worker[0]
      9000, 22.7721, 9.4377, 22.7721, 17.07618658888894
info: consumerQueue.Worker[0]
      10000, 18.7217, 9.4525, 22.8307, 16.881450130000076
info: consumerQueue.Worker[0]
      11000, 15.1119, 9.7273, 22.6003, 16.79442563636369
info: consumerQueue.Worker[0]
      12000, 9.4775, 8.9271, 21.9931, 16.621550566666787
info: consumerQueue.Worker[0]
      13000, 20.1041, 9.5016, 41.0872, 16.797700400000085
info: consumerQueue.Worker[0]
      14000, 15.7689, 10.8572, 23.1417, 16.79753264285732
info: consumerQueue.Worker[0]
      15000, 8.5464, 8.2807, 23.197, 16.6750239600002
info: consumerQueue.Worker[0]
      16000, 23.7221, 7.5318, 23.7221, 16.679289362500192
info: consumerQueue.Worker[0]
      17000, 15.6851, 9.0763, 23.757, 16.635886447059
info: consumerQueue.Worker[0]
      18000, 11.9298, 8.2709, 22.1088, 16.50975222777793
info: consumerQueue.Worker[0]
      19000, 11.9852, 9.2315, 30.0254, 16.584622478947484
info: consumerQueue.Worker[0]
      20000, 21.1928, 10.7062, 33.3384, 16.68676507000011
info: consumerQueue.Worker[0]
      21000, 21.4754, 9.1766, 22.9578, 16.661076466666724
info: consumerQueue.Worker[0]
      22000, 13.8564, 8.5367, 23.0262, 16.614205240909154
info: consumerQueue.Worker[0]
      23000, 19.4968, 8.1547, 22.5786, 16.55618230869573
info: consumerQueue.Worker[0]
      24000, 21.9811, 12.4096, 41.3994, 16.681033616666724
info: consumerQueue.Worker[0]
      25000, 12.0106, 9.0717, 23.0858, 16.63601991599996
info: consumerQueue.Worker[0]
      26000, 23.0216, 9.2496, 23.0216, 16.614984119230748
info: consumerQueue.Worker[0]
      27000, 20.0993, 10.7095, 31.07, 16.684942533333302
info: consumerQueue.Worker[0]
      28000, 10.4725, 10.2775, 37.1568, 16.847054421428588
info: consumerQueue.Worker[0]
      29000, 20.5247, 10.4941, 42.341, 17.031501217241377
info: consumerQueue.Worker[0]
      30000, 9.0682, 8.1235, 22.3532, 16.939843983333223
info: consumerQueue.Worker[0]
      31000, 7.5709, 7.2922, 20.6739, 16.84206408387085
info: consumerQueue.Worker[0]
      32000, 12.7184, 7.731, 19.2431, 16.755057449999864
info: consumerQueue.Worker[0]
      33000, 16.6029, 13.0548, 81.7841, 17.027249287878664
info: consumerQueue.Worker[0]
      34000, 20.7894, 10.2709, 33.8258, 17.140115632352813
info: consumerQueue.Worker[0]
      35000, 19.9981, 9.3625, 22.8343, 17.106082154285637
info: consumerQueue.Worker[0]
      36000, 41.2599, 9.5445, 41.2599, 17.158692738888767
info: consumerQueue.Worker[0]
      37000, 9.5188, 9.2957, 41.2879, 17.133511405405283
info: consumerQueue.Worker[0]
      38000, 12.2104, 9.5373, 23.755, 17.136680505263097
info: consumerQueue.Worker[0]
      39000, 12.5468, 12.2319, 24.681, 17.173321858974315
info: consumerQueue.Worker[0]
      40000, 14.6611, 11.929, 41.9096, 17.25914203499999
info: consumerQueue.Worker[0]
      41000, 17.712, 10.091, 24.5304, 17.255091882926795
info: consumerQueue.Worker[0]
      42000, 16.738, 10.2549, 23.2182, 17.241457042857046
info: consumerQueue.Worker[0]
      43000, 13.529, 9.4952, 23.3208, 17.20896672325574
info: consumerQueue.Worker[0]
      44000, 12.5004, 8.1165, 22.9465, 17.176110059090767
info: consumerQueue.Worker[0]
      45000, 17.8636, 11.407, 43.5652, 17.28810654222209
info: consumerQueue.Worker[0]
      46000, 18.51, 8.2134, 22.7881, 17.25396405869549
info: consumerQueue.Worker[0]
      47000, 19.8402, 10.6125, 41.7291, 17.322149870212698
info: consumerQueue.Worker[0]
      48000, 18.4687, 9.0787, 23.8804, 17.298922289583324
info: consumerQueue.Worker[0]
      49000, 53.4107, 10.0599, 53.4107, 17.35991141428572
info: consumerQueue.Worker[0]
      50000, 11.056, 10.8773, 53.4741, 17.578745115999997
info: consumerQueue.Worker[0]
      51000, 11.7982, 10.6809, 41.3887, 17.618947298039277
info: consumerQueue.Worker[0]
      52000, 31.1829, 9.2065, 31.1829, 17.620347040384626
info: consumerQueue.Worker[0]
```

## Consumer Normal

```dotnetcli
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/claudio/latency-eh/consumer
info: ConsumerQueue.Worker[0]
      Consuming messages from topic: evh-eventhub, broker(s): eh-eastus-godoy.servicebus.windows.net:9093
info: ConsumerQueue.Worker[0]
      count, cur, min, max, avg
info: ConsumerQueue.Worker[0]
      1000, 14.5137, 12.9376, 38.3351, 22.971288999999977
info: ConsumerQueue.Worker[0]
      2000, 18.018, 8.0033, 21.5667, 18.63800160000002
info: ConsumerQueue.Worker[0]
      3000, 23.1206, 8.23, 23.8726, 17.935531966666673
info: ConsumerQueue.Worker[0]
      4000, 13.3306, 8.1977, 29.8665, 17.67386027500004
info: ConsumerQueue.Worker[0]
      5000, 17.5591, 8.9916, 21.7732, 17.177303460000022
info: ConsumerQueue.Worker[0]
      6000, 21.9737, 7.0832, 21.9737, 16.74699480000002
info: ConsumerQueue.Worker[0]
      7000, 12.6424, 9.3343, 23.1588, 16.530130685714326
info: ConsumerQueue.Worker[0]
      8000, 16.7896, 10.2766, 42.4321, 16.89464984999998
info: ConsumerQueue.Worker[0]
      9000, 18.7627, 8.8994, 23.4183, 16.791155044444444
info: ConsumerQueue.Worker[0]
      10000, 12.3315, 9.8618, 20.2546, 16.619577100000033
info: ConsumerQueue.Worker[0]
      11000, 8.4685, 8.2519, 23.6313, 16.60636914545464
info: ConsumerQueue.Worker[0]
      12000, 8.782, 8.566, 22.8208, 16.52185575000005
info: ConsumerQueue.Worker[0]
      13000, 12.8584, 8.8602, 22.3661, 16.503560515384777
info: ConsumerQueue.Worker[0]
      14000, 20.2896, 9.8836, 22.7191, 16.47746677857167
info: ConsumerQueue.Worker[0]
      15000, 17.026, 8.6255, 22.491, 16.376644100000174
info: ConsumerQueue.Worker[0]
      16000, 15.7437, 6.5366, 23.7954, 16.37031764375019
info: ConsumerQueue.Worker[0]
      17000, 19.6401, 9.0861, 23.1299, 16.342956935294296
info: ConsumerQueue.Worker[0]
      18000, 54.8573, 7.8318, 54.8573, 16.42496228333356
info: ConsumerQueue.Worker[0]
      19000, 11.6047, 7.8312, 55.0183, 16.340596526316
info: ConsumerQueue.Worker[0]
      20000, 13.1949, 9.3464, 21.1273, 16.29900226500015
info: ConsumerQueue.Worker[0]
      21000, 15.1379, 10.8636, 23.1762, 16.32618774761918
info: ConsumerQueue.Worker[0]
      22000, 15.7806, 7.9162, 21.9318, 16.271174113636548
info: ConsumerQueue.Worker[0]
      23000, 14.6177, 7.8952, 41.7573, 16.308315269565473
info: ConsumerQueue.Worker[0]
      24000, 29.2239, 8.2018, 29.2767, 16.342603883333577
info: ConsumerQueue.Worker[0]
      25000, 15.8634, 7.2348, 29.3649, 16.303581296000235
info: ConsumerQueue.Worker[0]
      26000, 16.5763, 11.1466, 23.0855, 16.326463411538708
info: ConsumerQueue.Worker[0]
      27000, 15.3024, 8.1145, 21.6376, 16.282289407407603
info: ConsumerQueue.Worker[0]
      28000, 8.627, 8.4237, 22.9497, 16.24289081785726
info: ConsumerQueue.Worker[0]
      29000, 10.9448, 7.7824, 20.6468, 16.177534844827733
info: ConsumerQueue.Worker[0]
      30000, 20.7521, 8.8173, 30.4596, 16.205731576666764
info: ConsumerQueue.Worker[0]
      31000, 7.3311, 7.1132, 22.2473, 16.166964790322652
info: ConsumerQueue.Worker[0]
      32000, 12.3713, 7.4432, 21.882, 16.149639340625093
info: ConsumerQueue.Worker[0]
      33000, 12.7733, 8.7402, 21.7582, 16.118466390909163
info: ConsumerQueue.Worker[0]
      34000, 11.7791, 7.7272, 22.2307, 16.08745871176482
info: ConsumerQueue.Worker[0]
      35000, 19.0964, 8.0761, 21.0957, 16.046364648571533
info: ConsumerQueue.Worker[0]
      36000, 11.2027, 8.7569, 21.954, 16.03827647500012
info: ConsumerQueue.Worker[0]
      37000, 21.0366, 8.3615, 26.3643, 16.055763624324427
info: ConsumerQueue.Worker[0]
      38000, 21.2655, 8.8017, 23.8715, 16.064219652631706
info: ConsumerQueue.Worker[0]
      39000, 13.4423, 8.9303, 30.0204, 16.07614961025658
info: ConsumerQueue.Worker[0]
      40000, 13.242, 10.4719, 23.4852, 16.103156977500113
info: ConsumerQueue.Worker[0]
      41000, 16.3166, 9.1961, 30.7059, 16.154724319512273
info: ConsumerQueue.Worker[0]
      42000, 13.9902, 9.5757, 22.677, 16.147342830952415
info: ConsumerQueue.Worker[0]
      43000, 8.641, 8.4525, 33.4624, 16.270599686046506
info: ConsumerQueue.Worker[0]
      44000, 24.0025, 8.6963, 42.5605, 16.3276085795455
info: ConsumerQueue.Worker[0]
      45000, 6.8695, 6.6931, 24.1203, 16.299336524444524
info: ConsumerQueue.Worker[0]
      46000, 25.5734, 8.0351, 42.2116, 16.35064211304353
info: ConsumerQueue.Worker[0]
      47000, 22.1411, 8.4674, 25.6387, 16.351590970212843
info: ConsumerQueue.Worker[0]
      48000, 21.3682, 8.1887, 22.2249, 16.320789075000043
info: ConsumerQueue.Worker[0]
      49000, 18.8505, 9.0295, 54.73, 16.467050636734804
info: ConsumerQueue.Worker[0]
      50000, 19.5345, 10.1591, 43.2636, 16.522153204000162
info: ConsumerQueue.Worker[0]
      51000, 9.2034, 8.6846, 29.5933, 16.518495662745316
info: ConsumerQueue.Worker[0]
      52000, 8.862, 8.6273, 22.3348, 16.50805018461559
info: ConsumerQueue.Worker[0]
      53000, 7.8025, 7.5931, 36.264, 16.532992681132267
info: ConsumerQueue.Worker[0]
      54000, 9.665, 7.8572, 22.2931, 16.522301298148303
info: ConsumerQueue.Worker[0]
      55000, 15.8352, 8.4454, 45.373, 16.577270138181994
info: ConsumerQueue.Worker[0]
      56000, 16.1153, 10.4627, 28.5703, 16.63215181607169
info: ConsumerQueue.Worker[0]
      57000, 17.7328, 9.1039, 22.9916, 16.61350348596514
info: ConsumerQueue.Worker[0]
      58000, 17.548, 8.1983, 21.1426, 16.586293065517474
info: ConsumerQueue.Worker[0]
      59000, 22.3969, 11.8421, 22.7408, 16.595901708474806
info: ConsumerQueue.Worker[0]
      60000, 9.8836, 8.7182, 29.7647, 16.61005912500019
info: ConsumerQueue.Worker[0]
      61000, 14.9983, 10.8371, 24.3834, 16.628734168852645
info: ConsumerQueue.Worker[0]
      62000, 19.9359, 8.557, 22.1795, 16.598403783871166
info: ConsumerQueue.Worker[0]
      63000, 10.6993, 8.1897, 30.6881, 16.601281300000174
info: ConsumerQueue.Worker[0]
      64000, 10.5333, 7.748, 23.1105, 16.582737804687742
info: ConsumerQueue.Worker[0]
      65000, 7.8086, 7.6347, 21.5852, 16.557421926154085
info: ConsumerQueue.Worker[0]
      66000, 13.4872, 8.6431, 21.0486, 16.537477050000213
info: ConsumerQueue.Worker[0]
      67000, 13.8554, 8.0708, 21.7363, 16.512975908955397
info: ConsumerQueue.Worker[0]
      68000, 17.3338, 8.8959, 30.2171, 16.55022510882369
info: ConsumerQueue.Worker[0]
      69000, 18.321, 8.4939, 20.7907, 16.52261331594216
info: ConsumerQueue.Worker[0]
      70000, 13.4871, 10.277, 36.0765, 16.54742914285728
```
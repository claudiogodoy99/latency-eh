# Teste de latencia

O objeto deste projeto é testar a latência de consumo de eventos do Event Hubs. Visando evidenciar diferentes configurações, e também estratégias de pre-fetching da biblioteca Confluent Kafka.

> Teste com duas VM's e 1 PU Event Hub Premium


## Producer Otimizado

```json
 [
    {
        "Key": "compression.type",
        "Value": "none" //Event Hubs não da suporte à essa config
    },
    {
        "Key": "acks",
        "Value": "0"
    },
    {
        "Key": "linger.ms",
        "Value": "0"
    },
    {
        "Key": "socket.nagle.disable",
        "Value": "True"
    }
]
```

### Consumer Otimizado

```json
Consumer: [
    {
        "Key": "enable.auto.commit",
        "Value": "True"
    },
    {
        "Key": "fetch.wait.max.ms",
        "Value": "10"
    },
    {
        "Key": "socket.nagle.disable",
        "Value": "True"
    }
]
```

### PRODUCER Default E CONSUMER FETCH 100ms

```json
[
    {
        "Key": "acks",
        "Value": "0"
    }
]
```

### CONSUMER Fetch 10ms

```json
[
    {
        "Key": "fetch.wait.max.ms",
        "Value": "100"
    }
]
```

### CONSUMER Fetch 100ms

```json
[ 
    {
        "Key": "enable.auto.commit",
        "Value": "True"
    },
    {
        "Key": "fetch.wait.max.ms",
        "Value": "100"
    }
]
```

## Resultados

```s
| Config                       | Min     | Max     | Media               | 75th              | 95th             | 99th              | MPS   |
|------------------------------|---------|---------|---------------------|-------------------|------------------|-------------------|-------|
| Tudo Otimizado               | 9.0892  | 49.4253 | 19.618062326732638  | 23.813625000000002 | 36.173779999999994 | 45.306929         | 100   |
| P default e C 100ms          | 9.0997  | 110.3842 | 19.110160499999985  | 21.9132           | 26.19917         | 110.254034        | 100   |
| P Optmizado e C 10ms         | 7.8575  | 50.917  | 18.151512776119375  | 21.749850000000002 | 38.50294         | 50.639272000000005 | 100   |
| P Optmizado e C 100ms        | 8.1567  | 81.636  | 19.47032850505043   | 21.664475         | 41.890159999999995 | 63.706079000000074 | 100   |
| P Default e C fetch 100ms    | 16.302  | 50.9272 | 27.517815518518564  | 30.320875         | 48.458455        | 50.692226         | 300   |
| Producer Opt e C fetch 100ms | 9.6265  | 79.5632 | 25.29765607843135   | 27.992025         | 51.345065        | 79.114317         | 300   |
| Producer opt e C fetch 10ms  | 9.5102  | 52.8139 | 22.539752784313702  | 25.741675         | 34.474270000000004| 52.609812         | 300   |
| Tudo otimizado               | 9.7563  | 51.8449 | 25.474807791666898  | 29.451099999999997| 46.606145000000005| 51.276367         | 300   |
```
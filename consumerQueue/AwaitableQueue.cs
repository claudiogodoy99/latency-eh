using System.Threading.Channels;

public class AwaitableQueue<T>
{
    private CancellationToken _stoppingToken;
    private Channel<T> _channel = Channel.CreateUnbounded<T>(
            new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = true,
            });

    public AwaitableQueue(CancellationToken stoppingToken)
    {
        _stoppingToken = stoppingToken;
    }

    public async ValueTask EnqueueAsync(T item)
    {
        await _channel.Writer.WriteAsync(item, _stoppingToken);
    }

    public void Enqueue(T item)
    {
        _channel.Writer.TryWrite(item);
    }

    public ValueTask<T> DequeueAsync()
    {
        return _channel.Reader.ReadAsync(_stoppingToken);
    }
}
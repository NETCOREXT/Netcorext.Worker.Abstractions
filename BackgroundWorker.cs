using Microsoft.Extensions.Hosting;

namespace Netcorext.Worker;

public abstract class BackgroundWorker : IHostedService, IDisposable
{
    private Task _executingTask = null!;
    private CancellationTokenSource _cancellationToken = new CancellationTokenSource();
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (_cancellationToken.IsCancellationRequested)
        {
            _cancellationToken.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        _executingTask = ExecuteAsync(_cancellationToken.Token);

        return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _cancellationToken.Cancel();
        }
        finally
        {
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }

    protected abstract Task ExecuteAsync(CancellationToken cancellationToken = default);

    public virtual void Dispose()
    {
        _cancellationToken.Cancel();
    }
}

public abstract class BackgroundWorker<TWorker> : BackgroundWorker where TWorker : BackgroundWorker
{ }
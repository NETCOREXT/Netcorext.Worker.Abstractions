namespace Netcorext.Worker;

public interface IWorkerRunner<TWorker> : IDisposable where TWorker : BackgroundWorker
{
    Task InvokeAsync(CancellationToken cancellationToken = default);
}
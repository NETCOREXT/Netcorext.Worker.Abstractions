namespace Netcorext.Worker;

public interface IWorkerRunner<TWorker> : IDisposable where TWorker : BackgroundWorker
{
    Task InvokeAsync(TWorker worker, CancellationToken cancellationToken = default);
}
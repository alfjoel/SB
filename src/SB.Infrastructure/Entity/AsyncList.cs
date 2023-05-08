namespace SB.Infrastructure.Entity;

public class AsyncList<T>
{
    private readonly List<T> _list;
    private readonly SemaphoreSlim _semaphore;

    public AsyncList()
    {
        _list = new List<T>();
        _semaphore = new SemaphoreSlim(1, 1);
    }

    public async Task AddAsync(T item, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            _list.Add(item);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> RemoveAsync(T item, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return _list.Remove(item);
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _semaphore.Release();
        }

        
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return _list.Count;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T[]> ToArrayAsync(CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return _list.ToArray();
        }
        finally
        {
            _semaphore.Release();
        }
        
    }

    // Implementa otros métodos según sea necesario
}
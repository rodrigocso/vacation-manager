using Application;

namespace Infrastructure;

public class Repository<T> : IRepository<T>
{
    public void Add(T entity) => throw new NotImplementedException();

    public IEnumerable<T> GetAll() => throw new NotImplementedException();
}

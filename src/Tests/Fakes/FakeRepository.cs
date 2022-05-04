using System.Collections.Generic;
using Application;

namespace Tests.Fakes;

public class FakeRepository<T> : IRepository<T>
{
    private static readonly List<T> s_entities = new();

    public void Add(T entity) => s_entities.Add(entity);

    public void AddMany(IEnumerable<T> entities) => s_entities.AddRange(entities);

    public IEnumerable<T> GetAll() => s_entities;
}

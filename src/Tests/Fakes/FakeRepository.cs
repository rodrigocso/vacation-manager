using System.Collections.Generic;
using Application;

namespace Tests.Fakes;

public class FakeRepository<T> : IRepository<T>
{
    public static readonly List<T> Items = new();

    public void Add(T entity) => Items.Add(entity);

    public IEnumerable<T> GetAll() => Items;

    public static void AddMany(IEnumerable<T> entities) => Items.AddRange(entities);

    public static void Clear() => Items.Clear();
}

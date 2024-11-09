using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpec(ISpecefication<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecefication<T> spec);
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecefication<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecefication<T, TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    bool Exists(int id);
    Task<int> CountAsync(ISpecefication<T> spec);
}

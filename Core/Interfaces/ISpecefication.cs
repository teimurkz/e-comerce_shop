using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecefication<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool isDistinc { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecefication<T, TResult> : ISpecefication<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}

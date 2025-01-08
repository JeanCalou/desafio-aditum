using System.Linq.Expressions;
using MongoDB.Driver;

namespace Aditum.Challenge.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddOneAsync(T entity);

        Task ReplaceOneAsync(Expression<Func<T, bool>> filterExpression,
            T entity, ReplaceOptions options);

        Task DeleteAsync(Expression<Func<T, bool>> filterExpression);
    }
}

using System.Linq.Expressions;
using MongoDB.Driver;

namespace Aditum.Challenge.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filterExpression);

        Task<T> FindOneAsync(Guid id);

        Task AddOneAsync(T entity);

        Task ReplaceOneAsync(Expression<Func<T, bool>> filterExpression,
            T entity, ReplaceOptions options);

        Task DeleteAsync(Expression<Func<T, bool>> filterExpression);
    }
}

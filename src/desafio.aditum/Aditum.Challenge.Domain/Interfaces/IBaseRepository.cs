using System.Linq.Expressions;

namespace Aditum.Challenge.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filterExpression);

        Task InsertMany(List<T> list);

        Task DeleteAllDocuments();
    }
}

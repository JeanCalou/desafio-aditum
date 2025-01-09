using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface ICSVService
    {
        Task<IAsyncEnumerable<dynamic>> ReadCSV<T>(Stream file);
        Task<List<Restaurant>> ProcessCSVRestaurant(IAsyncEnumerable<dynamic> data);
    }
}

using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetAllByFilterAsync(DateTime time);
        Task InsertMany(List<Restaurant> restaurants);
        Task DeleteAllDocuments();
    }
}

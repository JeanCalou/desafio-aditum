using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetAllByFilterAsync(TimeSpan time);
        Task InsertMany(List<Restaurant> restaurants);
        Task DeleteAllDocuments();
    }
}

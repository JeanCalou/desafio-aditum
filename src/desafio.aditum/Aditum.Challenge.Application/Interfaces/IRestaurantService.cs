using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetAllAsync();
        Task InsertMany(List<Restaurant> restaurants);
        Task DeleteAllDocuments();
    }
}

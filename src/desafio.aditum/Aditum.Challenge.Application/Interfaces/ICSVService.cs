using Aditum.Challenge.Domain.Entities;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface ICSVService
    {
        IEnumerable<dynamic> ReadCSV(Stream file);
        List<Restaurant> ProcessCSVRestaurant(IEnumerable<dynamic> data);
    }
}

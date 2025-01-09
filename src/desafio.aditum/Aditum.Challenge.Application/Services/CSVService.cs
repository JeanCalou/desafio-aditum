using System.Globalization;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;

namespace Aditum.Challenge.Application.Services
{
    public class CSVService : ICSVService
    {
        public async Task<IAsyncEnumerable<dynamic>> ReadCSV<T>(Stream file)
        {           
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true
            };

            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, config);

            var data = csv.GetRecordsAsync<dynamic>();

            return data;
        }

        public async Task<List<Restaurant>> ProcessCSVRestaurant(IAsyncEnumerable<dynamic> data)
        {
            var list = data.OfType<IDictionary<string, object>>();

            List<Restaurant> restaurants = [];

            await foreach (var item in list)
            {
                var hours = item.Values.Last();

                Restaurant restaurant = new Restaurant(
                    new MongoDB.Bson.ObjectId(),
                    item.Values.First().ToString(),
                    hours.ToString().Split("-")[0],
                    hours.ToString().Split("-")[1]
                );

                restaurants.Add(restaurant);
            }

            return restaurants;
        }

    }
}

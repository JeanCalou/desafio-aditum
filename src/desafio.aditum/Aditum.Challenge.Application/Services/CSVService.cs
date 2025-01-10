using System.Globalization;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using MongoDB.Bson;

namespace Aditum.Challenge.Application.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<dynamic> ReadCSV(Stream file)
        {           
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true
            };

            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, config);

            var data = csv.GetRecords<dynamic>();

            return data;
        }

        public List<Restaurant> ProcessCSVRestaurant(IEnumerable<dynamic> data)
        {
            var list = data.OfType<IDictionary<string, object>>();

            List<Restaurant> restaurants = [];

            foreach (var item in list)
            {
                var hours = item.Values.Last();
                var openHour = DateTime.Parse(hours.ToString().Split("-")[0]);
                var closeHour = DateTime.Parse(hours.ToString().Split("-")[1]);

                Restaurant restaurant = new Restaurant(
                    Guid.NewGuid(),
                    item.Values.First().ToString(),
                    openHour,
                    closeHour
                );

                restaurants.Add(restaurant);
            }

            return restaurants;
        }

    }
}

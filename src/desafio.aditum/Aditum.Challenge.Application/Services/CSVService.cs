using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aditum.Challenge.Application.Interfaces;
using Aditum.Challenge.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using MongoDB.Bson;

namespace Aditum.Challenge.Application.Services
{
    public class CSVService : ICSVService
    {
        public async Task<List<dynamic>> ReadCSV<T>(Stream file)
        {           
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = true
            };

            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, config);

            var data = csv.GetRecordsAsync<dynamic>();

            List<dynamic> dynamicList = [];

            await foreach (var x in data) 
            {
                dynamicList.Add(x);
            }

            return dynamicList;
        }
    }
}

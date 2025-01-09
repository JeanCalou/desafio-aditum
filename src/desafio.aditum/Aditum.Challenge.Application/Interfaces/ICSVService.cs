using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aditum.Challenge.Application.Interfaces
{
    public interface ICSVService
    {
        Task<List<dynamic>> ReadCSV<T>(Stream file);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Services
{
    public interface IGraph
    {
        List<Path> SearchPaths(string from, string to);
        List<Path> SearchPaths(string from, string middle, string to);
    }
}

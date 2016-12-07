using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    public interface IFlight<T> 
        where T : class
    {
        T[] Parse(string path);

    }
}

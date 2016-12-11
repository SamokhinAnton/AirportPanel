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
        T[] Create(T[] flights);
        void View(T[] flights);

        T[] Delete(T[] flights);
        void Edit(T[] flights);

    }
}

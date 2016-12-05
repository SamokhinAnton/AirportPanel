using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel.Models
{
    public class PriceModel
    {
        public int Id { get; set; } 
        public FlightClass FlightClass { get; set; }
        public FlightModel Flight { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<PassengerModel> Passengers { get; set; }
    }

    public enum FlightClass
    {
        Business,
        Economy
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel.Models
{
    public class FlightModel
    {
        public int Id { get; set; }
        public bool IsArrived { get; set; }
        public DateTime Schedule { get; set; }
        public string FlightNumber { get; set; }
        public string CityPort { get; set; }
        public string Airline { get; set; }
        public int Gate { get; set; }
        public FlightStatus Status { get; set; }
        public char Terminal { get; set; }
        //public PriceModel[] Prices { get; set; }
    }
}

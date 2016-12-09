using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel.Models
{
    public class TicketModel
    {
        public int Id { get; set; } 
        public FlightClass FlightClass { get; set; }
        public decimal Price { get; set; }
    }
}

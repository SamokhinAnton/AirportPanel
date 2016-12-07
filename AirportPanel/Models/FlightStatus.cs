using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel.Models
{
    public enum FlightStatus
    {
        CheckIn,
        GateClosed,
        Arrived,
        DepartedAt,
        Unknown,
        Canceled,
        ExpectedAt,
        Delayed,
        InFlight
    }
}

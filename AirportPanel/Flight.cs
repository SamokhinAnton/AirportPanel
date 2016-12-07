using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Flight : Base, IFlight<FlightModel>
    {
        private FlightModel[] _flights { get; set; }
        public Flight()
        {
            var lines = FileHelper.ReadLines(FlightsPath);
            _flights = new FlightModel[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var arrInformation = lines[i].Split('|');
                _flights[i] = new FlightModel
                {
                    Id = int.Parse(arrInformation[0]),
                    IsArrived = bool.Parse(arrInformation[1]),
                    Schedule = DateTime.Parse(arrInformation[2]),
                    FlightNumber = arrInformation[3],
                    CityPort = arrInformation[4],
                    Airline = arrInformation[5],
                    Gate = int.Parse(arrInformation[6]),
                    Status = (FlightStatus)(int.Parse(arrInformation[7])),
                    Terminal = char.Parse(arrInformation[8])
                };
            };
        }

        public FlightModel[] GetFlights()
        {
            return _flights;
        }

        public void View(FlightModel[] information)
        {
            foreach (var item in information)
            {
                if (item.IsArrived)
                {
                    Console.WriteLine("{5}) Flight {0} arrived to {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
                        item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
                }
                else
                {
                    Console.WriteLine("{5}) Flight {0} departed from {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
                    item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
                }
            }
        }

        public FlightModel[] Parse(string path)
        {
            throw new NotImplementedException();
        }

        //public FlightModel[] CombineWithPrices(FlightModel[] flights, PriceModel[] prices)
        //{
        //    foreach (var flight in flights)
        //    {
        //        flight.Prices = prices.Where(p => p.FlightId == flight.Id).ToArray();
        //    }
        //    return flights;
        //}
    }
}

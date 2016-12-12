using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    public class SearchHelper
    {
        private Flight _flight { get; set; }
        private Passenger _passenger { get; set; }
        private FlightModel[] _flights { get; set; }
        public SearchHelper(Flight flight, Passenger passenger, FlightModel[] flights)
        {
            _flight = flight;
            _passenger = passenger;
            _flights = flights;
        }

        public void Search()
        {
            bool check = true;
            while (check)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("choose the type of search: Flight number, time, port, nearest or back to return to main menu: (fn/t/p/n/b)");
                Console.ResetColor();
                switch (Console.ReadLine())
                {
                    case "fn":
                        SearchFlightsByNumber();
                        break;
                    case "t":
                        SearchFlightsByDateTime();
                        break;
                    case "p":
                        SearchFlightsByCity();
                        break;
                    case "n":
                        SearchNearestFlights();
                        break;
                    case "pn":
                        SearchPassengersByName();
                        break;
                    case "pfn":
                        SearchPassengersByFlightNumber();
                        break;
                    case "pp":
                        SearchPassengersByPassport();
                        break;
                    case "eco":
                        SearchFlightsByEconomPrice();
                        break;
                    case "b":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("unknown identifier");
                        break;
                }
            }
        }

        public void SearchFlightsByNumber()
        {
            Console.WriteLine("type Flight number");
            var search = Console.ReadLine();
            var searchedFlights = _flights.Where(fn => string.Equals(fn.FlightNumber, search, StringComparison.OrdinalIgnoreCase)).ToArray();
            _flight.ViewFlights(searchedFlights);
        }

        public void SearchFlightsByDateTime()
        {
            Console.WriteLine("type time");
            var parsedSearch = DateTime.Parse(Console.ReadLine());
            var searchedFlights = _flights.Where(fn => fn.Schedule == parsedSearch).ToArray();
            _flight.ViewFlights(searchedFlights);
        }

        public void SearchFlightsByCity()
        {
            Console.WriteLine("type city/port");
            var search = Console.ReadLine();
            var searchedFlights = _flights.Where(fn => string.Equals(fn.CityPort, search, StringComparison.OrdinalIgnoreCase)).ToArray();
            _flight.ViewFlights(searchedFlights);
        }

        public void SearchNearestFlights()
        {
            Console.WriteLine("type dateTime");
            var parsedSearch = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("type port");
            var port = Console.ReadLine();
            Console.WriteLine("the nearest flight (1 hour)");
            var searchedFlights = _flights.Where(fn => string.Equals(fn.CityPort, port, StringComparison.OrdinalIgnoreCase) && (fn.Schedule - parsedSearch).TotalHours < 1).OrderBy(fn => fn.Schedule).ToArray();
            _flight.ViewFlights(searchedFlights);
        }

        public void SearchPassengersByName()
        {
            Console.WriteLine("type name");
            var search = Console.ReadLine().ToLower();
            var seachedPassengers = _flights.SelectMany(f => f.Passengers).Where(p => p.FirstName.ToLower().Contains(search) || p.SecondName.ToLower().Contains(search)).ToArray();
            _passenger.ViewPassenger(seachedPassengers);
        }

        public void SearchPassengersByFlightNumber()
        {
            Console.WriteLine("type Flight number");
            var search = Console.ReadLine();
            var seachedPassengers = _flights.SingleOrDefault(fn => string.Equals(fn.FlightNumber, search, StringComparison.OrdinalIgnoreCase))?.Passengers.ToArray();
            _passenger.ViewPassenger(seachedPassengers);
        }
        public void SearchPassengersByPassport()
        {
            Console.WriteLine("type passport");
            var search = Console.ReadLine();
            var seachedPassengers = _flights.SelectMany(f => f.Passengers).Where(p => p.Passport.Contains(search)).ToArray();
            _passenger.ViewPassenger(seachedPassengers);
        }

        public void SearchFlightsByEconomPrice()
        {
            Console.WriteLine("type economy ticket price");
            var search = Console.ReadLine();
            var searchedFlights = _flights.Where(f => f.Tickets.Any(t => t.FlightClass == FlightClass.Economy && t.Price < decimal.Parse(search))).ToArray();
            _flight.ViewFlights(searchedFlights);
        }
    }
}

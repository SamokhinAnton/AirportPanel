using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportPanel.Models;

namespace AirportPanel
{
    class Program : Base
    {
        static void Main()
        {
            var data = new DataParser();
            var flights = data.Flights;
            bool check = true;
            while (check)
            {
                var flight = new Flight();
                var passenger = new Passenger();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Select an action: Create flight, Create passenger, Delete flight, Delete passenger, Edit flight, Edit passenger, View flights, View prices, view passengers, Search, Emergency, Save&Exit: (cf/cp/ef/ep/df/dp/v/s/em/s&e)");
                Console.ResetColor();
                
                var action = Console.ReadLine().ToLower();
                switch (action) 
                {
                    case "cf":
                        flights = flight.Create(flights);
                        break;
                    case "cp":
                        flights = passenger.Create(flights);
                        break;
                    case "e":
                        flight.Edit(flights);
                        break;
                    case "ep":
                        passenger.Edit(flights);
                        break;
                    case "d":
                        flights = flight.Delete(flights);
                        break;
                    case "dpass":
                        flights = passenger.Delete(flights);
                        break;
                    case "v":
                        flight.View(flights);
                        break;
                    case "vpr":
                        flight.ViewPriceList(flights);
                        break;
                    case "vpass":
                        passenger.View(flights);
                        break;
                    case "s":
                        var search = new SearchHelper(flight, passenger, flights);
                        search.Search();
                        break;
                    case "em":
                        EmergencyMessage();
                        break;
                    case "exit":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("unknown identifier");
                        break;
                }
            }
        }
        

        //public static void View(FlightModel[] information)
        //{
        //    foreach (var item in information)
        //    {
        //        if (item.IsArrived)
        //        {
        //            Console.WriteLine("{5}) Flight {0} arrived to {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
        //                item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
        //        } else
        //        {
        //            Console.WriteLine("{5}) Flight {0} departed from {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
        //            item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
        //        }
        //    }
        //}
        
        public static void EmergencyMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A blizzard warning has been issued for this area.  Please seek shelter immediately.");
            Console.ResetColor();
        }
    }
}

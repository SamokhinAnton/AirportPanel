using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Flight
    {
        public FlightModel[] Create(FlightModel[] flights)
        {
            var newFlight = new FlightModel();
            Console.WriteLine("Enter Arriving or Departing");
            newFlight.IsArrived = bool.Parse(Console.ReadLine());
            Console.WriteLine("Enter Date type(DateTime)");
            newFlight.Schedule = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter FlightNumber type(string)");
            newFlight.FlightNumber = Console.ReadLine();
            Console.WriteLine("Enter City type(string)");
            newFlight.CityPort = Console.ReadLine();
            Console.WriteLine("Enter Airline type(string)");
            newFlight.Airline = Console.ReadLine();
            Console.WriteLine("Enter Gate type(int)");
            newFlight.Gate = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Status 0:CheckIn, 1:GateClosed, 2:Arrived, 3:DepartedAt, 4:Unknown, 5:Canceled, 6:ExpectedAt, 7:Delayed, 8:InFlight type(enum)");
            newFlight.Status = (FlightStatus)Enum.Parse(typeof(FlightStatus), Console.ReadLine());
            Console.WriteLine("Enter Terminal type(char)");
            newFlight.Terminal = char.Parse(Console.ReadLine());
            newFlight.Tickets = new TicketModel[2];
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Type price for {0}", (FlightClass)i);
                newFlight.Tickets[i] = new TicketModel
                {
                    FlightClass = (FlightClass)i,
                    Price = decimal.Parse(Console.ReadLine()),
                    Id = i
                };
            }
            newFlight.Id = (flights.OrderBy(f => f.Id).LastOrDefault()?.Id ?? 0) + 1;
            newFlight.Passengers = new PassengerModel[0];
            var tempArr = new FlightModel[flights.Length+1];
            Array.Copy(flights, tempArr, flights.Length);
            tempArr[tempArr.Length - 1] = newFlight;
            return tempArr;
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

        public FlightModel[] Delete(FlightModel[] flights)
        {
            Console.WriteLine("Enter the id of the record to remove");
            var id = int.Parse(Console.ReadLine());
            return flights.Where(f => f.Id != id).ToArray();
        }
    }
}

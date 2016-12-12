using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    public class Flight : IFlight<FlightModel>
    {
        public FlightModel[] Create(FlightModel[] flights)
        {
            var newFlight = new FlightModel();
            FillMainFlightFields(newFlight);
            newFlight.Tickets = new TicketModel[2];
            FillFlightTickets(newFlight);
            newFlight.Id = (flights.OrderBy(f => f.Id).LastOrDefault()?.Id ?? 0) + 1;
            newFlight.Passengers = new PassengerModel[0];
            var tempArr = new FlightModel[flights.Length+1];
            Array.Copy(flights, tempArr, flights.Length);
            tempArr[tempArr.Length - 1] = newFlight;
            return tempArr;
        }

        public void FillMainFlightFields(FlightModel newFlight)
        {
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
        }

        public void FillFlightTickets(FlightModel newFlight)
        {
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
        }

        public void View(FlightModel[] flights)
        {
            Console.WriteLine("Enter a or d for arrivels or departures flights");
            var status = Console.ReadLine().ToLower();
            if(string.Equals("a", status, StringComparison.OrdinalIgnoreCase))
            {
                ViewFlights(flights.Where(f => f.IsArrived).ToArray());
            } else if(string.Equals("d", status, StringComparison.OrdinalIgnoreCase))
            {
                ViewFlights(flights.Where(f => !f.IsArrived).ToArray());
            }
            
        }

        public void ViewPriceList(FlightModel[] flights)
        {
            foreach (var flight in flights)
            {
                Console.WriteLine("Ticket prices for flight {0}", flight.FlightNumber);
                foreach(var price in flight.Tickets)
                {
                    Console.WriteLine(" - {0} : {1}$", price.FlightClass, price.Price);
                }
            }
        }

        public FlightModel[] Delete(FlightModel[] flights)
        {
            Console.WriteLine("Enter the id of the record to remove");
            var id = int.Parse(Console.ReadLine());
            return flights.Where(f => f.Id != id).ToArray();
        }

        public void Edit(FlightModel[] flights)
        {
            Console.WriteLine("Enter the id of the record to edit");
            var id = int.Parse(Console.ReadLine());
            var flight = flights.SingleOrDefault(f => f.Id == id);
            Console.WriteLine("Enter Arriving or Departing/nCurrent value: {0}", flight.IsArrived);
            flight.IsArrived = bool.Parse(Console.ReadLine());
            Console.WriteLine("Enter Date type(DateTime)/nCurrent value: {0}", flight.Schedule);
            flight.Schedule = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter FlightNumber type(string)/nCurrent value: {0}", flight.FlightNumber);
            flight.FlightNumber = Console.ReadLine();
            Console.WriteLine("Enter City type(string)/nCurrent value: {0}", flight.CityPort);
            flight.CityPort = Console.ReadLine();
            Console.WriteLine("Enter Airline type(string)/nCurrent value: {0}", flight.Airline);
            flight.Airline = Console.ReadLine();
            Console.WriteLine("Enter Gate type(int)/nCurrent value: {0}", flight.Gate);
            flight.Gate = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Status 0:CheckIn, 1:GateClosed, 2:Arrived, 3:DepartedAt, 4:Unknown, 5:Canceled, 6:ExpectedAt, 7:Delayed, 8:InFlight type(enum)/nCurrent value: {0}", flight.Status);
            flight.Status = (FlightStatus)Enum.Parse(typeof(FlightStatus), Console.ReadLine());
            Console.WriteLine("Enter Terminal type(char)/nCurrent value: {0}", flight.Terminal);
            flight.Terminal = char.Parse(Console.ReadLine());
        }

        public void ViewFlights(FlightModel[] information)
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
    }
}

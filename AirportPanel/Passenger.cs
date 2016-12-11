using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Passenger : IFlight<FlightModel>
    {
        public FlightModel[] Create(FlightModel[] flights)
        {
            Console.WriteLine("type flight number");
            var flightNumber = Console.ReadLine();
            var flight = flights.SingleOrDefault(f => string.Equals(f.FlightNumber, flightNumber, StringComparison.OrdinalIgnoreCase));
            var newPassenger = new PassengerModel();
            var passengers = flight.Passengers;
            FillMainPassengerFields(newPassenger, flight.Tickets);
            Array.Resize(ref passengers, passengers.Length + 1);
            passengers[passengers.Length - 1] = newPassenger;
            flight.Passengers = passengers;
            return flights;
        }

        public void FillMainPassengerFields(PassengerModel newPassenger, TicketModel[] flightTickets)
        {
            Console.WriteLine("Enter First name");
            newPassenger.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Second name");
            newPassenger.SecondName = Console.ReadLine();
            Console.WriteLine("Enter Passport");
            newPassenger.Passport = Console.ReadLine();
            Console.WriteLine("Enter Nationality");
            newPassenger.Nationality = Console.ReadLine();
            Console.WriteLine("Enter Date of birthday");
            newPassenger.DateOfBirthday = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Sex 0:male, 1:female");
            newPassenger.Sex = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine());
            Console.WriteLine("Choose ticket:");
            for (int i = 0; i < Enum.GetValues(typeof(FlightClass)).Length; i++)
            {
                Console.WriteLine("   - {0}:{1}", i, (FlightClass)i);
            }
            var ticket = (FlightClass)Enum.Parse(typeof(FlightClass), Console.ReadLine());
            newPassenger.Ticket = flightTickets.SingleOrDefault(f => f.FlightClass == ticket);
        }

        public void View(FlightModel[] flights)
        {
            Console.WriteLine("write flight number");
            var flightNumber = Console.ReadLine();
            var flight = flights.SingleOrDefault(f => string.Equals(f.FlightNumber,flightNumber, StringComparison.OrdinalIgnoreCase));
            ViewPassenger(flight);
        }

        public void Edit(FlightModel[] flights)
        {
            Console.WriteLine("Write flight number");
            var flightNumber = Console.ReadLine();
            var flight = flights.SingleOrDefault(f => string.Equals(f.FlightNumber, flightNumber, StringComparison.OrdinalIgnoreCase));
            ViewPassenger(flight);
            Console.WriteLine("choose passenger Id");
            var passengerId = int.Parse(Console.ReadLine());
            var passenger = flight.Passengers.SingleOrDefault(p => p.Id == passengerId);
            FillMainPassengerFields(passenger, flight.Tickets);

        }

        public FlightModel[] Delete(FlightModel[] flights)
        {
            Console.WriteLine("Selecet flight Id");
            var flightNumber = Console.ReadLine();
            var flight = flights.SingleOrDefault(f => string.Equals(f.FlightNumber, flightNumber, StringComparison.OrdinalIgnoreCase));
            ViewPassenger(flight);
            Console.WriteLine("choose passenger Id to delete");
            var passengerId = int.Parse(Console.ReadLine());
            flight.Passengers = flight.Passengers.Where(p => p.Id != passengerId).ToArray();
            return flights;
        }

        public void ViewPassenger(FlightModel flight)
        {
            foreach (var passenger in flight.Passengers)
            {
                Console.WriteLine(" - Passenger {3}: {0} {1}, ticket: {2}", passenger.FirstName, passenger.SecondName, passenger.Ticket.FlightClass, passenger.Id);
            }
        }
    }
}

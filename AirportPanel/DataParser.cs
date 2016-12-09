using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    public class DataParser : Base
    {
        public FlightModel[] Flights { get; private set; }

        public DataParser()
        {
            Parser();
        }

        public void Parser()
        {
            var lines = FileHelper.ReadLines(FlightsPath);
            Flights = new FlightModel[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var information = lines[i].Split(';');
                var flightsMainInfo = information[0].Split('|');
                Flights[i] = ParseFlight(flightsMainInfo);
                if (information.Length > 1)
                {
                    var priceLines = information[1].Split('/');
                    Flights[i].Tickets = ParsePrices(priceLines);
                }
                if (information.Length > 1)
                {
                    var passengerLines = information[2].Split('/');
                    Flights[i].Passengers = ParsePassengers(passengerLines, Flights[i].Tickets);
                }
            };
        }

        public FlightModel ParseFlight(string[] flightsMainInfo)
        {
            var flight = new FlightModel
            {
                Id = int.Parse(flightsMainInfo[0]),
                IsArrived = bool.Parse(flightsMainInfo[1]),
                Schedule = DateTime.Parse(flightsMainInfo[2]),
                FlightNumber = flightsMainInfo[3],
                CityPort = flightsMainInfo[4],
                Airline = flightsMainInfo[5],
                Gate = int.Parse(flightsMainInfo[6]),
                Status = (FlightStatus)(int.Parse(flightsMainInfo[7])),
                Terminal = char.Parse(flightsMainInfo[8])
            };
            return flight;
        }

        public TicketModel[] ParsePrices(string[] priceLines)
        {
            var prices = new TicketModel[priceLines.Length];
            for (int j = 0; j < priceLines.Length; j++)
            {
                var price = priceLines[j].Split('|');
                prices[j] = new TicketModel()
                {
                    Id = int.Parse(price[0]),
                    FlightClass = (FlightClass)int.Parse(price[1]),
                    Price = decimal.Parse(price[2])
                };
            }
            return prices;
        }

        public PassengerModel[] ParsePassengers(string[] passengerLines, TicketModel[] tickets)
        {
            var passengers = new PassengerModel[passengerLines.Length];
            for (int j = 0; j < passengerLines.Length; j++)
            {
                var passenger = passengerLines[j].Split('|');
                passengers[j] = new PassengerModel()
                {
                    Id = int.Parse(passenger[0]),
                    FirstName = passenger[1],
                    SecondName = passenger[2],
                    Nationality = passenger[3],
                    Passport = passenger[4],
                    DateOfBirthday = DateTime.Parse(passenger[5]).Date,
                    Sex = (Sex)int.Parse(passenger[6]),
                    Ticket = tickets.SingleOrDefault(t => t.Id == int.Parse(passenger[7]))
                };
            }
            return passengers;
        }
        
    }
}

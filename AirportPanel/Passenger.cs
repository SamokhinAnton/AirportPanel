using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Passenger : IFlight<PassengerModel>
    {
        private PassengerModel[] _passengers { get; set; }
        public Passenger(string path, FlightModel[] flights)
        {
            var lines = FileHelper.ReadLines(path);
            _passengers = new PassengerModel[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var arrInformation = lines[i].Split('|');
                _passengers[i] = new PassengerModel
                {
                    Id = int.Parse(arrInformation[0]),
                    FirstName = arrInformation[1],
                    SecondName = arrInformation[2],
                    Nationality = arrInformation[3],
                    Passport = arrInformation[4],
                    DateOfBirthday = DateTime.Parse(arrInformation[5]),
                    Sex = (Sex)int.Parse(arrInformation[6]),
                    FlightId = int.Parse(arrInformation[7]),
                    FlightClass = (FlightClass)int.Parse(arrInformation[8]),
                    Flight = flights.SingleOrDefault(f => f.Id == int.Parse(arrInformation[7]))
                };
            };
        }

        public PassengerModel[] Parse(string path)
        {
            throw new NotImplementedException();
        }
    }
}

using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Passenger : Base, IFlight<PassengerModel>
    {
        public PassengerModel[] _passengers { get; private set; }
        public Passenger(FlightModel[] flights)
        {
            
        }

        //public void Parse()
        //{
        //    var lines = FileHelper.ReadLines(PassengerPath);
        //    _passengers = new PassengerModel[lines.Length];
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        var arrInformation = lines[i].Split('|');
        //        _passengers[i] = new PassengerModel
        //        {
        //            Id = int.Parse(arrInformation[0]),
        //            FirstName = arrInformation[1],
        //            SecondName = arrInformation[2],
        //            Nationality = arrInformation[3],
        //            Passport = arrInformation[4],
        //            DateOfBirthday = DateTime.Parse(arrInformation[5]),
        //            Sex = (Sex)int.Parse(arrInformation[6]),
        //            FlightClass = (FlightClass)int.Parse(arrInformation[8])
        //        };
        //    };
        //}
    }
}

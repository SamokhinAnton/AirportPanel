using AirportPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Passenger
    {
        public PassengerModel[] Create(PassengerModel[] passengers)
        {
            var newPassenger = new PassengerModel();

        }
        public void FillMainPassengerFields(PassengerModel newPassenger)
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
            
        }
    }
}

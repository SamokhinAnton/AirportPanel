using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirportPanel.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Nationality { get; set; }

        public string Passport { get; set; }

        public DateTime DateOfBirthday { get; set; }

        public Sex Sex { get; set; }

        public FlightClass FlightClass { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }
}

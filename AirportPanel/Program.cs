using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel
{
    class Program
    {
        static void Main()
        {
            const string path = @"../../db.txt";
            var information = new FlightInformation[0];
            string[] fileFieldsName;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                fileFieldsName = sr.ReadLine().Split('|');
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        information = ParseInformation(information, line);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Select an action: Create, Delete, Edit, View, Search, Emergency: (c/e/d/v/s/em)");
            Console.ResetColor();
            var action = Console.ReadLine().ToLower();
            switch (action) 
            {
                case "c":
                    Create(path, information, fileFieldsName, information.LastOrDefault().Id);
                    Main();
                    break;
                case "e":
                    Console.WriteLine("Enter the id of the record to edit");
                    Edit(path, fileFieldsName, Console.ReadLine());
                    break;
                case "d":
                    Console.WriteLine("Enter the id of the record to remove");
                    Delete(path, Console.ReadLine());
                    break;
                case "v":
                    View(information);
                    break;
                case "s":
                    Search(information);
                    break;
                case "em":
                    EmergencyMessage();
                    break;
                default:
                    Console.WriteLine("unknown identifier");
                    Main();
                    break;
            }

        }

        public enum FlightStatus
        {
            CheckIn,
            GateClosed,
            Arrived,
            DepartedAt,
            Unknown,
            Canceled,
            ExpectedAt,
            Delayed,
            InFlight
        }

        public struct FlightInformation
        {
            public int Id;
            public bool IsArrived;
            public DateTime Schedule;
            public string FlightNumber;
            public string CityPort;
            public string Airline;
            public int Gate;
            public FlightStatus Status;
            public char Terminal;
        }

        public static FlightInformation[] ParseInformation(FlightInformation[] information, string line)
        {
            var temp = new FlightInformation[information.Length + 1];
            Array.Copy(information, temp, information.Length);
            var arrInformation = line.Split('|');
            temp[temp.Length - 1] = new FlightInformation
            {
                Id = int.Parse(arrInformation[0]),
                IsArrived = bool.Parse(arrInformation[1]),
                Schedule = DateTime.Parse(arrInformation[2]),
                FlightNumber = arrInformation[3],
                CityPort = arrInformation[4],
                Airline = arrInformation[5],
                Gate = int.Parse(arrInformation[6]),
                Status = (FlightStatus)(int.Parse(arrInformation[7])),
                Terminal = char.Parse(arrInformation[8])     
            };
            return temp;
        }

        public static void Create(string path, FlightInformation[] information, string[] fileFieldsName, int Last = 0)
        {
            var fileText = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                fileText = sr.ReadToEnd();
            }
            
            var temp = new string[fileFieldsName.Length];
            temp[0] = (++Last).ToString();
            for (int i = 1; i < fileFieldsName.Length; i++)
            {
                Console.WriteLine("Enter {0}", fileFieldsName[i]);
                temp[i] = Console.ReadLine();
            }
            var str = string.Join("|", temp);
            try { 
                information = ParseInformation(information, str);
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
                {
                    sw.WriteLine(fileText);
                    sw.WriteLine(str);
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Main();
            }
        }

        public static void Delete(string path, string id)
        {
            string allLines = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line) && !string.Equals(line.Split('|')[0], id, StringComparison.OrdinalIgnoreCase))
                    {
                        allLines += line + '\n';
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(allLines);
            }
            Console.Clear();
            Main();
        }

        public static void View(FlightInformation[] information)
        {
            foreach (var item in information)
            {
                if (item.IsArrived)
                {
                    Console.WriteLine("{5}) Flight {0} arrived to {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
                        item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
                } else
                {
                    Console.WriteLine("{5}) Flight {0} departed from {1} airport {2} at {3}. The flight is operated by airlines {4}. Current status is {6}",
                    item.FlightNumber, item.CityPort, item.Schedule.ToString("D", CultureInfo.InvariantCulture), item.Schedule.ToString("HH:mm"), item.Airline, item.Id, item.Status);
                }
            }
            Main();
        }

        public static void Edit(string path, string[] fileFieldsName, string id)
        {
            string allLines = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                var testArrayInfo = new FlightInformation[0];
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line) && string.Equals(line.Split('|')[0], id, StringComparison.OrdinalIgnoreCase))
                    {
                        var editArray = line.Split('|');
                        for (int i = 1; i < fileFieldsName.Length; i++)
                        {
                            Console.WriteLine("Enter {0} to change or write 'l' to leave the value", fileFieldsName[i]);
                            Console.WriteLine("current value: {0}", editArray[i]);
                            var item = Console.ReadLine();
                            if (string.Equals(item, "l", StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            editArray[i] = item;
                        }
                        try
                        {
                            var newline = string.Join("|", editArray);
                            ParseInformation(testArrayInfo, newline);
                            line = newline;
                        } catch(Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("your data is not saved");
                            Console.WriteLine(e.Message);
                        }
                    }
                    allLines += line + '\n';
                }
            }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(allLines);
            }
            Main();
        }

        public static void Search(FlightInformation[] information)
        {
            Console.WriteLine("choose the type of search: Flight number, time, port, nearest or back to return to main menu: (fn/t/p/n/b)");
            FlightInformation[] searchedInformation;
            string search;
            switch (Console.ReadLine())
            {
                case "fn":
                    Console.WriteLine("type Flight number");
                    search = Console.ReadLine();
                    searchedInformation = information.Where(fn => string.Equals(fn.FlightNumber, search, StringComparison.OrdinalIgnoreCase)).ToArray();
                    View(searchedInformation);
                    break;
                case "t":
                    Console.WriteLine("type time");
                    search = Console.ReadLine();
                    searchedInformation = information.Where(fn => fn.Schedule == DateTime.Parse(search)).ToArray();
                    View(searchedInformation);
                    break;
                case "p":
                    Console.WriteLine("type city/port");
                    search = Console.ReadLine();
                    searchedInformation = information.Where(fn => string.Equals(fn.CityPort, search, StringComparison.OrdinalIgnoreCase)).ToArray();
                    View(searchedInformation);
                    break;
                case "n":
                    Console.WriteLine("type dateTime");
                    search = Console.ReadLine();
                    Console.WriteLine("type port");
                    var port = Console.ReadLine();
                    Console.WriteLine("the nearest flight (1 hour)");
                    searchedInformation = information.Where(fn => string.Equals(fn.CityPort, port, StringComparison.OrdinalIgnoreCase) && (fn.Schedule - DateTime.Parse(search)).Hours < 1).OrderBy(fn => fn.Schedule).ToArray();
                    View(searchedInformation);
                    break;
                case "b":
                    Main();
                    break;
                default:
                    Console.WriteLine("unknown identifier");
                    Search(information);
                    break;
            }
        }

        public static void EmergencyMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A blizzard warning has been issued for this area.  Please seek shelter immediately.");
            Console.ResetColor();
            Main();
        }
    }
}

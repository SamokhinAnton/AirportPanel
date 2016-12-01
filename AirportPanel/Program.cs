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
            
            bool check = true;
            while (check)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Select an action: Create, Delete, Edit, View, Search, Emergency, Exit: (c/e/d/v/s/em/exit)");
                Console.ResetColor();
                const string path = @"../../db.txt";
                var allLines = File.ReadAllLines(path, Encoding.Default).Where(l => !string.IsNullOrEmpty(l)).ToArray();
                var fileFieldsName = allLines[0].Split('|');
                var content = allLines.Where((l, i) => !string.IsNullOrEmpty(l) && i > 0).ToArray();
                var information = ParseInformation(content);

                var action = Console.ReadLine().ToLower();
                switch (action) 
                {
                    case "c":
                        Create(path, fileFieldsName, content, information, information.LastOrDefault().Id);
                        break;
                    case "e":
                        Console.WriteLine("Enter the id of the record to edit");
                        Edit(path, fileFieldsName, content, information, Console.ReadLine());
                        break;
                    case "d":
                        Console.WriteLine("Enter the id of the record to remove");
                        Delete(path, content, fileFieldsName, Console.ReadLine());
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
                    case "exit":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("unknown identifier");
                        break;
                }
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

        public static FlightInformation[] ParseInformation(string[] lines)
        {
            var informations = new FlightInformation[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var arrInformation = lines[i].Split('|');
                informations[i] = new FlightInformation
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
            };
            return informations;
        }

        public static void WriteLine(string path, string[] lines, string fileFieldsname)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(fileFieldsname);
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        public static void Create(string path, string[] fileFieldsName, string[] content, FlightInformation[] information, int last = 0)
        {            
            var temp = new string[fileFieldsName.Length];
            temp[0] = (++last).ToString();
            for (int i = 1; i < fileFieldsName.Length; i++)
            {
                Console.WriteLine("Enter {0}", fileFieldsName[i]);
                temp[i] = Console.ReadLine();
            }
            var str = string.Join("|", temp);
            try {
                temp = new string[content.Length + 1];
                Array.Copy(content, temp, content.Length);
                temp[temp.Length - 1] = str;
                information = ParseInformation(temp);
                WriteLine(path, temp, string.Join("|", fileFieldsName));
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Delete(string path, string[] content, string[] fileFieldsName, string id)
        {
            var lines = content.Where(c => !string.Equals(c.Split('|')[0], id, StringComparison.OrdinalIgnoreCase)).ToArray();
            WriteLine(path, lines, string.Join("|", fileFieldsName));
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
        }

        public static void Edit(string path, string[] fileFieldsName, string[] content, FlightInformation[] information, string id)
        {
            for(var i = 0; i < content.Length; i++)
            {
                if (!string.IsNullOrEmpty(content[i]) && string.Equals(content[i].Split('|')[0], id, StringComparison.OrdinalIgnoreCase))
                {
                    var editArray = content[i].Split('|');
                    for (int j = 1; j < fileFieldsName.Length; j++)
                    {
                        Console.WriteLine("Enter {0} to change or write 'l' to leave the value", fileFieldsName[j]);
                        Console.WriteLine("current value: {0}", editArray[j]);
                        var item = Console.ReadLine();
                        if (string.Equals(item, "l", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        editArray[j] = item;
                    }
                    content[i] = string.Join("|", editArray);
                }
                try
                {
                    information = ParseInformation(content);
                    WriteLine(path, content, string.Join("|", fileFieldsName));
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("your data is not saved");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void Search(FlightInformation[] information)
        {
            FlightInformation[] searchedInformation;
            string search;
            bool check = true;
            while (check)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("choose the type of search: Flight number, time, port, nearest or back to return to main menu: (fn/t/p/n/b)");
                Console.ResetColor();
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
                        var parsedSearch = DateTime.Parse(search);
                        searchedInformation = information.Where(fn => fn.Schedule == parsedSearch).ToArray();
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
                        parsedSearch = DateTime.Parse(search);
                        searchedInformation = information.Where(fn => string.Equals(fn.CityPort, port, StringComparison.OrdinalIgnoreCase) && (fn.Schedule - parsedSearch).TotalHours < 1).OrderBy(fn => fn.Schedule).ToArray();
                        View(searchedInformation);
                        break;
                    case "b":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("unknown identifier");
                        break;
                }
            }
        }

        public static void EmergencyMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A blizzard warning has been issued for this area.  Please seek shelter immediately.");
            Console.ResetColor();
        }
    }
}

using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using OOP_lab6;
using Microsoft.VisualBasic;

namespace OOP_lab6
{
    public class Program
    {
        public enum MenuOptions
        {
            AddFlightInfo = 1,
            RemoveFlightInfo,
            GetAllFlightByAirCompany,
            GetAllFlightByStatus,
            Exit,
            xs
        }

        public static Flight GetFlightInfoFromUser()
        {
            var flightInfo = new Flight();
            Console.WriteLine(@"Enter aircompany name:\t");
            flightInfo.Airline = Console.ReadLine();
            Console.WriteLine(@"Enter aircraft number name:\t");
            flightInfo.FlightNumber = Console.ReadLine();
            Console.WriteLine("Enter destination:/t");
            flightInfo.Destination = Console.ReadLine();
            Console.WriteLine("Enter departure time/t");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime departureTime))
            {
                flightInfo.DepartureTime = departureTime;
            }
            else
            {
                Console.WriteLine("Invalid date format. Using default departure time.");
            }

            Console.WriteLine("Enter arrival time:");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime arrivalTime))
            {
                flightInfo.ArrivalTime = arrivalTime;
            }
            else
            {
                Console.WriteLine("Invalid date format. Using default arrival time.");
            }
            Console.WriteLine("Enter flight status (OnTime, Delayed, Cancelled, Boarding, InFlight):");
            if (Enum.TryParse(Console.ReadLine(), out FlightStatus status))
            {
                flightInfo.Status = status;
            }
            else
            {
                Console.WriteLine("Invalid status. Using default status.");
            }

            Console.WriteLine("Enter duration:");
            if (TimeSpan.TryParse(Console.ReadLine(), out TimeSpan duration))
            {
                flightInfo.Duration = duration;
            }
            else
            {
                Console.WriteLine("Invalid duration format. Using default duration.");
            }

            Console.WriteLine("Enter aircraft type:");
            flightInfo.AircraftType = Console.ReadLine();

            Console.WriteLine("Enter terminal:");
            flightInfo.Terminal = Console.ReadLine();




            return flightInfo;
        }

        public static FlightStatus GetFlightStatusFromUser()
        {
            Console.WriteLine(@"Select flight status:");
            Console.WriteLine(@"1.) OnTime");
            Console.WriteLine(@"2.) Delayed");
            Console.WriteLine(@"3.) Cancelled");
            Console.WriteLine(@"4.) Boarding");
            Console.WriteLine(@"5.) InFlight");

            Byte flightStatus;

            while (!Byte.TryParse(Console.ReadLine(), out flightStatus))
            {
                Console.WriteLine($"Введено неправильні дані, перевірте та спробуйте знову");
            }

            FlightStatus selectedflightStatus = (FlightStatus)(flightStatus - 1);

            return selectedflightStatus;

        }

        public static void Menu(FlightInformationSystem flightSystem)
        {

            Console.WriteLine("Select menu item:");
            Console.WriteLine("1.) Add flight info");
            Console.WriteLine("2.) Remove flight info");
            Console.WriteLine("3.) Get all flights by aircompany");
            Console.WriteLine("4.) Get all flights by status");
            Console.WriteLine("5.) Exit");

            Byte menuOption;

            while (!Byte.TryParse(Console.ReadLine(), out menuOption))
            {
                Console.WriteLine($"Введено неправильні дані, перевірте та спробуйте знову");
            }

            MenuOptions selectedOption = (MenuOptions)menuOption;

            switch (selectedOption)
            {
                case MenuOptions.AddFlightInfo:
                    flightSystem.AddFlightInfo(GetFlightInfoFromUser());
                    Console.ReadLine();
                    break;
                case MenuOptions.RemoveFlightInfo:
                    Console.ReadLine();
                    break;
                case MenuOptions.GetAllFlightByAirCompany:
                    Console.WriteLine(@"Enter aircompany name");
                    flightSystem.GetAllFlightsByAirCompany(Console.ReadLine());
                    Console.ReadLine();
                    break;
                case MenuOptions.GetAllFlightByStatus:
                    flightSystem.GetAllFlightsByStatus(GetFlightStatusFromUser());
                    Console.ReadLine();
                    break;
                case MenuOptions.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            var sourceFilePath = Path.Combine(AppContext.BaseDirectory, @"flights_data.json");
            var outputFilePath = Path.Combine(AppContext.BaseDirectory, @"result.json");

            var flightSystem = new FlightInformationSystem(sourceFilePath,
                                                          outputFilePath);
            while (true)
            {
                Menu(flightSystem);
            }

        }
    }
}
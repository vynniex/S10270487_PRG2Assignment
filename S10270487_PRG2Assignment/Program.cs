//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using S10270487_PRG2Assignment;
using S10270487_PRG2Assignment.Specialized_Flight_Classes;

// Dictionaries -
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

// Load All Files -
LoadAirlines();
LoadBoardingGates();
LoadFlights();

// Main Program
while (true)
{
    DisplayMenu();
    Console.Write("\nPlease select your option: ");
    string option = Console.ReadLine().Trim();

    if (option == "1")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Flights for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        ListAllFlights();
    }
    else if (option == "2")
    {
        Console.WriteLine("");
    }
    else if (option == "3")
    {
        Console.WriteLine("");
    }
    else if (option == "4")
    {
        Console.WriteLine("");
    }
    else if (option == "5")
    {
        Console.WriteLine("");
    }
    else if (option == "6")
    {
        Console.WriteLine("");
    }
    else if (option == "7")
    {
        Console.WriteLine("");
    }
    else if (option == "0")
    {
        break;
    }
}
Console.WriteLine("Goodbye!");

// DisplayMenu Method -
void DisplayMenu()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine("----------------------------------------------");
}

// Load Methods
void LoadAirlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        sr.ReadLine();  // skips the header
        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string airName = data[0].Trim();
            string airlineCode = data[1].Trim();

            Airline airline = new Airline(airlineCode, airName);
            airlineDict[airlineCode] = airline;
        }
    }
}

void LoadBoardingGates()
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        sr.ReadLine();  // skips the header
        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string gateNo = data[0].Trim();
            string specialReqCode = data.Length > 1 ? data[1].Trim() : null;

            bool supportsCFTT = specialReqCode == "CFFT";
            bool supportsDDJB = specialReqCode == "DDJB";
            bool supportsLWTT = specialReqCode == "LWTT";

            BoardingGate gate = new BoardingGate(gateNo, supportsCFTT, supportsDDJB, supportsLWTT);
            boardingGateDict[gateNo] = gate;
        }
    }
}

void LoadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        sr.ReadLine();  // skips the header

        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string flightNo = data[0].Trim();
            string flightOrigin = data[1].Trim();
            string flightDest = data[2].Trim();
            DateTime expDepArrTime = DateTime.Parse(data[3].Trim());
            string specialReqCode = data.Length > 4 ? data[4].Trim() : "";  // handles empty requests

            Flight flight;

            if (specialReqCode == "CFFT")
            {
                flight = new CFFTFlight(flightNo, flightOrigin, flightDest, expDepArrTime, "Scheduled", 500);
            }
            else if (specialReqCode == "DDJB")
            {
                flight = new DDJBFlight(flightNo, flightOrigin, flightDest, expDepArrTime, "Scheduled", 700);
            }
            else if (specialReqCode == "LWTT")
            {
                flight = new LWTTFlight(flightNo, flightOrigin, flightDest, expDepArrTime, "Scheduled", 600);
            }
            else
            {
                flight = new NORMFlight(flightNo, flightOrigin, flightDest, expDepArrTime, "Scheduled");
            }
            
            flightDict[flightNo] = flight;  // adds flight to dict
        }
    }
}

// Option 1 - List All Flights
void ListAllFlights()
{
    Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-15}", 
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    foreach (var flight in flightDict.Values)
    {
        string airlineCode = flight.FlightNumber.Substring(0, 2).Trim();
        string airlineName = "";

        if (airlineDict.ContainsKey(airlineCode))
        {
            airlineName = airlineDict[airlineCode].Name;
        }

        Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-15}",
        flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt"));
    }
}

// Option 2 - List Boarding Gates
void AssignBoardingGate()
{

}

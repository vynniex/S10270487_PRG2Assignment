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
LoadBoardingGates();

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
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        ListBoardingGates();
    }
    else if (option == "3")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Assign a Boarding Gate to a Flight");
        Console.WriteLine("=============================================");
        AssignBoardingGates();
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
            string airlineName = data[0].Trim();
            string airlineCode = data[1].Trim();

            Airline airline = new Airline(airlineName, airlineCode);
            airlineDict[airlineCode] = airline;
        }
    }
}

void LoadBoardingGates()
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        sr.ReadLine();

        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string gateNo = data[0].Trim();
            bool supportsDDJB = bool.Parse(data[1].Trim());
            bool supportsCFFT = bool.Parse(data[2].Trim());
            bool supportsLWTT = bool.Parse(data[3].Trim());

            BoardingGate gate = new BoardingGate(gateNo, supportsCFFT, supportsDDJB, supportsLWTT);
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

            string airlineCode = flightNo.Split(' ')[0];

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

            airlineDict[airlineCode].AddFlight(flight); // adds flight to airline

            flightDict[flightNo] = flight;  // adds flight to flightDict
        }
    }
}

// Option 1 - List All Flights
void ListAllFlights()
{
    Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-15}", 
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    IOrderedEnumerable<Flight> sortedFlights = flightDict.Values.OrderBy(flight => flight.ExpectedTime);

    foreach (var flight in sortedFlights)
    {
        string airlineCode = flight.FlightNumber.Substring(0, 2);
        string airlineName = airlineDict.ContainsKey(airlineCode) ? airlineDict[airlineCode].Name : "Unknown";

        Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-25}",
            flight.FlightNumber, airlineName, flight.Origin, flight.Destination, flight.ExpectedTime.ToString("d/M/yyyy h:mm:ss tt"));
    }
}

// Option 2 - List Boarding Gates
void ListBoardingGates()
{
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20}",
        "Gate Name", "DDJB", "CFFT", "LWTT");

    foreach (var gate in boardingGateDict.Values)
    {
        string ddjb = gate.SupportsDDJB ? "True" : "False";
        string cfft = gate.SupportsCFFT ? "True" : "False";
        string lwtt = gate.SupportsLWTT ? "True" : "False";

        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20}",
            gate.GateName, ddjb, cfft, lwtt);
    }
}

// Option 3 - Assign Boarding Gate To A Flight
void AssignBoardingGates()
{
    Console.WriteLine("Enter Flight Number: ");
    string flightNo = Console.ReadLine().Trim();

    Flight flight = flightDict[flightNo];

    Console.WriteLine("Enter Boarding Gate Name: ");
    string gateNo = Console.ReadLine().Trim();

    BoardingGate gate = boardingGateDict[gateNo];

    string specialReqCode = "None";
    if (flight is CFFTFlight) specialReqCode = "CFFT";
    else if (flight is DDJBFlight) specialReqCode = "DDJB";
    else if (flight is LWTTFlight) specialReqCode = "LWTT";

    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
    Console.WriteLine($"Origin: {flight.Origin}");
    Console.WriteLine($"Destination: {flight.Destination}");
    Console.WriteLine($"Expected Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
    Console.WriteLine($"Special Request Code: {specialReqCode}");

    Console.WriteLine($"Boarding Gate Name: {gate.GateName}");
    Console.WriteLine($"Supports DDJB: {gate.SupportsDDJB}");
    Console.WriteLine($"Supports CFFT: {gate.SupportsCFFT}");
    Console.WriteLine($"Supports LWTT: {gate.SupportsLWTT}");

    Console.WriteLine("Would you like to update the status of this flight? (Y/N)");
    string choice = Console.ReadLine().Trim().ToUpper();

    if (choice == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.WriteLine("Please select the new status of the flight: ");

        string statusOption = Console.ReadLine().Trim();

        if (statusOption == "1")
        {
            flight.Status = "Delayed";
        }
        else if (statusOption == "2")
        {
            flight.Status = "Boarding";
        }
        else if (statusOption == "3")
        {
            flight.Status = "On Time";
        }

        gate.Flight = flight;
        Console.WriteLine($"Flight {flightNo} has been assigned to Boarding Gate {gateNo}!");
    }
}

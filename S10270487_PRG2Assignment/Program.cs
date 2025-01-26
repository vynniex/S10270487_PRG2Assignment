//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using S10270487_PRG2Assignment;

// Main Program
while (true)
{
    DisplayMenu();
    Console.WriteLine("Please select your option: ");
    string option = Console.ReadLine();
    if (option == "0") { break; }
    else if (option == "1")
    {
        Console.WriteLine("");
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
}
Console.WriteLine("Goodbye!");

// Methods
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
    Console.WriteLine("---------------------------");
}

// Dictionaries
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

// load files
LoadFlights(flightDict);

void LoadFlights(Dictionary<string, Flight> flightDict)
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string s = sr.ReadLine();

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string fNum = data[0];
            string fOrigin = data[1];
            string fDest = data[2];
            DateTime eDepart_Arrival = DateTime.Parse(data[3]);

            // Add to the flight dictionary
            Flight flight = new Flight(fNum, fOrigin, fDest, eDepart_Arrival);
            flightDict[fNum] = flight;
        }
    }

    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string s = sr.ReadLine();

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(',');
            string airname = data[0];
            string aircode = data[1];

            // Add to the airline dictionary
            Airline airline = new Airline(aircode, airname);
            airlineDict[aircode] = airline;
        }
    }
    Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
                "Flight Number", "Airline Number", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string, Airline> a in airlineDict)
    {

        foreach (KeyValuePair<string, Flight> f in flightDict)
        {

            Console.WriteLine("{0,-15} {1,-20} {2,-23} {3,-20} {4,-15}",
                f.Value.FlightNumber, a.Value.Name, f.Value.Origin, f.Value.Destination, f.Value.ExpectedTime);
        }
    }
}
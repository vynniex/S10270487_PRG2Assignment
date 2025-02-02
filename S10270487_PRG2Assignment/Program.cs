//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using S10270487_PRG2Assignment;
using S10270487_PRG2Assignment.Specialized_Flight_Classes;
using System;
using System.Diagnostics.Metrics;

// Dictionaries -
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

// Load All Files -
LoadAirlines();
LoadBoardingGates();
LoadFlights();
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");

// Main Program
while (true)
{
    DisplayMenu();
    Console.WriteLine("Please select your option: ");
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
        Console.WriteLine("=============================================");
        Console.WriteLine("Creation of Flight");
        Console.WriteLine("=============================================");
        CreateFlight();
    }
    else if (option == "5")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayAirlineFlights();
    }
    else if (option == "6")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayAirlineFlights();
        ModifyFlight();
    }
    else if (option == "7")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        DisplayFlightSchedule();
    }
    else if (option == "8")
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Bulk processing of unassigned flights to boarding gates for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        ProcessUnassignedFlights();
    }
    else if (option == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid input. Please select a valid option (0-8).");
    }
}

// DisplayMenu Method
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
    Console.WriteLine("8. Process Unassigned Flights");
    Console.WriteLine("0. Exit");
    Console.WriteLine("");
}

// Load Methods
void LoadAirlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        Console.WriteLine("Loading Airlines...");
        int count = 0;

        sr.ReadLine();  // skips the header
        while (!sr.EndOfStream)
        {
            string[] data = sr.ReadLine().Split(',');
            string airlineName = data[0].Trim();
            string airlineCode = data[1].Trim();

            Airline airline = new Airline(airlineName, airlineCode);
            airlineDict[airlineCode] = airline;
            count++;
        }
        
        Console.WriteLine($"{count} Airlines Loaded!");
    }
}

void LoadBoardingGates()
{
    Console.WriteLine("Loading Boarding Gates...");
    int count = 0;

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
            count++;
        }

        Console.WriteLine($"{count} Boarding Gates Loaded!");
    }
}

void LoadFlights()
{
    Console.WriteLine("Loading Flights...");
    int count = 0;

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
            count++;
        }
        Console.WriteLine($"{count} Flights Loaded!");
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

// Option 4 - Create Flight
void CreateFlight()
{
    // User input
    Console.Write("Enter Flight Number: ");
    string flightNo = Console.ReadLine().Trim();

    // Checks & Prevents duplicate flight numbers
    if (flightDict.ContainsKey(flightNo))
    {
        Console.WriteLine("Flight number already exists. Flight not created.");
        return;
    }

    // User input (continued)
    Console.Write("Enter Origin: ");
    string flightOrigin = Console.ReadLine().Trim();
    Console.Write("Enter Destination: ");
    string flightDest = Console.ReadLine().Trim();
    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");

    // Flight Time input validation
    DateTime flightTime;
    while (true)
    {
        string input = Console.ReadLine().Trim();
        try
        {
            flightTime = DateTime.Parse(input);
            break;
        }
        catch (FormatException)
        {
            Console.Write("Invalid format. Please enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        }
    }
    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
    string flightReq = Console.ReadLine().Trim().ToUpper();

    Flight newFlight;
    // Create new Flight object
    if (flightReq == "NONE")
    {
        newFlight = new NORMFlight(flightNo, flightOrigin, flightDest, flightTime, "Scheduled");
    }
    else if (flightReq == "CFFT")
    {
        newFlight = new CFFTFlight(flightNo, flightOrigin, flightDest, flightTime, "Scheduled", 150);
    }
    else if (flightReq == "DDJB")
    {
        newFlight = new DDJBFlight(flightNo, flightOrigin, flightDest, flightTime, "Scheduled", 300);
    }
    else if (flightReq == "LWTT")
    {
        newFlight = new LWTTFlight(flightNo, flightOrigin, flightDest, flightTime, "Scheduled", 500);
    }
    else
    {
        Console.WriteLine("Invalid request code. Flight not created.");
        return;
    }
    
    // Add to dictionary
    flightDict[flightNo] = newFlight;

    // Append to flights.csv file
    using (StreamWriter sw = new StreamWriter("flights.csv", true))
    {
        sw.WriteLine($"{flightNo},{flightOrigin},{flightDest},{flightTime:hh:mm tt},{flightReq}");
    }

    Console.WriteLine($"Flight {flightNo} has been added!");

    // Add another flight
    Console.WriteLine("Would you like to add another flight ? (Y / N)");
    string choice = Console.ReadLine().Trim().ToUpper();
    if (choice == "Y")
    {
        CreateFlight();
    }
    else if (choice != "N")
    {
        Console.WriteLine("Invalid input.");
    }
}

// Option 5 - Display Airline Flights
void DisplayAirlineFlights()
{
    Console.WriteLine("{0,-15} {1,-30}", "Airline Code", "Airline Name");

    foreach (var airline in airlineDict.Values)
    {
        Console.WriteLine("{0,-15} {1,-30}", airline.Code, airline.Name);
    }

    // User input airline code validation 
    string airlineCode;
    Airline selectedAirline = null;

    while (true)
    {
        Console.Write("Enter Airline Code: ");
        airlineCode = Console.ReadLine().Trim().ToUpper();

        if (airlineDict.ContainsKey(airlineCode))
        {
            selectedAirline = airlineDict[airlineCode];
            break;
        }
        else
        {
            Console.WriteLine("Invalid airline code. Please enter a valid airline code from the list above.");
        }
    }

    Console.WriteLine("=============================================");
    Console.WriteLine($"List of Flights for {selectedAirline.Name}");
    Console.WriteLine("=============================================");

    var airlineFlights = selectedAirline.Flights.Values.OrderBy(f => f.ExpectedTime).ToList();

    Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-15}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    foreach (var flight in airlineFlights)
    {
        Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-15}",
        flight.FlightNumber, selectedAirline.Name, flight.Origin, flight.Destination, flight.ExpectedTime.ToString("dd/M/yyyy h:mm:ss tt"));
    }
}

// Option 6 - Modify Flight Details
void ModifyFlight()
{
    while (true)
    {
        Console.WriteLine("Choose an existing Flight to modify or delete: ");
        string chosenFlight = Console.ReadLine().Trim().ToUpper();

        if (flightDict.ContainsKey(chosenFlight))
        {
            Flight selectedFlight = flightDict[chosenFlight];

            Console.WriteLine("1.Modify Flight");
            Console.WriteLine("2.Delete Flight");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine().Trim();

            // 1. Modify flight
            if (option == "1")
            {
                Console.WriteLine("1. Modify Basic Information");
                Console.WriteLine("2. Modify Status");
                Console.WriteLine("3. Modify Special Request Code");
                Console.WriteLine("4. Modify Boarding Gate");
                Console.Write("Choose an option: ");
                string modifyOption = Console.ReadLine();

                if (modifyOption == "1") // Modify Basic Information
                {
                    Console.Write(" Enter new Origin: ");
                    string newOrigin = Console.ReadLine();
                    Console.Write("Enter new Destination: ");
                    string newDest = Console.ReadLine();

                    // DateTime input validation
                    Console.Write("Enter new Expected Departure / Arrival Time(dd / mm / yyyy hh: mm):");
                    string inputNewTime = Console.ReadLine();
                    DateTime newTime;
                    if (DateTime.TryParseExact(inputNewTime, "d/M/yyyy h:mm", null, System.Globalization.DateTimeStyles.None, out newTime))
                    {
                        // Update the values in dictionary
                        selectedFlight.Origin = newOrigin;
                        selectedFlight.Destination = newDest;
                        selectedFlight.ExpectedTime = newTime;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please use dd/mm/yyyy hh:mm.");
                    }
                }
                else if (modifyOption == "2")
                {
                    Console.Write("Status: ");
                    string modifiedStatus = Console.ReadLine();
                    selectedFlight.Status = modifiedStatus;
                    Console.WriteLine("Flight status updated successfully.");
                }
                else if (modifyOption == "3")
                {
                    Console.Write("Special Request Code: ");
                    string modifiedReq = Console.ReadLine().Trim().ToUpper();
                    if (modifiedReq == "CFFT")
                        selectedFlight = new CFFTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime, selectedFlight.Status, 150);
                    else if (modifiedReq == "DDJB")
                        selectedFlight = new DDJBFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime, selectedFlight.Status, 300);
                    else if (modifiedReq == "LWTT")
                        selectedFlight = new LWTTFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime, selectedFlight.Status, 500);
                    else if (modifiedReq == "NONE")
                        selectedFlight = new NORMFlight(selectedFlight.FlightNumber, selectedFlight.Origin, selectedFlight.Destination, selectedFlight.ExpectedTime, selectedFlight.Status);

                    else
                    {
                        Console.WriteLine("Invalid Special Request Code. Valid options are: CFFT, DDJB, LWTT, None.");
                        return;
                    }
                }
                else if (modifyOption == "4")
                {
                    Console.Write("Boarding Gate: ");
                    string modifiedGate = Console.ReadLine();

                    if (!boardingGateDict.ContainsKey(modifiedGate))
                    {
                        Console.WriteLine("Invalid boarding gate. Please enter an existing gate.");
                        return;
                    }

                    // Remove flight from the previous boarding gate if it was assigned
                    var existingGate = boardingGateDict.FirstOrDefault(g => g.Value.Flight != null && g.Value.Flight.FlightNumber == chosenFlight);
                    if (!string.IsNullOrEmpty(existingGate.Key))
                    {
                        existingGate.Value.Flight = null;
                    }

                    // Assign the new boarding gate to the flight
                    BoardingGate gate = boardingGateDict[modifiedGate];
                    gate.Flight = selectedFlight;
                    Console.WriteLine("Boarding gate updated!");
                }
            }
            // 2. Delete flight
            else if (option == "2") 
            {
                // Remove flight from any boarding gate it might be assigned to
                var gateEntry = boardingGateDict.FirstOrDefault(g => g.Value.Flight != null && g.Value.Flight.FlightNumber == chosenFlight);
                if (!string.IsNullOrEmpty(gateEntry.Key))
                {
                    gateEntry.Value.Flight = null;
                }

                flightDict.Remove(chosenFlight);
                Console.WriteLine("Flight deleted successfully.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please select 1 or 2");
                return;
            }
        }
        else
        {
            Console.WriteLine("Invalid Flight Number. Please select an existing Flight Number from the list.");
        }
    }
}

// Option 7 - Display Flight Schedule
void DisplayFlightSchedule()
{
    // heading
    Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-35} {5,-15} {6,-15}",
            "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Status");

    // Convert flightDict.Values to a list
    List<Flight> flightList = flightDict.Values.ToList();

    // Sort flights
    for (int i = 0; i < flightList.Count - 1; i++)
    {
        for (int j = i + 1; j < flightList.Count; j++)
        {
            // If flight[i] is later than flight[j], swap
            if (flightList[i].CompareTo(flightList[j]) > 0)
            {
                var temp = flightList[i];
                flightList[i] = flightList[j];
                flightList[j] = temp;
            }
        }
    }

    foreach (var flight in flightList)
    {
        string airlineCode = flight.FlightNumber.Substring(0, 2);
        string airlineName = airlineDict.ContainsKey(airlineCode) ? airlineDict[airlineCode].Name : "Unknown";

        // Determine the special request code (if any)
        string specialRequest = flight is CFFTFlight ? "CFFT" :
                                flight is DDJBFlight ? "DDJB" :
                                flight is LWTTFlight ? "LWTT" : "None";

        // Assign the boarding gate based on special request code
        string boardingGate = "Unassigned";
        foreach (var gate in boardingGateDict.Values)
        {
            if (specialRequest == "CFFT" && gate.SupportsCFFT ||
                specialRequest == "DDJB" && gate.SupportsDDJB ||
                specialRequest == "LWTT" && gate.SupportsLWTT)
            {
                boardingGate = gate.GateName;
                break;
            }
        }

        Console.WriteLine("{0,-15} {1,-20} {2,-25} {3,-20} {4,-35} {5,-15} {6,-15}",
        flight.FlightNumber, airlineName, flight.Origin, flight.Destination,
        flight.ExpectedTime.ToString("d/M/yyyy h:mm:ss tt"), flight.Status, boardingGate);
    }
}

// Advanced Feature (a) - Process all unassigned flights to boarding gates in bulk
void ProcessUnassignedFlights()
{
    Queue<Flight> unassignedFlights = new Queue<Flight>();

    foreach (var flight in flightDict.Values)
    {
        if (!boardingGateDict.Values.Any(g => g.Flight == flight))
        {
            unassignedFlights.Enqueue(flight);
        }
    }

    int unassignedFlightCount = unassignedFlights.Count;
    Console.WriteLine("");
    Console.WriteLine($"Flights without Boarding Gate assigned: {unassignedFlightCount}");

    List<BoardingGate> availGates = boardingGateDict.Values.Where(g => g.Flight == null).ToList();
    int unassignedGateCount = availGates.Count;
    Console.WriteLine($"Gates without flights assigned: {unassignedGateCount}");
    Console.WriteLine("");

    int flightsAssigned = 0;
    int gatesAssigned = 0;

    Console.WriteLine("=============================================");
    Console.WriteLine("Flights Assigned to Boarding Gate");
    Console.WriteLine("=============================================");
    while (unassignedFlights.Count > 0)
    {
        Flight flight = unassignedFlights.Dequeue();
        BoardingGate assignedGate = null;

        if (flight is CFFTFlight || flight is DDJBFlight || flight is LWTTFlight)
        {
            assignedGate = availGates.FirstOrDefault(g => 
                (flight is CFFTFlight && g.SupportsCFFT) ||
                (flight is DDJBFlight && g.SupportsDDJB) ||
                (flight is LWTTFlight && g.SupportsLWTT));
        }
        else
        {
            assignedGate = availGates.FirstOrDefault(g => !g.SupportsCFFT && !g.SupportsDDJB && !g.SupportsLWTT);
        }

        if (assignedGate != null)
        {
            assignedGate.Flight = flight;
            availGates.Remove(assignedGate);
            flightsAssigned++;
            gatesAssigned++;

            // Display details of assigned flight
            Console.WriteLine($"Flight Number: {flight.FlightNumber}");
            Console.WriteLine($"Airline Name: {airlineDict[flight.FlightNumber.Split(' ')[0]].Name}");
            Console.WriteLine($"Origin: {flight.Origin}");
            Console.WriteLine($"Destination: {flight.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy h:mm:ss tt}");
            Console.WriteLine($"Special Request Code: {GetSpecialRequest(flight)}");
            Console.WriteLine($"Assigned Boarding Gate: {assignedGate.GateName}");
            Console.WriteLine("");
        }
    }

    // Summary
    Console.WriteLine("=============================================");
    Console.WriteLine("Bulk Boarding Gate Assignment Complete!");
    Console.WriteLine("=============================================");
    Console.WriteLine($"Total Flights Processed: {unassignedFlightCount}");
    Console.WriteLine($"Total Boarding Gates Processed: {unassignedGateCount}");
    Console.WriteLine("");
    Console.WriteLine($"Total Flights Assigned: {flightsAssigned}");
    Console.WriteLine($"Total Boarding Gates Assigned: {gatesAssigned}");
}

// helper function to get special request code**
string GetSpecialRequest(Flight flight)
{
    if (flight is CFFTFlight) return "CFFT";
    if (flight is DDJBFlight) return "DDJB";
    if (flight is LWTTFlight) return "LWTT";
    return "None";
}
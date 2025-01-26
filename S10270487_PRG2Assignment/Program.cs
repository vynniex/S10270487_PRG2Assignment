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
Console.WriteLine("Goodbye!")

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
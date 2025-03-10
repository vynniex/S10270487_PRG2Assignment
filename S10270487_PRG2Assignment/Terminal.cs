﻿//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using System;

namespace S10270487_PRG2Assignment
{
    class Terminal
    {
        // properties
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        // constructors
        public Terminal() { }
        public Terminal(string tn)
        {
            TerminalName = tn;
        }

        // methods
        public bool AddAirline(Airline a)
        {
            if (!Airlines.ContainsKey(a.Code))
            {
                Airlines[a.Code] = a;
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate b)
        {
            if (!BoardingGates.ContainsKey(b.GateName))
            {
                BoardingGates[b.GateName] = b;
                return true;
            }
            return false;
        }

        public Airline GetAirlineFromFlight(Flight f)
        {
            string airlineCode = f.FlightNumber.Substring(0, 2);
            return Airlines.ContainsKey(airlineCode) ? Airlines[airlineCode] : new Airline("Unknown", "XX");
        }

        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines.Values)
            {
                Console.WriteLine($"Airline: {airline.Name}, Fees: {airline.CalculateFees():C}");
            }
        }

        public override string ToString()
        {
            return "Terminal Name: " + TerminalName;
        }
    }
}
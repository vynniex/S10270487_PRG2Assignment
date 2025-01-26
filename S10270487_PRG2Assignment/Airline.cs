using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270487_PRG2Assignment
{
    class Airline
    {
        // properties
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        // constructors
        public Airline() { }
        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
        }

        // methods
        public bool AddFlight(Flight f)
        {
            if (!Flights.ContainsKey(f.FlightNumber))
            {
                Flights[f.FlightNumber] = f;
                return true;
            }
            return false;
        }

        public double CalculateFees()
        {
            return Flights.Values.Sum(flight => flight.CalculateFees());
        }

        public bool RemoveFlight(Flight f)
        {
            return Flights.Remove(f.FlightNumber);
        }

        public override string ToString()
        {
            return "Name: " + Name +
                "\tCode: " + Code;
        }
    }
}

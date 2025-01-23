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
        public Dictionary<string, Flight> Flights { get; set; }

        // constructors
        public Airline() { }
        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
            Flights = flights;
        }

        // methods
        public bool AddFlight(Flight)
        {

        }
        public double CalculateFees()
        {

        }
        public bool RemoveFlight(Flight)
        {

        }
        public override string ToString()
        {

        }
    }
}

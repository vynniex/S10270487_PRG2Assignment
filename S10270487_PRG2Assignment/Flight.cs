using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270487_PRG2Assignment
{
    abstract class Flight
    {
        // properties
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        // constructors
        public Flight() { }
        public Flight(string fNo, string origin, string dest, DateTime expTime, string status)
        {
            FlightNumber = fNo;
            Origin = origin;
            Destination = dest;
            ExpectedTime = expTime;
            Status = status;
        }

        // methods
        public abstract double CalculateFees();
        public override string ToString()
        {
            return "Flight Number: " + FlightNumber +
                "\tOrigin: " + Origin +
                "\tDestination: " + Destination +
                "\tExpected Time: " + ExpectedTime +
                "\tStatus: " + Status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270487_PRG2Assignment
{
    class BoardingGate
    {
        // properties
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        // constructors
        public BoardingGate() { }
        public BoardingGate(string gName, bool suppCFFT, bool suppDDJB, bool suppLWTT, Flight flight)
        {
            GateName = gName;
            SupportsCFFT = suppCFFT;
            SupportsDDJB = suppDDJB;
            SupportsLWTT |= suppLWTT;
            Flight = flight;
        }

        // methods
        public double CalculateFees()
        {
            return 0.0;
        }
        public override string ToString()
        {
            return "Gate Name: " + GateName +
                "\tSupports CFFT: " + SupportsCFFT +
                "\tSupports DDJB: " + SupportsDDJB +
                "\tSupports LWTT: " + SupportsLWTT +
                "\tFlight: " + Flight;
        }
    }
}

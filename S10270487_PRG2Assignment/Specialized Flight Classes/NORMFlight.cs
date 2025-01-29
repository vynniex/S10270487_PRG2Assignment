using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270487_PRG2Assignment.Specialized_Flight_Classes
{
    class NORMFlight : Flight   // inheritance
    {
        // constructors
        public NORMFlight(string fNo, string origin, string dest, DateTime expTime, string status) : base(fNo, origin, dest, expTime, status)
        {
            
        }

        // methods
        public override double CalculateFees()
        {
            double arrFee = Destination == "Singapore (SIN)" ? 500 : 0;     // ARRIVING flights fee
            double depFee = Origin == "Singapore (SIN)" ? 800 : 0;          // DEPARTING flights fee
            double gateFee = 300;                                           // base BOARDING GATE fee

            return arrFee + depFee + gateFee;
        }
        public override string ToString()
        {
            return base.ToString() +
                "\tFee: " + CalculateFees();
        }
    }
}

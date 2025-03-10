﻿//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270487_PRG2Assignment.Specialized_Flight_Classes
{
    class DDJBFlight : Flight   // inheritance
    {
        // properties
        public double RequestFee { get; set; }

        // constructors
        public DDJBFlight() : base() { }
        public DDJBFlight(string fNo, string origin, string dest, DateTime expTime, string status, double reqFee) :
            base(fNo, origin, dest, expTime, status)
        {
            RequestFee = reqFee;
        }

        // methods
        public override double CalculateFees()
        {
            double arrFee = Destination == "Singapore (SIN)" ? 500 : 0;     // ARRIVING flights fee
            double depFee = Origin == "Singapore (SIN)" ? 800 : 0;          // DEPARTING flights fee
            double gateFee = 300;                                           // base BOARDING GATE fee

            return arrFee + depFee + gateFee + RequestFee;
        }
        public override string ToString()
        {
            return base.ToString() +
                "\tSpecial Request: Double Decker Jet Bridge Requested (DDJB)" +
                "\tFee: " + CalculateFees();
        }
    }
}

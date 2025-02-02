﻿//==========================================================
// Student Number	: S10270487
// Student Name	: Wee Xin Yi
// Partner Name	: Loh Xue Ning
//==========================================================

using S10270487_PRG2Assignment.Specialized_Flight_Classes;
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
        public BoardingGate(string gName, bool suppCFFT, bool suppDDJB, bool suppLWTT)
        {
            GateName = gName;
            SupportsCFFT = suppCFFT;
            SupportsDDJB = suppDDJB;
            SupportsLWTT = suppLWTT;
            Flight = null;  // defaults to no flight assigned
        }

        // methods
        public double CalculateFees()
        {
            double baseFee = 300;
            if (Flight != null)
            {
                if (SupportsCFFT && Flight is CFFTFlight)
                {
                    baseFee += ((CFFTFlight)Flight).RequestFee;
                }
                if (SupportsDDJB && Flight is DDJBFlight)
                {
                    baseFee += ((DDJBFlight)Flight).RequestFee;
                }
                if (SupportsLWTT && Flight is LWTTFlight)
                {
                    baseFee += ((LWTTFlight)Flight).RequestFee;
                }
            }
            return baseFee;
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

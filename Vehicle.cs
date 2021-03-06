﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public abstract class Vehicle
    {
        public abstract VehicleType GetVehicleType();
        // https://dotnetfiddle.net/sD7Klz
    }

    public enum VehicleType
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5,
        Car = 6
    }

}
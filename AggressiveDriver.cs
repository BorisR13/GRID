using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
     class AggressiveDriver : Driver
    {
        private const double FuelPerKm = 2.7;

        public AggressiveDriver(string name, double totalTime, Car car) : base(name, totalTime, car)
        {
            base.FuelConsumptionPerKm = FuelPerKm;
            base.Speed *= 1.3;
        }
    }
}

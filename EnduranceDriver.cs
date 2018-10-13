using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
     class EnduranceDriver : Driver
    {
        private const double FuelPerKm = 1.5;

        public EnduranceDriver(string name, double totalTime, Car car) : base(name, totalTime, car)
        {
            base.FuelConsumptionPerKm = FuelPerKm;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
      class Driver
    {
        private string name;
        private string type;
        private double totalTime;
        private Car car;

        private string reasonToDnf;

        public string ReasonToDnf
        {
            get { return reasonToDnf; }
            set { reasonToDnf = value; }
        }
        public Car Car { get; set; }
        public double TotalTime { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        public double Speed { get; set; }

        public Driver(string name, double totalTime, Car car)
        {
            this.Name = name;
            this.TotalTime = totalTime;
            this.Car = car;
            this.Speed = (Car.HP / Car.Tyre.Degradation) / Car.FuelAmount;
        }


    }
}

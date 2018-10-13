using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
    public class Car
    {
        private const int MaxFuelAmount = 160;

        private int hp;
        private double fuelAmount;
        private Tyre tyre;

        public bool OutOfFuel => FuelAmount < 0;

        public Tyre Tyre
        {
            get { return tyre; }
            set { tyre = value; }
        }
        public double FuelAmount
        {
            get { return fuelAmount; }
            set { fuelAmount = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }

        public Car(int hp, double fuelAmount, Tyre tyre)
        {
            this.HP = hp;
            this.FuelAmount = fuelAmount;
            this.Tyre = tyre;
        }

    }
}

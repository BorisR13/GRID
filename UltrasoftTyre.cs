using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
    public class UltrasoftTyre : Tyre
    {
        private const string typeSoft = "Ultrasoft";

        private double grip;

        public new bool BlownUp = false;

        public double Grip
        {
            get { return grip; }
            set { grip = value; }
        }


        public UltrasoftTyre(double hardness, double grip) : base(typeSoft, hardness)
        {
            this.Grip = grip;
        }

        public new void Degradate()
        {
            double damage = this.Grip + this.Hardness;
            this.Degradation -= damage;
            BlowUp();
        }

        public new void BlowUp()
        {
            if (this.Degradation < 30)
            {
                this.BlownUp = true;
            }
        }
    }
}

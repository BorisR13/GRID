using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
    public abstract class Tyre
    {
        private string type;
        private double hardness;
        private double degradation;

        public bool BlownUp = false;

        public double Degradation
        {
            get { return degradation; }
            set { degradation = value; }
        }
        public double Hardness
        {
            get { return hardness; }
            set { hardness = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Tyre(string type, double hardness)
        {
            this.Type = type;
            this.Hardness = hardness;
            this.Degradation = 100;
        }

        public void Degradate()
        {
            this.Degradation -= this.Hardness;
            BlowUp();
        }

        public void BlowUp()
        {
            if (this.Degradation <= 0)
            {
                BlownUp = true;
            }
        }
    }
}

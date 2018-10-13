using System;
using System.Collections.Generic;
using System.Text;

namespace GRID
{
    public class HardTyre : Tyre   
    {
        private const string typeHard = "Hard";

        public HardTyre(double hardness) : base(typeHard, hardness)
        {
        }
    }
}

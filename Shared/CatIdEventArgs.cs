using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CatIdEventArgs : EventArgs
    {
        public int Id { get; private set; }

        public CatIdEventArgs(int id)
        {
            Id = id;
        }
    }

    public class CatWeightEventArgs : EventArgs
    {
        public double Weight { get; private set; }

        public CatWeightEventArgs(double weight)
        {
            Weight = weight;
        }
    }
}
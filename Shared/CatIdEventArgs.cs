using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Аргументы события, содержащие идентификатор кота
    /// </summary>
    public class CatIdEventArgs : EventArgs
    {
        public int Id { get;  set; }

        public CatIdEventArgs(int id)
        {
            Id = id;
        }
    }
    /// <summary>
    /// Аргументы события, содержащие значение веса
    /// </summary>
    public class CatWeightEventArgs : EventArgs
    {
        public double Weight { get;  set; }

        public CatWeightEventArgs(double weight)
        {
            Weight = weight;
        }
    }
}
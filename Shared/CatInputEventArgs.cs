using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CatInputEventArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Breed { get; private set; }
        public string Age { get; private set; }
        public string Weight { get; private set; }
        public string Color { get; private set; }
        public int Id { get; private set; }

        public CatInputEventArgs(
            int id,
            string name,
            string breed,
            string age,
            string weight,
            string color)
        {
            Id = id;
            Name = name;
            Breed = breed;
            Age = age;
            Weight = weight;
            Color = color;
        }
    }
}
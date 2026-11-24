using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CatInputEventArgs : EventArgs
    {
        /// <summary>
        /// кличка
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// порода
        /// </summary>
        public string Breed { get; private set; }
        /// <summary>
        /// возраст
        /// </summary>
        public string Age { get; private set; }
        /// <summary>
        /// вес
        /// </summary>
        public string Weight { get; private set; }
        /// <summary>
        /// окрас
        /// </summary>
        public string Color { get; private set; }
        /// <summary>
        /// айди
        /// </summary>
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// DTO нужен для передачи информации о коте из Presenter во View. Форма больше не должна получать доменный объект Cat.
    /// </summary>
    public class CatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Breed}), " +
                   $"{Age} г., {Weight} кг, окрас: {Color}";
        }
    }
}

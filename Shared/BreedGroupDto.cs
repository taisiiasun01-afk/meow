using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// DTO (Data Transfer Object) для группы котов, объединённых по породе
    /// </summary>
    public class BreedGroupDto
    {
        public string Breed { get; set; }
        public List<CatDto> Cats { get; set; }

        public int Count
        {
            get { return Cats == null ? 0 : Cats.Count; }
        }

        public BreedGroupDto()
        {
            Cats = new List<CatDto>();
        }
    }
}
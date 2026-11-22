using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
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
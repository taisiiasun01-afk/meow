using meow.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Интерфейс модели. Презентер работает с ней через этот интерфейс.
    /// </summary>
    internal interface ICatModel
    {
        event Action DataChanged;

        void Insert(Cat cat);
        void Update(Cat cat);
        void Delete(int id);
        List<Cat> GetAll();
    }
}

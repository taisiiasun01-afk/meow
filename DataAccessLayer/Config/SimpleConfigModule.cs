using Ninject.Modules;
using meow.core.interfaces;
using meow.core.Models;
using DataAccessLayer.EF;

namespace DataAccessLayer 
{
    /// <summary>
    /// Модуль конфигурации Ninject.
    /// Связывает интерфейс IRepository с конкретной реализацией.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Cat>>().To<EntityRepository<Cat>>().InSingletonScope();
        }
    }
}
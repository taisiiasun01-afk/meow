using Ninject.Modules;
using meow.core.interfaces; // <-- Твой namespace интерфейсов
using meow.core.Models;
using DataAccessLayer.EF;

namespace DataAccessLayer // <-- Пространство имен самого слоя
{
    /// <summary>
    /// Модуль конфигурации Ninject.
    /// Связывает интерфейс IRepository с конкретной реализацией.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            // Привязываем интерфейс к конкретному классу
            Bind<IRepository<Cat>>().To<EntityRepository<Cat>>().InSingletonScope();
        }
    }
}
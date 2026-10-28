using DataAccessLayer.EF;
using meow.core.interfaces;
using meow.core.interfaces.Interfaces;
using meow.core.Logic;
using meow.core.Models;
using Ninject.Modules;

namespace DataAccessLayer 
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Cat>>().To<EntityRepository<Cat>>().InSingletonScope();
            Bind<ICatLogic>().To<CatLogic>().InSingletonScope();
        }
    }
}
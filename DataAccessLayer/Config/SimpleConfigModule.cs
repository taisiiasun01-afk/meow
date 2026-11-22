using DataAccessLayer.EF;
using meow.core.interfaces;
using meow.core.Logic;
using Ninject.Modules;
using meow.core.Models;
using meow.core.Interfaces;

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
using DataAccessLayer.EF;
using meow.core.Logic;
using meow.core.Models;
using meow.core;
using meow.core.Interfaces;
using Shared;


namespace Presenter
{
    public static class CatProgram
    {
        public static CatPresenter Create(ICatView view)
        {
            using (var context = new CatsContext())
            {
                context.Database.EnsureCreated();
            }

            var repository = new EntityRepository<Cat>();

            var service = new CatService(repository);

            ICatLogic model = new CatLogic(service);

            return new CatPresenter(view, model);
        }
    }
}


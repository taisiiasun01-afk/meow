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
        /// <summary>
        /// Создает Presenter, связывая View и Mode
        /// </summary>
        /// <param name="view">Реализация View</param>
        /// <returns>Готовый Presenter</returns>
        public static CatPresenter Create(ICatView view)
        {
            
            var repository = new EntityRepository<Cat>();

            var service = new CatService(repository);

            ICatLogic model = new CatLogic(service);

            return new CatPresenter(view, model);
        }
    }
}


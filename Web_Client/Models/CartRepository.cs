using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web_Client.Models
{
    public class CartRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;
        public CartRepository(ApplicationContext context)
        {
            this.context = context;
        }
        //выбрать все записи из таблицы Articles
        public IQueryable<Dish> GetDishes()
        {
            return context.ShoppingCart.OrderBy(x => x.Name);
        }


        //сохранить новую либо обновить существующую запись в БД
        public int AddDish(Dish entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity.Id;
        }

        //удалить существующую запись
        public void DeleteDish(Dish entity)
        {
            context.ShoppingCart.Remove(entity);
            context.SaveChanges();
        }
    }
}

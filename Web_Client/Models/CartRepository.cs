using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Web_Client.Models
{
    public class CartRepository
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        private readonly ApplicationContext context;

        public  int USER_ID = 1;
        public CartRepository(ApplicationContext context)
        {
            this.context = context;
        }
        //выбрать все записи из таблицы Articles
        public List<Dish> GetDishes()
        {
            var lst = new List<Dish>();
            foreach (var item in context.ShoppingCart)
            {
                if (item.UserId == USER_ID)
                {
                    lst.Add(item);
                }
            }
            return lst;
        }


        //сохранить новую либо обновить существующую запись в БД
        public int AddDish(Dish entity)
        {
            entity.Id = default;
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
            return entity.Id;
        }
        //удаление товаров из корзины
        public void DeleteDish(int id)
        {
            // находим блюдо в корзине по идентификатору
            Dish dish = context.ShoppingCart.FirstOrDefault(d => d.Id == id);

            // удаляем выбранное блюдо из корзины
            if (dish != null)
            {
                context.ShoppingCart.Remove(dish);
                context.SaveChanges();
            }
        }
    }

}

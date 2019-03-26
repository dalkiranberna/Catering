using DataAccessLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UnitOfWork
    {
        public BaseRepository<Notification, int> Notifications;
        public BaseRepository<Product, int> Products;
        public BaseRepository<ShoppingCart, string> FoodShoppings;

        public CateringContext db;

        public UnitOfWork()
        {
            db = new CateringContext();

            Notifications = new BaseRepository<Notification, int>(db);
            Products = new BaseRepository<Product, int>(db);
            FoodShoppings = new BaseRepository<ShoppingCart, string>(db);

        }

        public bool Complete()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static CateringContext Create()
        {
            return new CateringContext();
        }
    }
}

using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public string MemberId { get; set; } //memberid == getuserid kişinin ürünlerini getir
        public virtual List<ProductItem> ProductItems { get; set; }
        //public int CategoryId { get; set; }
        //public virtual ProductCategory Category { get; set; }
        public Product()
        {
            Like = 0;
            Dislike = 0;
        }
    }

    /*public class ProductCategory : IEntity<int>
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public virtual List<Product> Products { get; set; }
    }*/

    public class ProductItem : IEntity<int>
    {
        public int Id { get; set; }
        public int ItemCount { get; set; }
        public virtual Product Product { get; set; } //ürünün cinsi
        public virtual ShoppingCart ShoppingCart { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Product.Price * ItemCount;
            }
        }
    }
}

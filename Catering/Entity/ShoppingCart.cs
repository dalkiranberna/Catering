using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ShoppingCart : IEntity<string>
    {
        [ForeignKey("Member")]
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Address { get; set; }
        public virtual Member Member { get; set; }
        public virtual List<Product> Products { get; set; }
        public decimal? SubTotal
        {
            get
            {
                return Products.Sum(x => x.Price);
            }
        }

        public ShoppingCart()
        {
            CreateDate = DateTime.Now;
        }
    }

    
}

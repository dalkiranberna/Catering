using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Order : IEntity<int> //sipariş fatura
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public Member Member { get; set; }
    }

    public class OrderItem //sipariş detay
    {
        public int OrderItemID { get; set; }
        public decimal Price { get; set; } //o ürün için ne kadar ödemiş
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}

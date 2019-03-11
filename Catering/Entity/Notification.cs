using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Type
    {
        Like,
        Dislike,
        Order
    }

    public class Notification : IEntity<int>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        //public string MemberId { get; set; }
        public virtual Member Member { get; set; }

        public Notification()
        {
            Date = DateTime.Now;
        }

    }
}

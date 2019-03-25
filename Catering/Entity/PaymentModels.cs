using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public interface IPaymentModel
    {
        int ID { get; set; }
        string TC { get; set; }
        string NameSurname { get; set; }
        bool IsApproved { get; set; }
    }
    public class BankTransferPayment : IPaymentModel //havale için yazacağımız sınıf
    {
        public int ID { get; set; }
        public string TC { get; set; }
        public string NameSurname { get; set; }
        public virtual Order Order { get; set; } //bire çok ilişki
        public bool IsApproved { get; set; } //işlemi onaylayıp onaylamadığımız (yöneticinin onayı)
    }

    public class CreditCardPayment : IPaymentModel
    {
        public int ID { get; set; }
        public string TC { get; set; }
        public string NameSurname { get; set; }
        public long CartNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public short CV2 { get; set; }
        public virtual Order Order { get; set; }
        public bool IsApproved { get; set; }
    }
}

using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public abstract class PaymentService
    {
        public abstract bool MakePayment(IPaymentModel pm);
    }

    public class BankTransferService : PaymentService
    {
        public override bool MakePayment(IPaymentModel pm)
        {
            var info = (BankTransferPayment)pm;
            //1.Bankaya bağlanıp ödeme var mı kontrol et
            //2.Ödeme varsa true döndür
            //3.Yoksa false döndür
            return true; //normalde bu sonucu bir banka apisinden almamız gerekir
        }
    }

    public class CreditCardService : PaymentService
    {//yukarıdaki classı duplicate ettik
        public override bool MakePayment(IPaymentModel pm)
        {
            var info = (BankTransferPayment)pm;
            //1.Kart bilgileri geçerli mi ve tutar çekiliyor mu kontrol et
            //2.Ödeme alındıysa true döndür
            //3.Ödeme başarısızsa false döndür
            return true;
        }
    }
}

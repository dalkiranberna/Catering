using BusinessLogicLayer;
using BusinessLogicLayer.Extensions;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catering.Controllers
{
	public class CartController : Controller
	{
		UnitOfWork _uw = new UnitOfWork();

		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				string uId = User.Identity.GetUserId();
				Member m = _uw.db.Users.Find(uId);

				if (m.ShoppingCart == null)
				{
					m.ShoppingCart = new ShoppingCart();
					m.ShoppingCart.ProductItems = new List<ProductItem>();
				}
				return View(m.ShoppingCart);
			}
			return RedirectToAction("Index", "Home");
		}

        public ActionResult Checkout()
        {
            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);

            //ViewBag.Total = m.ShoppingCart.SubTotal;
            //ViewBag.CartNo = m.ShoppingCart.Id;
            return View();
        }

        public ActionResult Delete(int id)
		{
            ProductItem toBeDeleted = _uw.db.ProductItems.Find(id); //silinecek ürünü bulur

            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);
            
            m.ShoppingCart.ProductItems.Remove(toBeDeleted);
            _uw.db.Entry(m).State = EntityState.Modified;
            _uw.db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { error = "Login to buy products."});

            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);

            if (m.ShoppingCart == null)
                m.ShoppingCart = new ShoppingCart();

            if (m.ShoppingCart.ProductItems == null)
                m.ShoppingCart.ProductItems = new List<ProductItem>();

            ProductItem chosenProduct = _uw.db.ProductItems.Find(id);
            m.ShoppingCart.ProductItems.Add(chosenProduct);

            _uw.db.Entry(m).State = EntityState.Modified;
            _uw.db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PayBankTransfer(int? approve)
        {
            if (approve.HasValue && approve.Value == 1)
            {
                //burada ödemeyi de kaydetmemiz lazım:
                BankTransferPayment p1 = new BankTransferPayment();
                p1.IsApproved = false;
                p1.NameSurname = User.Identity.GetNameSurname();
                p1.TC = User.Identity.GetTC();

                BankTransferService service = new BankTransferService();
                bool isPaid = service.MakePayment(p1);

                if (isPaid)
                {
                    CreateOrder(isPaid);
                    ResetShoppingCard();
                }

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Checkout");
        }

        private void ResetShoppingCard()
        {
            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);

            m.ShoppingCart.ProductItems.Clear();
            _uw.db.Entry(m).State = EntityState.Modified;
            _uw.db.SaveChanges();
        }

        private Order CreateOrder(bool isPaid)
        {
            string uId = User.Identity.GetUserId(); 
            Member m = db.Users.Find(uId);
            
            Order order = new Order();
            order.Member = m;
            order.IsPaid = isPaid;
            order.OrderItems = new List<OrderItem>();

            foreach (var item in m.ShoppingCart.ProductItems)
            {
                OrderItem oi = new OrderItem();
                order.Date = DateTime.Now;
                oi.Movies = item;
                oi.Count = 1;
                oi.Price = item.Price; //sonradan fiyat değişse bile o an ne kadara almış tuttuk
                order.OrderItems.Add(oi);
            }

            order.SubTotal = m.ShoppingCart.SubTotal.Value;
            _uw.db.Orders.Add(order);
            _uw.db.SaveChanges(); 

            return order;
        }
    }
}
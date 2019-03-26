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

	https://bootsnipp.com/snippets/j6QAx

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
					m.ShoppingCart.Products = new List<Product>();
				}
				return View(m.ShoppingCart);
			}
			return RedirectToAction("Index", "Home");
		}

        public ActionResult Checkout()
        {
            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);

            ViewBag.Total = m.ShoppingCart.SubTotal;
            ViewBag.CartNo = m.ShoppingCart.Id;
            return View();
        }

        public ActionResult Delete(int id)
		{
			Product toBeDeleted = _uw.db.Products.Find(id); //silinecek ürünü bulur

            string uId = User.Identity.GetUserId();
            Member m = _uw.db.Users.Find(uId);
            
            m.ShoppingCart.Products.Remove(toBeDeleted);
            _uw.db.Entry(m).State = EntityState.Modified;
			_uw.Complete();
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

            if (m.ShoppingCart.Products == null)
                m.ShoppingCart.Products = new List<Product>();

			Product chosenProduct = _uw.db.Products.Find(id);
            m.ShoppingCart.Products.Add(chosenProduct);

            _uw.db.Entry(m).State = EntityState.Modified;
			_uw.Complete();

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

            m.ShoppingCart.Products.Clear();
            _uw.db.Entry(m).State = EntityState.Modified;
			_uw.Complete();
        }

        private Order CreateOrder(bool isPaid)
        {
            string uId = User.Identity.GetUserId(); 
            Member m = _uw.db.Users.Find(uId);
            
            Order order = new Order();
            order.Member = m;
            order.IsPaid = isPaid;
            order.OrderItems = new List<OrderItem>();

            foreach (var item in m.ShoppingCart.Products)
            {
                OrderItem oi = new OrderItem();
                order.Date = DateTime.Now;
                oi.Product = item;
                oi.Count = 1;
                oi.Price = item.Price; //sonradan fiyat değişse bile o an ne kadara almış tuttuk
                order.OrderItems.Add(oi);
            }

            order.SubTotal = m.ShoppingCart.SubTotal.Value;
            _uw.db.Orders.Add(order);
			_uw.Complete();

			return order;
        }
    }
}
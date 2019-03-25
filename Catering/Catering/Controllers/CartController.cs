using BusinessLogicLayer;
using DataAccessLayer;
using Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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

		public ActionResult Delete()
		{
			return View();
		}
	}
}
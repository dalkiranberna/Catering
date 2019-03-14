using BusinessLogicLayer;
using Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catering.Controllers
{
    public class MyAccountController : Controller
    {

		UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Profile()
        {
			string uId = User.Identity.GetUserId();
			Member member = _uw.db.Users.Find(uId);
			MyAccountViewModel vm = new MyAccountViewModel();
			vm.Email = member.Email;
			vm.PhoneNumber = member.PhoneNumber;
			if (member.HasPhoto)
				ViewBag.Photo = "/Uploads/Members/" + uId + ".jpg";
			return View(vm);
		}
    }
}
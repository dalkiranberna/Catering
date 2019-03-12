using BusinessLogicLayer;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catering.Controllers
{
	public class MemberController : Controller
	{
		UnitOfWork _uw = new UnitOfWork();

		public ActionResult Index(int? food, int? del)
		{

			return View();
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult _Register(Member member, string pass, HttpPostedFileBase img)
		{
			UserStore<Member> store = new UserStore<Member>(UnitOfWork.Create());
			UserManager<Member> manager = new UserManager<Member>(store);

			var result = manager.Create(member, pass);

			if (result.Succeeded) //kayıt başarılıysa
			{

				if (img != null)
				{
					string path = Server.MapPath("/Uploads/Member/");
					img.SaveAs(path + member.Id + ".jpg");
					member.HasPhoto = true;

					manager.Update(member);
				}

				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Errors = result.Errors;
			}
			return View();
		}


		public ActionResult LoginPage()
		{
			return View();
		}

		public JsonResult Login(LoginViewModel info)
		{
			ApplicationSignInManager signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

			//2. Giriş yapmayı dene (result döner)
			SignInStatus result = signInManager.PasswordSignIn(info.Email, info.Password, true, false); //MVC username ile giriş yaptırıyor

			//3. Sonucu döndür
			//sonuç döndürürken enum'ı switch-case ile yazmak daha mantıklı. SignInStatus bir enum :
			switch (result)
			{
				case SignInStatus.Success:
					return Json(new { success = true });
				case SignInStatus.Failure:
					return Json(new { success = false });
			}
			return Json(new { success = false });
		}
	}
}
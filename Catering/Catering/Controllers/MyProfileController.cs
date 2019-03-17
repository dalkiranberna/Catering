using BusinessLogicLayer;
using Entity;
using Entity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catering.Controllers
{
    public class MyProfileController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();

        public ActionResult Index()
        {
            UserStore<Member> store = new UserStore<Member>(UnitOfWork.Create());
            UserManager<Member> manager = new UserManager<Member>(store);

            string uId = User.Identity.GetUserId(); //giriş yapan kişinin id si
            Member member = manager.FindById(uId); //o id ye sahip kişiyi bulduk
            
            string img = "/Uploads/Members/" + member.Id + ".jpg";

            ViewBag.Photo = img;

            return View(member);
        }

        public ActionResult Edit()
        {
            string uId = User.Identity.GetUserId();
            Member member = _uw.db.Users.Find(uId);
            MyAccountViewModel vm = new MyAccountViewModel();
            vm.Email = member.Email;
            vm.PhoneNumber = member.PhoneNumber;
            vm.UserName = member.UserName;
            vm.Password = member.Password;
            if (member.HasPhoto)
                ViewBag.Photo = "/Uploads/Members/" + uId + ".jpg";

            return View();
        }

        [HttpPost]
        public ActionResult Edit(MyAccountViewModel info, HttpPostedFileBase imgFile)
        {
            UserStore<Member> store = new UserStore<Member>(UnitOfWork.Create());
            UserManager<Member> manager = new UserManager<Member>(store);

            string uId = User.Identity.GetUserId();
            Member member = manager.FindById(uId);

            member.Email = info.Email;
            member.PhoneNumber = info.PhoneNumber;
            member.UserName = info.UserName;

            if (imgFile != null)
            {
                string path = Server.MapPath("/Uploads/Members/");
                string old = path + member.Id + ".jpg";
                if (System.IO.File.Exists(old))
                    System.IO.File.Delete(old);

                string _new = path + member.Id + ".jpg";
                imgFile.SaveAs(_new);

                member.HasPhoto = true;
            }
            if (manager.CheckPassword(member, info.Password))
                manager.Update(member);
            else
                ViewBag.Error = "Şifre yanlış!";

            if (member.HasPhoto)
                ViewBag.Photo = "/Uploads/Members/" + uId + ".jpg";

            return View(info);
        }
    }
}
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
    public class ProductController : Controller
    {
        UnitOfWork _uw = new UnitOfWork();

        public ActionResult Index()
        {
            return View(_uw.db.Products.ToList());
        }


        public ActionResult Create()
        {
            ViewBag.Products = _uw.db.Products.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                _uw.db.Products.Add(newProduct);
                _uw.Complete();
                return RedirectToAction("Index");
            }

            ViewBag.Products = _uw.db.Products.ToList();
            return View();
        }


        public JsonResult ImageUpload(HttpPostedFileBase ProductImage)
        {
            if (ProductImage != null && ProductImage.ContentLength != 0)
            {
                var path = Server.MapPath("/Uploads/Products/");
                ProductImage.SaveAs(path + ProductImage.FileName);

                FileList fList = new FileList();
                var files = fList.files;

                File f = new File(); //burada fileviewmodel'i kullandık
                f.name = ProductImage.FileName;
                f.url = "/Uploads/Products/ProductImage.FileName";
                f.thumbnailUrl = f.url;

                files.Add(f);
                return Json(fList);
            }
            return Json(false);

        }

        public ActionResult Store()
        {
            return View();
        }
    }
}
using BusinessLogicLayer;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catering.Controllers
{
    public class CateriingController : Controller
    {
		UnitOfWork _uw = new UnitOfWork();
        public ActionResult Index()
        {
			ViewBag.Themes = Enum.GetNames(typeof(OrganizationTheme));
			return View();
        }
    }
}
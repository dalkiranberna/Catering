using BusinessLogicLayer;
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

        public ActionResult Proile()
        {
            return View();
        }
    }
}
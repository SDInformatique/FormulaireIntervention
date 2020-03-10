using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormulaireIntervention.Models;

namespace FormulaireIntervention.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New_Client()
        {
            ViewBag.Message = "Nouveau Client";
            

            return View();
        }
        public ActionResult Existing_Client()
        {
            ViewBag.Message = "Client Existant";

            return View();
        }       
        public ActionResult Intervention()
        {
            ViewBag.Message = "Intervention";

            return View();
        }
        public ActionResult Resume()
        {
            ViewBag.Message = "Résumé";


            return View();
        }
        public ActionResult Signature()
        {
            ViewBag.Message = "Signature Client";

            return View();
        }
        public ActionResult Finish()
        {
            ViewBag.Message = "Fin";

            return View();
        }
    }
}
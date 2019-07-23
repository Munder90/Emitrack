using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;
using Integrador.Common;

namespace Integrador.Controllers
{
    public class ReportController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        public ActionResult Productos()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //var 


                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Productos(FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Clientes()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //var 


                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clientes(FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Ventas()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);


                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ventas(FormCollection collection)
        {
            try
            {


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;

namespace Integrador.Controllers
{
    public class ReportController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        public ActionResult Productos()
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
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
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
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
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Entities;
using Integrador.Models;

namespace Integrador.Controllers
{
    public class HomeController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
            }
            catch (Exception) { }
            return View();
        }

        public ActionResult Contacto()
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
            }
            catch (Exception) { }
            return View();
        }

        public ActionResult Emitrack()
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
            }
            catch (Exception) { }
            return View();
        }

        public ActionResult Buscar(string palabra)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
            }
            catch (Exception) { }

            IEnumerable<PRODUCTO> productos;

            productos = db.PRODUCTOes;

            if (!String.IsNullOrEmpty(palabra))
            {
                productos = productos.Where(x => (x.Nombre.ToUpper().Contains(palabra.ToUpper()) || x.Descripcion.ToUpper().Contains(palabra.ToUpper())) && x.Activo == true).ToList();
            }
            ViewBag.Error = "";
            ViewBag.Productos = "";

            if (productos.Count() > 0)
            {
                ViewBag.Productos = productos;
            }
            else
            {
                ViewBag.Error = "No hay coincidencias";
            }
            return View();
        }

        [HttpPost]
        public string Buscar(string palabra, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + palabra;
        }
    }
}

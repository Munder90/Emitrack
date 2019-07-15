using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Entities;
using Integrador.Models;

namespace Integrador.Controllers
{
    public class ProductoController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                ViewBag.Usuario = Usuario;
                ViewBag.Tipo = Tipo;
                if (Tipo == 1)
                {
                    List<PRODUCTO> pRODUCTO = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                    return View(pRODUCTO);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                try
                {
                    string Usuario = Session["usuario"].ToString();
                    int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                    ViewBag.Usuario = Usuario;
                    ViewBag.Tipo = Tipo;
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home");
                }

                PRODUCTO productos = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();

                return View(productos);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            try
            {
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                ViewBag.Usuario = Usuario;
                ViewBag.Tipo = Tipo;
                if (Tipo == 1)
                {
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Productos pro)
        {
            try
            {
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                ViewBag.Usuario = Usuario;
                ViewBag.Tipo = Tipo;
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = new PRODUCTO
                    {
                        Codigo = pro.Codigo,
                        Nombre = pro.Nombre,
                        Descripcion = pro.Descripcion,
                        Precio_A = pro.Precio_A,
                        Precio_V = pro.Precio_V,
                        Cantidad = pro.Cantidad,
                        Fecha_Mo = DateTime.Today,
                        Imagen = pro.Imagen,
                        Activo = true
                    };

                    db.PRODUCTOes.Add(pRODUCTO);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(pro);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                ViewBag.Usuario = Usuario;
                ViewBag.Tipo = Tipo;
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();

                    return View(pRODUCTO);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Productos pro)
        {
            try
            {
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                ViewBag.Usuario = Usuario;
                ViewBag.Tipo = Tipo;
                if (Tipo == 1)
                {

                    PRODUCTO pRODUCTO = new PRODUCTO
                    {
                        Codigo = pro.Codigo,
                        Nombre = pro.Nombre,
                        Descripcion = pro.Descripcion,
                        Precio_A = pro.Precio_A,
                        Precio_V = pro.Precio_V,
                        Cantidad = pro.Cantidad,
                        Fecha_Mo = DateTime.Today,
                        Imagen = pro.Imagen,
                        Activo = true
                    };

                    db.PRODUCTOes.Add(pRODUCTO);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(pro);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

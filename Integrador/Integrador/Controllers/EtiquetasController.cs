using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Common;
using Integrador.Entities;
using Integrador.Models;

namespace Integrador.Controllers
{
    public class EtiquetasController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        // GET: Etiquetas
        public ActionResult Index()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    List<PRODUCTO_T> pRODUCTO_Ts = db.PRODUCTO_T.Where(x => x.Activo == true).ToList();
                    return View(pRODUCTO_Ts);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Etiquetas/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_Ts = db.PRODUCTO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    Productos_T productos_T = new Productos_T
                    {
                        ID = pRODUCTO_Ts.ID,
                        Descripcion = pRODUCTO_Ts.Descripcion
                    };
                    return View(productos_T);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Etiquetas/Create
        public ActionResult Create()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
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

        // POST: Etiquetas/Create
        [HttpPost]
        public ActionResult Create(Productos_T productos_T)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_T = new PRODUCTO_T
                    {
                        Descripcion = productos_T.Descripcion,
                        Activo = true
                    };

                    db.PRODUCTO_T.Add(pRODUCTO_T);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(productos_T);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Etiquetas/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_Ts = db.PRODUCTO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    Productos_T productos_T = new Productos_T
                    {
                        ID = pRODUCTO_Ts.ID,
                        Descripcion = pRODUCTO_Ts.Descripcion
                    };
                    return View(productos_T);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Etiquetas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Productos_T productos_T)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {

                    PRODUCTO_T pRODUCTO_T = new PRODUCTO_T
                    {
                        Descripcion = productos_T.Descripcion,
                        Activo = true
                    };

                    db.PRODUCTO_T.Add(pRODUCTO_T);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(productos_T);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Etiquetas/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_Ts = db.PRODUCTO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    Productos_T productos_T = new Productos_T
                    {
                        ID = pRODUCTO_Ts.ID,
                        Descripcion = pRODUCTO_Ts.Descripcion
                    };
                    return View(productos_T);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Etiquetas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Productos_T productos_T)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_T = db.PRODUCTO_T.Where(x => x.ID == productos_T.ID).FirstOrDefault();
                    pRODUCTO_T.Activo = false;

                    db.Entry(pRODUCTO_T).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO_T pRODUCTO_T = db.PRODUCTO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    pRODUCTO_T.Activo = false;

                    db.Entry(pRODUCTO_T).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

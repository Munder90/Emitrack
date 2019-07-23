using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;
using System.Data.Entity;
using Integrador.Common;

namespace Integrador.Controllers
{
    public class PagosController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Pagos
        public ActionResult Index()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    List<PAGO_T> pt = db.PAGO_T.Where(x => x.Activo == true).ToList();
                    List<Pagos_T> pagos = new List<Pagos_T>();
                    foreach (PAGO_T pagos_ in pt)
                    {
                        Pagos_T pagos_T = new Pagos_T
                        {
                            ID = pagos_.ID,
                            Nombre = pagos_.Nombre
                        };
                        pagos.Add(pagos_T);
                    }

                    return View(pagos);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PAGO_T pt = db.PAGO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    Pagos_T pagos = new Pagos_T
                    {
                        Nombre = pt.Nombre,
                        Descripcion = pt.Descripcion
                    };

                    return View(pagos);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Pagos/Create
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

        // POST: Pagos/Create
        [HttpPost]
        public ActionResult Create(Pagos_T pagos)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PAGO_T pAGO_T = new PAGO_T
                    {
                        Nombre = pagos.Nombre,
                        Descripcion = pagos.Descripcion,
                        Activo = true
                    };

                    db.PAGO_T.Add(pAGO_T);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(pagos);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pagos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PAGO_T pt = db.PAGO_T.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    Pagos_T pagos = new Pagos_T
                    {
                        Nombre = pt.Nombre,
                        Descripcion = pt.Descripcion
                    };
                    return View(pagos);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Pagos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Pagos_T pagos)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PAGO_T pAGO_T = db.PAGO_T.Where(x => x.ID == pagos.ID).FirstOrDefault();
                    pAGO_T.Activo = false;

                    db.Entry(pAGO_T).State = EntityState.Modified;
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
                    PAGO_T pAGO_T = db.PAGO_T.Where(x => x.ID == id).FirstOrDefault();
                    pAGO_T.Activo = false;

                    db.Entry(pAGO_T).State = EntityState.Modified;
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

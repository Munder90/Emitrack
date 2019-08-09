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
    public class BanerController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Baner
        public ActionResult Index()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    List<BANER> bANERs = db.BANERs.ToList();
                    List<Baner> baner_Ds = new List<Baner>();
                    foreach (BANER b in bANERs)
                    {
                        string fecha = b.Fecha.Value.ToString("dd/MM/yyyy");
                        if (b.Fecha > System.DateTime.Today)
                        {
                            Baner bd = new Baner
                            {
                                Id = b.ID,
                                Descripcion = b.Descripcion,
                                Imagen = b.Imagen,
                                Fecha = fecha,
                                Activo = b.Activo.Value
                            };
                            baner_Ds.Add(bd);
                        }
                        else
                        {
                            b.Activo = false;
                            db.Entry(b).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return View(baner_Ds);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Baner/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    BANER b = db.BANERs.Where(x => x.ID == id).FirstOrDefault();
                    List<BANER_D> bANER_Ds = db.BANER_D.Where(x => x.ID_Baner == b.ID).ToList();
                    List<Baner_D> baner_Ds = new List<Baner_D>();
                    foreach (BANER_D item in bANER_Ds)
                    {
                        Baner_D baner_D = new Baner_D
                        {
                            Id = item.ID,
                            Id_baner = item.ID_Baner,
                            Producto = item.Producto,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad
                        };
                        baner_Ds.Add(baner_D);
                    }

                    Baner bd = new Baner
                    {
                        Id = b.ID,
                        Descripcion = b.Descripcion,
                        Imagen = b.Imagen,
                        Fecha = b.Fecha.Value.ToString("dd/MM/yyyy"),
                        Detalle = baner_Ds,
                    };

                    return View(bd);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Baner/Create
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

        // POST: Baner/Create
        [HttpPost]
        public ActionResult Create(Baner baner, HttpPostedFileBase upload)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    try
                    {
                        string imagen = upload.FileName;
                        int cuenta = db.BANERs.ToList().Count;
                        string ruta = "/images/Baner/" + cuenta + ".PNG";
                        upload.SaveAs(Server.MapPath("~" + ruta));

                        BANER b = new BANER
                        {
                            Descripcion = baner.Descripcion,
                            Imagen = ruta,
                            Fecha = DateTime.Parse(baner.Fecha),
                            Activo = true
                        };
                        db.BANERs.Add(b);
                        db.SaveChanges();
                        //List<BANER_D> bANER_Ds = new List<BANER_D>();
                        foreach (Baner_D baner_D in baner.Detalle)
                        {
                            BANER_D bANER_D = new BANER_D
                            {
                                ID_Baner = b.ID,
                                Producto = baner_D.Producto,
                                Precio = baner_D.Precio,
                                Cantidad = baner_D.Cantidad
                            };
                            db.BANER_D.Add(bANER_D);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        return View(baner);
                    }

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Baner/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    BANER b = db.BANERs.Where(x => x.ID == id).FirstOrDefault();
                    List<BANER_D> bANER_Ds = db.BANER_D.Where(x => x.ID_Baner == b.ID).ToList();
                    List<Baner_D> baner_Ds = new List<Baner_D>();
                    foreach (BANER_D item in bANER_Ds)
                    {
                        Baner_D baner_D = new Baner_D
                        {
                            Id = item.ID,
                            Id_baner = item.ID_Baner,
                            Producto = item.Producto,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad
                        };
                        baner_Ds.Add(baner_D);
                    }

                    Baner bd = new Baner
                    {
                        Id = b.ID,
                        Descripcion = b.Descripcion,
                        Imagen = b.Imagen,
                        Fecha = b.Fecha.Value.ToString("dd/MM/yyyy"),
                        Detalle = baner_Ds,
                    };

                    return View(bd);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Baner/Edit/5
        [HttpPost]
        public ActionResult Edit(Baner baner, HttpPostedFileBase upload)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    try
                    {
                        string imagen = upload.FileName;
                        string ruta = "/images/Baner/" + baner.Id + ".PNG";
                        upload.SaveAs(Server.MapPath("~" + ruta));

                        BANER b = db.BANERs.Where(x => x.ID == baner.Id).FirstOrDefault();
                        b.Descripcion = baner.Descripcion;
                        b.Imagen = ruta;
                        b.Fecha = DateTime.Parse(baner.Fecha);
                        b.Activo = baner.Activo;

                        db.Entry(b).State = EntityState.Modified;
                        db.SaveChanges();
                        //List<BANER_D> bANER_Ds = new List<BANER_D>();
                        foreach (Baner_D baner_D in baner.Detalle)
                        {
                            BANER_D bANER_D = db.BANER_D.Where(x => x.ID_Baner == b.ID && x.Producto == baner_D.Producto).FirstOrDefault();
                            if (bANER_D != null)
                            {
                                bANER_D.Precio = baner_D.Precio;
                                bANER_D.Cantidad = baner_D.Cantidad;

                                db.BANER_D.Add(bANER_D);
                                db.SaveChanges();
                            }
                            else
                            {
                                bANER_D = new BANER_D
                                {
                                    ID_Baner = b.ID,
                                    Producto = baner_D.Producto,
                                    Precio = baner_D.Precio,
                                    Cantidad = baner_D.Cantidad
                                };
                                db.BANER_D.Add(bANER_D);
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return View(baner);
                    }

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Baner/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    BANER b = db.BANERs.Where(x => x.ID == id).FirstOrDefault();
                    List<BANER_D> bANER_Ds = db.BANER_D.Where(x => x.ID_Baner == b.ID).ToList();
                    List<Baner_D> baner_Ds = new List<Baner_D>();
                    foreach (BANER_D item in bANER_Ds)
                    {
                        Baner_D baner_D = new Baner_D
                        {
                            Id = item.ID,
                            Id_baner = item.ID_Baner,
                            Producto = item.Producto,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad
                        };
                        baner_Ds.Add(baner_D);
                    }

                    Baner bd = new Baner
                    {
                        Id = b.ID,
                        Descripcion = b.Descripcion,
                        Imagen = b.Imagen,
                        Fecha = b.Fecha.Value.ToString("dd/MM/yyyy"),
                        Detalle = baner_Ds,
                    };

                    return View(bd);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Baner/Delete/5
        [HttpPost]
        public ActionResult Delete(Baner baner)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    try
                    {
                        BANER b = db.BANERs.Where(x => x.ID == baner.Id).FirstOrDefault();
                        b.Activo = false;

                        db.Entry(b).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                    catch (Exception)
                    {
                        return View(baner);
                    }

                    return View("Index");
                }
                return RedirectToAction("Index", "Home");
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
                    BANER b = db.BANERs.Where(x => x.ID == id).FirstOrDefault();
                    b.Activo = false;

                    db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult BuscaProducto(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            var c = (from x in db.PRODUCTOes
                     where (x.Nombre.Contains(Prefix) && x.Activo == true)
                     select new { ID = x.ID, Producto = x.Nombre }).ToList();

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);

            return cc;
        }
    }
}

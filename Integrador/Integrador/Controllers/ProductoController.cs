using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Common;
using Integrador.Entities;
using Integrador.Models;

namespace Integrador.Controllers
{
    public class ProductoController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    List<PRODUCTO> pRODUCTO = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                    List<Productos> productos = new List<Productos>();
                    foreach (PRODUCTO pro in pRODUCTO)
                    {
                        string fecha = pro.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                        Productos p = new Productos
                        {
                            ID = pro.ID,
                            Nombre = pro.Nombre,
                            Cantidad = pro.Cantidad,
                            Fecha_Mo = fecha,
                            Descripcion = pro.Descripcion,
                            Precio_A = pro.Precio_A.Value,
                            Precio_V = pro.Precio_V,
                            Activo = pro.Activo.Value,
                            Imagen = pro.Imagen,
                            Codigo = pro.Codigo
                        };
                        productos.Add(p);
                    }
                    return View(productos);
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
                    FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                    string Usuario = Session["usuario"].ToString();
                    int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                }
                catch (Exception)
                {
                    //return RedirectToAction("Index", "Home");
                }

                PRODUCTO productos = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                string fecha = productos.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                Productos p = new Productos
                {
                    ID = productos.ID,
                    Nombre = productos.Nombre,
                    Cantidad = productos.Cantidad,
                    Fecha_Mo = fecha,
                    Descripcion = productos.Descripcion,
                    Precio_A = productos.Precio_A.Value,
                    Precio_V = productos.Precio_V,
                    Activo = productos.Activo.Value,
                    Imagen = productos.Imagen,
                    Codigo = productos.Codigo
                };

                return View(p);
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

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Productos pro, HttpPostedFileBase upload)
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
                        string imagen = pro.Codigo + ".PNG";
                        string ruta = "/images/Productos/" + pro.Codigo + ".PNG";
                        upload.SaveAs(Server.MapPath(ruta));

                        PRODUCTO pRODUCTO = new PRODUCTO
                        {
                            Codigo = pro.Codigo,
                            Nombre = pro.Nombre,
                            Descripcion = pro.Descripcion,
                            Precio_A = pro.Precio_A,
                            Precio_V = pro.Precio_V,
                            Cantidad = pro.Cantidad,
                            Fecha_Mo = DateTime.Today,
                            Imagen = imagen,
                            Activo = true,
                            N_Ventas = 0
                        };

                        db.PRODUCTOes.Add(pRODUCTO);
                        db.SaveChanges();

                        string[] etiquetas = pro.Etiquetas.Split(';');
                        //PRODUCTO p = db.PRODUCTOes.Where(x => x.Nombre.Equals(pRODUCTO.Nombre)).FirstOrDefault();
                        foreach (string eti in etiquetas)
                        {
                            PRODUCTO_T pt = db.PRODUCTO_T.Where(x => x.Descripcion == eti.Trim() && x.Activo == true).FirstOrDefault();
                            if (pt != null)
                            {
                                PRODUCTO_TIPO pRODUCTO_TIPO = new PRODUCTO_TIPO
                                {
                                    Producto = pRODUCTO.ID,
                                    Tipo = pt.ID
                                };
                                db.PRODUCTO_TIPO.Add(pRODUCTO_TIPO);
                                db.SaveChanges();
                            }
                        }

                        return RedirectToAction("Index");
                    }
                    catch(Exception)
                    {
                        return View(pro);
                    }
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    string fecha = pRODUCTO.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                    Productos p = new Productos
                    {
                        ID = pRODUCTO.ID,
                        Nombre = pRODUCTO.Nombre,
                        Cantidad = pRODUCTO.Cantidad,
                        Fecha_Mo = fecha,
                        Descripcion = pRODUCTO.Descripcion,
                        Precio_A = pRODUCTO.Precio_A.Value,
                        Precio_V = pRODUCTO.Precio_V,
                        Activo = pRODUCTO.Activo.Value,
                        Imagen = pRODUCTO.Imagen,
                        Codigo = pRODUCTO.Codigo
                    };

                    return View(p);
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
        public ActionResult Edit(int id, Productos pro, HttpPostedFileBase upload)
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
                        string ruta;
                        string imagen = "";
                        PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                        try
                        {
                            imagen = pro.Codigo + ".PNG";
                            ruta = "~/images/Productos/" + pro.Codigo + ".PNG";
                            upload.SaveAs(Server.MapPath(ruta));
                        }
                        catch (Exception)
                        {
                            ruta = pRODUCTO.Imagen;
                        }

                        pRODUCTO.Codigo = pro.Codigo;
                        pRODUCTO.Nombre = pro.Nombre;
                        pRODUCTO.Descripcion = pro.Descripcion;
                        pRODUCTO.Precio_A = pro.Precio_A;
                        pRODUCTO.Precio_V = pro.Precio_V;
                        pRODUCTO.Cantidad = pro.Cantidad;
                        pRODUCTO.Fecha_Mo = DateTime.Today;
                        pRODUCTO.Imagen = imagen;
                        pRODUCTO.Activo = true;

                        db.Entry(pRODUCTO).State = EntityState.Modified;
                        db.SaveChanges();

                        string[] etiquetas = pro.Etiquetas.Split(';');
                        //PRODUCTO p = db.PRODUCTOes.Where(x => x.Nombre.Equals(pRODUCTO.Nombre)).FirstOrDefault();
                        List<PRODUCTO_TIPO> ps = db.PRODUCTO_TIPO.Where(x => x.Producto == pRODUCTO.ID).ToList();
                        List<PRODUCTO_T> pro_t = db.PRODUCTO_T.Where(x => x.Activo == true).ToList();
                        foreach (string eti in etiquetas)
                        {
                            PRODUCTO_T pt = pro_t.Where(x => x.Descripcion == eti.Trim()).FirstOrDefault();
                            if (ps.Count() > 0 && pt != null)
                            {
                                foreach (PRODUCTO_TIPO tIPO in ps)
                                {
                                    if (tIPO.Tipo != pt.ID)
                                    {
                                        PRODUCTO_TIPO pRODUCTO_TIPO = new PRODUCTO_TIPO
                                        {
                                            Producto = pRODUCTO.ID,
                                            Tipo = pt.ID
                                        };
                                        db.PRODUCTO_TIPO.Add(pRODUCTO_TIPO);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            else if (pt != null)
                            {
                                PRODUCTO_TIPO pRODUCTO_TIPO = new PRODUCTO_TIPO
                                {
                                    Producto = pRODUCTO.ID,
                                    Tipo = pt.ID
                                };
                                db.PRODUCTO_TIPO.Add(pRODUCTO_TIPO);
                                db.SaveChanges();
                            }
                        }

                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        return View(pro);
                    }
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
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    string fecha = pRODUCTO.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                    Productos p = new Productos
                    {
                        ID = pRODUCTO.ID,
                        Nombre = pRODUCTO.Nombre,
                        Cantidad = pRODUCTO.Cantidad,
                        Fecha_Mo = fecha,
                        Descripcion = pRODUCTO.Descripcion,
                        Precio_A = pRODUCTO.Precio_A.Value,
                        Precio_V = pRODUCTO.Precio_V,
                        Activo = pRODUCTO.Activo.Value,
                        Imagen = pRODUCTO.Imagen,
                        Codigo = pRODUCTO.Codigo
                    };

                    return View(p);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Productos productos)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == productos.ID).FirstOrDefault();
                    pRODUCTO.Activo = false;

                    db.Entry(pRODUCTO).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    pRODUCTO.Activo = false;

                    db.Entry(pRODUCTO).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Detalle(int id)
        {
            try
            {
                try
                {
                    FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                    string Usuario = Session["usuario"].ToString();
                    int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                }
                catch (Exception)
                {
                    //return RedirectToAction("Index", "Home");
                }

                PRODUCTO productos = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                string fecha = productos.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                Productos p = new Productos
                {
                    ID = productos.ID,
                    Nombre = productos.Nombre,
                    Cantidad = productos.Cantidad,
                    Fecha_Mo = fecha,
                    Descripcion = productos.Descripcion,
                    Precio_A = productos.Precio_A.Value,
                    Precio_V = productos.Precio_V,
                    Activo = productos.Activo.Value,
                    Imagen = productos.Imagen,
                    Codigo = productos.Codigo
                };

                return View(p);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Carga(int id)
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

        public ActionResult Categoria(int id)
        {
            try
            {
                try
                {
                    FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                    string Usuario = Session["usuario"].ToString();
                    int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                }
                catch (Exception)
                {
                    //return RedirectToAction("Index", "Home");
                }

                List<PRODUCTO_TIPO> pt = db.PRODUCTO_TIPO.Where(x => x.Tipo == id).ToList();
                List<Productos> ps = new List<Productos>();
                foreach (PRODUCTO_TIPO pro_tip in pt)
                {
                    PRODUCTO productos = db.PRODUCTOes.Where(x => x.ID == pro_tip.Producto).FirstOrDefault();

                    string fecha = productos.Fecha_Mo.Value.ToString("dd/MM/yyyy");
                    Productos p = new Productos
                    {
                        ID = productos.ID,
                        Nombre = productos.Nombre,
                        Cantidad = productos.Cantidad,
                        Fecha_Mo = fecha,
                        Descripcion = productos.Descripcion,
                        Precio_A = productos.Precio_A.Value,
                        Precio_V = productos.Precio_V,
                        Activo = productos.Activo.Value,
                        Imagen = productos.Imagen,
                        Codigo = productos.Codigo
                    };
                    ps.Add(p);
                }

                return View(ps);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult Etiquetas(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            //List<LOCALIDAD> lOCALIDADs = db.LOCALIDADs.Where(x => x.CP.Equals(cp)).ToList();

            var c = (from x in db.PRODUCTO_T
                     where (x.Descripcion.Contains(Prefix))
                     select new { ID = x.ID, Descripcion = x.Descripcion }).ToList();

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);

            return cc;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Integrador.Common;
using Integrador.Entities;
using Integrador.Models;
using System.Web.Routing;

namespace Integrador.Controllers
{
    public class VentasController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Ventas
        public ActionResult Index()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Usuario != null && Tipo != 1)
                {
                    List<VENTA> vENTAs = db.VENTAs.Where(x => x.Usuario == Usuario).OrderByDescending(x=>x.Fecha).ToList();
                    List<Ventas> ventas = new List<Ventas>();
                    List<PAGO_T> pagos = db.PAGO_T.Where(x => x.Activo == true).ToList();
                    List<USUARIO_DIR> direccion = db.USUARIO_DIR.Where(x => x.Usuario == Usuario && x.Activo == true).ToList();
                    foreach (VENTA v in vENTAs)
                    {
                        string fecha = v.Fecha.Value.ToString("dd/MM/yyyy");
                        USUARIO_DIR dir1 = direccion.Where(x => x.ID == v.Direccion.Value).FirstOrDefault();
                        string dir = dir1.CP + ", " + dir1.Calle + " " + dir1.Numero_Ext + " " + dir1.Numero_Int;
                        string pago = pagos.Where(x => x.ID == v.Metod_Pago.Value).Select(x => x.Nombre).FirstOrDefault();
                        bool comp = false;
                        if (v.Comprobante != null)
                        { comp = true; }
                        Ventas ventas1 = new Ventas
                        {
                            ID = v.ID,
                            Usuario = v.Usuario,
                            Fecha = fecha,
                            Total = v.Total.Value,
                            Entrega = v.Entrega.Value,
                            Direccion = dir,
                            Metod_Pago = pago,
                            Factura = v.Factura.Value,
                            Comprobante = comp
                        };

                        ventas.Add(ventas1);
                    }

                    return View(ventas);
                }
                if (Usuario != null && Tipo == 1)
                {
                    List<VENTA> vENTAs = db.VENTAs.OrderByDescending(x => x.Fecha).ToList();
                    List<Ventas> ventas = new List<Ventas>();
                    List<PAGO_T> pagos = db.PAGO_T.Where(x => x.Activo == true).ToList();
                    List<USUARIO_DIR> direccion = db.USUARIO_DIR.Where(x => x.Activo == true).ToList();
                    foreach (VENTA v in vENTAs)
                    {
                        string fecha = v.Fecha.Value.ToString("dd/MM/yyyy");
                        USUARIO_DIR dir1 = direccion.Where(x => x.ID == v.Direccion.Value).FirstOrDefault();
                        string dir = dir1.CP + ", " + dir1.Calle + " " + dir1.Numero_Ext + " " + dir1.Numero_Int;
                        string pago = pagos.Where(x => x.ID == v.Metod_Pago.Value).Select(x => x.Nombre).FirstOrDefault();
                        bool comp = false;
                        if (v.Comprobante != null)
                        { comp = true; }
                        Ventas ventas1 = new Ventas
                        {
                            ID = v.ID,
                            Usuario = v.Usuario,
                            Fecha = fecha,
                            Total = v.Total.Value,
                            Entrega = v.Entrega.Value,
                            Direccion = dir,
                            Metod_Pago = pago,
                            Factura = v.Factura.Value,
                            Comprobante = comp
                        };

                        ventas.Add(ventas1);
                    }

                    return View(ventas);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Usuario != null)
                {
                    VENTA vENTAs = db.VENTAs.Where(x => x.ID == id).FirstOrDefault();
                    List<VENTA_D> vENTA_D = db.VENTA_D.Where(x => x.ID_Venta == vENTAs.ID).ToList();
                    List<Ventas_D> ventas_Ds = new List<Ventas_D>();
                    List<PAGO_T> pagos = db.PAGO_T.Where(x => x.Activo == true).ToList();
                    List<USUARIO_DIR> direccion = db.USUARIO_DIR.Where(x => x.Activo == true).ToList();
                    List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                    string fecha = vENTAs.Fecha.Value.ToString("dd/MM/yyyy");
                    USUARIO_DIR dir1 = direccion.Where(x => x.ID == vENTAs.Direccion.Value).FirstOrDefault();
                    string dir = dir1.CP + ", " + dir1.Calle + " " + dir1.Numero_Ext + " " + dir1.Numero_Int;
                    string pago = pagos.Where(x => x.ID == vENTAs.Metod_Pago.Value).Select(x => x.Nombre).FirstOrDefault();
                    bool comp = false;
                    if (vENTAs.Comprobante != null)
                    { comp = true; }
                    foreach (VENTA_D vd in vENTA_D)
                    {
                        string pro = pRODUCTOs.Where(x => x.ID == vd.Producto).Select(x => x.Nombre).FirstOrDefault();
                        Ventas_D ventas_D = new Ventas_D
                        {
                            ID = vd.ID,
                            ID_Venta = vd.ID_Venta.Value,
                            Producto = vd.Producto.Value,
                            Producto_l = pRODUCTOs.Where(x => x.ID == vd.Producto).Select(x => x.Nombre).FirstOrDefault(),
                            Cantidad = vd.Cantidad.Value,
                            Total = vd.Total.Value
                        };
                        ventas_Ds.Add(ventas_D);
                    }
                    Ventas ventas = new Ventas
                    {
                        ID = vENTAs.ID,
                        Usuario = vENTAs.Usuario,
                        Fecha = fecha,
                        Total = vENTAs.Total.Value,
                        Entrega = vENTAs.Entrega.Value,
                        Direccion = dir,
                        Metod_Pago = pago,
                        Factura = vENTAs.Factura.Value,
                        Comprobante = comp,
                        Comprobante1 = vENTAs.Comprobante,
                        Detalle = ventas_Ds
                    };

                    return View(ventas);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Ventas/Create
        public ActionResult Comprar()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Usuario != null && Tipo != 1)
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

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Comprar(Carritos ventas)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                bool correcto = true;
                int nventa = 0;
                if (Usuario != null && Tipo != 1)
                {
                    try
                    {
                        USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == Usuario).FirstOrDefault();
                        CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        List<CARRITO_D> cARRITO_Ds = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID).ToList();
                        List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                        VENTA vENTA = new VENTA
                        {
                            Usuario = Usuario,
                            Fecha = System.DateTime.Today,
                            Total = cARRITO.Total,
                            Entrega = false,
                            Direccion = ventas.Direccion,
                            Metod_Pago = ventas.Metod_Pago,
                            Factura = ventas.Factura
                        };
                        uSUARIO.N_Compras += 1;
                        db.Entry(uSUARIO).State = EntityState.Modified;
                        db.VENTAs.Add(vENTA);
                        db.SaveChanges();
                        nventa = vENTA.ID;

                        List<Carritos_D> carritos_Ds = new List<Carritos_D>();
                        foreach (CARRITO_D cARRITO_D in cARRITO_Ds)
                        {
                            PRODUCTO pro = pRODUCTOs.Where(x => x.ID == cARRITO_D.Producto).FirstOrDefault();
                            int falta = 0;
                            VENTA_D vENTA_D = new VENTA_D
                            {
                                ID_Venta = vENTA.ID,
                                Producto = pro.ID,
                                Cantidad = cARRITO_D.Cantidad,
                                Total = cARRITO_D.Total
                            };
                            if (pro.Cantidad >= vENTA_D.Cantidad)
                            {
                                pro.Cantidad -= vENTA_D.Cantidad.Value;
                            }
                            else if (pro.Cantidad > 0)
                            {
                                pro.Cantidad = 0;
                                falta = vENTA_D.Cantidad.Value - pro.Cantidad;
                            }
                            else
                            {
                                falta = vENTA_D.Cantidad.Value;
                            }

                            pro.N_Ventas += cARRITO_D.Cantidad;
                            db.Entry(pro).State = EntityState.Modified;
                            db.VENTA_D.Add(vENTA_D);
                            db.SaveChanges();
                            
                        }
                    }
                    catch (Exception)
                    {
                        correcto = false;
                    }
                    finally
                    {
                        if (correcto == true)
                        {
                            CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                            List<CARRITO_D> cARRITO_Ds = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID).ToList();
                            foreach (CARRITO_D cD in cARRITO_Ds)
                            {
                                db.CARRITO_D.Remove(cD);
                                db.SaveChanges();
                            }
                            db.CARRITOes.Remove(cARRITO);
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Orden", "Report", new { id = nventa });
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Comprobante()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo != 1)
                {
                    string ruta;
                    string imagen = "";
                    try
                    {
                        HttpPostedFileBase file = Request.Files["file"];
                        int doc = Convert.ToInt32(Request.Form.GetValues("id").FirstOrDefault());
                        VENTA vENTA = db.VENTAs.Where(x => x.ID == doc).FirstOrDefault();

                        imagen = vENTA.ID + ".PNG";
                        ruta = "~/images/Comprobante/" + vENTA.ID + ".PNG";
                        file.SaveAs(Server.MapPath(ruta));

                        vENTA.Comprobante = imagen;
                        db.Entry(vENTA).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }

        public ActionResult Ver(int id)
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
                        VENTA vENTA = db.VENTAs.Where(x => x.ID == id).FirstOrDefault();
                        ViewBag.ID = id;
                        ViewBag.Imagen = vENTA.Comprobante;
                        return View();
                    }
                    catch (Exception)
                    {
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int doc)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    VENTA vENTA = db.VENTAs.Where(x => x.ID == doc).FirstOrDefault();
                    vENTA.Comprobante = null;

                    db.Entry(vENTA).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //return RedirectToAction("Details", "Ventas", doc);
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new { id = doc });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

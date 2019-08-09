using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Integrador.Common;
using Integrador.Entities;
using Integrador.Models;

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
                    List<VENTA> vENTAs = db.VENTAs.Where(x => x.Usuario == Usuario).ToList();
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
                if (Usuario != null && Tipo != 1)
                {
                    VENTA vENTAs = db.VENTAs.Where(x => x.ID == id && x.Usuario == Usuario).FirstOrDefault();
                    List<VENTA_D> vENTA_D = db.VENTA_D.Where(x => x.ID_Venta == vENTAs.ID).ToList();
                    List<Ventas_D> ventas_Ds = new List<Ventas_D>();
                    List<PAGO_T> pagos = db.PAGO_T.Where(x => x.Activo == true).ToList();
                    List<USUARIO_DIR> direccion = db.USUARIO_DIR.Where(x => x.Usuario == Usuario && x.Activo == true).ToList();
                    string fecha = vENTAs.Fecha.Value.ToString("dd/MM/yyyy");
                    string dir = direccion.Where(x => x.ID == vENTAs.Direccion.Value).ToString();
                    string pago = pagos.Where(x => x.ID == vENTAs.Metod_Pago.Value).Select(x => x.Nombre).FirstOrDefault();
                    bool comp = false;
                    if (vENTAs.Comprobante != null)
                    { comp = true; }
                    foreach (VENTA_D vd in vENTA_D)
                    {
                        Ventas_D ventas_D = new Ventas_D
                        {
                            ID = vd.ID,
                            ID_Venta = vd.ID_Venta.Value,
                            Producto = vd.Producto.Value,
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

                        List<Carritos_D> carritos_Ds = new List<Carritos_D>();
                        foreach (CARRITO_D cARRITO_D in cARRITO_Ds)
                        {
                            PRODUCTO pro = pRODUCTOs.Where(x => x.ID == cARRITO_D.Producto).FirstOrDefault();
                            VENTA_D vENTA_D = new VENTA_D
                            {
                                ID_Venta = vENTA.ID,
                                Producto = pro.ID,
                                Cantidad = cARRITO_D.Cantidad,
                                Total = cARRITO_D.Total
                            };
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

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ventas/Edit/5
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

        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ventas/Delete/5
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

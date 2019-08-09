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
    public class CarritoController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Carrito
        public ActionResult Carrito()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo != 1)
                {
                    try
                    {
                        CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        List<CARRITO_D> cARRITO_Ds = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID).ToList();
                        List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();

                        //Carritos carritos = new Carritos();
                        List<Carritos_D> carritos_Ds = new List<Carritos_D>();
                        foreach(CARRITO_D cARRITO_D in cARRITO_Ds)
                        {
                            string pro = pRODUCTOs.Where(x => x.ID == cARRITO_D.Producto).Select(x => x.Nombre).FirstOrDefault();
                            Carritos_D carritos_D = new Carritos_D
                            {
                                ID = cARRITO_D.ID,
                                ID_Carrito = cARRITO_D.ID_Carrito.Value,
                                Producto_id = cARRITO_D.Producto.Value,
                                Producto = pro,
                                Cantidad = cARRITO_D.Cantidad.Value,
                                Total = cARRITO_D.Total.Value
                            };
                            carritos_Ds.Add(carritos_D);
                        }
                        Carritos carritos = new Carritos
                        {
                            ID = cARRITO.ID,
                            Usuario = cARRITO.Usuario,
                            Total = cARRITO.Total.Value,
                            Detalle = carritos_Ds
                        };

                        List<USUARIO_DIR> uSUARIO_DIRs = db.USUARIO_DIR.Where(x => x.Usuario == Usuario && x.Activo == true).ToList();
                        List<Usuarios_Dir> ud = new List<Usuarios_Dir>();
                        foreach (USUARIO_DIR dIR in uSUARIO_DIRs)
                        {
                            ESTADO edo = db.ESTADOes.Where(x => x.ID == dIR.Estado).FirstOrDefault();
                            MUNICIPIO mun = db.MUNICIPIOs.Where(x => x.ID == dIR.Municipio).FirstOrDefault();
                            LOCALIDAD col = db.LOCALIDADs.Where(x => x.ID == dIR.Colonia).FirstOrDefault();
                            Usuarios_Dir dir = new Usuarios_Dir
                            {
                                ID = dIR.ID,
                                Usuario = dIR.Usuario,
                                Calle = dIR.Calle,
                                Numero_Ext = dIR.Numero_Ext,
                                Numero_Int = dIR.Numero_Int,
                                Colonia = col.Nombre,
                                Municipio = mun.Nombre,
                                Estado = edo.Nombre,
                                CP = col.CP
                            };
                            ud.Add(dir);
                        }
                        List<PAGO_T> pAGO_Ts = db.PAGO_T.Where(x => x.Activo == true).ToList();
                        ViewBag.Direcciones = ud;
                        ViewBag.Pagos = pAGO_Ts;

                        return View(carritos);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Agregar(int id, int Cantidad)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo != 1)
                {
                    try
                    {
                        CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        if(cARRITO==null)
                        {
                            CARRITO carro = new CARRITO
                            {
                                Usuario = Usuario,
                                Total = 0
                            };
                            db.CARRITOes.Add(carro);
                            db.SaveChanges();
                            cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        }
                        CARRITO_D pro = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID && x.Producto == id).FirstOrDefault();
                        PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                        decimal Total = pRODUCTO.Precio_V * Cantidad;
                        if (pro == null)
                        {
                            CARRITO_D cARRITO_D = new CARRITO_D
                            {
                                ID_Carrito = cARRITO.ID,
                                Producto = pRODUCTO.ID,
                                Cantidad = Cantidad,
                                Total = Total
                            };
                            db.CARRITO_D.Add(cARRITO_D);
                            cARRITO.Total += Total;
                            db.Entry(cARRITO).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            pro.Cantidad += Cantidad;
                            pro.Total += Total;
                            db.Entry(pro).State = EntityState.Modified;
                            cARRITO.Total += Total;
                            db.Entry(cARRITO).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return RedirectToAction("Detalle", "Producto", new { id = id });
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Detalle", "Producto", new { id = id });
        }

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Usuario != null)
                {
                    CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                    CARRITO_D cARRITO_D = db.CARRITO_D.Where(x => x.ID == id).FirstOrDefault();
                    cARRITO.Total -= cARRITO_D.Total;

                    db.Entry(cARRITO).State = EntityState.Modified;
                    db.CARRITO_D.Remove(cARRITO_D);
                    db.SaveChanges();
                }

                return RedirectToAction("Carrito");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Agregar2(int id, int Cantidad)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo != 1)
                {
                    try
                    {
                        CARRITO cARRITO = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        List<CARRITO_D> pros = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID).ToList();
                        CARRITO_D pro = pros.Where(x => x.Producto == id).FirstOrDefault();
                        PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                        decimal Total = pRODUCTO.Precio_V * Cantidad;
                        decimal TotalCarro = 0;

                        pro.Cantidad = Cantidad;
                        pro.Total = Total;
                        db.Entry(pro).State = EntityState.Modified;
                        db.SaveChanges();

                        pros = db.CARRITO_D.Where(x => x.ID_Carrito == cARRITO.ID).ToList();
                        foreach (CARRITO_D item in pros)
                        {
                            TotalCarro += item.Total.Value;
                        }
                        cARRITO.Total = TotalCarro;
                        db.Entry(cARRITO).State = EntityState.Modified;
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
    }
}

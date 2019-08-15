using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;
using Integrador.Common;
using System.Web.Hosting;

namespace Integrador.Controllers
{
    public class ReportController : Controller
    {
        INTEGRAEntities db = new INTEGRAEntities();
        public ActionResult Productos()
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                List<PRODUCTO_T> tipo = db.PRODUCTO_T.Where(x => x.Activo == true).ToList();
                List<Productos> productos = new List<Productos>();
                List<USUARIO> uSUARIOs = db.USUARIOs.Where(x => x.Activo == true && x.T_Usuario == 2).ToList();
                foreach (PRODUCTO p in pRODUCTOs)
                {
                    string eti = "";
                    List<PRODUCTO_TIPO> pt = db.PRODUCTO_TIPO.Where(x => x.Producto == p.ID).ToList();
                    foreach (PRODUCTO_TIPO ti in pt)
                    {
                        eti += tipo.Where(x => x.ID == ti.Tipo).Select(x => x.Descripcion).FirstOrDefault() + ", ";
                    }
                    Productos productos1 = new Productos
                    {
                        ID = p.ID,
                        Nombre = p.Nombre,
                        Cantidad = p.Cantidad,
                        Precio_A = p.Precio_A.Value,
                        Precio_V = p.Precio_V,
                        Descripcion = p.Descripcion,
                        Codigo = p.Codigo,
                        Imagen = p.Imagen,
                        Fecha_Mo = p.Fecha_Mo.Value.ToString("dd/MM/yyyy"),
                        Etiquetas = eti,
                        Activo = p.Activo.Value,
                        Nventas = p.N_Ventas.Value
                    };
                    productos.Add(productos1);
                }
                ViewBag.Productos = productos;
                ViewBag.Tipos = tipo;
                ViewBag.Usuarios = uSUARIOs;
                return View();
            }
            catch(Exception x)
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);


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

        [HttpPost]
        public ActionResult Orden(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());

                VENTA c = db.VENTAs.Where(x => x.ID == id).FirstOrDefault();
                List<VENTA_D> d = db.VENTA_D.Where(x => x.ID_Venta == c.ID).ToList();
                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == c.Usuario).FirstOrDefault();
                USUARIO_DIR dir = db.USUARIO_DIR.Where(x => x.ID == c.Direccion).FirstOrDefault();
                PAGO_T pago = db.PAGO_T.Where(x => x.ID == c.Metod_Pago).FirstOrDefault();
                ESTADO edo = db.ESTADOes.Where(x => x.ID == dir.Estado).FirstOrDefault();
                MUNICIPIO mun = db.MUNICIPIOs.Where(x => x.ID == dir.Municipio).FirstOrDefault();
                LOCALIDAD col = db.LOCALIDADs.Where(x => x.ID == dir.Colonia).FirstOrDefault();

                ReportCabecera cab = new ReportCabecera
                {
                    PEDIDO = c.ID.ToString(),
                    USUARIO = uSUARIO.Nombre + " " + uSUARIO.Apellido_P + " " + uSUARIO.Apellido_M,
                    FECHA = c.Fecha.Value.Date.ToString("dd/MM/yyyy"),
                    DIRECCION = dir.Calle + " " + dir.Numero_Ext + " " + dir.Numero_Int + ", " + col.Nombre + ", " + mun.Nombre + ", " + edo.Nombre + ", " + dir.CP,
                    PAGO = pago.Nombre + ": " + pago.Descripcion,
                    TOTAL = c.Total.ToString()
                };
                List<ReportDetalle> det = new List<ReportDetalle>();
                List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                foreach (VENTA_D dd in d)
                {
                    PRODUCTO pp = pRODUCTOs.Where(x => x.ID == dd.Producto).FirstOrDefault();
                    ReportDetalle rd = new ReportDetalle
                    {
                        PRODUCTO1 = pp.Codigo,
                        TEXTO = (pp.Descripcion ?? ""),
                        CANTIDAD = dd.Cantidad.ToString(),
                        PRECIO = pp.Precio_V.ToString(),
                        IMPORTE = dd.Total.ToString()
                    };
                    det.Add(rd);
                }

                ReportEsqueleto re = new ReportEsqueleto();
                string recibeRuta = re.crearPDF(cab, det);
                ViewBag.url = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + HostingEnvironment.ApplicationVirtualPath + "/" + recibeRuta;
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}

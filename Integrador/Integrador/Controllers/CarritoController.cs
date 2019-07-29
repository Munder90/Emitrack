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
                        //CARRITO cARRITO = new CARRITO
                        //{
                        //    Usuario = Usuario,
                        //    Total = 0
                        //};
                        //db.CARRITOes.Add(cARRITO);
                        //db.SaveChanges();

                        //CARRITO carrito = db.CARRITOes.Where(x => x.Usuario == Usuario).FirstOrDefault();
                        //PRODUCTO pRODUCTO = db.PRODUCTOes.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                        //decimal Total = pRODUCTO.Precio_V * Cantidad;
                        //CARRITO_D cARRITO_D = new CARRITO_D
                        //{
                        //    ID_Carrito = carrito.ID,
                        //    Producto = pRODUCTO.ID,
                        //    Cantidad = Cantidad,
                        //    Total = Total
                        //};
                        //db.CARRITO_D.Add(cARRITO_D);
                        //db.SaveChanges();
                    }
                    return RedirectToAction("Detalle", "Producto", new { id = id });
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Detalle", "Producto", new { id = id });
        }

        // GET: Carrito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carrito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrito/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrito/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carrito/Edit/5
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

        // GET: Carrito/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carrito/Delete/5
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

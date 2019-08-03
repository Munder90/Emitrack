using Integrador.Entities;
using Integrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Integrador.Common
{
    public class FnCommon
    {
        public static USUARIO ObtenerUsuario(INTEGRAEntities db, string user_id)
        {
            return db.USUARIOs.Where(a => a.ID.Equals(user_id) && a.Activo.Equals(true)).FirstOrDefault();
        }
        public static void ObtenerConfPage(INTEGRAEntities db, /*int pagina_id,*/ string user_id, ControllerBase controller, int? pagina_id_textos = null)
        {
            var user = ObtenerUsuario(db, user_id);
            try { controller.ViewBag.Categoria = db.PRODUCTO_T.Where(x => x.Activo == true).ToList(); } catch (Exception) { }
            if (user != null)
            {
                controller.ViewBag.Usuario = user.ID;
                controller.ViewBag.Tipo = user.T_Usuario;
            }
            try
            {
                var carrito = db.CARRITOes.Where(x => x.Usuario.Equals(user.ID)).FirstOrDefault();
                controller.ViewBag.Carrito = carrito;
                var carrito_d = db.CARRITO_D.Where(x => x.ID_Carrito == carrito.ID).ToList();
                List<Carritos_D> carritos_Ds = new List<Carritos_D>();
                List<PRODUCTO> pRODUCTOs = db.PRODUCTOes.Where(x => x.Activo == true).ToList();
                foreach (CARRITO_D cARRITO_D in carrito_d)
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
                controller.ViewBag.CarritoD = carritos_Ds;
            }
            catch (Exception) { }
        }
    }
}
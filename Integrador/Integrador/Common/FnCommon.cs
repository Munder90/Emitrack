using Integrador.Entities;
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
            try { controller.ViewBag.Categoria = db.PRODUCTO_T.Where(x => x.Activo.Equals(true)).ToList(); } catch (Exception) { }
            if (user != null)
            {
                controller.ViewBag.Usuario = user.ID;
                controller.ViewBag.Tipo = user.T_Usuario;
            }
            try
            {
                var carrito = db.CARRITOes.Where(x => x.Usuario.Equals(user.ID)).FirstOrDefault();
                controller.ViewBag.Carrito = carrito;
                controller.ViewBag.CarritoD = db.CARRITO_D.Where(x => x.ID_Carrito.Equals(carrito.ID)).ToList();
            }
            catch (Exception) { }
        }
    }
}
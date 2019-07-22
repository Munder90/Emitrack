using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;
using System.Text.RegularExpressions;
using System.Data.Entity;
using Integrador.Common;

namespace Integrador.Controllers
{
    public class UsuarioController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            ViewBag.Usuario = Session["usuario"].ToString();
            ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = u.Fecha_N.Date,
                    Pregunta = u.Pregunta
                };
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //ViewBag.Usuario = Session["usuario"].ToString();
                //ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuario/Create
        public ActionResult Registro()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            // TODO: Add insert logic here
            int tipo;
            if (ModelState.IsValid)
            {
                if (!ExisteUsuario(usuario.ID1))
                {
                    if (usuario.Password == usuario.Manager)
                    {
                        if (ComprobarEmail(usuario.Email) && !String.IsNullOrEmpty(usuario.Email))
                        {
                            try
                            {
                                if (usuario.ID1 != null && !db.USUARIOs.Any(x => x.ID == usuario.ID1))
                                {
                                    if (usuario.T_Usuario == 0)
                                    {
                                        tipo = 2;
                                    }
                                    else
                                    {
                                        tipo = 1;
                                    }
                                    Cryptography c = new Cryptography();
                                    usuario.Password = c.Encrypt(usuario.Password);
                                    USUARIO u = new USUARIO
                                    {
                                        ID = usuario.ID1,
                                        Nombre = usuario.Nombre,
                                        Apellido_P = usuario.Apellido_P,
                                        Apellido_M = usuario.Apellido_M,
                                        Fecha_N = usuario.Fecha_N,
                                        Email = usuario.Email,
                                        Password = usuario.Password,
                                        T_Usuario = tipo,
                                        Pregunta = usuario.Pregunta,
                                        Respuesta = usuario.Respuesta,
                                        Imagen = usuario.Imagen,
                                        Activo = true
                                    };

                                    db.USUARIOs.Add(u);
                                    db.SaveChanges();

                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            catch
                            {
                                return View(usuario);
                            }
                        }
                        else
                        {
                            ViewBag.Error = "El correo no es correcto";
                        }
                    }
                    else
                    {
                        TempData["MensajePass"] = "La contraseña no coincide";
                    }
                }
                else
                {
                    TempData["MensajeUsuario"] = "El usuario ya existe. Introduzca un ID de usuario diferente";
                }
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = u.Fecha_N.Date
                };
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //ViewBag.Usuario = Session["usuario"].ToString();
                //ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Usuario us)
        {
            try
            {
                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                uSUARIO.Nombre = us.Nombre;
                uSUARIO.Apellido_P = us.Apellido_P;
                uSUARIO.Apellido_M = us.Apellido_M;
                uSUARIO.Fecha_N = us.Fecha_N;

                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return RedirectToAction("Details", id);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditPass(string id)
        {
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                Pass us = new Pass
                {
                    Id = u.ID,
                };
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //ViewBag.Usuario = Session["usuario"].ToString();
                //ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditPass(string id, Pass us)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());

                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                Cryptography c = new Cryptography();
                string pass_a = c.Decrypt(uSUARIO.Password);
                if ((us.Apass.Equals(pass_a)))
                {
                    if (us.Npass1.Equals(us.Npass2))
                    {
                        if (ModelState.IsValid)
                        {
                            uSUARIO.Password = c.Encrypt(us.Npass1);
                            db.Entry(uSUARIO).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Details", id);
                        }
                    }
                }
                ViewBag.message = "Los datos no coinciden";
                return View(us);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditQues(string id)
        {
            if (id != null)
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult EditQues(string id, FormCollection collection)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
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

        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                USUARIO uSUARIO = db.USUARIOs.Find(id);
                uSUARIO.Activo = false;
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Dashboard/5
        public ActionResult Dashboard(string id)
        {
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = u.Fecha_N.Date,
                    Pregunta = u.Pregunta
                };
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                //ViewBag.Usuario = Session["usuario"].ToString();
                //ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Direccion(string id)
        {
            if (id != null)
            {
                List<USUARIO_DIR> d = db.USUARIO_DIR.Where(x => x.Usuario == id).ToList();
                List<Usuarios_Dir> ud = new List<Usuarios_Dir>();

                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (ud.Count() > 0)
                {
                    return View(ud);
                }
                else
                {
                    ViewBag.Error = "No tienes Direcciones Registradas";
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DireccionNueva(string id)
        {
            if (id != null)
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DireccionNueva(string id, Usuarios_Dir Model)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DireccionEdit(string id)
        {
            if (id != null)
            {
                USUARIO_DIR d = db.USUARIO_DIR.Where(x => x.Usuario == id).FirstOrDefault();
                Usuarios_Dir ud = new Usuarios_Dir();

                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(ud);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DireccionEdit(string id, FormCollection collection)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Pass(string id)
        {
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = u.Fecha_N.Date,
                };
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Pass(string id, FormCollection collection)
        {
            try
            {
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public bool ExisteUsuario(string user)
        {
            var existeusuario = db.USUARIOs.Where(t => t.ID == user).SingleOrDefault();
            if (existeusuario == null)
                return false;
            else
                return true;
        }

        public static bool ComprobarEmail(string email)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, sFormato))
            {
                if (Regex.Replace(email, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public JsonResult Colonia(string Prefix, int cp)
        {
            if (Prefix == null)
                Prefix = "";

            //List<LOCALIDAD> lOCALIDADs = db.LOCALIDADs.Where(x => x.CP.Equals(cp)).ToList();

            var c = (from x in db.LOCALIDADs
                     where (x.Nombre.Contains(Prefix) && x.CP == cp)
                     select new { ID = x.ID, Colonia = x.Nombre }).ToList();

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);

            return cc;
        }

        public JsonResult CodigoP(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            var c = (from x in db.LOCALIDADs
                     where x.CP.ToString().Contains(Prefix)
                     select new { CP = x.CP }).ToList();

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);

            return cc;
        }

        public JsonResult EdoMun(int cp)
        {
            var c = (from x in db.LOCALIDADs
                     join y in db.MUNICIPIOs on x.Municipio equals y.ID
                     join z in db.ESTADOes on y.Estado equals z.ID
                     where (x.CP == cp)
                     select new { estado = z.Nombre, municipio = y.Nombre }).FirstOrDefault();

            JsonResult cc = Json(c, JsonRequestBehavior.AllowGet);

            return cc;
        }
    }
}

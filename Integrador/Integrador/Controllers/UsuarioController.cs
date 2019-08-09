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
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string User1 = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    List<USUARIO> uSUARIOs = db.USUARIOs.ToList();
                    List<Usuario> usuarios = new List<Usuario>();
                    List<USUARIO_T> tipo = db.USUARIO_T.ToList();
                    foreach(USUARIO u in uSUARIOs)
                    {
                        if (u.ID != User1)
                        {
                            Usuario usuario = new Usuario
                            {
                                ID1 = u.ID,
                                Nombre = u.Nombre,
                                Apellido_P = u.Apellido_P,
                                Apellido_M = u.Apellido_M,
                                Fecha_N = u.Fecha_N.Date.ToString("dd/MM/yyyy"),
                                T_Usuario = u.T_Usuario,
                                T_Usuario_l = tipo.Where(x => x.ID == u.T_Usuario).Select(x => x.Desc).FirstOrDefault(),
                                Email = u.Email,
                                Pregunta = u.Pregunta,
                                Respuesta = u.Respuesta
                            };
                            usuarios.Add(usuario);
                        }
                    }
                    return View(usuarios);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
            if (id != null)
            {
                USUARIO u = db.USUARIOs.Where(x => x.ID == id).FirstOrDefault();
                string fecha = u.Fecha_N.Date.ToString("dd/MM/yyyy");
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = fecha,
                    Pregunta = u.Pregunta
                };
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuario/Create
        public ActionResult Registro()
        {
            FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            // TODO: Add insert logic here
            FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
            int tipo = 1;
            if (ModelState.IsValid)
            {
                if (!ExisteUsuario(usuario.ID1))
                {
                    if (!ExisteEmail(usuario.Email))
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
                                            tipo = db.USUARIO_T.Where(x => x.Desc == usuario.T_Usuario_l).Select(x => x.ID).FirstOrDefault();
                                        }
                                        Cryptography c = new Cryptography();
                                        usuario.Password = c.Encrypt(usuario.Password);
                                        USUARIO u = new USUARIO
                                        {
                                            ID = usuario.ID1,
                                            Nombre = usuario.Nombre,
                                            Apellido_P = usuario.Apellido_P,
                                            Apellido_M = usuario.Apellido_M,
                                            Fecha_N = DateTime.Parse(usuario.Fecha_N),
                                            Email = usuario.Email,
                                            Password = usuario.Password,
                                            T_Usuario = tipo,
                                            Pregunta = usuario.Pregunta,
                                            Respuesta = usuario.Respuesta,
                                            Imagen = usuario.Imagen,
                                            Activo = true,
                                            N_Compras = 0
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
                        TempData["MensajeUsuario"] = "El correo ya existe. Utilice un correo diferente";
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
                string fecha = u.Fecha_N.Date.ToString("dd/MM/yyyy");
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = fecha
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string User1 = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());

                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == us.ID1).FirstOrDefault();
                uSUARIO.Nombre = us.Nombre;
                uSUARIO.Apellido_P = us.Apellido_P;
                uSUARIO.Apellido_M = us.Apellido_M;
                uSUARIO.Fecha_N = DateTime.Parse(us.Fecha_N);

                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Dashboard", id);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Pass(string id)
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
        public ActionResult Pass(string id, Pass us)
        {
            try
            {
                //ViewBag.Usuario = Session["usuario"].ToString();
                //ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());

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
                            return RedirectToAction("Dashboard", new { id = Usuario });
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

        public ActionResult Pregunta(string id)
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
        public ActionResult Pregunta(string id, Pregunta pregunta)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == Usuario && x.Activo == true).FirstOrDefault();
                Cryptography c = new Cryptography();
                pregunta.Apass = c.Encrypt(pregunta.Apass);
                if (pregunta.Apass == uSUARIO.Password)
                {
                    uSUARIO.Pregunta = pregunta.Nques;
                    uSUARIO.Respuesta = pregunta.Nress;
                    db.Entry(uSUARIO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Dashboard", new { id = Usuario });
                }

                return View(pregunta);
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
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
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
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
                string fecha = u.Fecha_N.Date.ToString("dd/MM/yyyy");
                Usuario us = new Usuario
                {
                    ID1 = u.ID,
                    Email = u.Email,
                    Nombre = u.Nombre,
                    Apellido_P = u.Apellido_P,
                    Apellido_M = u.Apellido_M,
                    Fecha_N = fecha,
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
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (id != null)
                {
                    List<USUARIO_DIR> d = db.USUARIO_DIR.Where(x => x.Usuario == id).ToList();
                    List<Usuarios_Dir> ud = new List<Usuarios_Dir>();
                    foreach(USUARIO_DIR dIR in d)
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
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DireccionNueva(string id)
        {
            if (id != null)
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DireccionNueva(string id, Usuarios_Dir Model)
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
                        int edo = db.ESTADOes.Where(x => x.Nombre == Model.Estado).Select(x => x.ID).FirstOrDefault();
                        int mun = db.MUNICIPIOs.Where(x => x.Estado == edo && x.Nombre == Model.Municipio).Select(x => x.ID).FirstOrDefault();
                        int col = db.LOCALIDADs.Where(x => x.Municipio == mun && x.CP == Model.CP && x.Nombre == Model.Colonia).Select(x => x.ID).FirstOrDefault();
                        USUARIO_DIR dIR = new USUARIO_DIR
                        {
                            Usuario = Usuario,
                            Calle = Model.Calle,
                            Numero_Ext = Model.Numero_Ext,
                            Numero_Int = Model.Numero_Int,
                            Colonia = col,
                            Municipio = mun,
                            Estado = edo,
                            CP = Model.CP,
                            Activo = true
                        };

                        db.USUARIO_DIR.Add(dIR);
                        db.SaveChanges();

                        return RedirectToAction("Direccion", Usuario);
                    }
                    catch (Exception)
                    {
                        return View(Model);
                    }
                }

                return View(Model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DireccionEdit(int id)
        {
            FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
            string Usuario = Session["usuario"].ToString();
            int Tipo = Convert.ToInt32(Session["tipo"].ToString());
            if (Tipo != 1)
            {
                USUARIO_DIR d = db.USUARIO_DIR.Where(x => x.ID == id).FirstOrDefault();
                ESTADO edo = db.ESTADOes.Where(x => x.ID == d.Estado).FirstOrDefault();
                MUNICIPIO mun = db.MUNICIPIOs.Where(x => x.ID == d.Municipio).FirstOrDefault();
                LOCALIDAD col = db.LOCALIDADs.Where(x => x.ID == d.Colonia).FirstOrDefault();
                Usuarios_Dir ud = new Usuarios_Dir
                {
                    ID = d.ID,
                    Usuario = d.Usuario,
                    Calle = d.Calle,
                    Numero_Ext = d.Numero_Ext,
                    Numero_Int = d.Numero_Int,
                    Colonia = col.Nombre,
                    Municipio = mun.Nombre,
                    Estado = edo.Nombre,
                    CP = col.CP,
                };
                return View(ud);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DireccionEdit(Usuarios_Dir dir)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo != 1)
                {
                    USUARIO_DIR d = db.USUARIO_DIR.Where(x => x.ID == dir.ID).FirstOrDefault();
                    int edo = db.ESTADOes.Where(x => x.Nombre == dir.Estado).Select(x => x.ID).FirstOrDefault();
                    int mun = db.MUNICIPIOs.Where(x => x.Estado == edo && x.Nombre == dir.Municipio).Select(x => x.ID).FirstOrDefault();
                    int col = db.LOCALIDADs.Where(x => x.Municipio == mun && x.CP == dir.CP && x.Nombre == dir.Colonia).Select(x => x.ID).FirstOrDefault();

                    d.Usuario = dir.Usuario;
                    d.Calle = dir.Calle;
                    d.Numero_Ext = dir.Numero_Ext;
                    d.Numero_Int = dir.Numero_Int;
                    d.Colonia = col;
                    d.Municipio = mun;
                    d.Estado = edo;
                    d.CP = dir.CP;

                    db.Entry(d).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Direccion", Usuario);
                }

                return View(dir);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirmedDir(int id)
        {
            try
            {
                FnCommon.ObtenerConfPage(db, User.Identity.Name, this.ControllerContext.Controller);
                string Usuario = Session["usuario"].ToString();
                int Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (Tipo == 1)
                {
                    USUARIO_DIR dIR = db.USUARIO_DIR.Where(x => x.ID == id && x.Activo == true).FirstOrDefault();
                    dIR.Activo = false;

                    db.Entry(dIR).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
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

        public bool ExisteEmail(string email)
        {
            var existeemail = db.USUARIOs.Where(t => t.Email == email).SingleOrDefault();
            if (existeemail == null)
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

        public JsonResult Tipo(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";

            //List<LOCALIDAD> lOCALIDADs = db.LOCALIDADs.Where(x => x.CP.Equals(cp)).ToList();

            var c = (from x in db.USUARIO_T
                     where (x.Desc.Contains(Prefix))
                     select new { ID = x.ID, Desc = x.Desc }).ToList();

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

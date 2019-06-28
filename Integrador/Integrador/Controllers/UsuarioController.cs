using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Integrador.Models;
using Integrador.Entities;
using System.Text.RegularExpressions;

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
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
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
                    Fecha_N = u.Fecha_N.Date,
                    Pregunta = u.Pregunta
                };
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
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
                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
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
                return View(ud);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Direccion(string id, FormCollection collection)
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
    }
}

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
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            string usuario = Session["usuario"].ToString();
            if (usuario == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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

        // GET: Usuario/Dashboard/5
        public ActionResult Dashboard(string id)
        {
            return View();
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

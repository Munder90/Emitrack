﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Integrador.Models;
using Integrador.Entities;
using System.Globalization;
using System.Data.Entity;

namespace Integrador.Controllers
{
    public class CuentasController : Controller
    {
        readonly INTEGRAEntities db = new INTEGRAEntities();
        // GET: Cuentas/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
            }

            LoginViewModel m = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(m);
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            returnUrl = "/Home/Index";

            USUARIO user = new USUARIO
            {
                ID = model.ID,
                Password = model.Password
            };

            Cryptography c = new Cryptography();
            string pass = c.Encrypt(user.Password);

            user = db.USUARIOs.Where(a => a.ID.Equals(user.ID) && a.Password.Equals(pass) && a.Activo == true).FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(model.ID, false);

                var authTicket = new FormsAuthenticationTicket(1, user.ID.ToUpper(), DateTime.Now, DateTime.Now.AddDays(1), false, "Administrador");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                Session["usuario"] = user.ID;
                Session["tipo"] = user.T_Usuario;

                if (returnUrl != null)
                {
                    bool us = false;
                    string utest = ConfigurationManager.AppSettings["userTest"];
                    if (utest == null)
                        utest = "";
                    if (utest == "X")
                        us = true;

                    if (!us)
                    {
                        var checkUser = db.USUARIOLOGs.SingleOrDefault(x => x.USUARIO_ID == user.ID.ToUpper());

                        try
                        {
                            if (checkUser == null)
                            {
                                USUARIOLOG usuLog = new USUARIOLOG();
                                usuLog.USUARIO_ID = user.ID.ToUpper();
                                usuLog.POS = 1;
                                usuLog.SESION = System.Web.HttpContext.Current.Session.SessionID;
                                usuLog.NAVEGADOR = Request.Browser.Type;
                                usuLog.UBICACION = System.Environment.UserName + " - " + RegionInfo.CurrentRegion.DisplayName;
                                usuLog.FECHA = DateTime.Now;
                                usuLog.LOGIN = true;
                                db.USUARIOLOGs.Add(usuLog);
                                db.SaveChanges();
                                Session["userlog"] = usuLog;
                                //return Redirect(returnUrl);
                                return RedirectToAction("Dashboard", "Usuario", new { id = user.ID });
                            }
                            else
                            {
                                return RedirectToAction("validateLoginView", new { USUARIO_ID = user.ID.ToUpper(), returnUrl = returnUrl });
                                ////checkUser.USUARIO_ID = user.ID;
                                ////checkUser.POS = 1;
                                ////checkUser.SESION = System.Web.HttpContext.Current.Session.SessionID;
                                ////checkUser.NAVEGADOR = Request.Browser.Type;
                                ////checkUser.UBICACION = RegionInfo.CurrentRegion.DisplayName;
                                ////checkUser.FECHA = DateTime.Now;
                                ////checkUser.LOGIN = true;
                                ////db.SaveChanges();
                                ////Session["userlog"] = checkUser;
                                ////return Redirect(returnUrl);
                                ////return RedirectToAction("Index", "Home");
                            }

                        }
                        catch
                        {
                            //Hay que revisar las posibilidades de error
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    ////USUARIOLOG usuLog2 = new USUARIOLOG();
                    ////Session["userlog"] = null;
                    ////Session["userlog"] = new USUARIOLOG();

                    return Redirect(returnUrl);
                }
                return RedirectToAction("Dashboard", "Usuario", new { id = user.ID });
            }

            else
            {
                ModelState.AddModelError("", "Usuario/contraseña incorrecta.");
                return View(model);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            try
            {
                Session["usuario"] = null;
                Session["tipo"] = null;
                USUARIOLOG usu = new USUARIOLOG();

                bool us = false;
                string utest = ConfigurationManager.AppSettings["userTest"];
                if (utest == null)
                    utest = "";
                if (utest == "X")
                    us = true;

                if (!us)
                {
                    ////usu = (USUARIOLOG)Session["userlog"];
                    usu.SESION = System.Web.HttpContext.Current.Session.SessionID;

                    if (usu != null)
                    {
                        var checkUser = db.USUARIOLOGs.SingleOrDefault(x => x.USUARIO_ID == User.Identity.Name);
                        if (checkUser != null)
                            if (checkUser.SESION == System.Web.HttpContext.Current.Session.SessionID)
                            {
                                db.Entry(checkUser).State = System.Data.Entity.EntityState.Deleted;
                                db.SaveChanges();
                            }
                    }
                }
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotViewModel pass)
        {
            var existeusuario = db.USUARIOs.Where(t => t.ID == pass.Usuario || t.Email == pass.Email).SingleOrDefault();
            if (existeusuario != null)
            {
                return RedirectToAction("Recuperar","Cuentas", new { user = existeusuario.ID });
            }
            else
            {
                ViewBag.Error = "El usuario o email no existe";
                return View(pass);
            }
        }

        public ActionResult Recuperar(string user)
        {
            try
            {
                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == user && x.Activo == true).FirstOrDefault();
                if (uSUARIO != null)
                {
                    ForgotPasswordViewModel pass = new ForgotPasswordViewModel
                    {
                        User = uSUARIO.ID,
                        Pregunta = uSUARIO.Pregunta
                    };
                    return View(pass);
                }
            }
            catch
            { }
            return View();
        }

        [HttpPost]
        public ActionResult Recuperar(ForgotPasswordViewModel pass)
        {
            USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == pass.User).FirstOrDefault();

            if (pass.Respuesta == uSUARIO.Respuesta)
            {
                return RedirectToAction("Pass", "Cuentas", new { id = uSUARIO.ID });
            }
            else
            {
                return View(pass);
            }



            return View();
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
                return View(us);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Pass(string id, Pass us)
        {
            try
            {
                USUARIO uSUARIO = db.USUARIOs.Where(x => x.ID == us.Id && x.Activo == true).FirstOrDefault();
                Cryptography c = new Cryptography();
                string pass_a = c.Decrypt(uSUARIO.Password);
                //if ((us.Apass.Equals(pass_a)))
                //{
                if (us.Npass1.Equals(us.Npass2))
                {
                    if (ModelState.IsValid)
                    {
                        uSUARIO.Password = c.Encrypt(us.Npass1);
                        db.Entry(uSUARIO).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
                //}
                ViewBag.message = "Los datos no coinciden";
                return View(us);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult validateLoginView(string USUARIO_ID, string returnUrl)
        {
            ////int pagina = 221; //ID EN BASE DE DATOS
            string u = User.Identity.Name;
            var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
            ViewBag.usuario = user;
            var checkUser = db.USUARIOLOGs.SingleOrDefault(x => x.USUARIO_ID == USUARIO_ID);
            if (returnUrl == "/Account/validateLoginView")
                returnUrl = "/";
            ViewBag.returnUrl = returnUrl;

            return View(checkUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult validateLoginView([Bind(Include = "USUARIO_ID,POS,SESION,NAVEGADOR,UBICACION,FECHA,LOGIN")] USUARIOLOG uSUARIOLOG, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                uSUARIOLOG.POS = 1;
                uSUARIOLOG.SESION = System.Web.HttpContext.Current.Session.SessionID;
                uSUARIOLOG.NAVEGADOR = Request.Browser.Type;
                ////uSUARIOLOG.UBICACION = System.Environment.MachineName + "/" + System.Environment.UserName + " - " + RegionInfo.CurrentRegion.DisplayName;
                uSUARIOLOG.UBICACION = System.Environment.UserName + " - " + RegionInfo.CurrentRegion.DisplayName;
                uSUARIOLOG.FECHA = DateTime.Now;
                uSUARIOLOG.LOGIN = true;
                try
                {
                    db.Entry(uSUARIOLOG).State = EntityState.Modified;
                    db.SaveChanges();
                    ////Session["userlog"] = uSUARIOLOG;
                }
                catch
                {
                    //Tal vez hay error en la conexión
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
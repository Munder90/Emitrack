using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Integrador.Models;
using Integrador.Entities;


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

                ViewBag.Usuario = Session["usuario"].ToString();
                ViewBag.Tipo = Convert.ToInt32(Session["tipo"].ToString());
                if (returnUrl != null)
                {
                    bool us = false;
                    string utest = ConfigurationManager.AppSettings["userTest"];
                    if (utest == null)
                        utest = "";
                    if (utest == "X")
                        us = true;

                    //if (!us)
                    //{
                    //    //var checkUser = db.USUARIOLOGs.SingleOrDefault(x => x.USUARIO_ID == user.ID.ToUpper());

                    //    try
                    //    {
                    //        if (checkUser == null)
                    //        {
                    //            //USUARIOLOG usuLog = new USUARIOLOG();
                    //            usuLog.USUARIO_ID = user.ID.ToUpper();
                    //            usuLog.POS = 1;
                    //            usuLog.SESION = System.Web.HttpContext.Current.Session.SessionID;
                    //            usuLog.NAVEGADOR = Request.Browser.Type;
                    //            usuLog.UBICACION = System.Environment.UserName + " - " + RegionInfo.CurrentRegion.DisplayName;
                    //            usuLog.FECHA = DateTime.Now;
                    //            usuLog.LOGIN = true;
                    //            //db.USUARIOLOGs.Add(usuLog);
                    //            db.SaveChanges();
                    //            ////Session["userlog"] = usuLog;
                    //            return Redirect(returnUrl);
                    //        }
                    //        else
                    //        {
                    //            return RedirectToAction("validateLoginView", new { USUARIO_ID = user.ID.ToUpper(), returnUrl = returnUrl });
                    //            ////checkUser.USUARIO_ID = user.ID;
                    //            ////checkUser.POS = 1;
                    //            ////checkUser.SESION = System.Web.HttpContext.Current.Session.SessionID;
                    //            ////checkUser.NAVEGADOR = Request.Browser.Type;
                    //            ////checkUser.UBICACION = RegionInfo.CurrentRegion.DisplayName;
                    //            ////checkUser.FECHA = DateTime.Now;
                    //            ////checkUser.LOGIN = true;
                    //            ////db.SaveChanges();
                    //            ////Session["userlog"] = checkUser;
                    //            ////return Redirect(returnUrl);
                    //        }

                    //    }
                    //    catch
                    //    {
                    //        //Hay que revisar las posibilidades de error
                    //    }
                    //}

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
            FormsAuthentication.SignOut();
            Session["usuario"] = null;
            Session["tipo"] = null;
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Recuperar", existeusuario.ID);
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
            return View();
        }
    }
}
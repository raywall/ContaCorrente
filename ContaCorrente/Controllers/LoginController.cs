using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ContaCorrente.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                var usuario = new Business.UsuarioDao().Carregar(form["usuario"]);

                if (usuario != null && (usuario.Ativo & usuario.Password.Equals(form["senha"])))
                {
                    var authTicket = new FormsAuthenticationTicket(1, usuario.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(45), true, "");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

                    Response.Cookies.Add(cookie);

                    Session["IDUsuario"] = usuario.ID;
                    return RedirectToAction("VisualizaExtrato", "Contas");
                }
                else
                {
                    Session["IDUsuario"] = null;
                    ViewBag.Mensagem = "Usuário ou senha inválidos, verifique e tente novamente!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session["IDConta"] = null;
            Session["IDUsuario"] = null;

            return RedirectToAction("Index", "Login");
        }
    }
}
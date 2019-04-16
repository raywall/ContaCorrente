using ContaCorrente.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContaCorrente.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        #region Contas
        public ActionResult MovimentaConta()
        {
            ViewBag.IDUsuario = Session["IDUsuario"];
            return View();
        }

        [HttpPost]
        public ActionResult MovimentaConta(FormCollection form)
        {
            ViewBag.IDUsuario = Session["IDUsuario"];

            try
            {
                var data = form["data"].ToString().ToDate();

                Models.Conta conta = null;

                if (data != DateTime.MinValue)
                {
                    conta = new Models.Conta()
                    {
                        idConta = form["numeroconta"].ToString().ToLong(),
                        DataCadastro = data,
                        Nome = form["correentista"]
                    };
                }

                if (conta != null)
                {
                    if (new Business.ContaDao().Inserir(conta))
                    {
                        new Business.MovimentoDao().Movimenta(new Models.Movimento()
                        {
                            idConta = conta.idConta,
                            dataMovimento = data,
                            Descricao = "Saldo inicial",
                            Credito = true,
                            valor = form["saldoinicial"].ToString().ToDecimal()
                        });

                        ViewBag.Mensagem = "Nova conta cadastrada!";
                    }
                    else
                        ViewBag.Mensagem = "Ocorreu um erro durante o o cadastro da conta!";
                }
                else
                    ViewBag.Mensagem = "Ocorreu um erro durante o cadastro da conta!";
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return View();
        }
        #endregion

        #region Movimentação e extrato
        public ActionResult VisualizaExtrato(long IDConta = 0)
        {
            if (IDConta > 0)
                Session["IDConta"] = IDConta;
            else if (IDConta == 0 & Session["IDConta"] == null)
                Session["IDConta"] = 12360;

            ViewBag.IDConta = Session["IDConta"].ToString().ToLong();
            ViewBag.IDUsuario = Session["IDUsuario"];

            ViewBag.TiposLancamento = new List<SelectListItem> {
                new SelectListItem {
                    Text="Crédito",
                    Value = "1"
                },
                new SelectListItem {
                    Text="Débito",
                    Value = "2"
            }};

            return View();
        }

        [HttpPost]
        public ActionResult VisualizaExtrato(FormCollection form)
        {
            ViewBag.IDConta = form["IDConta"].ToString().ToLong();
            ViewBag.IDUsuario = Session["IDUsuario"];
            ViewBag.TiposLancamento = new List<SelectListItem> {
                new SelectListItem {
                    Text="Crédito",
                    Value = "1"
                },
                new SelectListItem {
                    Text="Débito",
                    Value = "2"
            }};

            try
            {
                var tipo = (ContaCorrente.Models.Movimento.TipoMovimento)form["TiposLancamento"].ToString().ToInt();
                var data = form["data"].ToString().ToDate();

                Models.Movimento movimento = null;

                if (data != DateTime.MinValue)
                    movimento = new Models.Movimento()
                    {
                        idConta = form["IDConta"].ToString().ToLong(),
                        dataMovimento = data,
                        Descricao = form["descricao"],
                        valor = form["valorlancamento"].ToString().ToDecimal(),
                        Credito = (tipo == Models.Movimento.TipoMovimento.Credito)
                    };

                if (movimento != null)
                {
                    if (new Business.MovimentoDao().Movimenta(movimento))
                        ViewBag.Mensagem = "Novo movimento inserido na conta!";
                    else
                        ViewBag.Mensagem = "Ocorreu um erro durante o lançamento do movimento na conta!";
                }
                else
                    ViewBag.Mensagem = "Ocorreu um erro durante o lançamento do movimento na conta!";
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = ex.Message;
            }

            return View();
        }
        #endregion
    }
}
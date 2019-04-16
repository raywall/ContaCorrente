using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ContaCorrente.Extensions
{
    public static class ExtensionMethods
    {
        private const string uol = @"https://economia.uol.com.br/";

        #region Public methods
        /// <summary>
        /// Retorna o valor em dolar de acordo com a cotação atual
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static decimal DolarComercial(this decimal valor)
        {
            var resultado = valor;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;

                var request = WebRequest.Create(uol);
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = 2000;

                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var content = reader.ReadToEnd();

                content = content.Substring(content.IndexOf(@"</h3></div><a href='https://economia.uol.com.br/cotacoes/cambio/dolar-comercial-estados-unidos/' data-audience-click="));
                content = content.Substring(content.IndexOf("<div id=\"grafico\" data-json="), content.IndexOf(@"class='graficoDolar'") - content.IndexOf("<div id=\"grafico\" data-json="));

                var valores = Regex.Replace(content, @"[^0-9.;]", string.Empty).Replace(";;;", ";").Replace(".", ",").Split(';');
                
                decimal.TryParse(valores.Last(), out resultado);

                reader.Close();
                response.Close();

                return valor / resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cotação do dolar comercial
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static decimal CotacaoDolarComercial(this Models.Conta conta)
        {
            decimal resultado = 0;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;

                var request = WebRequest.Create(uol);
                request.Credentials = CredentialCache.DefaultCredentials;

                var response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream);
                var content = reader.ReadToEnd();

                content = content.Substring(content.IndexOf(@"</h3></div><a href='https://economia.uol.com.br/cotacoes/cambio/dolar-comercial-estados-unidos/' data-audience-click="));
                content = content.Substring(content.IndexOf("<div id=\"grafico\" data-json="), content.IndexOf(@"class='graficoDolar'") - content.IndexOf("<div id=\"grafico\" data-json="));

                var valores = Regex.Replace(content, @"[^0-9.;]", string.Empty).Replace(";;;", ";").Replace(".", ",").Split(';');

                decimal.TryParse(valores.Last(), out resultado);

                reader.Close();
                response.Close();

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Total disponível na conta
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        public static decimal TotalDisponivel(this Models.Conta conta)
        {
            try
            {
                return conta.Creditos().Select(s => s.Valor).Sum() - conta.Debitos().Select(s => s.Valor).Sum();
            }
            catch { }

            return 0;
        }

        /// <summary>
        /// Tenta converter uma string em int
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string valor)
        {
            decimal resultado = 0;
            decimal.TryParse(valor.Replace(".", ","), out resultado);

            return resultado;
        }

        /// <summary>
        /// Tenta converter uma string em long
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static long ToLong(this string valor)
        {
            long resultado = 0;
            long.TryParse(valor, out resultado);

            return resultado;
        }

        /// <summary>
        /// Tenta converter uma string em int
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static int ToInt(this string valor)
        {
            int resultado = 0;
            int.TryParse(valor, out resultado);

            return resultado;
        }

        /// <summary>
        /// Tenta converter uma string para datetime
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string valor)
        {
            DateTime resultado = DateTime.MinValue;
            DateTime.TryParse(valor, out resultado);

            return resultado;
        }

        /// <summary>
        /// Creditos da conta no banco
        /// </summary>
        /// <returns></returns>
        public static List<Models.Movimento> Creditos(this Models.Conta conta)
        {
            return new Business.MovimentoBus().Creditos(conta.IDConta).OrderBy(o => o.DataMovimento).ToList();
        }

        /// <summary>
        /// Debitos da conta no banco
        /// </summary>
        /// <returns></returns>
        public static List<Models.Movimento> Debitos(this Models.Conta conta)
        {
            return new Business.MovimentoBus().Debitos(conta.IDConta).OrderBy(o => o.DataMovimento).ToList();
        }

        /// <summary>
        /// Retorna todos os movimentos da conta no banco
        /// </summary>
        /// <returns></returns>
        public static List<Models.Movimento> Movimentos(this Models.Conta conta)
        {
            return new Business.MovimentoBus().Movimentos(conta.IDConta).OrderBy(o => o.DataMovimento).ToList();
        }
        #endregion
    }
}
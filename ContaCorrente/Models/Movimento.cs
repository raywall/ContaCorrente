using ContaCorrente.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContaCorrente.Models
{
    public class Movimento
    {
        /// <summary>
        /// Tipo de movimento (crédito/débito)
        /// </summary>
        public enum TipoMovimento
        {
            Credito = 1,
            Debito = 2
        }

        /// <summary>
        /// id da movimentação
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// ID da conta
        /// </summary>
        public long idConta { get; set; }

        /// <summary>
        /// Descrição da transação
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data de movimentação do valor
        /// </summary>
        public DateTime dataMovimento { get; set; }

        /// <summary>
        /// Valor movimentado
        /// </summary>
        public decimal valor { get; set; }

        /// <summary>
        /// Tipo de movimento
        /// </summary>
        public bool Credito { get; set; }
    }
}
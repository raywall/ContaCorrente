using System;
using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Models
{
    public class Movimento : IDisposable
    {
        /// <summary>
        /// Implements the IDispose interface
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region IEnumerable declarations
        /// <summary>
        /// Tipo de movimento (crédito/débito)
        /// </summary>
        public enum TipoMovimento
        {
            Credito = 1,
            Debito = 2
        }
        #endregion

        #region Property declarations
        /// <summary>
        /// id da movimentação
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// ID da conta
        /// </summary>
        [Required(ErrorMessage = "Para inserir uma movimentação é necessário informar o número da conta a qual pertence.")]
        public long idConta { get; set; }

        /// <summary>
        /// Descrição da transação
        /// </summary>
        [Required(ErrorMessage = "Descreva a movimentação que esta sendo realizada.")]
        public string Descricao { get; set; }

        /// <summary>
        /// Data de movimentação do valor
        /// </summary>
        public DateTime dataMovimento { get; set; }

        /// <summary>
        /// Valor movimentado
        /// </summary>
        [Required(ErrorMessage = "Informe o valor do movimento a ser realizado.")]
        public decimal valor { get; set; }

        /// <summary>
        /// Tipo de movimento
        /// </summary>
        [Required(ErrorMessage = "Indique se trata-se de uma movimentação de crédito/débito.")]
        public bool Credito { get; set; }
        #endregion
    }
}

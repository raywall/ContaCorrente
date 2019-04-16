using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Models
{
    public partial class Conta : IDisposable
    {
        /// <summary>
        /// Implements the IDispose interface
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Property declarations
        /// <summary>
        /// ID da conta do correntista
        /// </summary>
        [Required(ErrorMessage = "O número da conta precisa ser informado.")]
        public long idConta { get; set; }

        /// <summary>
        /// Data do cadastro
        /// </summary>
        public DateTime DataCadastro { get; set; }

        /// <summary>
        /// Nome do correntista
        /// </summary>
        [Required(ErrorMessage = "Informe o nome do correntista.")]
        public string Nome { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Informa o saldo da conta baseado em sua movimentação
        /// </summary>
        /// <returns></returns>
        public decimal Saldo()
        {
            return 0;
        }
        #endregion
    }
}

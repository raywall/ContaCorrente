using System;
using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Models
{
    public class Usuario : IDisposable
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
        /// ID do usuário no banco
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "Informe o nome de usuário.")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required(ErrorMessage = "Informe a senha do usuário para acesso ao sistema.")]
        public string Password { get; set; }

        /// <summary>
        /// Indica se o usuário está ativo ou inativo no sistema
        /// </summary>
        [Required(ErrorMessage = "Informe se o usuário está ativo/inativo.")]
        public bool Ativo { get; set; }
        #endregion
    }
}

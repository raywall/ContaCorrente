using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContaCorrente.Models
{
    public class Conta
    {
        #region Properties
        /// <summary>
        /// ID da conta do correntista
        /// </summary>
        public long idConta { get; set; }

        /// <summary>
        /// Data do cadastro
        /// </summary>
        public DateTime DataCadastro { get; set; }

        /// <summary>
        /// Nome do correntista
        /// </summary>
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

        /// <summary>
        /// Creditos da conta no banco
        /// </summary>
        /// <returns></returns>
        public List<Movimento> Creditos()
        {
            return new Business.MovimentoDao().Creditos(idConta).OrderBy(o => o.dataMovimento).ToList();
        }

        /// <summary>
        /// Debitos da conta no banco
        /// </summary>
        /// <returns></returns>
        public List<Movimento> Debitos()
        {
            return new Business.MovimentoDao().Debitos(idConta).OrderBy(o => o.dataMovimento).ToList();
        }

        /// <summary>
        /// Retorna todos os movimentos da conta no banco
        /// </summary>
        /// <returns></returns>
        public List<Movimento> Movimentos()
        {
            return new Business.MovimentoDao().Movimentos(idConta).OrderBy(o => o.dataMovimento).ToList();
        }
        #endregion
    }
}
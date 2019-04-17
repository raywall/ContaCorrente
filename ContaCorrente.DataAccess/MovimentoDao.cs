using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ContaCorrente.DataAccess
{
    public class MovimentoDao : Connection
    {
        #region Public methods
        /// <summary>
        /// Lança um crédito/debito em uma conta
        /// </summary>
        /// <param name="movimento"></param>
        /// <returns></returns>
        public bool Movimenta(Models.Movimento movimento)
        {
            try
            {
                return (sqlConn.Execute(@"INSERT INTO [dbo].[Movimentos] (IDConta, DataMovimento, Descricao, Valor, Credito) VALUES (@IDConta, @DataMovimento, @Descricao, @Valor, @Credito)",
                    new
                    {
                        IDConta = movimento.IDConta,
                        DataMovimento = movimento.DataMovimento,
                        Descricao = movimento.Descricao,
                        Valor = movimento.Valor,
                        Credito = movimento.Credito
                    }) == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista os créditos de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public List<Models.Movimento> Creditos(long idConta)
        {
            var retorno = new List<Models.Movimento>();

            try
            {
                retorno = sqlConn.Query<Models.Movimento>(@"SELECT * FROM [dbo].[Movimentos] WHERE Credito = 1 AND IDConta = @IDconta", new { IDConta = idConta }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        /// <summary>
        /// Lista os débitos de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public List<Models.Movimento> Debitos(long idConta)
        {
            var retorno = new List<Models.Movimento>();

            try
            {
                retorno = sqlConn.Query<Models.Movimento>(@"SELECT * FROM [dbo].[Movimentos] WHERE Credito = 0 AND IDConta = @IDConta", new { IDConta = idConta }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        /// <summary>
        /// Listar todos os movimentos de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public List<Models.Movimento> Movimentos(long idConta)
        {
            var retorno = new List<Models.Movimento>();

            try
            {
                retorno = sqlConn.Query<Models.Movimento>("SELECT * FROM [dbo].[Movimentos] WHERE IDConta = @IDConta", new { IDConta = idConta }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
        #endregion
    }
}

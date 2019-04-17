using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace ContaCorrente.DataAccess
{
    public class ContaDao : Connection
    {
        #region Public methods
        /// <summary>
        /// Insere uma nova conta no banco
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        public bool Inserir(Models.Conta conta)
        {
            try
            {
                return (sqlConn.Execute(@"INSERT INTO[dbo].[Contas](IDConta, Nome) VALUES(@IDConta, @Nome)", new { IDConta = conta.IDConta, Nome = conta.Nome }) == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carrega os dados de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public Models.Conta Carregar(long idConta)
        {
            var retorno = new Models.Conta();

            try
            {
                retorno = sqlConn.Query<Models.Conta>(@"SELECT * FROM [dbo].[Contas] WHERE IDConta = @IDConta", new { IDConta = idConta }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }

        /// <summary>
        /// Excluui uma conta do banco
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        public bool Excluir(Models.Conta conta)
        {
            try
            {
                var creditos = new MovimentoDao().Creditos(conta.IDConta);
                var debitos = new MovimentoDao().Debitos(conta.IDConta);

                if (sqlConn.Execute("DELETE [dbo].[Movimentos] WHERE IDConta = @IDConta", new { IDConta = conta.IDConta }) == 1)
                    if (sqlConn.Execute("DELETE [dbo].[Contas] WHERE IDConta = @IDConta", new { IDConta = conta.IDConta }) == 1)
                        return true;
                    else
                    {
                        if (creditos != null && creditos.Count > 0)
                            foreach (var credito in creditos)
                                new MovimentoDao().Movimenta(credito);

                        if (debitos != null && debitos.Count > 0)
                            foreach (var debito in debitos)
                                new MovimentoDao().Movimenta(debito);

                        return false;
                    }
            
                throw new Exception("Não foi possível remover os registros de movimentação da conta!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar contas cadastradas no banco
        /// </summary>
        /// <returns></returns>
        public List<Models.Conta> Listar()
        {
            var retorno = new List<Models.Conta>();

            try
            {
                retorno = sqlConn.Query<Models.Conta>(@"SELECT * FROM [dbo].[Contas]").ToList();
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

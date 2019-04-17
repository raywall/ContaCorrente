using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                sqlConn.Open();
                using (var sqlCommand = new SqlCommand(string.Format(@"INSERT INTO[dbo].[Contas](IDConta, Nome) VALUES({0}, {1})", conta.IDConta, conta.Nome)))
                    return (sqlCommand.ExecuteNonQuery() == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
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
                sqlConn.Open();
                using (var sqlCommand = new SqlCommand(string.Format(@"SELECT * FROM [dbo].[Contas] WHERE IDConta = {0}", idConta), sqlConn))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (sqlReader.HasRows)
                        while (sqlReader.Read())
                            retorno = sqlReader.ConvertModel<Models.Conta>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
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

                sqlConn.Open();

                using (var sqlCommand = new SqlCommand(string.Format("DELETE [dbo].[Movimentos] WHERE IDConta = @IDConta", conta.IDConta), sqlConn))
                    if (sqlCommand.ExecuteNonQuery() == 1)
                        using (var sqlCommandSec = new SqlCommand(string.Format("DELETE [dbo].[Contas] WHERE IDConta = @IDConta", conta.IDConta), sqlConn))
                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                if (creditos != null && creditos.Count > 0)
                                    foreach (var credito in creditos)
                                        new MovimentoDao().Movimenta(credito);

                                if (debitos != null && debitos.Count > 0)
                                    foreach (var debito in debitos)
                                        new MovimentoDao().Movimenta(debito);

                                return true;
                            }
                            else

                                return false;
            
                throw new Exception("Não foi possível remover os registros de movimentação da conta!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
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
                sqlConn.Open();
                using (var sqlCommand = new SqlCommand(@"SELECT * FROM [dbo].[Contas]", sqlConn))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (sqlReader.HasRows)
                        while (sqlReader.Read())
                            retorno.Add(sqlReader.ConvertModel<Models.Conta>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
            }

            return retorno;
        }
        #endregion
    }
}

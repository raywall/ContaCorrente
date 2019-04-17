using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContaCorrente.DataAccess
{
    public class UsuarioDao : Connection
    {
        #region Public methods
        /// <summary>
        /// Insere um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Inserir(Models.Usuario usuario)
        {
            try
            {
                sqlConn.Open();

                var sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[Usuarios] (Username, Password) VALUES (@Username, @Password)", sqlConn);

                sqlCommand.Parameters.Add("@Username", System.Data.SqlDbType.VarBinary).Value = usuario.Username;
                sqlCommand.Parameters.Add("@Password", System.Data.SqlDbType.VarBinary).Value = usuario.Password;

                return (sqlCommand.ExecuteNonQuery() == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// Carrega os dados de um usuário pelo ID
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Models.Usuario Carregar(long idUsuario)
        {
            var retorno = new Models.Usuario();

            try
            {
                sqlConn.Open();

                var sqlCommand = new SqlCommand(@"SELECT * FROM [dbo].[Usuarios] WHERE ID = @IDUsuario", sqlConn);
                sqlCommand.Parameters.Add("@IDUsuario", System.Data.SqlDbType.BigInt).Value = idUsuario;

                SqlDataReader sqlReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (sqlReader.HasRows)
                    while (sqlReader.Read())
                        retorno = sqlReader.ConvertModel<Models.Usuario>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
            }

            return retorno;
        }

        /// <summary>
        /// Carrega os dados de um usuário pelo Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Models.Usuario Carregar(string username)
        {
            var retorno = new Models.Usuario();

            try
            {
                sqlConn.Open();

                var sqlCommand = new SqlCommand(@"SELECT * FROM [dbo].[Usuarios] WHERE Username = @Username", sqlConn);
                sqlCommand.Parameters.Add("@Username", System.Data.SqlDbType.VarChar).Value = username;

                SqlDataReader sqlReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (sqlReader.HasRows)
                    while (sqlReader.Read())
                        retorno = sqlReader.ConvertModel<Models.Usuario>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
            }

            return retorno;
        }

        /// <summary>
        /// Listar contas cadastradas no banco
        /// </summary>
        /// <returns></returns>
        public List<Models.Usuario> Listar()
        {
            var retorno = new List<Models.Usuario>();

            try
            {
                sqlConn.Open();
                using (var sqlCommand = new SqlCommand(@"SELECT * FROM [dbo].[Usuarios]", sqlConn))
                {
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (sqlReader.HasRows)
                        while (sqlReader.Read())
                            retorno.Add(sqlReader.ConvertModel<Models.Usuario>());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
                    sqlConn.Close();
            }

            return retorno;
        }
        #endregion
    }
}

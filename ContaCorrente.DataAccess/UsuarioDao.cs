using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

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
                return (sqlConn.Execute(@"INSERT INTO [dbo].[Usuarios] (Username, Password) VALUES (@Username, @Password)", new { IDConta = usuario.Username, @Password = usuario.Password }) == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carrega os dados de um usuário pelo ID
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Models.Usuario Carregar(long idUsuario)
        {
            try
            {
                return sqlConn.Query<Models.Usuario>(@"SELECT * FROM [dbo].[Usuarios] WHERE ID = @IDUsuario", new { IDUsuario = idUsuario }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carrega os dados de um usuário pelo Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Models.Usuario Carregar(string username)
        {
            try
            {
                return sqlConn.Query<Models.Usuario>(@"SELECT * FROM [dbo].[Usuarios] WHERE Username = @Username", new { Username = username }).FirstOrDefault();
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
        public List<Models.Usuario> Listar()
        {
            try
            {
                return sqlConn.Query<Models.Usuario>(@"SELECT * FROM [dbo].[Usuarios]").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

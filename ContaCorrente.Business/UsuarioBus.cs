using ContaCorrente.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Business
{
    public class UsuarioBus
    {
        #region Private methods
        private UsuarioDao usuarioDao;
        #endregion

        #region Public methods
        public UsuarioBus()
        {
            this.usuarioDao = new UsuarioDao();
        }

        /// <summary>
        /// Insere um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Inserir(Models.Usuario usuario)
        {
            try
            {
                if (usuario == null || string.IsNullOrEmpty(usuario.Username))
                    throw new Exception("Você deve especificar o nome de usuário a ser cadastrado!");

                if (usuario == null || Listar().Where(w => w.Username == usuario.Username).Count() > 0)
                    throw new Exception("Este nome de usuário já existe no sistema!");

                if (usuario == null || string.IsNullOrEmpty(usuario.Password))
                    throw new Exception("Você deve especificar o senha do usuário a ser cadastrado!");

                return usuarioDao.Inserir(usuario);
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
                if (idUsuario == 0)
                    throw new Exception("Você deve especificar o id do usuário a ser consultado!");

                return usuarioDao.Carregar(idUsuario);
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
                if (string.IsNullOrEmpty(username))
                    throw new Exception("Você deve especificar o nome de usuário a ser consultado!");

                return usuarioDao.Carregar(username);
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
                return usuarioDao.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

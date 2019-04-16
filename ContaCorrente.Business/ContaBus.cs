using ContaCorrente.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContaCorrente.Business
{
    public class ContaBus
    {
        #region Private methods
        private ContaDao contaDao;
        #endregion

        #region Public methods
        public ContaBus()
        {
            this.contaDao = new ContaDao();
        }

        /// <summary>
        /// Insere uma nova conta no banco
        /// </summary>
        /// <param name="conta"></param>
        /// <returns></returns>
        public bool Inserir(Models.Conta conta)
        {
            try
            {
                if (conta == null || conta.idConta == 0)
                    throw new Exception("Você deve especificar a conta a ser cadastrada!");

                if (Listar().Where(w => w.idConta == conta.idConta).ToList().Count() > 0)
                    throw new Exception(string.Format("A conta {0} já existe em nosso sistema!", conta.idConta));

                return contaDao.Inserir(conta);
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
            try
            {
                if (idConta == 0)
                    throw new Exception("Você deve especificar a conta a ser consultada!");

                return contaDao.Carregar(idConta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return contaDao.Excluir(conta);
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
            try
            {
                return contaDao.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

﻿using ContaCorrente.DataAccess;
using System;
using System.Collections.Generic;

namespace ContaCorrente.Business
{
    public class MovimentoBus
    {
        #region Private methods
        private MovimentoDao movimentoDao;
        #endregion

        #region Public methods
        public MovimentoBus()
        {
            this.movimentoDao = new MovimentoDao();
        }

        /// <summary>
        /// Lança um crédito/debito em uma conta
        /// </summary>
        /// <param name="movimento"></param>
        /// <returns></returns>
        public bool Movimenta(Models.Movimento movimento)
        {
            try
            {
                if (movimento == null)
                    throw new Exception("Você deve especificar os dados de movimentação a serem registrados!");

                else if (movimento.IDConta == 0)
                    throw new Exception("O número da conta não pode ser igual a zero!");

                else if (movimento.Valor == 0 & Creditos(movimento.IDConta).Count > 0)
                    throw new Exception("Você deve informar o valor do lançamento!");

                return movimentoDao.Movimenta(movimento);
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
            try
            {
                return movimentoDao.Creditos(idConta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista os débitos de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public List<Models.Movimento> Debitos(long idConta)
        {
            try
            {
                return movimentoDao.Debitos(idConta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos os movimentos de uma conta
        /// </summary>
        /// <param name="idConta"></param>
        /// <returns></returns>
        public List<Models.Movimento> Movimentos(long idConta)
        {
            try
            {
                return movimentoDao.Movimentos(idConta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

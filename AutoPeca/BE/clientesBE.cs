using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    class clientesBE
    {
        private VO.Clientes vo;
        private DAO.ClienteDAO dao;

        public clientesBE(VO.Clientes vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.cpf))
            {
                throw new Exception("Nome obrigatorio");
            }

            dao = new DAO.ClienteDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.ClienteDAO(this.vo);
            dao.alterar();
        }
        public VO.Clientes carregar(int id)
        {
            dao = new DAO.ClienteDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.ClienteDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Clientes> listar()
        {
            dao = new DAO.ClienteDAO(this.vo);
            return dao.listar();
        }
    }
}

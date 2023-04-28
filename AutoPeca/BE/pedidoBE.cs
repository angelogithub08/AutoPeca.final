using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class pedidoBE : BaseBE
    {
        private VO.Pedidos vo;
        private DAO.PedidosDAO dao;

        public pedidoBE(VO.Pedidos vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.datetime))
            {
                throw new Exception("Codigo obrigatorio");
            }

            dao = new DAO.PedidosDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.PedidosDAO(this.vo);
            dao.alterar();
        }
        public VO.Pedidos carregar(int id)
        {
            dao = new DAO.PedidosDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.PedidosDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Pedidos> listar()
        {
            dao = new DAO.PedidosDAO(this.vo);
            return dao.listar();
        }
    }
}
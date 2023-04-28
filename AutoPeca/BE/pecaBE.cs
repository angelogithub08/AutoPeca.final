using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.BE
{
    public class pecaBE : BaseBE
    {
        private VO.Pecas vo;
        private DAO.pecasDAO dao;

        public pecaBE(VO.Pecas vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.Descricao))
            {
                throw new Exception("Modelo do veículo Obrigatorio");
            }

            dao = new DAO.pecasDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.pecasDAO(this.vo);
            dao.alterar();
        }
        public VO.Pecas carregar(int id)
        {
            dao = new DAO.pecasDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.pecasDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Pecas> listar()
        {
            dao = new DAO.pecasDAO(this.vo);
            return dao.listar();
        }
    }
}
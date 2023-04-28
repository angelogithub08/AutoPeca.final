using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.BE
{
    public class FabricanteBE : BaseBE
    {
        private VO.Fabricante vo;
        private DAO.FabricantesDAO dao;

        public FabricanteBE(VO.Fabricante vo)
        {
            this.vo = vo;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.vo.nome))
            {
                throw new Exception("Modelo do veículo Obrigatorio");
            }

            dao = new DAO.FabricantesDAO(this.vo);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.FabricantesDAO(this.vo);
            dao.alterar();
        }
        public VO.Fabricante carregar(int id)
        {
            dao = new DAO.FabricantesDAO(this.vo);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.FabricantesDAO(this.vo);
            dao.remover(id);
        }

        public List<VO.Fabricante> listar()
        {
            dao = new DAO.FabricantesDAO(this.vo);
            return dao.listar();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class FabricantesDAO : BaseDAO
    {
        private VO.Fabricante vo;
        public FabricantesDAO(VO.Fabricante vo)
        {
            if (DAO.listaFabricante == null)
            {
                DAO.listaFabricante = new List<VO.Fabricante>();
            }
            this.vo = vo;
        }
            
            public void incluir()
            {
                try
                {
                    string sql = "insert into fabricante (nome,descricao) " +
                        "values (@nome,@desc)";
                    db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                    db.AddParameter("@desc", vo.desc, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }
            public void alterar()
            {
                try
                {
                string sql = "update fabricante set " +
                    "nome = @nome ," +
                    "descricao = @desc " +
                    " where id= @cod" ;
                    db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);
                    db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                    db.AddParameter("@desc", vo.desc, ParameterDirection.Input);       

                db.Execute(sql, CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }
            public void remover(int cod)
            {
                try
                {
                    string sql = $"delete from fabricante where id = @cod";
                    db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);
                    db.Execute(sql, CommandType.Text);

            }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

            public VO.Fabricante carregar(int codigo)
            {
                string sql = $"SELECT codigo,nome,descricao from fabricante where id=@cod";
                db.AddParameter("@cod", codigo, ParameterDirection.Input);
                try
                {
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                        while (dr.Read())
                        {
                            vo = LoadFabricante(dr);
                        }
                        return vo;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

        private VO.Fabricante LoadFabricante(DbDataReader dr)
        {
            vo = new VO.Fabricante();
            vo.codigo = Convert.ToInt32(dr["id"]);
            vo.nome = dr["nome"] != DBNull.Value ? dr["nome"].ToString() : "";
            vo.desc = dr["descricao"] != DBNull.Value ? dr["descricao"].ToString() : "";
            return vo;
            }

            public List<VO.Fabricante> listar()
            {
                try
                {
                    string sql = "SELECT * FROM fabricante;";
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                    var objResultado = new List<VO.Fabricante>();

                    while (dr.Read())
                        {
                            vo = LoadFabricante(dr);
                            objResultado.Add(vo);
                        }
                        return objResultado;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(message: "Erro no Código : " + ex.Message);
                }
            }
    }
}
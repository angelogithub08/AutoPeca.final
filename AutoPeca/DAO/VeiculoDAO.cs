using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class VeiculoDAO : BaseDAO
    {
        private VO.Veiculo vo;
        public VeiculoDAO(VO.Veiculo vo) {

            if (DAO.listaVeiculo == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            this.vo = vo;
        }
       
        public void incluir()
        {
            try
            {
                string sql = "insert into veiculo (modelo,ano,potencia,id_fabricante) " +
                    "values (@Mod,@ano,@mot, @idfab)";
                db.AddParameter("@Mod", vo.modelo, ParameterDirection.Input);
                db.AddParameter("@ano", vo.ano, ParameterDirection.Input);
                db.AddParameter("@mot", vo.potencia, ParameterDirection.Input);
                db.AddParameter("@idfab", vo.fabricante.codigo, ParameterDirection.Input);

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
                string sql = "update veiculo set " +
                    "modelo = @Mod ," +
                    "ano = @ano ," +
                    "potencia = @mot " +  
                    "where id = @id";
                db.AddParameter("@Mod", vo.modelo, ParameterDirection.Input);
                db.AddParameter("@ano", vo.ano, ParameterDirection.Input);
                db.AddParameter("@mot", vo.potencia, ParameterDirection.Input);
                db.AddParameter("@idfab", vo.fabricante, ParameterDirection.Input);
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void remover(int id)
        {
            try
            {
                string sql = $"delete from veiculo where id = @id";
                db.AddParameter("@id", vo.codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
       
        public VO.Veiculo carregar(int id)        {
            string sql = $"SELECT id,modelo,potencia,ano,id_fabricante from veiculo where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadVeiculos(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Veiculo LoadVeiculos(DbDataReader dr)
        {
            vo = new VO.Veiculo();
            vo.codigo = Convert.ToInt32(dr["id"]);
            vo.modelo = dr["modelo"] != DBNull.Value ? dr["modelo"].ToString() : "";
            vo.ano = dr["ano"] != DBNull.Value ? int.Parse(dr["ano"].ToString()) : 0;
            vo.potencia = dr["potencia"] != DBNull.Value ? dr["potencia"].ToString() : "";
            vo.fabricante = new VO.Fabricante();
            vo.fabricante.codigo = Convert.ToInt32(dr["id_fabricante"]);

            return vo;
        }

        public List<VO.Veiculo> listar()
        {
            try
            {
                string sql = "SELECT * FROM veiculo;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Veiculo>();

                    while (dr.Read())
                    {
                        vo  = LoadVeiculos(dr);
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

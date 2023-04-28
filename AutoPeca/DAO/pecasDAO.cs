using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;


namespace AutoPeca.DAO
{
    public class pecasDAO : BaseDAO
    {
        private VO.Pecas vo;
        public pecasDAO(VO.Pecas vo)
        {

            if (DAO.listaPeca == null)
            {
                DAO.listaPeca = new List<VO.Pecas>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into pecas (Codigo,CodigoBarras,descricao) " +
                    "values (@cod,@codB,@desc)";
                db.AddParameter("@cod", vo.Codigo, ParameterDirection.Input);
                db.AddParameter("@codB", vo.CodigoBarras, ParameterDirection.Input);
                db.AddParameter("@desc", vo.Descricao, ParameterDirection.Input);

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
                string sql = "update pecas set " +                    
                    "CodigoBarras = @codB ," +
                    "descricao = @desc " +
                    "where Codigo = @cod";                
                db.AddParameter("@codB", vo.CodigoBarras, ParameterDirection.Input);
                db.AddParameter("@desc", vo.Descricao, ParameterDirection.Input);
                db.AddParameter("@cod", vo.Codigo, ParameterDirection.Input);


                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void remover(int Codigo)
        {
            try
            {
                string sql = $"delete from pecas where Codigo = @cod";
                db.AddParameter("@cod", vo.Codigo, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Pecas carregar(int Codigo)
        {
            string sql = $"SELECT Codigo,CodigoBarras,descricao from Pecas where Codigo=@cod";
            db.AddParameter("@cod", Codigo, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadPecas(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Pecas LoadPecas(DbDataReader dr)
        {
            vo = new VO.Pecas();
            vo.Codigo = Convert.ToInt32(dr["Codigo"]);
            vo.CodigoBarras = dr["CodigoBarras"] != DBNull.Value ? dr["CodigoBarras"].ToString() : "";
            vo.Descricao = dr["descricao"] != DBNull.Value ? (dr["descricao"].ToString()) : "";
            return vo;
        }

        public List<VO.Pecas> listar()
        {
            try
            {
                string sql = "SELECT * FROM Pecas;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Pecas>();

                    while (dr.Read())
                    {
                        vo = LoadPecas(dr);
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

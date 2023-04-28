using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class PedidosDAO : BaseDAO
    {
        private VO.Pedidos vo;
        public PedidosDAO(VO.Pedidos vo)
        {
            if (DAO.listaPedidos == null)
            {
                DAO.listaPedidos = new List<VO.Pedidos>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into pedidos (cod,codigocliente,codigopeca, datatime) " +
                    "values (@cod,@codC,@codP, @dat)";
                db.AddParameter("@cod", vo.cod, ParameterDirection.Input);
                db.AddParameter("@codC", vo.codigodocliente.codigo, ParameterDirection.Input);
                db.AddParameter("@codP", vo.codigodapeca.Codigo, ParameterDirection.Input);
                db.AddParameter("@dat", vo.datetime, ParameterDirection.Input);

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
                string sql = "update pedidos set " +                   
                    "datatime = @dat " +
                    "where cod = @cod";
                db.AddParameter("@cod", vo.cod, ParameterDirection.Input);
                db.AddParameter("@dat", vo.datetime, ParameterDirection.Input);


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
                string sql = $"delete from pedidos where cod = @cod";
                db.AddParameter("@cod", vo.cod, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Pedidos carregar(int Codigo)
        {
            string sql = $"SELECT cod,codigocliente,codigopecas,datatime from pedidos where cod=@cod";
            db.AddParameter("@cod", Codigo, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadPedidos(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Pedidos LoadPedidos(DbDataReader dr)
        {
            vo = new VO.Pedidos();
            vo.cod = Convert.ToInt32(dr["cod"]);
            vo.codigodocliente = new VO.Clientes();
            vo.codigodocliente.codigo = Convert.ToInt32(dr["codigocliente"]);
            vo.codigodapeca = new VO.Pecas();
            vo.codigodapeca.Codigo = Convert.ToInt32(dr["codigopeca"]);
            vo.datetime= dr["datatime"] != DBNull.Value ? (dr["datatime"].ToString()) : "";
            return vo;
        }

        public List<VO.Pedidos> listar()
        {
            try
            {
                string sql = "SELECT * FROM pedidos;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Pedidos>();

                    while (dr.Read())
                    {
                        vo = LoadPedidos(dr);
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
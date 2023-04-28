using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class ClienteDAO : BaseDAO
    {
        private VO.Clientes vo;
        public ClienteDAO(VO.Clientes vo)
        {
            if (DAO.listaCliente == null)
            {
                DAO.listaVeiculo = new List<VO.Veiculo>();
            }
            this.vo = vo;
        }
            
            public void incluir()
            {
                try
                {
                    string sql = "insert into cliente (codigo,nome,cpf,end,num,cidade,estado,pais) " +
                        "values (@cod,@nome,@cpf,@end,@num,@cidade,@estado,@pais)";
                    db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);
                    db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                    db.AddParameter("@cpf", vo.cpf, ParameterDirection.Input);
                    db.AddParameter("@end", vo.end, ParameterDirection.Input);
                    db.AddParameter("@num", vo.num, ParameterDirection.Input);
                    db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                    db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                    db.AddParameter("@pais", vo.pais, ParameterDirection.Input);

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
                    string sql = "update cliente set " +                       
                        "nome = @nome, " +
                        "cpf = @cpf, " +
                        "end = @end, " +
                        "num = @num, " +
                        "cidade = @cidade, " +
                        "estado = @estado, " +
                        "pais = @pais " +
                        "where codigo = @cod";
                    db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                    db.AddParameter("@cpf", vo.cpf, ParameterDirection.Input);
                    db.AddParameter("@end", vo.end, ParameterDirection.Input);
                    db.AddParameter("@num", vo.num, ParameterDirection.Input);
                    db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                    db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                    db.AddParameter("@pais", vo.pais, ParameterDirection.Input);
                    db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);

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
                    string sql = $"delete from cliente where codigo = @cod";
                    db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);
                    db.Execute(sql, CommandType.Text);

            }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

            public VO.Clientes carregar(int codigo)
            {
                string sql = $"SELECT codigo,nome,cpf,end,num,cidade,estado,pais from cliente where codigo=@cod";
                db.AddParameter("@cod", codigo, ParameterDirection.Input);
                try
                {
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                        while (dr.Read())
                        {
                            vo = LoadCliente(dr);
                        }
                        return vo;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

        private VO.Clientes LoadCliente(DbDataReader dr)
        {
            vo = new VO.Clientes();
            vo.codigo = Convert.ToInt32(dr["codigo"]);
            vo.nome = dr["nome"] != DBNull.Value ? dr["nome"].ToString() : "";
            vo.cpf = dr["cpf"] != DBNull.Value ? dr["cpf"].ToString() : "";
            vo.end = dr["end"] != DBNull.Value ? dr["end"].ToString() : "";
            vo.num = dr["num"] != DBNull.Value ? dr["num"].ToString() : "";
            vo.cidade = dr["cidade"] != DBNull.Value ? dr["cidade"].ToString() : "";
            vo.estado = dr["estado"] != DBNull.Value ? dr["estado"].ToString() : "";
            vo.pais = dr["pais"] != DBNull.Value ? dr["pais"].ToString() : "";
            return vo;
            }

            public List<VO.Clientes> listar()
            {
                try
                {
                    string sql = "SELECT * FROM cliente;";
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                    var objResultado = new List<VO.Clientes>();

                    while (dr.Read())
                        {
                            vo = LoadCliente(dr);
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
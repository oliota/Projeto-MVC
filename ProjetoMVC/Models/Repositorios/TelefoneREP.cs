using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoMVC.Models.Repositorios
{
    public class TelefoneREP : Repositorio<Telefone, int>
    {

        ///<summary>Exclui um telefone pela entidade
        ///<param name="entity">Referência de Telefone que será excluído.</param>
        ///</summary>
        ///

        string tabela = "Telefones";
        public override void Delete(Telefone entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE " + tabela + " Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        ///<summary>Exclui um telefone pelo ID
        ///<param name="id">Id do registro que será excluído.</param>
        ///</summary>
        public override void DeleteById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE " + tabela + " Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        ///<summary>Obtém todos os telefones
        ///<returns>Retorna os telefones cadastrados.</returns>
        ///</summary>
        public override List<Telefone> GetAll()
        {
            string sql = "Select " +
                " a.Id, " +
                " a.Numero, " + 
                " b.Nome as Fornecedor " +
                " FROM " + tabela + " a , Fornecedores b " +
                " where b.Id=a.Fornecedor " +
                " ORDER BY b.Nome";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Telefone> list = new List<Telefone>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            list.Add(PreencherDados(reader));
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }
        ///<summary>Preenche os dados de um telefone
        ///<param name="reader">Referencia da tupla obtida na consulta.</param>
        ///<returns>Retorna uma referência de Telefone preenchida com os dados obtidos.</returns>
        ///</summary>
        private Telefone PreencherDados(SqlDataReader reader)
        {
            return new Telefone()
            {
                Id = (int)reader["Id"],
                Fornecedor = reader["Fornecedor"].ToString(),
                Numero = reader["Numero"].ToString()
            };
        }

        ///<summary>Obtém um telefone pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Telefone do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Telefone GetById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select " +
                    " a.Id, " +
                    " a.Numero, " + 
                    " b.Nome as Fornecedor " +
                    " FROM " + tabela + " a , Fornecedores b  " +
                    " WHERE a.Id=@Id " +
                    " and a.Fornecedor=b.Id " +
                    " ORDER BY a.Numero";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                return PreencherDados(reader);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return null;
            }
        }

        ///<summary>Obtém um telefone pelo ID
        ///<param name="nome">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Telefone do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Telefone GetByName(string numero)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select " +
                    " a.Id, " +
                    " a.Numero, " +
                    " b.Nome as Fornecedor  " +
                    " FROM " + tabela + " a , Fornecedores b " +
                    " WHERE a.Numero='@Numero' " +
                    " and a.Fornecedor=b.Id " +
                    " ORDER BY a.Numero ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Numero", numero);
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                return PreencherDados(reader);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return null;
            }
        }

        public override List<Telefone> GetByRef(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select " +
                    " a.Id, " +
                    " a.Numero, " +
                    " b.Nome as Fornecedor " +
                    " FROM " + tabela + " a , Fornecedores b " +
                    " WHERE a.Fornecedor=@Fornecedor " +
                    " and  b.Id=@Fornecedor " +
                    " ORDER BY a.Numero";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Fornecedor", id);
                List<Telefone> list = new List<Telefone>();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(PreencherDados(reader));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }


        ///<summary>Salva um telefone no banco
        ///<param name="entity">Referência de Telefone que será salva.</param>
        ///</summary>
        public override void Save(Telefone entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                FornecedorREP fornecedorREP = new FornecedorREP();
                Fornecedor fornecedor = fornecedorREP.GetByName(entity.Fornecedor);

                string sql = "INSERT INTO " + tabela + "(Fornecedor,Numero ) values(@Fornecedor,@Numero )";
                SqlCommand cmd = new SqlCommand(sql, conn); 
                cmd.Parameters.AddWithValue("@Fornecedor", fornecedor.Id);
                ComplementarParametros(ref cmd, entity);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        ///<summary>Atualiza um telefone no banco
        ///<param name="entity">Referência de Telefone que será atualizada.</param>
        ///</summary>
        public override void Update(Telefone entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE " + tabela + "" +
                    " SET " +
                    " Nome=@Numero " + 
                    " Where Id=@Id ";

                SqlCommand cmd = new SqlCommand(sql, conn); 
                ComplementarParametros(ref cmd, entity);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        ///<summary>Complementa os parametros para executar comandos no banco de dados
        ///<param name="cmd">Acesso direto ao objeto de que receberá os novos parametros.</param>
        ///<param name="entity">Referencia do objeto que possui as informações.</param>
        ///</summary>
        private void ComplementarParametros(ref SqlCommand cmd, Telefone entity)
        {
            cmd.Parameters.AddWithValue("@Numero", entity.Numero); 
        }

    }
}
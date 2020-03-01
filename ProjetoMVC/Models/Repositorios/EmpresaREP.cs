using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoMVC.Models.Repositorios
{
    public class EmpresaREP : Repositorio<Empresa, int>
    {//TODO - refatorar comentarios


        ///<summary>Exclui uma empresa pela entidade
        ///<param name="entity">Referência de Empresa que será excluída.</param>
        ///</summary>
        ///

        string tabela = "Empresas";
        public override void Delete(Empresa entity)
        {
            FornecedorREP fornecedorREP = new FornecedorREP();
            List<Fornecedor> fornecedores = fornecedorREP.GetByRef(entity.Id);

            foreach (var fornecedor in fornecedores) 
                fornecedorREP.Delete(fornecedor); 
             
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

        ///<summary>Exclui uma empresa pelo ID
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

        ///<summary>Obtém todas as empresas
        ///<returns>Retorna as empresas cadastradas.</returns>
        ///</summary>
        public override List<Empresa> GetAll()
        {
            string sql = "Select * FROM " + tabela + " ORDER BY NomeFantasia";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Empresa> list = new List<Empresa>(); 
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

        ///<summary>Preenche os dados de uma empresa
        ///<param name="reader">Referencia da tupla obtida na consulta.</param>
        ///<returns>Retorna uma referência de Empresa preenchida com os dados obtidos.</returns>
        ///</summary>
        private Empresa PreencherDados(SqlDataReader reader)
        {
            return new Empresa() {
                Id = (int)reader["Id"],
                NomeFantasia = reader["NomeFantasia"].ToString(),
                UF = reader["UF"].ToString(),
                CNPJ = reader["CNPJ"].ToString()
            };  
        }
         
        ///<summary>Obtém uma empresa pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de empresa do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Empresa GetById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select * FROM " + tabela + " WHERE Id=@Id";
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


        ///<summary>Obtém uma empresa pelo NomeFantasia
        ///<param name="nome">Nome da empresa que será obtida.</param>
        ///<returns>Retorna uma referência de Empresa do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Empresa GetByName(string nome)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select * FROM " + tabela + " WHERE NomeFantasia=@NomeFantasia";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NomeFantasia", nome); 
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

        public override List<Empresa> GetByRef(int id)
        {
            throw new NotImplementedException();
        }

        ///<summary>Salva a empresa no banco
        ///<param name="entity">Referência de Empresa que será salva.</param>
        ///</summary>
        public override void Save(Empresa entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO " + tabela + " " +
                    " (NomeFantasia,UF,CNPJ ) " +
                    " VALUES " +
                    " (@NomeFantasia,@UF,@CNPJ) ";
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

        ///<summary>Atualiza a empresa no banco
        ///<param name="entity">Referência de Empresa que será atualizada.</param>
        ///</summary>
        public override void Update(Empresa entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE " + tabela +
                    " SET " +
                    " NomeFantasia=@NomeFantasia ," +
                    " UF=@UF ," +
                    " CNPJ=@CNPJ " +
                    " Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
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
        private void ComplementarParametros(ref SqlCommand cmd, Empresa entity)
        {
            cmd.Parameters.AddWithValue("@NomeFantasia", entity.NomeFantasia);
            cmd.Parameters.AddWithValue("@UF", entity.UF);
            cmd.Parameters.AddWithValue("@CNPJ", entity.CNPJ);
        }
    }
}
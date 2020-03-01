using ProjetoMVC.Ultis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoMVC.Models.Repositorios
{
    public class FornecedorREP : Repositorio<Fornecedor, int>
    {

        ///<summary>Exclui um fornecedor pela entidade
        ///<param name="entity">Referência de Fornecedor que será excluída.</param>
        ///</summary>
        ///

        string tabela = "Fornecedores";
        public override void Delete(Fornecedor entity)
        {
            TelefoneREP telefoneREP = new TelefoneREP();
            List<Telefone> telefones = telefoneREP.GetByRef(entity.Id);

            foreach (var telefone in telefones)
                telefoneREP.Delete(telefone);

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

        ///<summary>Exclui um fornecedor pelo ID
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

        ///<summary>Obtém todas os fornecedores
        ///<returns>Retorna os fornecedores cadastrados.</returns>
        ///</summary>
        public override List<Fornecedor> GetAll()
        {
            string sql = "Select " +
                " a.Id, " +
                " a.Nome, " +
                " a.CpfCnpj, " +
                " a.DataCadastro, " +
                " a.RG, " +
                " a.DataNascimento, " +
                " b.NomeFantasia as Empresa " +
                " FROM " + tabela + " a , Empresas b " +
                " where b.Id=a.Empresa " +
                " ORDER BY Nome";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Fornecedor> list = new List<Fornecedor>();
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
        ///<summary>Preenche os dados de um fornecedor
        ///<param name="reader">Referencia da tupla obtida na consulta.</param>
        ///<returns>Retorna uma referência de Fornecedor preenchida com os dados obtidos.</returns>
        ///</summary>
        private Fornecedor PreencherDados(SqlDataReader reader)
        {
            return new Fornecedor()
            {
                Id = (int)reader["Id"],
                Empresa = reader["Empresa"].ToString(),
                Nome = reader["Nome"].ToString(),
                CpfCnpj = reader["CpfCnpj"].ToString(),
                DataHoraCadastro = DateTime.Parse(reader["DataCadastro"].ToString()),
                RG = reader["RG"].ToString(),
                DataNascimento = Validador.converter(reader["DataNascimento"].ToString()),
                Idade = Validador.calcularIdade(Validador.converter(reader["DataNascimento"].ToString()))
            };
        }

        ///<summary>Obtém um fornecedor pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Fornecedor do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Fornecedor GetById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select " +
                    " a.Id, " +
                    " a.Nome, " +
                    " a.CpfCnpj, " +
                    " a.DataCadastro, " +
                    " a.RG, " +
                    " a.DataNascimento, " +
                    " b.NomeFantasia as Empresa " +
                    " FROM " + tabela + " a , Empresas b  " +
                    " WHERE a.Id=@Id " +
                    " and a.Empresa=b.Id " +
                    " ORDER BY a.Nome";
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

        ///<summary>Obtém um fornecedor pelo ID
        ///<param name="nome">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Fornecedor do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Fornecedor GetByName(string nome)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select " +
                    " a.Id, " +
                    " a.Nome, " +
                    " a.CpfCnpj, " +
                    " a.DataCadastro, " +
                    " a.RG, " +
                    " a.DataNascimento, " +
                    " b.NomeFantasia as Empresa  " +
                    " FROM " + tabela + " a , Empresas b " +
                    " WHERE a.Nome=@Nome " +
                    " and a.Empresa=b.Id " +
                    " ORDER BY a.Nome ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", nome);
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

        public override List<Fornecedor> GetByRef(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select a.Id, " +
                    " a.Nome, " +
                    " a.CpfCnpj, " +
                    " a.DataCadastro, " +
                    " a.RG, " +
                    " a.DataNascimento, " +
                    " b.NomeFantasia as Empresa " +
                    " FROM " + tabela + " a , Empresas b " +
                    " WHERE a.Empresa=@Empresa " +
                    " and  b.Id=@Empresa " +
                    " ORDER BY a.Nome";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Empresa", id);
                List<Fornecedor> list = new List<Fornecedor>();
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


        ///<summary>Salva um fornecedor  no banco
        ///<param name="entity">Referência de Fornecedor que será salva.</param>
        ///</summary>
        public override void Save(Fornecedor entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                EmpresaREP empresaREP = new EmpresaREP();
                Empresa empresa = empresaREP.GetByName(entity.Empresa);

                string sql = "INSERT INTO " + tabela +
                    "(Empresa,Nome,CpfCnpj,DataCadastro,RG,DataNascimento ) " +
                    "values(@Empresa," +
                    "@Nome," +
                    "@CpfCnpj," +
                    "CONVERT(datetime, @DataCadastro, 103)," +
                    "@RG," +
                    "CONVERT(datetime, @DataNascimento, 103)" +
                    ")";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                cmd.Parameters.AddWithValue("@Empresa", empresa.Id);
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


        ///<summary>Atualiza um fornecedor no banco
        ///<param name="entity">Referência de Fornecedor que será atualizada.</param>
        ///</summary>
        public override void Update(Fornecedor entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE " + tabela + "" +
                    " SET " +
                    " Nome=@Nome, " +
                    " CpfCnpj=@CpfCnpj " +
                    " Where Id=@Id ";

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
        private void ComplementarParametros(ref SqlCommand cmd, Fornecedor entity)
        {
            cmd.Parameters.AddWithValue("@Nome", entity.Nome);
            cmd.Parameters.AddWithValue("@CpfCnpj", entity.CpfCnpj);
            cmd.Parameters.AddWithValue("@RG", entity.RG);
            cmd.Parameters.AddWithValue("@DataNascimento", entity.DataNascimento);
        }

    }
}
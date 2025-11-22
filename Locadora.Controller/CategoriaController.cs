using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;

namespace Locadora.Controller
{
    public class CategoriaController
    {
        public void AdicionarCategoria(Categoria categoria)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using(SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    var command = connection.CreateCommand();  
                    command.Transaction = transaction;
                    command.CommandText = @"INSERT INTO dbo.tblCategorias (Nome, Descricao, Diaria) 
                                            VALUES (@Nome, @Descricao, @Diaria)
                                            SELECT SCOPE_IDENTITY()";

                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Diaria", categoria.Diaria);
                    int categoriaId = Convert.ToInt32(command.ExecuteScalar());
                    categoria.setCategoriaId(categoriaId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }


        }

        public List<Categoria> ListarCategorias()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Categoria.SELECTALLCATEGORIAS, connection);
                
                SqlDataReader reader = command.ExecuteReader();
                List<Categoria> categorias = new List<Categoria>();
                while (reader.Read())
                {
                    Categoria categoria = new Categoria(
                        reader["Nome"].ToString(),
                        Convert.ToDecimal(reader["Diaria"]),
                        reader["Descricao"].ToString());
                    categoria.setCategoriaId(Convert.ToInt32(reader["CategoriaId"]));
                   
                    categorias.Add(categoria);
                }
                reader.Close();
                return categorias;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar categorias: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        
        public Categoria BuscarCategoriaPorId(int categoriaId)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT CategoriaId, Nome, Diaria, Descricao FROM dbo.tblCategorias WHERE CategoriaId = @CategoriaId;", connection);
                command.Parameters.AddWithValue("@CategoriaId", categoriaId);
                
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Categoria categoria = new Categoria(
                        reader["Nome"].ToString(),
                        Convert.ToDecimal(reader["Diaria"]),
                        reader["Descricao"].ToString());
                    categoria.setCategoriaId(Convert.ToInt32(reader["CategoriaId"]));
                    reader.Close();
                    return categoria;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar categoria por ID: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }   

        public void UpdateCategoria(Categoria categoria)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());


            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE dbo.tblCategorias SET Nome = @Nome, Descricao = @Descricao, Diaria = @Diaria WHERE CategoriaId = @CategoriaId;", connection);
                command.Parameters.AddWithValue("@Nome", categoria.Nome);
                command.Parameters.AddWithValue("@Descricao", categoria.Descricao);
                command.Parameters.AddWithValue("@Diaria", categoria.Diaria);
                command.Parameters.AddWithValue("@CategoriaId", categoria.CategoriaId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar categoria: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteCategoria(int categoriaId)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM dbo.tblCategorias WHERE CategoriaId = @CategoriaId;", connection);
                command.Parameters.AddWithValue("@CategoriaId", categoriaId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar categoria: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}

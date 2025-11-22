using Locadora.Controller.Interfaces;
using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utils.Databases;

namespace Locadora.Controller
{
    public class VeiculoController : IVeiculo
    {
        public void AdicionarVeiculo(Veiculo veiculo)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Veiculo.INSERTVEICULO, connection, transaction);

                    command.Parameters.AddWithValue("@CategoriaId", veiculo.CategoriaId);
                    command.Parameters.AddWithValue("@Placa", veiculo.Placa);
                    command.Parameters.AddWithValue("@Marca", veiculo.Marca);
                    command.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                    command.Parameters.AddWithValue("@Ano", veiculo.Ano);
                    command.Parameters.AddWithValue("@StatusVeiculo", veiculo.StatusVeiculo);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void AtualizarStatusVeiculo(string statusVeiculo, string placa)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            Veiculo veiculo = BuscarVeiculoPorPlaca(placa) ??
                throw new Exception("Veiculo não encontrado para atualizar o status");

            using(SqlTransaction transaction = connection.BeginTransaction())
            try
            {
                SqlCommand command = new SqlCommand(Veiculo.UPDATESTATUSVEICULO, connection);
                    command.Transaction = transaction;

                command.Parameters.AddWithValue("@StatusVeiculo", statusVeiculo);
                command.Parameters.AddWithValue("@VeiculoID", veiculo.VeiculoId);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar status do veículo: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Veiculo> ListarTodosVeiculos()
        {
            var veiculos = new List<Veiculo>();

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            var categoriaController = new CategoriaController();

            using (SqlCommand command = new SqlCommand(Veiculo.SELECTALLVEICULOS, connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Veiculo veiculo = new Veiculo(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetString(5)
                            );
                           
                            veiculo.setNomeCategoria(categoriaController.BuscarCategoriaPorId(veiculo.CategoriaId).Nome);
                            veiculos.Add(veiculo);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao listar veiculos: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return veiculos;
        }

        public Veiculo BuscarVeiculoPorPlaca(string placa)
        {
            var veiculos = new List<Veiculo>();

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            var categoriaController = new CategoriaController();

            Veiculo veiculo = null;

            using (SqlCommand command = new SqlCommand(Veiculo.SELECTVEICULOBYPLACA, connection))
            {
                try
                {
                    command.Parameters.AddWithValue(@"Placa", placa); // tem que passar pq vamos usar placa
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             veiculo = new Veiculo(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6)
                            );
                            veiculo.setVeiculoId(reader.GetInt32(0));

                            veiculo.setNomeCategoria(categoriaController.BuscarCategoriaPorId(veiculo.CategoriaId).Nome);
                            veiculos.Add(veiculo);
                        }                       
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao encontrar veiculos do banco de dados: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao encontrar veiculos do banco de dados: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return veiculo ?? throw new Exception("Veiculo não encontrado");
            }
            
        }

 
        public void DeletarVeiculo(int veiculoId)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using(SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Veiculo.DELETEVEICULO, connection);

                    command.Transaction = transaction;

                    command.Parameters.AddWithValue("@VeiculoID", veiculoId);
              
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar veiculo: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }   
        }

    }
}

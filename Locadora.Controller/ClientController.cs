using Locadora.Models;
using Microsoft.Data.SqlClient;
using System.ClientModel.Primitives;
using Utils.Databases;

namespace Locadora.Controller
{
    public class ClientController
    {
        public void AdicionarCliente(Cliente cliente, Documento documento)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())  //Sqltransaction faz um rollback
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.INSERTCLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("Telefone", cliente.Telefone ?? (object)DBNull.Value);


                    int clienteId = Convert.ToInt32(command.ExecuteScalar());
                    cliente.setClienteId(clienteId);

                    var documentoController = new DocumentoController();

                    documento.setClienteId(clienteId);

                    documentoController.AdicionarDocumento(documento, connection, transaction);

                    transaction.Commit();  //gravar alteração no banco de dados
                }
                catch(Exception ex)
                {
                    transaction.Rollback(); //caso dê erro, desfaz a alteração
                    throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                }
                finally
                {
                     connection.Close();
                }
            }
        }

        //public List<Cliente> ListarClientes()
        //{
        //    var connection = new SqlConnection(ConnectionDB.GetConnectionString());
        //    try
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand(Cliente.SELECTALLCLIENTES, connection);

        //        SqlDataReader reader = command.ExecuteReader();

        //        List<Cliente> clientes = new List<Cliente>();
        //        while (reader.Read())
        //        {
        //            var cliente = new Cliente(reader["Nome"].ToString(),
        //                                        reader["Email"].ToString(),
        //                                        reader["Telefone"] != DBNull.Value ?
        //                                        reader["Telefone"].ToString() : null);
        //            cliente.setClienteId(Convert.ToInt32(reader["ClienteId"]));

        //            var documento = new Documento(reader["TipoDocumento"].ToString(),
        //                                        reader["Numero"].ToString(),
        //                                        DateOnly.FromDateTime(reader.GetDateTime(6)),
        //                                        DateOnly.FromDateTime(reader.GetDateTime(7)));
        //            cliente.setDocumento(documento);

        //            clientes.Add(cliente);
        //        }
        //        return clientes;
        //    }
        //    catch(SqlException ex)
        //    {
        //        throw new Exception("Erro ao listar clientes: " + ex.Message);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("Erro inesperado ao listar clientes: " + ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        public Cliente BuscarClientePorEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.SELECTCLIENTEPOREMAIL, connection);
                command.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var cliente = new Cliente(reader["Nome"].ToString(),
                                                reader["Email"].ToString(),
                                                reader["Telefone"] != DBNull.Value ?
                                                reader["Telefone"].ToString() : null);
                    cliente.setClienteId(Convert.ToInt32(reader["ClienteId"]));
                    return cliente;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao buscar cliente por email: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void AtualizarTelefoneCliente(string telefone, string email)
        {
            //buscar o cliente
            //atualizar a propriedade telefone
            //salvar no banco

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email informado é inválido.");

            var clienteEncontrado = BuscarClientePorEmail(email);
            if (clienteEncontrado is null)
                throw new Exception("Cliente não encontrado para o email: " + email);

            clienteEncontrado.setTelefone(telefone);
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.UPDATETELEFONECLIENTE, connection);
                command.Parameters.AddWithValue("@Telefone", clienteEncontrado.Telefone);
                command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteId);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao atualizar telefone do cliente: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao atualizar telefone do cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AtualizarDocumentoCliente(string email, Documento documento)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email informado é inválido.");

            var clienteEncontrado = BuscarClientePorEmail(email) ??
                    throw new Exception("Cliente não encontrado para o email: " + email);


            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    documento.setClienteId(clienteEncontrado.ClienteId);
                    DocumentoController documentoController = new DocumentoController();

                    documentoController.AtualizarDocumento(documento, connection, transaction);
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Erro ao atualizar documento do cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao atualizar documento do cliente: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeletarCliente(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("O email informado é inválido.");

            var clienteEncontrado = BuscarClientePorEmail(email);

            if (clienteEncontrado is null)
                throw new Exception("Cliente não encontrado para o email: " + email);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection);
                command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteId);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar cliente: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao deletar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

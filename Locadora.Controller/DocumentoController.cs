using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;
using Microsoft.Data.SqlClient;

namespace Locadora.Controller
{
    public class DocumentoController
    {
        public void AdicionarDocumento(Documento documento, SqlConnection connection, SqlTransaction transaction)
        {
            {
                try
                {
                    SqlCommand command = new SqlCommand(Documento.INSERTDOCUMENTO, connection, transaction);

                    command.Parameters.AddWithValue("@ClienteID", documento.ClienteId);
                    command.Parameters.AddWithValue("@TipoDocumento", documento.TipoDocumento);
                    command.Parameters.AddWithValue("@Numero", documento.Numero);
                    command.Parameters.AddWithValue("@DataEmissao", documento.DataEmissao);
                    command.Parameters.AddWithValue("@DataValidade", documento.DataValidade);

                    command.ExecuteNonQuery();
                    /*transaction.Commit();  *///gravar alteração no banco de dados
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); //caso dê erro, desfaz a alteração
                    throw new Exception("Erro ao adicionar documento: " + ex.Message);
                }
            }
        }


        public void AtualizarDocumento(Documento documento, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                SqlCommand command = new SqlCommand(Documento.UPDATEDOCUMENTO, connection, transaction);

                command.Parameters.AddWithValue("@ClienteID", documento.ClienteId);
                command.Parameters.AddWithValue("@TipoDocumento", documento.TipoDocumento);
                command.Parameters.AddWithValue("@Numero", documento.Numero);
                command.Parameters.AddWithValue("@DataEmissao", documento.DataEmissao);
                command.Parameters.AddWithValue("@DataValidade", documento.DataValidade);

                command.ExecuteNonQuery();
     

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar documento: " + ex.Message);
            }
        }

        //public void DeletarCliente(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        throw new Exception("O email informado é inválido.");

        //    var clienteEncontrado = BuscarClientePorEmail(email);

        //    if (clienteEncontrado is null)
        //        throw new Exception("Cliente não encontrado para o email: " + email);

        //    SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
        //    connection.Open();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection);
        //        command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteId);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception("Erro ao deletar cliente: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro inesperado ao deletar cliente: " + ex.Message);
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}
    }
}


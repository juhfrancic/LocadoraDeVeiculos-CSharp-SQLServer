// See https://aka.ms/new-console-template for more information
using Locadora.Controller;
using Locadora.Models;

//Cliente cliente = new Cliente("Novo Cliente 3", "novoemail3@uol.com.br");
//Console.WriteLine(cliente);

var clienteController = new ClientController();

//try
//{
//    clienteController.AdicionarCliente(cliente);
//}
//catch (Exception ex)
//{
//    if(ex.Message.Contains("Violation of UNIQUE KEY")){
//        Console.WriteLine("Não é possível adicionar cliente com email duplicado.");
//    }
//}

//clienteController.ListarClientes();

//try
//{
//    var listaDeClientes = clienteController.ListarClientes();

//    foreach (var clienteLista in listaDeClientes)
//    {
//        Console.WriteLine(clienteLista);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao listar clientes: " + ex.Message);
//}

//try
//{
//    clienteController.AtualizarTelefoneCliente("9999-9999", "novoemail3@uol.com.br");
//    Console.WriteLine("Telefone atualizado com sucesso.");
//    Console.WriteLine(clienteController.BuscarClientePorEmail("novoemail3@uol.com.br"));
//}
//catch(Exception ex)
//{
//    Console.WriteLine("Erro ao atualizar telefone do cliente: " + ex.Message);
//}


try
{
    clienteController.DeletarCliente("novoemail3@uol.com.br");
    Console.WriteLine("Cliente deletado com sucesso.");
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao deletar cliente: " + ex.Message);
}
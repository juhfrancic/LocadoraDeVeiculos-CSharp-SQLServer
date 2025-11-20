// See https://aka.ms/new-console-template for more information
using Locadora.Controller;
using Locadora.Models;

Cliente cliente = new Cliente("Novo cliente 4 agora com documento", "novo4@email.com.br");
Documento documento = new Documento("RG", "123454322", DateOnly.Parse("2020-02-02"), DateOnly.Parse("2030-02-02"));
//Console.WriteLine(cliente);

var clienteController = new ClientController();

//try
//{
//    clienteController.AdicionarCliente(cliente, documento);
//}
//catch (Exception ex)
//{
//    if (ex.Message.Contains("Violation of UNIQUE KEY"))
//    {
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
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao atualizar telefone do cliente: " + ex.Message);
//}


//try
//{
//    clienteController.DeletarCliente("novo4@.com.br");
//    Console.WriteLine("Cliente deletado com sucesso.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao deletar cliente: " + ex.Message);
//}

var novoDocumento = new Documento(
    "RG",
    "123456789",
    new DateOnly(2020, 5, 10),
    new DateOnly(2030, 5, 10)
);

try
{
    clienteController.AtualizarDocumentoCliente("novo1@email.com.br", novoDocumento);
    Console.WriteLine("Documento atualizado com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao atualizar documento do cliente: " + ex.Message);
}
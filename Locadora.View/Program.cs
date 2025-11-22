// See https://aka.ms/new-console-template for more information
using Locadora.Controller;
using Locadora.Models;
using Locadora.Models.Enums;

#region Testes Clientes e Documentos
//Cliente cliente = new Cliente("Novo cliente 4 agora com documento", "novo4@email.com.br");
//Documento documento = new Documento("RG", "123454322", DateOnly.Parse("2020-02-02"), DateOnly.Parse("2030-02-02"));
////Console.WriteLine(cliente);

//var clienteController = new ClientController();

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

//var novoDocumento = new Documento(
//    "RG",
//    "123456789",
//    new DateOnly(2020, 5, 10),
//    new DateOnly(2030, 5, 10)
//);

//try
//{
//    clienteController.AtualizarDocumentoCliente("novo1@email.com.br", novoDocumento);
//    Console.WriteLine("Documento atualizado com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao atualizar documento do cliente: " + ex.Message);
//}
#endregion

#region Testes Categoria
var categoriaController = new CategoriaController();

Categoria categoria = new Categoria("Hatch", 500.00m, "Econômico");
Console.WriteLine(categoria);

//try
//{
//    categoriaController.AdicionarCategoria(categoria);
//    Console.WriteLine("Categoria adicionada com sucesso!");
//}
//catch(Exception ex)
//{
//    Console.WriteLine("Erro ao adicionar categoria" + ex.Message);
//}

//try
//{
//    categoria = categoriaController.BuscarCategoriaPorId(1002);
//    Console.WriteLine("Categoria encontrada com sucesso!");
//}
//catch(Exception ex)
//{
//    Console.WriteLine("Categoria não encontrada: " + ex.Message);
//}

//var categorias = categoriaController.ListarCategorias();

//try
//{
//    foreach(var c in categorias)
//    {
//        Console.WriteLine(c);
//    }
//}
//catch(Exception ex)
//{
//    Console.WriteLine("Erro ao listar categorias: " + ex.Message);
//}

//categoria = new Categoria("Hatch", 600.00m, "Muito econômico");



//try
//{
//    categoriaController.DeleteCategoria(1002);
//    Console.WriteLine("Categoria deletada com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine("Erro ao deletar categoria: " + ex.Message);
//}
#endregion

var veiculoController = new VeiculoController();

//try
//{
//    var veiculo = new Veiculo(3, "123456", "Dodge", "Dodge Ram", 2024, EStatusVeiculo.Disponivel.ToString());
//    veiculoController.AdicionarVeiculo(veiculo);
//    veiculoController.ListarTodosVeiculos();
//}
//catch(Exception ex)
//{
//    Console.WriteLine("Erro: " + ex.Message);
//}

//var veiculos = veiculoController.ListarTodosVeiculos();
//try
//{
//    foreach(var v in veiculos)
//    {
//        Console.WriteLine(v);
//    }

//}
//catch(Exception ex)
//{
//    Console.WriteLine("Erro ao listar veiculos: " + ex.Message);
//}

//Console.WriteLine(veiculoController.BuscarVeiculoPorPlaca("ABC1234"));

//var veiculo = veiculoController.BuscarVeiculoPorPlaca("ABC1234");
//veiculoController.DeletarVeiculo(veiculo.VeiculoId);
//Console.WriteLine("Sucesso ao deletar veiculo!");

veiculoController.AtualizarStatusVeiculo(EStatusVeiculo.Manutencao.ToString(), "DEF5678");
Console.WriteLine(veiculoController.BuscarVeiculoPorPlaca("DEF5678"));
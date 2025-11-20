using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public static readonly string INSERTCATEGORIA = @"INSER INTO dbo.tblCategorias (Nome, Descricao, Diaria) 
                                                        VALUES (@Nome, @Descricao, @Diaria);";

        public static readonly string SELECTALLCATEGORIAS = @"SELECT CategoriaId, Nome, Diaria, Descricao FROM dbo.tblCategorias";
        
        public static readonly string BUSCARCATEGORIAPORID = @"SELECT CategoriaId, Nome, Diaria, Descricao 
                                                            FROM dbo.tblCategorias 
                                                            WHERE CategoriaId = @CategoriaId";

        public static readonly string UPDATECATEGORIA = @"UPDATE dbo.tblCategorias 
                                                        SET Nome = @Nome, 
                                                            Descricao = @Descricao, 
                                                            Diaria = @Diaria 
                                                        WHERE CategoriaId = @CategoriaId";

        public static readonly string DELETECATEGORIA = @"DELETE FROM dbo.tblCategorias 
                                                        WHERE CategoriaId = @CategoriaId";

        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public decimal Diaria { get; private set; }
        public string Descricao { get; private set; }
        public Categoria(string nome, decimal preco, string descricao = "")
        {
            Nome = nome;
            Diaria = preco;
            Descricao = descricao;
        }
        public void setCategoriaId(int categoriaId)
        {
            CategoriaId = categoriaId;
        }
        public override string? ToString()
        {
            return $"CategoriaId: {CategoriaId},\n " +
                   $"NomeCategoria: {Nome},\n"  +
                   $"PrecoDiaria: {Diaria},\n"  +
                   $"Descrição: {Descricao}\n";
        }
    }
}

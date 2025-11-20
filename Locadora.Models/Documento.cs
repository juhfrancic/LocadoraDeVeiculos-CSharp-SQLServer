using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Documento
    {
        public static readonly string INSERTDOCUMENTO = "INSERT INTO dbo.tblDocumentos" +
                                                    "(ClienteID, TipoDocumento, Numero, DataEmissao, DataValidade) " +
                                                    "VALUES (@ClienteID, @TipoDocumento, @Numero, @DataEmissao, @DataValidade)";
        
        public static readonly string UPDATEDOCUMENTO = @"UPDATE tblDocumentos
                                                            SET TipoDocumento = @TipoDocumento,
                                                            Numero = @Numero,
                                                            DataEmissao = @DataEmissao,
                                                            DataValidade = @DataValidade
                                                            WHERE ClienteID = @ClienteID";

        public int DocumentoId { get; private set; }
        public int ClienteId { get; private set; }
        public string TipoDocumento { get; private set; }
        public string Numero { get; private set; }
        public DateOnly DataEmissao { get; private set; }
        public DateOnly DataValidade { get; private set; }

        public Documento(string tipoDocumento, string numero, 
                             DateOnly dataEmissao, DateOnly dataValidade)
        {
            TipoDocumento = tipoDocumento;
            Numero = numero;
            DataEmissao = dataEmissao;
            DataValidade = dataValidade;
        }

        public void setClienteId(int clienteId)
        {
            ClienteId = clienteId;
        }


        public override string? ToString()
        {
            return $"TipoDocumento: {TipoDocumento}," +
                   $"\nNumero: {Numero},\nDataEmissao: {DataEmissao},\nDataValidade: {DataValidade}";
        }
    }
}

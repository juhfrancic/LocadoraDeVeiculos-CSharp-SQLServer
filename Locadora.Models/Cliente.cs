namespace Locadora.Models
{
    public class Cliente
    {
        public readonly static string INSERTCLIENTE = "INSERT INTO dbo.tblClientes (Nome, Email, Telefone) " +
        "VALUES (@Nome, @Email, @Telefone); " +
        "SELECT SCOPE_IDENTITY();";

        public readonly static string SELECTALLCLIENTES = "SELECT * FROM dbo.tblClientes";

        public readonly static string UPDATETELEFONECLIENTE = "UPDATE tblClientes SET Telefone = @Telefone " +
                                                                "WHERE ClienteID = @IdCliente";

        public readonly static string SELECTCLIENTEPOREMAIL = "SELECT * FROM dbo.tblClientes WHERE Email = @Email";

        public int ClienteId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string? Telefone { get; private set; } = String.Empty;

        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public Cliente(string nome, string email, string? telefone) : this(nome, email)
        {
            Telefone = telefone;
        }

        public void setTelefone(string telefone)
        {
            Telefone = telefone;
        }

        public void setClienteId(int id)
        {
            ClienteId = id;
        }
        public override string ToString()
        {
            return $"Nome: {Nome},\nEmail: {Email},\nTelefone: {(Telefone == string.Empty ? "Sem telefone" : Telefone)}\n";
        }
    }
}

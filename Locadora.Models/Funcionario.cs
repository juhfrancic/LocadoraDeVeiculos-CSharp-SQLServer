using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Funcionario
    {
        public int FuncionarioID { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public decimal? Salario { get; private set; }

        public Funcionario(string nome, string cPF, string email)
        {
            Nome = nome;
            CPF = cPF;
            Email = email;
        }

        public Funcionario(string nome, string cPF, string email, decimal? salario) : this(nome, cPF, email)
        {
            Salario = salario;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, CPF: {CPF}, Email: {Email}, Salario: {Salario}";
        }
    }

}

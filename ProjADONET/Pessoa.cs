using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjADONET
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateOnly DataNascimento { get; private set; }

        public List<Telefone> Telefones { get; private set; } = new List<Telefone>();
        public List<Endereco> Enderecos { get; private set; } = new List<Endereco>();

        public Pessoa(string nome, string cpf, DateOnly dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void AddTelefone(Telefone telefone)
        {
            Telefones.Add(telefone);
        }

        public void AddEndereco(Endereco endereco)
        {
            Enderecos.Add(endereco);
        }

        public override string? ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nCpf: {Cpf}\nData de Nascimento: {DataNascimento}";
        }   
    }
}

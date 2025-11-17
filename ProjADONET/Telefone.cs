using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjADONET
{
    public class Telefone
    {
        public int Id { get; private set; }
        public string Ddd { get; private set; }
        public string Numero { get; private set; }
        public string Tipo { get; private set; }
        public int PessoaId { get; private set; }

        public Telefone(string ddd, string numero, string tipo, int pessoaId)
        {
            Ddd = ddd;
            Numero = numero;
            Tipo = tipo;
            PessoaId = pessoaId;
        }

        public void SetId(int id)
        {
            Id = id; 
        }

        public override string? ToString()
        {
            return $"DDD: {Ddd}\nNumero: {Numero}\nTipo: {Tipo}";
        }
    }
}

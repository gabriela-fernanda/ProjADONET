using Microsoft.Data.SqlClient;
using ProjADONET;
using System.Reflection.PortableExecutable;

var connection = new SqlConnection(DBConnection.GetConnectionString());

#region Insert
//var pessoa = new Pessoa("Luan Santana", "15874698541", new DateOnly(1998, 12, 13));

var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, dataNascimento) VALUES (@Nome, @CPF, @DataNascimento)" + 
                      $"SELECT SCOPE_IDENTITY();";

//connection.Open();

var command = new SqlCommand(sqlInsertPessoa, connection);

//command.Parameters.AddWithValue("@Nome", pessoa.Nome);
//command.Parameters.AddWithValue("@CPF", pessoa.Cpf);
//command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

//int pessoaId = Convert.ToInt32(command.ExecuteScalar());

//var telefone = new Telefone("11", "987654321", "Celular", pessoaId);

//var sqlInsertTelefone = $"INSERT INTO Telefones (ddd, numero, tipo, pessoaId) VALUES (@Ddd, @Numero, @Tipo, @PessoaId)";

//command = new SqlCommand(sqlInsertTelefone, connection);
//command.Parameters.AddWithValue("@Ddd", telefone.Ddd);
//command.Parameters.AddWithValue("@Numero", telefone.Numero);
//command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
//command.Parameters.AddWithValue("@PessoaId", telefone.PessoaId);

//command.ExecuteNonQuery();

//var endereco = new Endereco("Rua das Flores", 123, "Apto 45", "Jardim Primavera", "São Paulo", "SP", "01234-567", 2);

//var sqlInsertEndereco = $"INSERT INTO Enderecos (logradouro, numero, complemento, bairro, cidade, estado, cep, pessoaId) " +
//                        $"VALUES (@Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Cep, @PessoaId)";

//command = new SqlCommand(sqlInsertEndereco, connection);
//command.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
//command.Parameters.AddWithValue("@Numero", (object)endereco.Numero ?? DBNull.Value);
//command.Parameters.AddWithValue("@Complemento", (object)endereco.Complemento ?? DBNull.Value);
//command.Parameters.AddWithValue("@Bairro", endereco.Bairro);
//command.Parameters.AddWithValue("@Cidade", endereco.Cidade);
//command.Parameters.AddWithValue("@Estado", endereco.Estado);
//command.Parameters.AddWithValue("@Cep", endereco.Cep);
//command.Parameters.AddWithValue("@PessoaId", endereco.PessoaId);

//command.ExecuteNonQuery();

//connection.Close();
#endregion

#region Select
connection.Open();

var pessoas = new List<Pessoa>();

var sqlSelectPessoas = "SELECT id, nome, cpf, dataNascimento FROM Pessoas";
command = new SqlCommand(sqlSelectPessoas, connection);
var reader = command.ExecuteReader();

while (reader.Read())
{
    var pessoaLida = new Pessoa(
        reader.GetString(1),
        reader.GetString(2),
        DateOnly.FromDateTime(reader.GetDateTime(3))
    );
    pessoaLida.SetId(reader.GetInt32(0));

    pessoas.Add(pessoaLida);
}

reader.Close();

var sqlSelectTelefones = "SELECT id, ddd, numero, tipo, pessoaId FROM Telefones";
command = new SqlCommand(sqlSelectTelefones, connection);
reader = command.ExecuteReader();

while (reader.Read())
{
    var telefoneLido = new Telefone(
        reader.GetString(1),
        reader.GetString(2),
        reader.GetString(3),
        reader.GetInt32(4)
    );

    telefoneLido.SetId(reader.GetInt32(0));

    var pessoa = pessoas.FirstOrDefault(p => p.Id == telefoneLido.PessoaId);
    pessoa.AddTelefone(telefoneLido);
}

reader.Close();

var sqlSelectEnderecos = "SELECT id, logradouro, numero, complemento, bairro, cidade, estado, cep, pessoaId FROM Enderecos";
command = new SqlCommand(sqlSelectEnderecos, connection);
reader = command.ExecuteReader();

while (reader.Read())
{
    var enderecoLido = new Endereco(
        reader.GetString(1),
        reader.IsDBNull(2) ? null : reader.GetInt32(2),
        reader.IsDBNull(3) ? null : reader.GetString(3),
        reader.GetString(4),
        reader.GetString(5),
        reader.GetString(6),
        reader.GetString(7),
        reader.GetInt32(8)
    );
    enderecoLido.SetId(reader.GetInt32(0));
    var pessoa = pessoas.FirstOrDefault(p => p.Id == enderecoLido.PessoaId);
    pessoa.AddEndereco(enderecoLido);
}

connection.Close();

foreach (var p in pessoas)
{
    Console.WriteLine("\n===pessoa==");
    Console.WriteLine(p);

    if (p.Telefones.Count == 0)
    {
        Console.WriteLine("(sem telefones)");
    }
    else
    {
        foreach (var tel in p.Telefones)
        {
            Console.WriteLine();
            Console.WriteLine(tel);
        }
    }

    if (p.Enderecos.Count == 0)
    {
        Console.WriteLine("(sem endereços)");
    }
    else
    {
        foreach (var end in p.Enderecos)
        {
            Console.WriteLine();
            Console.WriteLine(end);
        }
    }
}

#endregion

#region Update

connection.Open();  

var sqlUpdatePessoa = "UPDATE Pessoas SET nome = @Nome WHERE id = @Id";

command = new SqlCommand(sqlUpdatePessoa, connection);
command.Parameters.AddWithValue("@Nome", "Carlos Alberto Santos");
command.Parameters.AddWithValue("@Id", 1);

command.ExecuteNonQuery();

connection.Close();
#endregion

#region Delete

connection.Open();

var sqlDeletePessoa = "DELETE FROM Pessoas WHERE id = @Id";

command = new SqlCommand(sqlDeletePessoa, connection);
command.Parameters.AddWithValue("@Id", 1);

command.ExecuteNonQuery();

connection.Close();
#endregion
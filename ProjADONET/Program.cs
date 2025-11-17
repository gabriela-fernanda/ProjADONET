using Microsoft.Data.SqlClient;
using ProjADONET;

var connection = new SqlConnection(DBConnection.GetConnectionString());

#region Insert
var pessoa = new Pessoa("Luan Santana", "15874698541", new DateOnly(1998, 12, 13));

var sqlInsertPessoa = $"INSERT INTO Pessoas (nome, cpf, dataNascimento) VALUES (@Nome, @CPF, @DataNascimento)" + 
                      $"SELECT SCOPE_IDENTITY();";

connection.Open();

var command = new SqlCommand(sqlInsertPessoa, connection);

command.Parameters.AddWithValue("@Nome", pessoa.Nome);
command.Parameters.AddWithValue("@CPF", pessoa.Cpf);
command.Parameters.AddWithValue("@DataNascimento", pessoa.DataNascimento);

int pessoaId = Convert.ToInt32(command.ExecuteScalar());

var telefone = new Telefone("11", "987654321", "Celular", pessoaId);

var sqlInsertTelefone = $"INSERT INTO Telefones (ddd, numero, tipo, pessoaId) VALUES (@Ddd, @Numero, @Tipo, @PessoaId)";

command = new SqlCommand(sqlInsertTelefone, connection);
command.Parameters.AddWithValue("@Ddd", telefone.Ddd);
command.Parameters.AddWithValue("@Numero", telefone.Numero);
command.Parameters.AddWithValue("@Tipo", telefone.Tipo);
command.Parameters.AddWithValue("@PessoaId", telefone.PessoaId);

command.ExecuteNonQuery();

connection.Close();
#endregion

#region Select
connection.Open();

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

    Console.WriteLine(pessoaLida);
}
reader.Close();
connection.Close();

connection.Open();

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

    Console.WriteLine(telefoneLido);
}

reader.Close();
connection.Close();
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
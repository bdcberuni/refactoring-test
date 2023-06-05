using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class ClientRepository : IClientRepository
    {
        public Client? GetById(int id)
        {
            Client? client = null;
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetClientById"
                };

                var parametr = new SqlParameter("@clientId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parametr);

                connection.Open();

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    client = Client.Create(
                        id: int.Parse(reader["ClientId"].ToString()!),
                        name: reader["Name"].ToString()!);

                    client.ClientStatus = (ClientStatus)int.Parse(reader["ClientStatus"].ToString()!);
                }
            }

            return client;
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace LegacyApp
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDConnectionFactory _dbConnectionFactory;
        private readonly ILogger<ClientRepository> _logger;
        public ClientRepository(IDConnectionFactory dbConnectionFactory, ILogger<ClientRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public Client GetById(int id)
        {
            Client client = null;

            try
            {
                using var connection = _dbConnectionFactory.GetInstance();
                var command = GetCommand(id);
                connection.Open();
                client = Execute(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while fetching client...");
            }
          
            return client;
        }

        private SqlCommand GetCommand(int id)
        {
            var command = new SqlCommand
            {
                Connection = _dbConnectionFactory.GetInstance(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "uspGetClientById"
            };

            var parametr = new SqlParameter("@clientId", SqlDbType.Int) { Value = id };
            command.Parameters.Add(parametr);
            return command;
        }
        private Client Execute(SqlCommand command)
        {
            Client client = null;
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                client = new Client
                {
                    Id = int.Parse(reader["ClientId"].ToString()),
                    Name = reader["Name"].ToString(),
                    ClientStatus = (ClientStatus)int.Parse(reader["ClientStatus"].ToString())
                };
            }

            return client;
        }
    }
}
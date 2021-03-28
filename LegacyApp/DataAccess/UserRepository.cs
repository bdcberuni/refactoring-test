using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace LegacyApp
{
    public class UserRepository : IUserRepository
    {
        private readonly IDConnectionFactory _dbConnectionFactory;
        private readonly ILogger<ClientRepository> _logger;
        public UserRepository(IDConnectionFactory dbConnectionFactory, ILogger<ClientRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public void AddUser(User user)
        { 
            try
            {
                using var connection = _dbConnectionFactory.GetInstance();
                var command = GetCommand(user);
                connection.Open();
                Execute(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while saving user...");
            }
        }

        private SqlCommand GetCommand(User user)
        {
            var command = new SqlCommand
            {
                Connection = _dbConnectionFactory.GetInstance(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "uspAddUser"
            };

            var firstNameParameter = new SqlParameter("@Firstname", SqlDbType.VarChar, 50) { Value = user.Firstname };
            command.Parameters.Add(firstNameParameter);
            var surnameParameter = new SqlParameter("@Surname", SqlDbType.VarChar, 50) { Value = user.Surname };
            command.Parameters.Add(surnameParameter);
            var dateOfBirthParameter = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = user.DateOfBirth };
            command.Parameters.Add(dateOfBirthParameter);
            var emailAddressParameter = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) { Value = user.EmailAddress };
            command.Parameters.Add(emailAddressParameter);
            var hasCreditLimitParameter = new SqlParameter("@HasCreditLimit", SqlDbType.Bit) { Value = user.HasCreditLimit };
            command.Parameters.Add(hasCreditLimitParameter);
            var creditLimitParameter = new SqlParameter("@CreditLimit", SqlDbType.Int) { Value = user.CreditLimit };
            command.Parameters.Add(creditLimitParameter);
            var clientIdParameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = user.Client.Id };
            command.Parameters.Add(clientIdParameter);
            return command;

        }
        private void Execute(SqlCommand command)
        {
         
            command.ExecuteNonQuery();
        }
    }
}
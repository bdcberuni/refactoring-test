using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class DbConnectionFactory : IDConnectionFactory
    {
        private readonly string _connectionString;
        public DbConnectionFactory(string connectionStringKey)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            _connectionString = connectionString;
            
        }
        public SqlConnection GetInstance()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

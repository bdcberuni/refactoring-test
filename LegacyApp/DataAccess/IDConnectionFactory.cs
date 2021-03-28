using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public interface IDConnectionFactory
    {
        SqlConnection GetInstance();
    }
}
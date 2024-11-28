using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _09_Database_Connections;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";

        IDbConnection dbConnection = new SqlConnection(connectionString);

        string sqlCommand = "SELECT GETDATE()";
        DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

        Console.WriteLine(rightNow);
    }
}

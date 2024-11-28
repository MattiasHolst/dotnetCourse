using System.Data;
using System.Globalization;
using _09_Database_Connections.Models;
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

        Computer myComputer = new()
        {
            Motherboard = "Z690",
            HasWifi = true,
            HasLTE = false,
            ReleaseDate = DateTime.Now,
            Price = 943.87m,
            VideoCard = "RTX 2060"
        };

        string sql = @$"INSERT INTO TutorialAppSchema.Computer (
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES (
            '{myComputer.Motherboard}',
            '{myComputer.HasWifi}',
            '{myComputer.HasLTE}',
            '{myComputer.ReleaseDate}',
            '{myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)}',
            '{myComputer.VideoCard}')";

        Console.WriteLine(sql);

        int result = dbConnection.Execute(sql);

        Console.WriteLine(result);

        string sqlSelect = @"
        SELECT  
            Computer.Motherboard,
            Computer.HasWifi,
            Computer.HasLTE,
            Computer.ReleaseDate,
            Computer.Price,
            Computer.VideoCard 
        FROM TutorialAppSchema.Computer";

        IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);

        Console.WriteLine("'Motherboard','HasWifi','HasLTE','ReleaseDate','Price', 'VideoCard'");
        foreach (Computer computer in computers)
        {
            Console.WriteLine("'" + computer.Motherboard
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate
                + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                + "','" + computer.VideoCard + "'");
        }
    }
}

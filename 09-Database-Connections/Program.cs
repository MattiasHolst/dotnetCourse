﻿using System.Data;
using System.Globalization;
using _09_Database_Connections.Data;
using _09_Database_Connections.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _09_Database_Connections;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        DataContextDapper dapper = new(config);
        DataContextEF entityFramework = new(config);

        string sqlCommand = "SELECT GETDATE()";
        DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

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

        entityFramework.Add(myComputer);
        entityFramework.SaveChanges();

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

        //int result = dapper.ExecuteSqlWithRowCount(sql);
        bool result = dapper.ExecuteSql(sql);

        Console.WriteLine(result);

        string sqlSelect = @"
        SELECT  
            Computer.ComputerId,
            Computer.Motherboard,
            Computer.HasWifi,
            Computer.HasLTE,
            Computer.ReleaseDate,
            Computer.Price,
            Computer.VideoCard 
        FROM TutorialAppSchema.Computer";

        IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

        Console.WriteLine("'ComputerId', 'Motherboard','HasWifi','HasLTE','ReleaseDate','Price', 'VideoCard'");
        foreach (Computer computer in computers)
        {
            Console.WriteLine("'" + computer.ComputerId
                + "','" + computer.Motherboard
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate
                + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                + "','" + computer.VideoCard + "'");
        }

        IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

        if (computersEf != null)
        {

            Console.WriteLine("'ComputerId','Motherboard','HasWifi','HasLTE','ReleaseDate','Price', 'VideoCard'");
            foreach (Computer computer in computersEf)
            {
                Console.WriteLine("'" + computer.ComputerId
                    + "','" + computer.Motherboard
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLTE
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + computer.VideoCard + "'");
            }
        }
    }
}

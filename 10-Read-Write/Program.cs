using System.Globalization;
using System.Text.Json;
using _10_Read_Write.Data;
using _10_Read_Write.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace _10_Read_Write;

class Program
{
    static void Main(string[] args)
    {

        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        DataContextDapper dapper = new DataContextDapper(config);

        // string sql = @$"INSERT INTO TutorialAppSchema.Computer (
        //     Motherboard,
        //     HasWifi,
        //     HasLTE,
        //     ReleaseDate,
        //     Price,
        //     VideoCard
        // ) VALUES (
        //     '{myComputer.Motherboard}',
        //     '{myComputer.HasWifi}',
        //     '{myComputer.HasLTE}',
        //     '{myComputer.ReleaseDate}',
        //     '{myComputer.Price.ToString("0.00", CultureInfo.InvariantCulture)}',
        //     '{myComputer.VideoCard}')";

        // File.WriteAllText("log.txt", "\n" + sql + "\n");

        // using StreamWriter openFile = new("log.txt", append: true);

        // openFile.WriteLine("\n" + sql + "\n");

        // openFile.Close();

        string computersJson = File.ReadAllText("Computers.json");

        // Console.WriteLine(computersJson);

        // JsonSerializerOptions options = new()
        // {
        //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        // };

        // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
        
        IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);


        if (computers != null)
        {
            foreach (Computer computer in computers)
            {
                Console.WriteLine(computer.Motherboard);
            }
        }


    }
}

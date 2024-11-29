using System.Globalization;
using System.Text.Json;
using _10_Read_Write.Data;
using _10_Read_Write.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace _10_Read_Write;

class Program
{
    static void Main(string[] args)
    {

        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        DataContextDapper dapper = new DataContextDapper(config);


        // File.WriteAllText("log.txt", "\n" + sql + "\n");

        // using StreamWriter openFile = new("log.txt", append: true);

        // openFile.WriteLine("\n" + sql + "\n");

        // openFile.Close();

        string computersJson = File.ReadAllText("Computers.json");

        // Console.WriteLine(computersJson);

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };



        IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

        IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);


        if (computersNewtonsoft != null)
        {
            foreach (Computer computer in computersNewtonsoft)
            {
                // Console.WriteLine(computer.Motherboard);
                string sql = @$"INSERT INTO TutorialAppSchema.Computer (
                    Motherboard,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES (
                    '{EscapeSingleQuote(computer.Motherboard)}',
                    '{computer.HasWifi}',
                    '{computer.HasLTE}',
                    '{computer.ReleaseDate}',
                    '{computer.Price.ToString("0.00", CultureInfo.InvariantCulture)}',
                    '{EscapeSingleQuote(computer.VideoCard)}')";

                dapper.ExecuteSql(sql);
            }
        }

        JsonSerializerSettings settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);

        File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

        string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

        File.WriteAllText("computersCopySystem.txt", computersCopySystem);



    }

    static string EscapeSingleQuote(string input)
    {
        string output = input.Replace("'", "''");

        return output;
    }
}

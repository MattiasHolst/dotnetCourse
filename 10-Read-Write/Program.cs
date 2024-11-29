using System.Globalization;
using System.Text.Json;
using _10_Read_Write.Data;
using _10_Read_Write.Models;
using AutoMapper;
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

        string computersJson = File.ReadAllText("ComputersSnake.json");

        Mapper mapper = new(new MapperConfiguration((cfg) =>
        {
            cfg.CreateMap<ComputerSnake, Computer>()
            .ForMember(destination =>
            destination.ComputerId, options =>
            options.MapFrom(source => source.computer_id))
            .ForMember(destination =>
            destination.CPUCores, options =>
            options.MapFrom(source => source.cpu_cores))
            .ForMember(destination =>
            destination.HasLTE, options =>
            options.MapFrom(source => source.has_lte))
            .ForMember(destination =>
            destination.HasWifi, options =>
            options.MapFrom(source => source.has_wifi))
            .ForMember(destination =>
            destination.Motherboard, options =>
            options.MapFrom(source => source.motherboard))
            .ForMember(destination =>
            destination.VideoCard, options =>
            options.MapFrom(source => source.video_card))
            .ForMember(destination =>
            destination.ReleaseDate, options =>
            options.MapFrom(source => source.release_date))
            .ForMember(destination =>
            destination.Price, options =>
            options.MapFrom(source => source.price));
        }));

        // Console.WriteLine(computersJson);

        IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

        if (computersSystem != null)
        {
            IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);

            foreach (Computer computer in computerResult)
            {
                Console.WriteLine(computer.Motherboard);
            }
        }


        // IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);



        // if (computersNewtonsoft != null)
        // {
        //     foreach (Computer computer in computersNewtonsoft)
        //     {
        //         // Console.WriteLine(computer.Motherboard);
        //         string sql = @$"INSERT INTO TutorialAppSchema.Computer (
        //             Motherboard,
        //             HasWifi,
        //             HasLTE,
        //             ReleaseDate,
        //             Price,
        //             VideoCard
        //         ) VALUES (
        //             '{EscapeSingleQuote(computer.Motherboard)}',
        //             '{computer.HasWifi}',
        //             '{computer.HasLTE}',
        //             '{computer.ReleaseDate}',
        //             '{computer.Price.ToString("0.00", CultureInfo.InvariantCulture)}',
        //             '{EscapeSingleQuote(computer.VideoCard)}')";

        //         dapper.ExecuteSql(sql);
        //     }
        // }

        // JsonSerializerSettings settings = new()
        // {
        //     ContractResolver = new CamelCasePropertyNamesContractResolver()
        // };

        // string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings);

        // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

        // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

        // File.WriteAllText("computersCopySystem.txt", computersCopySystem);



    }

    static string EscapeSingleQuote(string input)
    {
        string output = input.Replace("'", "''");

        return output;
    }
}

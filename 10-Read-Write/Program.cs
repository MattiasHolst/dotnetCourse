using System.Globalization;
using _10_Read_Write.Models;

namespace _10_Read_Write;

class Program
{
    static void Main(string[] args)
    {

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

        
    }
}

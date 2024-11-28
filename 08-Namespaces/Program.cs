using _08_Namespaces.Models;

namespace _08_Namespaces;

class Program
{
    static void Main(string[] args)
    {
        Computer myComputer = new(){
            Motherboard = "Z690",
            HasWifi = true,
            HasLTE = false,
            ReleaseDate = DateTime.Now,
            Price = 943.87m,
            VideoCard = "RTX 2060"
        };
        Console.WriteLine(myComputer.Motherboard);
        Console.WriteLine(myComputer.HasWifi);
        Console.WriteLine(myComputer.ReleaseDate);
        Console.WriteLine(myComputer.VideoCard);
    }
}

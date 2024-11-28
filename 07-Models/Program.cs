namespace _07_Models;

public class Computer
{
    public string Motherboard { get; set; }
    public int CPUCores { get; set; }
    public bool HasWifi { get; set; }
    public bool HasLTE { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public string VideoCard { get; set; }

    public Computer()
    {
        VideoCard ??= "";
        Motherboard ??= "";
    }
}

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
            CPUCores = 1,
            VideoCard = "RTX 2060"
        };
        myComputer.HasWifi = false;

        Console.WriteLine(myComputer.Motherboard);
        Console.WriteLine(myComputer.HasWifi);
        Console.WriteLine(myComputer.ReleaseDate);
        Console.WriteLine(myComputer.VideoCard);

    }
}

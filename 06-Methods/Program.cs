
internal class Program
{
    public static void Main(string[] args)
    {


        int[] intsToCompress = [10, 15, 20, 25, 30, 12, 34];

        DateTime startTime = DateTime.Now;

        int totalValue = 0;


        totalValue = intsToCompress.Sum();
        Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
        Console.WriteLine(totalValue);

        startTime = DateTime.Now;

        totalValue = 0;
        totalValue = GetSum(intsToCompress);
        Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
        Console.WriteLine(totalValue);


    }



    static private int GetSum(int[] intsToCompress)
    {
        int totalValue = 0;
        foreach (int intForCompression in intsToCompress)
        {
            totalValue += intForCompression;
        }
        return totalValue;
    }
}


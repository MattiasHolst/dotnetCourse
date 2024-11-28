
int[] intsToCompress = [10, 15, 20, 25, 30, 12, 34];

DateTime startTime = DateTime.Now;

int totalValue = intsToCompress[0] + intsToCompress[1]
+ intsToCompress[2] + intsToCompress[3] + intsToCompress[4]
+ intsToCompress[5] + intsToCompress[6];

Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

Console.WriteLine(totalValue);

//146
totalValue = 0;
startTime = DateTime.Now;
for (int i = 0; i < intsToCompress.Length; i++)
{
    totalValue += intsToCompress[i];
}

Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
Console.WriteLine(totalValue);

totalValue = 0;
startTime = DateTime.Now;
foreach (int intForCompression in intsToCompress)
{
    totalValue += intForCompression;
}
Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
Console.WriteLine(totalValue);

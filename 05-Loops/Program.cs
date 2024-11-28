
using System.Globalization;

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

int index = 0;
totalValue = 0;
startTime = DateTime.Now;
while (index < intsToCompress.Length)
{
    totalValue += intsToCompress[index];
    index++;
}

Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
Console.WriteLine(totalValue);

index = 0;
totalValue = 0;
startTime = DateTime.Now;
do
{
    totalValue += intsToCompress[index];
    index++;
} while (index < intsToCompress.Length);

Console.WriteLine((DateTime.Now - startTime).TotalSeconds);
Console.WriteLine(totalValue);

totalValue = 0;
foreach (int intForCompression in intsToCompress)
{
    if (intForCompression > 20)
    {

        totalValue += intForCompression;
    }
}
Console.WriteLine(totalValue);
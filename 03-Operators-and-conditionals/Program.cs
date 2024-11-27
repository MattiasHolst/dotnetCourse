// See https://aka.ms/new-console-template for more information
int myInt = 5;
int mySecondInt = 10;

Console.WriteLine(myInt);

myInt++;
Console.WriteLine(myInt);

myInt += 7;
Console.WriteLine(myInt);

myInt -= 8;
Console.WriteLine(myInt);

Console.WriteLine(myInt * mySecondInt);
Console.WriteLine(mySecondInt / myInt);
Console.WriteLine(mySecondInt + myInt);
Console.WriteLine(myInt - mySecondInt);

Console.WriteLine(5 + 5 * 10);
Console.WriteLine((5 + 5) * 10);

Console.WriteLine(Math.Pow(5, 4));
Console.WriteLine(Math.Sqrt(25));

string myString = "test";

Console.WriteLine(myString);

myString += ". second part";

Console.WriteLine(myString);


myString = myString + ". \"third\\ part";

Console.WriteLine(myString);

string[] myStringArray = myString.Split(". ");

Console.WriteLine(myStringArray[0]);
Console.WriteLine(myStringArray[1]);
Console.WriteLine(myStringArray[2]);

Console.WriteLine(myInt.Equals(mySecondInt));
Console.WriteLine(myInt.Equals(mySecondInt / 2));

Console.WriteLine(myInt != mySecondInt);
Console.WriteLine(myInt == mySecondInt / 2);

Console.WriteLine(myInt >= mySecondInt);
Console.WriteLine(myInt > mySecondInt);
Console.WriteLine(myInt <= mySecondInt);
Console.WriteLine(myInt < mySecondInt);

Console.WriteLine(5 < 10 && 5 > 10);
Console.WriteLine(5 < 10 || 5 > 10);



string[] myGroceryArray = ["Cheese", "Milk"];
//string[] myGroceryArray = new string[2];

myGroceryArray[1] = "Yoghurt";

Console.WriteLine(myGroceryArray[0]);
Console.WriteLine(myGroceryArray[1]);
// Console.WriteLine(myGroceryArray[2]); Index out of bounds

// List<string> myGroceryList = new List<string>();
List<string> myGroceryList = ["Ice Cream", "Coffee"];

// myGroceryList.Add("Ice Cream");
// myGroceryList.Add("Coffee");

Console.WriteLine(myGroceryList[0]);
Console.WriteLine(myGroceryList[1]);
//Console.WriteLine(myGroceryList[2]); Index out of range

IEnumerable<string> myGroceryEnumerable = myGroceryList;

List<string> mySecondGroceryList = myGroceryEnumerable.ToList();

int[,] myMultiDimensionalArray = {
    {1, 2}, //0 
    {3, 4}, //1
    {5, 6}  //2
};

Console.WriteLine(myMultiDimensionalArray[0, 0]);
Console.WriteLine(myMultiDimensionalArray[2, 1]);



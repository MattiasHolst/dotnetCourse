namespace _11_Async_Await;

class Program
{
    static async Task Main(string[] args)
    {
        Task firstTask = new(() =>
        {
            Thread.Sleep(100);
            Console.WriteLine("Task 1");
        });
        firstTask.Start();
        await firstTask;
        Console.WriteLine("After the Task was created");
    }
}

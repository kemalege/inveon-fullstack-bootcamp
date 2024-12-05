namespace InveonConsoleApp.AsyncSync;

public class AsyncSyncMethods
{
    public int CalculateSum(int a, int b)
    {
        return a + b;
    }

    public async Task<int> CalculateSumAsync(int a, int b)
    {
        return await Task.Run(() => a + b);
    }

    public async Task PerformHeavyCalculationAsync()
    {
        int result = await Task.Run(() =>
        {
            int sum = 0;
            for (int i = 0; i < 1000000; i++)
            {
                sum += i;
            }

            return sum;
        });

        Console.WriteLine($"Calculation result: {result}");
    }

    public void LongRunningSyncOperation()
    {
        Console.WriteLine("Synchronous operation started...");
        Thread.Sleep(5000);
        Console.WriteLine("Synchronous operation completed!");
    }

    public async Task LongRunningAsyncOperation()
    {
        Console.WriteLine("Asynchronous operation started...");
        await Task.Delay(5000);
        Console.WriteLine("Asynchronous operation completed!");
    }

    // Static Task Methods
    public async Task FetchDataFromApisAsync()
    {
        Task<string> api1 = Task.Run(() => "API 1 Result");
        Task<string> api2 = Task.Run(() => "API 2 Result");

        string[] results = await Task.WhenAll(api1, api2);

        Console.WriteLine($"Result from API 1: {results[0]}");
        Console.WriteLine($"Result from API 2: {results[1]}");
    }
    
    
    public async Task FetchFastestApiAsync()
    {
        Task<string> api1 = Task.Run(async () =>
        {
            await Task.Delay(3000);
            return "API 1 Result";
        });

        Task<string> api2 = Task.Run(async () =>
        {
            await Task.Delay(1000);
            return "API 2 Result";
        });

        Task<string> firstCompletedTask = await Task.WhenAny(api1, api2);

        Console.WriteLine($"First completed API result: {await firstCompletedTask}");
    }

    public Task<string> GetMockDataAsync()
    {
        return Task.FromResult("Mock Data");
    }

    public Task PerformNoOperationAsync()
    {
        return Task.CompletedTask;
    }

    public async Task IntensiveOperation()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Processing step {i}");
            await Task.Yield();
        }
    }

    public Task RunTaskSynchronously()
    {
        var task = Task.Run(() => Console.WriteLine("Running task"));
        return task;
    }

    public async Task WaitForTaskCompletionAsync()
    {
        await Task.Delay(1000);
        Console.WriteLine("Task completed!");
    }

    public Task<int> SimulateExceptionAsync()
    {
        return Task.FromException<int>(new InvalidOperationException("Simulated error"));
    }

    public async Task ConfigureAwaitExampleAsync()
    {
        await Task.Delay(1000).ConfigureAwait(false);
    }

    public Task<int> SimulateCanceledTaskAsync()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();
        return Task.FromCanceled<int>(cts.Token);
    }

}
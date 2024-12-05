namespace InveonConsoleApp.AsyncSync;

public class AsyncAwaitExample
{
    public async Task<string> FetchDataFromApiAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode(); 
            
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }
    }
}
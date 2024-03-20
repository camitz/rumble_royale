namespace LinqSample;

public class AsyncLinqDemo
{
    int[] ids = Enumerable.Range(1, 10).ToArray();
    
    public async Task<List<string>> GetCharacters()
    {
        List<string> characters = new List<string>();
        foreach (var number in ids)
        {
            string url = $"https://api.sampleapis.com/futurama/characters/{number}";
            string response = await new HttpClient().GetStringAsync(url);
            characters.Add(response);
        }
        return characters;
    }
}

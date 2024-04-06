namespace LinqSample;

public class AsyncLinqDemo
{
    private readonly int[] _ids = Enumerable.Range(1, 10).ToArray();
    
    public async Task<List<string>> GetCharacters()
    {
        var characters = new List<string>();
        var client = new HttpClient();

        async IAsyncEnumerable<string> DefineAsyncEnumerable()
        {
            foreach (var id in _ids)
            {
                var response = await client.GetStringAsync($"https://api.sampleapis.com/futurama/characters/{id}");
                yield return response;
            }
        }

        var asyncEnumerable = DefineAsyncEnumerable();
        await foreach (var character in asyncEnumerable)
        {
            characters.Add(character);
        }

        return characters;

    }
}

namespace LinqSample;

public class ParallelOperationsDemo
{
    public async Task<List<int>> GetCharacters()
    {
        var characters = new List<int>();

        async IAsyncEnumerable<int> DefineAsyncEnumerable()
        {
            for (int i = 0; i < 10_000_000; i++)
            {
                var t = await Task.Run(() => 1 + 1);
                yield return t;
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

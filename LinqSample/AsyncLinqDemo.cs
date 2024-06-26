﻿namespace LinqSample;

public class AsyncLinqDemo
{
    public async Task<List<int>> GetCharacters()
    {
        var characters = new List<int>();

        async IAsyncEnumerable<int> DefineAsyncEnumerable()
        {
            for (int i = 0; i < 100_000_00; i++)
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

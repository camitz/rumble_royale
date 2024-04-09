
using LinqSample;

var stopwatch = System.Diagnostics.Stopwatch.StartNew();
var rs =  await new AsyncLinqDemo().GetCharacters();
stopwatch.Stop();
Console.WriteLine(stopwatch.Elapsed);
Console.WriteLine(rs.Sum());
return;

var knuth = "Programming is the art of telling another human being what one wants the computer to do.";

var knuthArray = knuth.Split(' ');

Console.WriteLine(knuthArray.Average(x => x.Length));

knuthArray
    .OrderBy(x => x, StringComparer.CurrentCulture)
    .ToList()
    .ForEach(Console.WriteLine);

try
{
    knuthArray
        .Where(x => x.Length > 3)
        .OrderBy(x => x.Length)
        .SelectMany(x => x.ToCharArray())
        .ToList()
        .ForEach(Console.Write);
}
catch (NullReferenceException e)
{
}

Console.WriteLine();
Console.WriteLine();

    (
        from x in knuthArray
        where x.Length > 3
        orderby x.Length 
        
        from y in x
        select x.ToCharArray()
    )
    .ToList()
    .ForEach(Console.Write);

Console.WriteLine();
Console.WriteLine();

public static class StatsExtensions
{
    public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
        var enumerable = source as TSource[] ?? source.ToArray();
        var avg = enumerable.Average(selector);
        return enumerable.Sum(x => Math.Pow(selector(x) - avg, 2)) / enumerable.Length;
    }
}


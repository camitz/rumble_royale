var knuth = "Programming is the art of telling another human being what one wants the computer to do.";

var knuthArray = knuth.Split(' ');

Console.WriteLine(knuthArray.Average(x => x.Length));
Console.WriteLine(knuthArray.Variance(x => x.Length));

knuthArray
    .OrderBy(x => x.Length)
    .ToList()
    .ForEach(Console.WriteLine);

knuthArray
    .Where(x => x.Length > 3)
    .OrderByDescending(x => x.Length)
    .SelectMany(x => x.ToCharArray())
    .ToList()
    .ForEach(Console.WriteLine);

(
        from x in knuthArray
        where x.Length > 3
        orderby x.Length descending

        from y in x
        select x.ToCharArray()
    )
    .ToList()
    .ForEach(Console.WriteLine);

public static class StatsExtensions
{
    public static double Variance<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
        var enumerable = source as TSource[] ?? source.ToArray();
        var avg = enumerable.Average(selector);
        return enumerable.Sum(x => Math.Pow(selector(x) - avg, 2)) / enumerable.Length;
    }
}

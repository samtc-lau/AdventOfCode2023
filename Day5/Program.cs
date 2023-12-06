//using var streamReader = new StreamReader("input.txt");
//var output = 0L;
//var input = streamReader.ReadLine();

//var seeds = input.Split(':')[1].Trim().Split(' ').Select(long.Parse);
//input = streamReader.ReadLine();

//while (input is not null)
//{
//    if (input == "")
//    {
//        input = streamReader.ReadLine();
//        continue;
//    }

//    if (input.Contains("map"))
//    {
//        // Finalize mapping
//        seeds = seeds.Select(Math.Abs);
//        Console.WriteLine("Finalize mapping:");
//        Console.WriteLine(string.Join(" ", seeds));
//        input = streamReader.ReadLine();
//        continue;
//    }

//    var transform = GetTransform(input);
//    seeds = seeds.Select(s =>
//    {
//        if (s >= transform.SourceStart && s <= transform.SourceEnd)
//        {
//            // Mark transformed as negative number
//            return -(s + transform.Offset);
//        }
//        return s;
//    });

//    Console.WriteLine(string.Join(" ", seeds));

//    input = streamReader.ReadLine();
//}

//// Finalize mapping
//seeds = seeds.Select(Math.Abs);
//output = seeds.Min();
//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0L;
var input = streamReader.ReadLine();

var seeds = input.Split(':')[1].Trim().Split(' ').Select(long.Parse);
var seedsRanges = new List<SeedsRange>();
foreach (var i in Enumerable.Range(0, seeds.Count()))
{
    if (i % 2 == 0)
    {
        var start = seeds.ElementAt(i);
        var range = seeds.ElementAt(i + 1);
        seedsRanges.Add(new SeedsRange(start, start + range - 1));
    }
}
input = streamReader.ReadLine();

while (input is not null)
{
    if (input == "")
    {
        input = streamReader.ReadLine();
        continue;
    }

    if (input.Contains("map"))
    {
        // Finalize mapping
        seedsRanges = seedsRanges.Select(s => new SeedsRange(Math.Abs(s.Start), Math.Abs(s.End))).ToList();
        Console.WriteLine("Finalize mapping:");
        Console.WriteLine(string.Join(" ", seedsRanges));
        input = streamReader.ReadLine();
        continue;
    }

    var transform = GetTransform(input);
    var seedsRangesCount = seedsRanges.Count;

    for (var i = 0; i < seedsRangesCount; i++)
    {
        var seedsRange = seedsRanges[i];
        if (seedsRange.Start > transform.SourceEnd || seedsRange.End < transform.SourceStart)
        {
            // No intersact
            continue;
        }

        if (seedsRange.Start < transform.SourceStart)
        {
            // Non-intersact left
            var newSeedsRangeLeft = new SeedsRange(seedsRange.Start, transform.SourceStart - 1);
            seedsRanges.Add(newSeedsRangeLeft);
            seedsRangesCount++;
        }

        if (seedsRange.End > transform.SourceEnd)
        {
            // Non-intersact right
            var newSeedsRangeRight = new SeedsRange(transform.SourceEnd + 1, seedsRange.End);
            seedsRanges.Add(newSeedsRangeRight);
            seedsRangesCount++;
        }

        // Transform intersact and mark as negative number
        seedsRanges[i] = new SeedsRange(
            -(Math.Max(seedsRange.Start, transform.SourceStart) + transform.Offset),
            -(Math.Min(seedsRange.End, transform.SourceEnd) + transform.Offset));
    }

    Console.WriteLine(string.Join(" ", seedsRanges));

    input = streamReader.ReadLine();
}

// Finalize mapping
seedsRanges = seedsRanges.Select(s => new SeedsRange(Math.Abs(s.Start), Math.Abs(s.End))).ToList();
Console.WriteLine("Finalize mapping:");
Console.WriteLine(string.Join(" ", seedsRanges));

output = seedsRanges.Min(s => s.Start);
Console.WriteLine($"Part Two Answer: {output}");

Transform GetTransform (string input)
{
    var source = long.Parse(input.Split(' ')[1]);
    var target = long.Parse(input.Split(' ')[0]);
    var range = long.Parse(input.Split(' ')[2]);

    return new Transform
    {
        SourceStart = source,
        SourceEnd = source + range - 1,
        Offset = target - source
    };
}

record struct Transform (long SourceStart, long SourceEnd, long Offset);

record struct SeedsRange (long Start, long End);
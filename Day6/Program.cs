//using var streamReader = new StreamReader("input.txt");
//var output = 1;
//var times = streamReader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
//var distances = streamReader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
//IEnumerable<(int time, int distance)> records = times.Zip(distances, (t, d) => (t, d));

//foreach (var record in records)
//{
//    var waysToWin = GetWaysToWin(record.time, record.distance);
//    output *= waysToWin;
//    Console.WriteLine(waysToWin);
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var time = int.Parse(streamReader.ReadLine().Replace(" ", "").Split(':').Last());
var distance = long.Parse(streamReader.ReadLine().Replace(" ", "").Split(':').Last());
var output = GetWaysToWin(time, distance);

Console.WriteLine($"Part Two Answer: {output}");

long GetDistance(int totalTime, int holdTime)
{
    return (totalTime - holdTime) * (long)holdTime;
}

int GetWaysToWin(int totalTime, long distanceToBeat)
{
    // Assume all holding are win and deduct from here
    var waysToWin = totalTime + 1;

    // holding n ms is the same as holding (total time - n) ms, so only loop for the first half cases
    foreach(var i in Enumerable.Range(0, totalTime / 2 + 1))
    {
        if (GetDistance(totalTime, i) > distanceToBeat)
        {
            // win
            break;
        }

        // lose: reduce 2 counting the n and (total time - n) case
        waysToWin -= 2;
    }

    if (waysToWin < 0)
    {
        waysToWin = 0;
    }

    return waysToWin;
}
//using var streamReader = new StreamReader("input.txt");
//var output = 1L;
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

long GetWaysToWin(long totalTime, long distanceToBeat)
{
    // Solving the quadratic equation get the 2 possible hold time for the distanceToBeat
    // Since, (totalTime - holdTime) * holdTime = distance
    // Hence, holdTime ^ 2 - totalTime * holdTime + distance = 0 (a = 1, b = -totalTime, c = distance)
    (var holdTimeX, var holdTimeY) = GetQuadraticSolutions(1, -totalTime, distanceToBeat);

    // Time between the 2 possible hold time are ways to win (travel longer than distanceToBeat)
    return (long)Math.Floor(holdTimeX) - (long)Math.Floor(holdTimeY);
}

(double x, double y) GetQuadraticSolutions(long a, long b, long c)
{
    return ((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a), (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
}
//using var streamReader = new StreamReader("input.txt");
//var output = 0L;
//var input = streamReader.ReadLine();

//var x = 0;
//var galaxies = new List<Coordinate>();
//var emptyColumns = Enumerable.Range(0, input.Length).ToList();
//var emptyRows = new List<int>();

//while (!string.IsNullOrEmpty(input))
//{
//    var isEmptyRow = true;
//    foreach (var y in Enumerable.Range(0, input.Length))
//    {
//        if (input[y] == '#')
//        {
//            emptyColumns.Remove(y);
//            isEmptyRow = false;
//            galaxies.Add(new Coordinate(x, y));
//        }
//    }

//    if (isEmptyRow)
//    {
//        emptyRows.Add(x);
//    }

//    x++;
//    input = streamReader.ReadLine();
//}

//galaxies = galaxies.Select(g => Expand(g, emptyRows, emptyColumns)).ToList();

//foreach (var i in Enumerable.Range(0, galaxies.Count - 1))
//{
//    foreach (var j in Enumerable.Range(i + 1, galaxies.Count - i - 1))
//    {
//        output += GetDistance(galaxies[i], galaxies[j]);
//    }
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0L;
var input = streamReader.ReadLine();

var x = 0;
var galaxies = new List<Coordinate>();
var emptyColumns = Enumerable.Range(0, input.Length).ToList();
var emptyRows = new List<int>();

while (!string.IsNullOrEmpty(input))
{
    var isEmptyRow = true;
    foreach (var y in Enumerable.Range(0, input.Length))
    {
        if (input[y] == '#')
        {
            emptyColumns.Remove(y);
            isEmptyRow = false;
            galaxies.Add(new Coordinate(x, y));
        }
    }

    if (isEmptyRow)
    {
        emptyRows.Add(x);
    }

    x++;
    input = streamReader.ReadLine();
}

galaxies = galaxies.Select(g => Expand(g, emptyRows, emptyColumns, 1000000)).ToList();

foreach (var i in Enumerable.Range(0, galaxies.Count - 1))
{
    foreach (var j in Enumerable.Range(i + 1, galaxies.Count - i - 1))
    {
        output += GetDistance(galaxies[i], galaxies[j]);
    }
}

Console.WriteLine($"Part Two Answer: {output}");


Coordinate Expand(Coordinate coordinate, List<int> emptyRows, List<int> emptyColumns, int expandFactor = 2)
{
    return coordinate with
    {
        X = coordinate.X + emptyRows.Count(r => r < coordinate.X) * (expandFactor - 1),
        Y = coordinate.Y + emptyColumns.Count(c => c < coordinate.Y) * (expandFactor - 1),
    };
}

long GetDistance (Coordinate from, Coordinate to)
{
    return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y); 
}

record struct Coordinate(long X, long Y);
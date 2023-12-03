//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var input = streamReader.ReadLine();

//var line = 0;
//var validCoodinates = new HashSet<Coordinate>();
//var partNumbers = new List<PartNumbers>();

//while (!string.IsNullOrEmpty(input))
//{
//    (var validCoodinatesOfLine, var partNumbersOfLine) = GetValidCoodinatesAndPartNumbers(input, line);
//    validCoodinates.UnionWith(validCoodinatesOfLine);
//    partNumbers.AddRange(partNumbersOfLine);
//    line++;
//    input = streamReader.ReadLine();
//}

//foreach (var partNumber in partNumbers)
//{
//    foreach (var coordiante in partNumber.Coordinates)
//    {
//        if (validCoodinates.Contains(coordiante))
//        {
//            Console.WriteLine($"{partNumber.Number} At {partNumber.Coordinates.First()} IS valid");
//            output += partNumber.Number;
//            break;
//        }
//    }
//}
//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

var line = 0;
var possibleGearCoodinates = new HashSet<Coordinate>();
var partNumbers = new List<PartNumbers>();

while (!string.IsNullOrEmpty(input))
{
    (var possibleGearCoodinatesOfLine, var partNumbersOfLine) = GetPossibleGearCoodinatesAndPartNumbers(input, line);
    possibleGearCoodinates.UnionWith(possibleGearCoodinatesOfLine);
    partNumbers.AddRange(partNumbersOfLine);
    line++;
    input = streamReader.ReadLine();
}

foreach (var possibleGearCoodinate in possibleGearCoodinates)
{
    var adjacentCoodinates = GetAdjacentCoodinates(possibleGearCoodinate);
    var adjacentNumbers = partNumbers.Where(p => p.Coordinates.Any(c => adjacentCoodinates.Contains(c)));
    if (adjacentNumbers.Count() == 2 )
    {
        var ratio = adjacentNumbers.First().Number * adjacentNumbers.Last().Number;
        Console.WriteLine($"Gear at {possibleGearCoodinate} with {adjacentNumbers.First().Number} and {adjacentNumbers.Last().Number}");
        output += ratio;
    }
}
Console.WriteLine($"Part Two Answer: {output}");

(HashSet<Coordinate> possibleGearCoodinates, IEnumerable<PartNumbers> partNumbers) GetPossibleGearCoodinatesAndPartNumbers(string input, int line)
{
    var possibleGearCoodinates = new HashSet<Coordinate>();
    var partNumbers = new List<PartNumbers>();
    for (int i = 0; i < input.Length; i++)
    {
        var c = input[i];
        var coodiante = new Coordinate(i, line);

        if (c == '*')
        {
            possibleGearCoodinates.Add(coodiante);
        }
        else if (char.IsDigit(c))
        {
            var partNumber = int.Parse(c.ToString());
            var coordinates = new List<Coordinate> { new Coordinate(i, line) };
            while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
            {
                i++;
                c = input[i];
                partNumber = partNumber * 10 + int.Parse(c.ToString());
                coordinates.Add(new Coordinate(i, line));
            }
            partNumbers.Add(new PartNumbers(partNumber, coordinates));
        }
    }
    return (possibleGearCoodinates, partNumbers);
}

(HashSet<Coordinate> validCoodinates, IEnumerable<PartNumbers> partNumbers) GetValidCoodinatesAndPartNumbers(string input, int line)
{
    var validCoodinates = new HashSet<Coordinate>();
    var partNumbers = new List<PartNumbers>();
    for (int i = 0; i < input.Length; i++)
    {
        var c = input[i];
        var coodiante = new Coordinate(i, line);

        if (IsSymbol(c))
        {
            var adjacentCoodinates = GetAdjacentCoodinates(coodiante);
            validCoodinates.UnionWith(adjacentCoodinates);
        }
        else if (char.IsDigit(c))
        {
            var partNumber = int.Parse(c.ToString());
            var coordinates = new List<Coordinate>{ new Coordinate(i, line) };
            while (i + 1 < input.Length && char.IsDigit(input[i + 1]))
            {
                i++;
                c = input[i];
                partNumber = partNumber * 10 + int.Parse(c.ToString());
                coordinates.Add(new Coordinate(i, line));
            }
            partNumbers.Add(new PartNumbers(partNumber, coordinates));
        }
    }
    return (validCoodinates, partNumbers);
}

HashSet<Coordinate> GetAdjacentCoodinates(Coordinate coordinate)
{
    return new HashSet<Coordinate>
    {
        coordinate with { X = coordinate.X - 1 },
        coordinate with { X = coordinate.X - 1 , Y = coordinate.Y - 1},
        coordinate with { X = coordinate.X - 1 , Y = coordinate.Y + 1},
        coordinate with { X = coordinate.X + 1 },
        coordinate with { X = coordinate.X + 1 , Y = coordinate.Y - 1},
        coordinate with { X = coordinate.X + 1 , Y = coordinate.Y + 1},
        coordinate with { Y = coordinate.Y - 1},
        coordinate with { Y = coordinate.Y + 1},
    };
}

bool IsSymbol(char c)
{
    return !char.IsDigit(c) && c != '.';
}

record struct PartNumbers(int Number, IEnumerable<Coordinate> Coordinates);

record struct Coordinate(int X, int Y);
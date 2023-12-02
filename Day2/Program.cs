//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var actualCubeSet = new CubeSet(12, 13, 14);
//var input = streamReader.ReadLine();
//while (!string.IsNullOrEmpty(input))
//{
//    var gameInfo = GetGameInfo(input);
//    var isPossible = true;
//    foreach(var set in gameInfo.sets)
//    {
//        if (!IsGamePossible(actualCubeSet, set))
//        {
//            Console.WriteLine($"Game {gameInfo.id} NOT possible: {set}");
//            isPossible = false;
//            break;
//        }
//    }
//    if (isPossible)
//    {
//        Console.WriteLine($"Game {gameInfo.id} IS possible");
//        output += gameInfo.id;
//    }
//    input = streamReader.ReadLine();
//}
//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();
while (!string.IsNullOrEmpty(input))
{
    var gameInfo = GetGameInfo(input);
    var minimunSet = GetMinimumSet(gameInfo.sets);
    var power = GetPower(minimunSet);
    output += power;
    Console.WriteLine($"Game {gameInfo.id} power is {power}: {minimunSet}");
    input = streamReader.ReadLine();
}
Console.WriteLine($"Part Two Answer: {output}");

bool IsGamePossible(CubeSet actualCubeSet, CubeSet gameInfoCubeSet)
{
    return actualCubeSet.Red >= gameInfoCubeSet.Red &&
        actualCubeSet.Green >= gameInfoCubeSet.Green &&
        actualCubeSet.Blue >= gameInfoCubeSet.Blue;
}

(int id, List<CubeSet> sets) GetGameInfo(string input)
{
    var id = int.Parse(input.Split(':')[0].Split(' ')[1]);
    var sets = new List<CubeSet>();
    var setsString = input.Split(':')[1].Split(';');
    foreach (var s in setsString)
    {
        var red = 0;
        var green = 0;
        var blue = 0;

        var colors = s.Split(',');

        foreach(var c in colors)
        {
            switch (c.Split(' ')[2])
            {
                case "red":
                    red = int.Parse(c.Split(' ')[1]);
                    break;
                case "green":
                    green = int.Parse(c.Split(' ')[1]);
                    break;
                case "blue":
                    blue = int.Parse(c.Split(' ')[1]);
                    break;
            }
        }

        sets.Add(new CubeSet(red, green, blue));
    }
    return (id, sets);
}

CubeSet GetMinimumSet (IEnumerable<CubeSet> sets)
{
    var red = 0;
    var green = 0;
    var blue = 0;

    foreach (var set in sets)
    {
        red = Math.Max(red, set.Red);
        green = Math.Max(green, set.Green);
        blue = Math.Max(blue, set.Blue);
    }

    return new CubeSet(red, green, blue);
}

int GetPower (CubeSet set)
{
    return set.Red * set.Green * set.Blue;
}

record struct CubeSet(int Red, int Green, int Blue);
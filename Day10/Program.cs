//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var input = streamReader.ReadLine();

//// Pipe is a key-value pair where key is its coordinate and value is the coordinates of where it is connected to
//var pipes = new Dictionary<Coordinate, Coordinate[]>();
//var x = 0;
//Coordinate start = default;

//while (!string.IsNullOrEmpty(input))
//{
//    foreach(var y in Enumerable.Range(0, input.Length))
//    {
//        var coordinate = new Coordinate(x, y);
//        switch (input[y])
//        {
//            case '.': break;
//            case '|': pipes.Add(coordinate, new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { X = x + 1 } }); break;
//            case '-': pipes.Add(coordinate, new Coordinate[] { coordinate with { Y = y - 1 }, coordinate with { Y = y + 1 } }); break;
//            case 'L': pipes.Add(coordinate, new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { Y = y + 1 } }); break;
//            case 'F': pipes.Add(coordinate, new Coordinate[] { coordinate with { X = x + 1 }, coordinate with { Y = y + 1 } }); break;
//            case 'J': pipes.Add(coordinate, new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { Y = y - 1 } }); break;
//            case '7': pipes.Add(coordinate, new Coordinate[] { coordinate with { X = x + 1 }, coordinate with { Y = y - 1 } }); break;
//            case 'S': start = coordinate; break;
//        }
//    }
//    x++;

//    input = streamReader.ReadLine();
//}

//// Add start to pipes
//var coordinateNextToStart = new Coordinate[] { start with { X = start.X - 1 }, start with { X = start.X + 1 }, start with { Y = start.Y - 1 }, start with { Y = start.Y + 1 } };
//pipes.Add(start, coordinateNextToStart.Where(c => pipes.ContainsKey(c) && pipes[c].Any(ct => ct == start)).ToArray());

//var left = start;
//var right = start;
//var lastLeft = pipes[start].FirstOrDefault();
//var lastRight = pipes[start].LastOrDefault();

//do
//{
//    output++;

//    var tempLeft = left;
//    var t = pipes[left];
//    left = pipes[left].FirstOrDefault(c => c != lastLeft);
//    lastLeft = tempLeft;

//    var tempRight = right;
//    right = pipes[right].FirstOrDefault(c => c != lastRight);
//    lastRight = tempRight;
//}
//while (left != right);

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

// Pipe is a key-value pair where key is its coordinate and value is the coordinates of where it is connected to, and its symbol
var pipes = new Dictionary<Coordinate, (Coordinate[], char)>();
var pipesInLoop = new Dictionary<Coordinate, (Coordinate[], char)>();
var x = 0;
var inputLength = input.Length;
Coordinate start = default;

while (!string.IsNullOrEmpty(input))
{
    foreach (var y in Enumerable.Range(0, input.Length))
    {
        var coordinate = new Coordinate(x, y);
        switch (input[y])
        {
            case '.': break;
            case '|': pipes.Add(coordinate, (new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { X = x + 1 } }, input[y])); break;
            case '-': pipes.Add(coordinate, (new Coordinate[] { coordinate with { Y = y - 1 }, coordinate with { Y = y + 1 } }, input[y])); break;
            case 'L': pipes.Add(coordinate, (new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { Y = y + 1 } }, input[y])); break;
            case 'F': pipes.Add(coordinate, (new Coordinate[] { coordinate with { X = x + 1 }, coordinate with { Y = y + 1 } }, input[y])); break;
            case 'J': pipes.Add(coordinate, (new Coordinate[] { coordinate with { X = x - 1 }, coordinate with { Y = y - 1 } }, input[y])); break;
            case '7': pipes.Add(coordinate, (new Coordinate[] { coordinate with { X = x + 1 }, coordinate with { Y = y - 1 } }, input[y])); break;
            case 'S': start = coordinate; break;
        }
    }
    x++;

    input = streamReader.ReadLine();
}

// Add start to pipes, hard code start as 'L' since don't wanna bother with the logic
var coordinateNextToStart = new Coordinate[] { start with { X = start.X - 1 }, start with { X = start.X + 1 }, start with { Y = start.Y - 1 }, start with { Y = start.Y + 1 } };
pipes.Add(start, (coordinateNextToStart.Where(c => pipes.ContainsKey(c) && pipes[c].Item1.Any(ct => ct == start)).ToArray(), 'L'));
pipesInLoop.Add(start, pipes[start]);

var left = start;
var right = start;
var lastLeft = pipes[start].Item1.FirstOrDefault();
var lastRight = pipes[start].Item1.LastOrDefault();

do
{
    var tempLeft = left;
    var t = pipes[left];
    left = pipes[left].Item1.FirstOrDefault(c => c != lastLeft);
    lastLeft = tempLeft;
    pipesInLoop.Add(left, pipes[left]);

    var tempRight = right;
    right = pipes[right].Item1.FirstOrDefault(c => c != lastRight);
    lastRight = tempRight;
    pipesInLoop.TryAdd(right, pipes[right]);
}
while (left != right);

// Scan line by line, looking at a single line at a time and determine if tile is in loop by looking at '|'
// treat 'L', 'J', '7', 'F' as half '|' (example: 'L' + '7' = '|' and 'L' + 'J' cancel out)
// '-' only matters in the sense that it should not be count as an in-loop tile
// note: tiles in a line will always start as not in loop and end as not in loop. 'L' and 'F' will always be "closed" by a '7' or 'J'.
// example: given "..|..|..L-J..FJ..L7...", we know "00|11|00L-J00FJ11L7000" (0: out, 1: in)
foreach (var x2 in Enumerable.Range(0, x))
{
    var isInLoop = false;
    var lastPipe = ' ';
    foreach (var y2 in Enumerable.Range(0, inputLength))
    {
        var coordinate = new Coordinate(x2, y2);

        if (!pipesInLoop.ContainsKey(coordinate) && isInLoop)
        {
            output++;
            continue;
        }
        else if (!pipesInLoop.ContainsKey(coordinate))
        {
            continue;
        }
        else if (pipesInLoop[coordinate].Item2 == '|')
        {
            isInLoop = !isInLoop;
            continue;
        }
        else if (pipesInLoop[coordinate].Item2 == 'L' || pipesInLoop[coordinate].Item2 == 'F')
        {
            lastPipe = pipesInLoop[coordinate].Item2;
            continue;
        }
        else if ((pipesInLoop[coordinate].Item2 == 'J' && lastPipe == 'F') || 
            pipesInLoop[coordinate].Item2 == '7' && lastPipe == 'L')
        {
            isInLoop = !isInLoop;
            continue;
        }
    }
}


Console.WriteLine($"Part Two Answer: {output}");


record struct Coordinate (int X, int Y);
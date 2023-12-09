//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var input = streamReader.ReadLine();

//var instructions = input;
//var map = new Dictionary<string, Direction>();


//// Popuplate map
//while (input != null)
//{
//    if (input == string.Empty)
//    {
//        input = streamReader.ReadLine();
//        continue;
//    }

//    var from = input[..3];
//    var toL = input.Substring(7, 3);
//    var toR = input.Substring(12, 3);

//    map[from] = new Direction(toL, toR);

//    input = streamReader.ReadLine();
//}

//var location = "AAA";

//while (location != "ZZZ")
//{
//    foreach (var instruction in instructions)
//    {
//        if (location == "ZZZ")
//        {
//            break;
//        }

//        if (instruction == 'L')
//        {
//            location = map[location].L;
//        }
//        else
//        {
//            location = map[location].R;
//        }

//        output++;
//    }
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

var instructions = input;
var map = new Dictionary<string, Direction>();


// Popuplate map
while (input != null)
{
    if (input == string.Empty)
    {
        input = streamReader.ReadLine();
        continue;
    }

    var from = input[..3];
    var toL = input.Substring(7, 3);
    var toR = input.Substring(12, 3);

    map[from] = new Direction(toL, toR);

    input = streamReader.ReadLine();
}

var starts = map.Where(l => l.Key.EndsWith("A")).Select(l => l.Key).ToList();
var destinations = map.Where(l => l.Key.EndsWith("Z")).Select(l => l.Key).ToList();
var StartAndDestinationTracks = new Dictionary<StartAndDestination, List<InstuctionIndexAndSteps>>();

foreach (var start in starts)
{
    foreach (var destination in destinations)
    {
        var startAndDestination = new StartAndDestination(start, destination);
        StartAndDestinationTracks[startAndDestination] = new List<InstuctionIndexAndSteps>();
        var currentLocation = start;
        var visiteds = new HashSet<int>();
        var locationsAfterFullLoop = new HashSet<string>();
        var done = false;
        var steps = 0L;

        while (!done)
        {
            if (!locationsAfterFullLoop.Add(currentLocation) && !visiteds.Any())
            {
                // Can't reach destination
                break;
            }

            foreach (var i in Enumerable.Range(0, instructions.Length))
            {
                var instruction = instructions[i];

                if (instruction == 'L')
                {
                    currentLocation = map[currentLocation].L;
                }
                else
                {
                    currentLocation = map[currentLocation].R;
                }

                steps++;

                if (currentLocation == destination)
                {
                    StartAndDestinationTracks[startAndDestination].Add(new InstuctionIndexAndSteps(i, steps));
                    if (!visiteds.Add(i))
                    {
                        // looped
                        done = true;
                        break;
                    }
                }
            }
        }
    }
}

var tracks = StartAndDestinationTracks.Where(s => s.Value.Count > 0).ToList();

// All starting locations somehow map one to one to a single unique destination,
// and all reach there at the last step of the instruction
// I am planning to handle cases where starting locations can reach multiple destinations,
// and reaching destination in the middle of the instruction but I guess not needed
// Simply getting the LCM of the steps to get to destination will do in this case.
foreach (var track in tracks)
{
    Console.WriteLine(track.Value.First().Steps);
}

// Use an online LCM Calculator to get the LCM of the steps of all tracks
// https://www.calculatorschool.com/Numbers/HCFandLCMCalculators.aspx
Console.WriteLine($"Part Two Answer: LCM of above");

record struct Direction(string L, string R);

record struct InstuctionIndexAndSteps(int InstuctionIndex, long Steps);

record struct StartAndDestination(string Start, string Destination);
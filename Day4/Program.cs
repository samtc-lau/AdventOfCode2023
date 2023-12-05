//using var streamReader = new StreamReader("input.txt");
//var output = 0L;
//var input = streamReader.ReadLine();

//while (!string.IsNullOrEmpty(input))
//{
//    var point = GetPoints(input);
//    Console.WriteLine(point);
//    output += point;
//    input = streamReader.ReadLine();
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

// Init Dictionary
var numberOfCards = 198;
var cardCounts = new Dictionary<int, int>();
foreach (var i in Enumerable.Range(1, numberOfCards))
{
    cardCounts[i] = 1;
}

var card = 1;

while (!string.IsNullOrEmpty(input))
{
    var matches = GetMatches(input);

    // Start with the next card, loop {match} times
    foreach (var i in Enumerable.Range(card + 1, matches))
    {
        if (!cardCounts.ContainsKey(i)) break;
        cardCounts[i] += cardCounts[card];
    }

    card++;
    input = streamReader.ReadLine();
}

foreach (var count in cardCounts.Values)
{
    Console.WriteLine(count);
    output += count;
}

Console.WriteLine($"Part Two Answer: {output}");

long GetPoints(string input)
{
    var matches = GetMatches(input);
    return (long)Math.Pow(2, matches - 1);
}

HashSet<int> GetNumbers(string input)
{
    return input.Trim().Split(' ').Where(c => !string.IsNullOrWhiteSpace(c)).Select(int.Parse).ToHashSet();
}

int GetMatches(string input)
{
    var winningNumbers = GetNumbers(input.Split(':')[1].Split('|')[0]);
    var myNumbers = GetNumbers(input.Split(':')[1].Split('|')[1]);
    return winningNumbers.Intersect(myNumbers).Count();
}


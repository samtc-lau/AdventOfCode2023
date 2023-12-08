//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var input = streamReader.ReadLine();

//var labels = new Dictionary<char, int>
//{
//    { '2', 0 },
//    { '3', 1 },
//    { '4', 2 },
//    { '5', 3 },
//    { '6', 4 },
//    { '7', 5 },
//    { '8', 6 },
//    { '9', 7 },
//    { 'T', 8 },
//    { 'J', 9 },
//    { 'Q', 10 },
//    { 'K', 11 },
//    { 'A', 12 },
//};

//var hands = new List<Hand>();

//while (!string.IsNullOrEmpty(input))
//{
//    var cards = input.Split(' ').First();
//    var bet = int.Parse(input.Split(' ').Last());
//    var hand = new Hand(cards, GetAbsoluteRank(cards), bet);
//    hands.Add(hand);

//    input = streamReader.ReadLine();
//}

//hands = hands.OrderBy(h => h.AbsoluteRank).ToList();

//foreach (var i in Enumerable.Range(1, hands.Count))
//{
//    output += i * hands[i - 1].Bet;
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

var labels = new Dictionary<char, int>
{
    { 'J', 0 },
    { '2', 1 },
    { '3', 2 },
    { '4', 3 },
    { '5', 4 },
    { '6', 5 },
    { '7', 6 },
    { '8', 7 },
    { '9', 8 },
    { 'T', 9 },
    { 'Q', 10 },
    { 'K', 11 },
    { 'A', 12 },
};

var hands = new List<Hand>();

while (!string.IsNullOrEmpty(input))
{
    var cards = input.Split(' ').First();
    var bet = int.Parse(input.Split(' ').Last());
    var hand = new Hand(cards, GetAbsoluteRankPartTwo(cards), bet);
    hands.Add(hand);

    input = streamReader.ReadLine();
}

hands = hands.OrderBy(h => h.AbsoluteRank).ToList();

foreach (var i in Enumerable.Range(1, hands.Count))
{
    output += i * hands[i - 1].Bet;
}

Console.WriteLine($"Part Two Answer: {output}");

int GetAbsoluteRank(string hand)
{
    var rank = 0;

    // Treat hand as base-13 number and transform to base-10
    foreach (var i in Enumerable.Range(0, hand.Length))
    {
        rank += labels[hand[i]] * (int)Math.Pow(labels.Count, hand.Length - i - 1);
    }

    // Determine hand type and add that to the base-13 rank
    var frequencies = new Dictionary<char, int>();

    foreach (var card in hand)
    {
        if (frequencies.ContainsKey(card))
        {
            frequencies[card]++;
        }
        else
        {
            frequencies[card] = 1;
        }
    }

    if (frequencies.Count == 1) 
    {
        // five of a kind
        rank += 6 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 2 && frequencies.ContainsValue(4))
    {
        // four of a kind
        rank += 5 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 2 && frequencies.ContainsValue(3))
    {
        // full house
        rank += 4 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 3 && frequencies.ContainsValue(3))
    {
        // three of a kind
        rank += 3 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 3 && frequencies.ContainsValue(2))
    {
        // two pairs
        rank += 2 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 4)
    {
        // one pair
        rank += (int)Math.Pow(labels.Count, hand.Length);
    }

    return rank;
}

int GetAbsoluteRankPartTwo(string hand)
{
    var rank = 0;

    // Treat hand as base-13 number and transform to base-10
    foreach (var i in Enumerable.Range(0, hand.Length))
    {
        rank += labels[hand[i]] * (int)Math.Pow(labels.Count, hand.Length - i - 1);
    }

    // Determine hand type and add that to the base-13 rank
    var frequencies = new Dictionary<char, int>();

    foreach (var card in hand)
    {
        if (frequencies.ContainsKey(card))
        {
            frequencies[card]++;
        }
        else
        {
            frequencies[card] = 1;
        }
    }

    // Treat 'J' as the most frequent non-J card
    // 'JJJJJ' is an edge case where we cannot change it to any other card
    if (frequencies.TryGetValue('J', out var numberOfJ) && numberOfJ != 5)
    {
        var maxFrequency = frequencies.Where(f => f.Key != 'J').Max(f => f.Value);
        var kindOfTheMost = frequencies.First(f => f.Key != 'J' && f.Value == maxFrequency).Key;
        frequencies[kindOfTheMost] += numberOfJ;
        frequencies.Remove('J');
    }

    if (frequencies.Count == 1)
    {
        // five of a kind
        rank += 6 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 2 && frequencies.ContainsValue(4))
    {
        // four of a kind
        rank += 5 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 2 && frequencies.ContainsValue(3))
    {
        // full house
        rank += 4 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 3 && frequencies.ContainsValue(3))
    {
        // three of a kind
        rank += 3 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 3 && frequencies.ContainsValue(2))
    {
        // two pairs
        rank += 2 * (int)Math.Pow(labels.Count, hand.Length);
    }
    else if (frequencies.Count == 4)
    {
        // one pair
        rank += (int)Math.Pow(labels.Count, hand.Length);
    }

    return rank;
}

record struct Hand (string Cards, int AbsoluteRank, int Bet);
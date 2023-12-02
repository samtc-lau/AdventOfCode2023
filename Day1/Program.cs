//using (var streamReader = new StreamReader("input.txt"))
//{
//    var output = 0;
//    var input = streamReader.ReadLine();
//    while (!string.IsNullOrEmpty(input))
//    {
//        var value = GetValue(input);
//        output += value;
//        Console.WriteLine($"{input} - {value}");
//        input = streamReader.ReadLine();
//    }
//    Console.WriteLine($"Part One Answer: {output}");
//}

using (var streamReader = new StreamReader("input.txt"))
{
    var output = 0;
    var input = streamReader.ReadLine();
    while (!string.IsNullOrEmpty(input))
    {
        var value = GetValuePartTwo(input);
        output += value;
        Console.WriteLine($"{input} - {value}");
        input = streamReader.ReadLine();
    }
    Console.WriteLine($"Part Two Answer: {output}");
}

int GetValue(string input)
{
    var left = 0;
    var right = input.Length - 1;
    var leftValue = 0;
    var rightValue = 0;
    while (!int.TryParse(input[left].ToString(), out leftValue))
    {
        left++;
    }
    while (!int.TryParse(input[right].ToString(), out rightValue))
    {
        right--;
    }
    return leftValue * 10 + rightValue;
}

int GetValuePartTwo(string input)
{
    var digitsAsString = new HashSet<string>{
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
    };

    var maxDigitsAsStringLength = digitsAsString.Max(x => x.Length);
    var minDigitsAsStringLength = digitsAsString.Min(x => x.Length);
    var left = 0;
    var right = input.Length - 1;
    var leftValue = 0;
    var rightValue = 0;
    while (!int.TryParse(input[left].ToString(), out leftValue))
    {
        try
        {
            var leftValueLeft = left;
            var leftValueRight = left + minDigitsAsStringLength - 1;
            var valueString = input[leftValueLeft..(leftValueRight + 1)];

            while (!digitsAsString.Contains(valueString) && valueString.Length <= maxDigitsAsStringLength)
            {
                leftValueRight++;
                valueString = input[leftValueLeft..(leftValueRight + 1)];
            }
            if (digitsAsString.Contains(valueString))
            {
                leftValue = ToInt(valueString);
                break;
            }
            left++;
        }
        catch
        {
            left++;
        }
    }
    while (!int.TryParse(input[right].ToString(), out rightValue))
    {
        try
        {
            var rightValueLeft = right - minDigitsAsStringLength + 1;
            var rightValueRight = right;
            var valueString = input[rightValueLeft..(rightValueRight + 1)];

            while (!digitsAsString.Contains(valueString) && valueString.Length <= maxDigitsAsStringLength)
            {
                rightValueLeft--;
                valueString = input[rightValueLeft..(rightValueRight + 1)];
            }
            if (digitsAsString.Contains(valueString))
            {
                rightValue = ToInt(valueString);
                break;
            }
            right--;
        }
        catch
        {
            right--;
        }
    }
    return leftValue * 10 + rightValue;
}

int ToInt(string digit)
{
    switch(digit)
    {
        case "one": return 1;
        case "two": return 2;
        case "three": return 3;
        case "four": return 4;
        case "five": return 5;
        case "six": return 6;
        case "seven": return 7;
        case "eight": return 8;
        case "nine": return 9;
        default: return 0;
    }
}

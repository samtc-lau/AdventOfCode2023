//using var streamReader = new StreamReader("input.txt");
//var output = 0;
//var input = streamReader.ReadLine();

//while (!string.IsNullOrEmpty(input))
//{
//    output += GetNext(input.Split(' ').Select(int.Parse).ToArray());

//    input = streamReader.ReadLine();
//}

//Console.WriteLine($"Part One Answer: {output}");

using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

while (!string.IsNullOrEmpty(input))
{
    output += GetPrevious(input.Split(' ').Select(int.Parse).ToArray());

    input = streamReader.ReadLine();
}

Console.WriteLine($"Part Two Answer: {output}");

int GetNext(int[] inputs)
{
    var differentials = GetDifferentials(inputs);

    if (differentials.All(d => d == 0))
    {
        return inputs.Last();
    }

    return inputs.Last() + GetNext(differentials);
}
int GetPrevious(int[] inputs)
{
    var differentials = GetDifferentials(inputs);

    if (differentials.All(d => d == 0))
    {
        return inputs.First();
    }

    return inputs.First() - GetPrevious(differentials);
}


int[] GetDifferentials(int[] inputs)
{
    var result = new int[inputs.Length - 1];
    foreach (var i in Enumerable.Range(1, inputs.Length - 1))
    {
        result[i - 1] = inputs[i] - inputs[i - 1];
    }
    return result;
}
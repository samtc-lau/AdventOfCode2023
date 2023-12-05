using var streamReader = new StreamReader("input.txt");
var output = 0;
var input = streamReader.ReadLine();

while (!string.IsNullOrEmpty(input))
{

    input = streamReader.ReadLine();
}

Console.WriteLine($"Part One Answer: {output}");
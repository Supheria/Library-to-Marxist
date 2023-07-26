using md_formatter;

while (true)
{
    var fileName = Console.ReadLine() ?? "";
    if (fileName.GetMatch(@"^""(.+)""$", out var match))
        fileName = match.Groups[1].Value;
    var _ = new Formatter(fileName, fileName);
    Console.WriteLine();
}
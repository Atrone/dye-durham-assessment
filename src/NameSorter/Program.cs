using NameSorter;

const string OutputFileName = "sorted-names-list.txt";

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: name-sorter <path-to-unsorted-names-file>");
    return 1;
}

var sorter = new NameListSorter(
    new NameFileReader(args[0]),
    new LastNameThenGivenNamesComparer(),
    [new ConsoleNameWriter(Console.Out), new NameFileWriter(OutputFileName)]);

try
{
    sorter.Run();
    return 0;
}
catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or InvalidDataException)
{
    Console.Error.WriteLine($"Error: {exception.Message}");
    return 1;
}

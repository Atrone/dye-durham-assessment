namespace NameSorter.Tests;

public sealed class NameListSorterTests : IDisposable
{
    private readonly TemporaryDirectory _directory = new();

    public void Dispose() => _directory.Dispose();

    [Fact]
    public void Run_SendsTheSortedNamesToEveryWriter()
    {
        var screen = new RecordingNameWriter();
        var file = new RecordingNameWriter();
        var sorter = new NameListSorter(
            new InMemoryNameReader("Janet Parsons", "Marin Alvarez", "Leo Gardner"),
            new LastNameThenGivenNamesComparer(),
            [screen, file]);

        sorter.Run();

        string[] expected = ["Marin Alvarez", "Leo Gardner", "Janet Parsons"];
        Assert.Equal(expected, screen.WrittenNames);
        Assert.Equal(expected, file.WrittenNames);
    }

    [Fact]
    public void Run_WritesNothingWhenTheNamesCannotBeRead()
    {
        var writer = new RecordingNameWriter();
        var sorter = new NameListSorter(new UnreadableNameReader(), new LastNameThenGivenNamesComparer(), [writer]);

        Assert.Throws<InvalidDataException>(() => sorter.Run());
        Assert.Null(writer.WrittenNames);
    }

    [Fact]
    public void Run_PrintsAndSavesTheExampleListInSortedOrder()
    {
        var inputPath = _directory.PathTo("unsorted-names-list.txt");
        var outputPath = _directory.PathTo("sorted-names-list.txt");
        File.WriteAllLines(inputPath,
        [
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Shelby Nathan Yoder",
            "Marin Alvarez",
            "London Lindsey",
            "Beau Tristan Bentley",
            "Leo Gardner",
            "Hunter Uriah Mathew Clarke",
            "Mikayla Lopez",
            "Frankie Conner Ritter",
        ]);
        var console = new StringWriter();
        var sorter = new NameListSorter(
            new NameFileReader(inputPath),
            new LastNameThenGivenNamesComparer(),
            [new ConsoleNameWriter(console), new NameFileWriter(outputPath)]);

        sorter.Run();

        string[] expected =
        [
            "Marin Alvarez",
            "Adonis Julius Archer",
            "Beau Tristan Bentley",
            "Hunter Uriah Mathew Clarke",
            "Leo Gardner",
            "Vaughn Lewis",
            "London Lindsey",
            "Mikayla Lopez",
            "Janet Parsons",
            "Frankie Conner Ritter",
            "Shelby Nathan Yoder",
        ];
        Assert.Equal(expected, console.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        Assert.Equal(expected, File.ReadAllLines(outputPath));
    }

    private sealed class InMemoryNameReader(params string[] fullNames) : INameReader
    {
        public IReadOnlyList<PersonName> ReadNames() => fullNames.Select(PersonName.Parse).ToList();
    }

    private sealed class UnreadableNameReader : INameReader
    {
        public IReadOnlyList<PersonName> ReadNames() => throw new InvalidDataException();
    }

    private sealed class RecordingNameWriter : INameWriter
    {
        public List<string>? WrittenNames { get; private set; }

        public void Write(IReadOnlyList<PersonName> names) =>
            WrittenNames = names.Select(name => name.FullName).ToList();
    }
}

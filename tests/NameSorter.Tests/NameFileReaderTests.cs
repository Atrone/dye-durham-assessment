namespace NameSorter.Tests;

public sealed class NameFileReaderTests : IDisposable
{
    private readonly TemporaryDirectory _directory = new();

    public void Dispose() => _directory.Dispose();

    [Fact]
    public void ReadNames_ReadsOneNamePerLineInFileOrder()
    {
        var path = WriteNamesFile("Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer");

        var names = new NameFileReader(path).ReadNames();

        Assert.Equal(new[] { "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer" }, names.Select(name => name.FullName));
    }

    [Fact]
    public void ReadNames_SkipsBlankLines()
    {
        var path = WriteNamesFile("", "Janet Parsons", "   ", "Vaughn Lewis", "");

        var names = new NameFileReader(path).ReadNames();

        Assert.Equal(new[] { "Janet Parsons", "Vaughn Lewis" }, names.Select(name => name.FullName));
    }

    [Fact]
    public void ReadNames_ReportsTheLineNumberOfAnInvalidName()
    {
        var path = WriteNamesFile("Janet Parsons", "Madonna");

        var exception = Assert.Throws<InvalidDataException>(() => new NameFileReader(path).ReadNames());

        Assert.Contains("Line 2", exception.Message);
    }

    private string WriteNamesFile(params string[] lines)
    {
        var path = _directory.PathTo("names.txt");
        File.WriteAllLines(path, lines);
        return path;
    }
}

namespace NameSorter.Tests;

public sealed class NameFileWriterTests : IDisposable
{
    private readonly TemporaryDirectory _directory = new();

    public void Dispose() => _directory.Dispose();

    [Fact]
    public void Write_ReplacesAnyExistingFileWithOneNamePerLine()
    {
        var path = _directory.PathTo("sorted-names-list.txt");
        File.WriteAllText(path, "Output from a previous run");
        PersonName[] names = [PersonName.Parse("Marin Alvarez"), PersonName.Parse("Adonis Julius Archer")];

        new NameFileWriter(path).Write(names);

        Assert.Equal(new[] { "Marin Alvarez", "Adonis Julius Archer" }, File.ReadAllLines(path));
    }
}

namespace NameSorter.Tests;

public sealed class ConsoleNameWriterTests
{
    [Fact]
    public void Write_PrintsEachNameOnItsOwnLine()
    {
        var console = new StringWriter();
        PersonName[] names = [PersonName.Parse("Marin Alvarez"), PersonName.Parse("Adonis Julius Archer")];

        new ConsoleNameWriter(console).Write(names);

        Assert.Equal($"Marin Alvarez{Environment.NewLine}Adonis Julius Archer{Environment.NewLine}", console.ToString());
    }
}

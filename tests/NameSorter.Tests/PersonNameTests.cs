namespace NameSorter.Tests;

public sealed class PersonNameTests
{
    [Theory]
    [InlineData("Janet Parsons", "Parsons", "Janet")]
    [InlineData("Adonis Julius Archer", "Archer", "Adonis", "Julius")]
    [InlineData("Hunter Uriah Mathew Clarke", "Clarke", "Hunter", "Uriah", "Mathew")]
    public void Parse_TakesTheLastWordAsTheLastNameAndTheRestAsGivenNames(
        string fullName, string expectedLastName, params string[] expectedGivenNames)
    {
        var name = PersonName.Parse(fullName);

        Assert.Equal(expectedLastName, name.LastName);
        Assert.Equal(expectedGivenNames, name.GivenNames);
    }

    [Fact]
    public void Parse_IgnoresExtraWhitespaceBetweenNames()
    {
        var name = PersonName.Parse("  Beau \t Tristan   Bentley ");

        Assert.Equal("Beau Tristan Bentley", name.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Madonna")]
    public void Parse_RejectsANameWithoutAGivenName(string fullName)
    {
        Assert.Throws<FormatException>(() => PersonName.Parse(fullName));
    }

    [Fact]
    public void Parse_RejectsMoreThanThreeGivenNames()
    {
        Assert.Throws<FormatException>(() => PersonName.Parse("Anna Beth Cara Dawn Evans"));
    }
}

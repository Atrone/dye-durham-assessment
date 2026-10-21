namespace NameSorter.Tests;

public sealed class LastNameThenGivenNamesComparerTests
{
    private readonly LastNameThenGivenNamesComparer _comparer = new();

    [Fact]
    public void Compare_OrdersByLastNameFirst()
    {
        AssertSortsBefore("Zoe Adams", "Adam Baker");
    }

    [Fact]
    public void Compare_OrdersByGivenNamesWhenLastNamesMatch()
    {
        AssertSortsBefore("Adam Bentley", "Beau Tristan Bentley");
    }

    [Fact]
    public void Compare_UsesLaterGivenNamesWhenEarlierOnesMatch()
    {
        AssertSortsBefore("John Adam Smith", "John Zed Smith");
    }

    [Fact]
    public void Compare_PutsFewerGivenNamesFirstWhenTheSharedOnesMatch()
    {
        AssertSortsBefore("John Smith", "John Adam Smith");
    }

    [Fact]
    public void Compare_IgnoresCase()
    {
        Assert.Equal(0, _comparer.Compare(PersonName.Parse("Leo Gardner"), PersonName.Parse("leo GARDNER")));
    }

    [Fact]
    public void Compare_SortsAccentedLettersWithTheirBaseLetter()
    {
        AssertSortsBefore("Ana Ávila", "Ana Baker");
    }

    private void AssertSortsBefore(string earlier, string later)
    {
        var earlierName = PersonName.Parse(earlier);
        var laterName = PersonName.Parse(later);

        Assert.True(_comparer.Compare(earlierName, laterName) < 0, $"Expected \"{earlier}\" to sort before \"{later}\".");
        Assert.True(_comparer.Compare(laterName, earlierName) > 0, $"Expected \"{later}\" to sort after \"{earlier}\".");
    }
}

namespace NameSorter;

public sealed class LastNameThenGivenNamesComparer : IComparer<PersonName>
{
    // Culture-aware so "Ávila" sorts with the A's, but invariant so the order is the same on every machine.
    private static readonly StringComparer NamePartComparer = StringComparer.InvariantCultureIgnoreCase;

    public int Compare(PersonName? x, PersonName? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        var byLastName = NamePartComparer.Compare(x.LastName, y.LastName);
        return byLastName != 0 ? byLastName : CompareGivenNames(x.GivenNames, y.GivenNames);
    }

    private static int CompareGivenNames(IReadOnlyList<string> x, IReadOnlyList<string> y)
    {
        var sharedCount = Math.Min(x.Count, y.Count);

        for (var i = 0; i < sharedCount; i++)
        {
            var byGivenName = NamePartComparer.Compare(x[i], y[i]);
            if (byGivenName != 0)
            {
                return byGivenName;
            }
        }

        return x.Count.CompareTo(y.Count);
    }
}

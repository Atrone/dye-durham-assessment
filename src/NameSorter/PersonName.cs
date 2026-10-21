namespace NameSorter;

public sealed class PersonName
{
    private const int MinGivenNames = 1;
    private const int MaxGivenNames = 3;

    private PersonName(IReadOnlyList<string> givenNames, string lastName)
    {
        GivenNames = givenNames;
        LastName = lastName;
    }

    public IReadOnlyList<string> GivenNames { get; }

    public string LastName { get; }

    public string FullName => string.Join(' ', GivenNames.Append(LastName));

    public static PersonName Parse(string fullName)
    {
        ArgumentNullException.ThrowIfNull(fullName);

        // A null separator splits on any whitespace, so extra spaces or tabs between names don't matter.
        var parts = fullName.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        var givenNameCount = parts.Length - 1;

        if (givenNameCount is < MinGivenNames or > MaxGivenNames)
        {
            throw new FormatException(
                $"\"{fullName.Trim()}\" is not a valid name. Expected {MinGivenNames} to {MaxGivenNames} given names followed by a last name.");
        }

        var givenNames = parts[..^1];
        var lastName = parts[^1];
        return new PersonName(Array.AsReadOnly(givenNames), lastName);
    }

    public override string ToString() => FullName;
}

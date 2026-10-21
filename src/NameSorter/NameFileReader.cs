namespace NameSorter;

public sealed class NameFileReader(string path) : INameReader
{
    public IReadOnlyList<PersonName> ReadNames()
    {
        var names = new List<PersonName>();
        var lineNumber = 0;

        foreach (var line in File.ReadLines(path))
        {
            lineNumber++;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            try
            {
                names.Add(PersonName.Parse(line));
            }
            catch (FormatException exception)
            {
                throw new InvalidDataException($"Line {lineNumber} of {path}: {exception.Message}", exception);
            }
        }

        return names;
    }
}

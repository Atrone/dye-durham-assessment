namespace NameSorter;

public sealed class NameFileWriter(string path) : INameWriter
{
    public void Write(IReadOnlyList<PersonName> names) =>
        File.WriteAllLines(path, names.Select(name => name.FullName));
}

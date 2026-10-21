namespace NameSorter;

public sealed class ConsoleNameWriter(TextWriter console) : INameWriter
{
    public void Write(IReadOnlyList<PersonName> names)
    {
        foreach (var name in names)
        {
            console.WriteLine(name.FullName);
        }
    }
}

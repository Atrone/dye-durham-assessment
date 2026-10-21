namespace NameSorter;

public sealed class NameListSorter(
    INameReader reader,
    IComparer<PersonName> sortOrder,
    IEnumerable<INameWriter> writers)
{
    public void Run()
    {
        var sortedNames = reader.ReadNames().OrderBy(name => name, sortOrder).ToList();

        foreach (var writer in writers)
        {
            writer.Write(sortedNames);
        }
    }
}

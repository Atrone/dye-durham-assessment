namespace NameSorter;

public interface INameWriter
{
    void Write(IReadOnlyList<PersonName> names);
}

namespace NameSorter.Tests;

internal sealed class TemporaryDirectory : IDisposable
{
    private readonly DirectoryInfo _directory = Directory.CreateTempSubdirectory("name-sorter-tests-");

    public string PathTo(string fileName) => Path.Combine(_directory.FullName, fileName);

    public void Dispose() => _directory.Delete(recursive: true);
}

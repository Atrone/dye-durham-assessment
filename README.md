# Name Sorter

Sorts a list of names by last name, then by given names. The result is printed to the screen and written to `sorted-names-list.txt` in the current directory, replacing any existing file.

## Requirements

[.NET 10 SDK](https://dotnet.microsoft.com/download).

## Usage

```sh
dotnet run --project src/NameSorter -- ./unsorted-names-list.txt
```

Or build the `name-sorter` executable and run it directly:

```sh
dotnet publish src/NameSorter -c Release -o publish
./publish/name-sorter ./unsorted-names-list.txt
```

The input file has one name per line: one to three given names followed by a last name. Blank lines are skipped. If the file can't be read or a line isn't a valid name, the program reports the problem, exits with code 1, and writes nothing.

## Tests

```sh
dotnet test
```

GitHub Actions runs the build and tests on every push (`.github/workflows/ci.yml`).

## Code overview

All application code is in `src/NameSorter`:

- `PersonName` parses and validates a single name.
- `LastNameThenGivenNamesComparer` defines the sort order.
- `NameListSorter` reads the names, sorts them, and passes the result to each writer.
- `NameFileReader`, `ConsoleNameWriter` and `NameFileWriter` handle input and output. To add another output, implement `INameWriter` and add it to the writers in `Program.cs`.

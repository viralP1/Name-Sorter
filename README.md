# Name Sorter

A .NET 10 console application that reads a list of names from a text file, sorts the names by last name and then by given names, prints the sorted result to the console, and writes the output to `sorted-names-list.txt`.

## Requirements

- .NET 10 SDK

## Build

From the solution root, run:

```bash
dotnet build
```

## Run

From the solution root, run:

```bash
dotnet run --project Name-Sorter -- ./unsorted-names-list.txt
```

The application will:

1. Read the names from the supplied text file.
2. Parse each valid name into its given names and last name.
3. Sort the names by last name.
4. Use the given names as the secondary sort order when last names match.
5. Print the sorted names to the console.
6. Write or overwrite `sorted-names-list.txt` in the current working directory.

## Input Rules

Each name must contain:

- At least 1 given name
- No more than 3 given names
- Exactly 1 last name

Examples of valid names:

```text
Janet Parsons
Adonis Julius Archer
Hunter Uriah Mathew Clarke
```

Blank lines are ignored.

If an invalid name is encountered, the application stops and reports the invalid input instead of silently skipping it.

## Example

Given an input file containing:

```text
Janet Parsons
Vaughn Lewis
Adonis Julius Archer
Shelby Nathan Yoder
Marin Alvarez
London Lindsey
Beau Tristan Bentley
Leo Gardner
Hunter Uriah Mathew Clarke
Mikayla Lopez
Frankie Conner Ritter
```

Running:

```bash
dotnet run --project Name-Sorter -- ./unsorted-names-list.txt
```

Produces:

```text
Marin Alvarez
Adonis Julius Archer
Beau Tristan Bentley
Hunter Uriah Mathew Clarke
Leo Gardner
Vaughn Lewis
London Lindsey
Mikayla Lopez
Janet Parsons
Frankie Conner Ritter
Shelby Nathan Yoder
```

The same sorted output is written to:

```text
sorted-names-list.txt
```

If the file already exists, it is overwritten.

## Tests

Run all tests from the solution root with:

```bash
dotnet test
```

The test suite covers:

- Parsing names with one or multiple given names
- Validation of valid and invalid name formats
- Handling of empty and whitespace input
- Sorting by last name
- Sorting by given names when last names match
- Case-insensitive sorting
- Empty collections
- File reading
- Missing file handling
- File creation
- File overwrite behavior
- Domain validation in `PersonName`

## Design

The application separates responsibilities into focused components:

- `PersonName` represents a valid person name and enforces the domain rules for given names and last name.
- `NameParser` converts raw text input into `PersonName` instances.
- `NameSorter` contains the sorting behavior.
- `NameFileService` handles reading from and writing to files.
- `Program` coordinates the application flow.

The solution intentionally avoids unnecessary abstractions or infrastructure because the problem domain is small. The focus is on clear responsibilities, readability, testability, and maintainability.

## Error Handling

The application validates the command-line input and file contents.

Examples of handled errors include:

- Missing input file path
- Input file not found
- Empty or whitespace-only names
- Names with no given name
- Names with more than three given names

When malformed input is encountered, the application exits with a clear error message identifying the invalid name.

## Project Structure

```text
Name-Sorter/
│
├── Name-Sorter/
│   ├── Models/
│   │   └── PersonName.cs
│   ├── Services/
│   │   ├── NameFileService.cs
│   │   ├── NameParser.cs
│   │   └── NameSorter.cs
│   ├── Program.cs
│   └── Name-Sorter.csproj
│
├── Name-Sorter.Tests/
│   ├── Models/
│   │   └── PersonNameTests.cs
│   ├── Services/
│   │   ├── NameFileServiceTests.cs
│   │   ├── NameParserTests.cs
│   │   └── NameSorterTests.cs
│   └── Name-Sorter.Tests.csproj
│
└── README.md
```

## Assumptions

- The final token in each non-empty line is treated as the last name.
- All preceding tokens are treated as given names.
- Names are compared using case-insensitive ordinal string comparison.
- The input format is plain text with one name per line.
- Invalid names cause the application to stop rather than being silently ignored.
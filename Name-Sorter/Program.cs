using Name_Sorter.Services;

const string outputFilename = "sorted-names-list.txt";

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: name-sorter <path-to-unsorted-names-file>");
    return 1;
}

try
{
    var nameFileService = new NameFileService();
    var nameParser = new NameParser();
    var nameSorter = new NameSorter();

    var names = nameFileService
        .ReadNames(args[0])
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .Select(nameParser.Parse);

    var sortedNames = nameSorter.Sort(names);

    foreach(var name in sortedNames)
    {
        Console.WriteLine(name);
    }

    nameFileService.WriteNames(
        outputFilename,
        sortedNames.Select(name => name.ToString()));

    return 0;
}
catch(Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}
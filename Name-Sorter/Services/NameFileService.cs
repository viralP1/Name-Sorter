
namespace Name_Sorter.Services;

public sealed class NameFileService
{
    public IEnumerable<string> ReadNames(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' could not be found.", filePath);
        }

        return File.ReadLines(filePath);
    }

    public void WriteNames(string filePath, IEnumerable<string> names)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path cannot be empty.", nameof(filePath));
        }

        ArgumentNullException.ThrowIfNull(names);

        File.WriteAllLines(filePath, names);
    }
}

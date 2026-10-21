using Name_Sorter.Models;

namespace Name_Sorter.Services;

public sealed class NameParser
{
    public PersonName Parse(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length is < 2 or > 4)
        {
            throw new ArgumentException(
                $"Invalid name '{name}'. A name must contain between 1 and 3 given names and exactly 1 last name.",
                nameof(name));
        }

        var givenNames = parts[..^1];
        var lastName = parts[^1];

        return new PersonName(givenNames, lastName);
    }
}

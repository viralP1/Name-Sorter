using Name_Sorter.Models;

namespace Name_Sorter.Services;

public sealed class NameSorter
{
    public IReadOnlyList<PersonName> Sort(IEnumerable<PersonName> names)
    {
        ArgumentNullException.ThrowIfNull(names);

        return names.OrderBy(name => name.LastName, StringComparer.OrdinalIgnoreCase)
            .ThenBy(
                name => string.Join(' ', name.GivenNames),
                StringComparer.OrdinalIgnoreCase)
            .ToList();
    } 
}


namespace Name_Sorter.Models;

public sealed class PersonName
{
    public IReadOnlyList<string> GivenNames { get; }
    public string LastName { get; }

    public PersonName(IEnumerable<string> givenNames, string lastName)
    {
        GivenNames = givenNames?.ToArray()
            ?? throw new ArgumentNullException(nameof(givenNames));

        if (GivenNames.Count is < 1 or > 3)
        {
            throw new ArgumentException("A person must have between 1 and 3 given names.", nameof(givenNames));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }

        LastName = lastName.Trim();
    }

    public override string ToString()
    {
        return $"{string.Join(' ', GivenNames)} {LastName}";
    }
}

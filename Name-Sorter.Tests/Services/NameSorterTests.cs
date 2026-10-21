
using Name_Sorter.Models;
using Name_Sorter.Services;

namespace Name_Sorter.Tests.Services;

public sealed class NameSorterTests
{
    private readonly NameSorter _sorter = new();

    [Fact]
    public void Sort_OrdersNamesByLastName()
    {
        var names = new[]
        {
            new PersonName(["Janet"], "Parsons"),
            new PersonName(["Marin"], "Alvarez"),
            new PersonName(["Leo"], "Gardner")
        };

        var result = _sorter.Sort(names);

        Assert.Equal("Alvarez", result[0].LastName);
        Assert.Equal("Gardner", result[1].LastName);
        Assert.Equal("Parsons", result[2].LastName);
    }

    [Fact]
    public void Sort_WhenLastNamesMatch_OrdersByGivenNames()
    {
        var names = new[]
        {
            new PersonName(["John"], "Smith"),
            new PersonName(["Adam"], "Smith"),
            new PersonName(["Zachary"], "Smith")
        };

        var result = _sorter.Sort(names);

        Assert.Equal("Adam", result[0].GivenNames[0]);
        Assert.Equal("John", result[1].GivenNames[0]);
        Assert.Equal("Zachary", result[2].GivenNames[0]);
    }

    [Fact]
    public void Sort_WithMultipleGivenNames_OrdersByFullGivenNameSequence()
    {
        var names = new[]
        {
            new PersonName(["John", "Michael"], "Smith"),
            new PersonName(["John", "Adam"], "Smith"),
            new PersonName(["Adam"], "Smith")
        };

        var result = _sorter.Sort(names);

        Assert.Equal(
            [
                "Adam Smith",
                "John Adam Smith",
                "John Michael Smith"
            ],
            result.Select(name => name.ToString()));
    }

    [Fact]
    public void Sort_IsCaseInsensitive()
    {
        var names = new[]
        {
            new PersonName(["John"], "smith"),
            new PersonName(["Adam"], "Smith"),
            new PersonName(["Zara"], "brown")
        };

        var result = _sorter.Sort(names);

        Assert.Equal("Zara brown", result[0].ToString());
        Assert.Equal("Adam Smith", result[1].ToString());
        Assert.Equal("John smith", result[2].ToString());
    }

    [Fact]
    public void Sort_WithEmptyCollection_ReturnsEmptyCollection()
    {
        var result = _sorter.Sort([]);

        Assert.Empty(result);
    }

    [Fact]
    public void Sort_WithNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => _sorter.Sort(null!));
    }
}

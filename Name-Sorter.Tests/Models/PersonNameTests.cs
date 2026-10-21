
using Name_Sorter.Models;

namespace Name_Sorter.Tests.Models;

public sealed class PersonNameTests
{
    [Fact]
    public void Constructor_WithValidName_CreatesPersonName()
    {
        var person = new PersonName(
            ["Hunter", "Uriah", "Mathew"],
            "Clarke");

        Assert.Equal(
            ["Hunter", "Uriah", "Mathew"],
            person.GivenNames);

        Assert.Equal("Clarke", person.LastName);
    }

    [Fact]
    public void Constructor_TrimsLastName()
    {
        var person = new PersonName(
            ["Janet"],
            "  Parsons  ");

        Assert.Equal("Parsons", person.LastName);
    }

    [Fact]
    public void ToString_ReturnsFullName()
    {
        var person = new PersonName(
            ["Adonis", "Julius"],
            "Archer");

        Assert.Equal(
            "Adonis Julius Archer",
            person.ToString());
    }

    [Fact]
    public void Constructor_WithNoGivenNames_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => new PersonName([], "Parsons"));
    }

    [Fact]
    public void Constructor_WithMoreThanThreeGivenNames_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => new PersonName(
                ["One", "Two", "Three", "Four"],
                "Parsons"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Constructor_WithEmptyLastName_ThrowsArgumentException(
    string lastName)
    {
        Assert.Throws<ArgumentException>(
            () => new PersonName(["Janet"], lastName));
    }

    [Fact]
    public void Constructor_WithNullGivenNames_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => new PersonName(null!, "Parsons"));
    }
}

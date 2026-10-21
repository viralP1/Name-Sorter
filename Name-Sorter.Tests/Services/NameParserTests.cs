using Name_Sorter.Services;

namespace Name_Sorter.Tests.Services;

public sealed class NameParserTests
{
    private readonly NameParser _parser = new();

    [Fact]
    public void Parse_WithOneGivenName_ReturnsExpectedPersonName()
    {
        var result = _parser.Parse("Janet Parsons");

        Assert.Single(result.GivenNames);
        Assert.Equal("Janet", result.GivenNames[0]);
        Assert.Equal("Parsons", result.LastName);
    }

    [Fact]
    public void Parse_WithThreeGivenNames_ReturnsExpectedPersonName()
    {
        var result = _parser.Parse("Hunter Uriah Mathew Clarke");

        Assert.Equal(
            ["Hunter", "Uriah", "Mathew"],
            result.GivenNames);

        Assert.Equal("Clarke", result.LastName);
    }

    [Fact]
    public void Parse_WithExtraSpaces_ParsesSuccessfully()
    {
        var result = _parser.Parse("  Janet   Parsons  ");

        Assert.Equal("Janet", result.GivenNames[0]);
        Assert.Equal("Parsons", result.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Parse_WithEmptyOrWhitespaceName_ThrowsArgumentException(string input)
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse(input));
    }

    [Fact]
    public void Parse_WithOnlyLastName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _parser.Parse("Parsons"));
    }

    [Fact]
    public void Parse_WithMoreThanThreeGivenNames_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => _parser.Parse("One Two Three Four Five"));
    }
}

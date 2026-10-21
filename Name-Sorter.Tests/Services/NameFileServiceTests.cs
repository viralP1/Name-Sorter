
using Name_Sorter.Services;

namespace Name_Sorter.Tests.Services;

public sealed class NameFileServiceTests
{
    private readonly NameFileService _fileService = new();

    [Fact]
    public void ReadNames_WithExistingFile_ReturnsFileLines()
    {
        var filePath = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(
                filePath,
                [
                    "Janet Parsons",
                    "Marin Alvarez"
                ]);

            var result = _fileService.ReadNames(filePath).ToList();

            Assert.Equal(
                [
                    "Janet Parsons",
                    "Marin Alvarez"
                ],
                result);
        }
        finally
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    public void ReadNames_WithMissingFile_ThrowsFileNotFoundException()
    {
        var filePath = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.txt");

        Assert.Throws<FileNotFoundException>(
            () => _fileService.ReadNames(filePath).ToList());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void ReadNames_WithEmptyPath_ThrowsArgumentException(string filePath)
    {
        Assert.Throws<ArgumentException>(
            () => _fileService.ReadNames(filePath).ToList());
    }

    [Fact]
    public void WriteNames_CreatesFileWithExpectedContent()
    {
        var filePath = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.txt");

        try
        {
            _fileService.WriteNames(
                filePath,
                [
                    "Marin Alvarez",
                    "Janet Parsons"
                ]);

            var result = File.ReadAllLines(filePath);

            Assert.Equal(
                [
                    "Marin Alvarez",
                    "Janet Parsons"
                ],
                result);
        }
        finally
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

    [Fact]
    public void WriteNames_WhenFileExists_OverwritesExistingContent()
    {
        var filePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(filePath, "Old content");

            _fileService.WriteNames(
                filePath,
                [
                    "Marin Alvarez"
                ]);

            var result = File.ReadAllLines(filePath);

            Assert.Single(result);
            Assert.Equal("Marin Alvarez", result[0]);
        }
        finally
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    public void WriteNames_WithNullNames_ThrowsArgumentNullException()
    {
        var filePath = Path.GetTempFileName();

        try
        {
            Assert.Throws<ArgumentNullException>(
                () => _fileService.WriteNames(filePath, null!));
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}

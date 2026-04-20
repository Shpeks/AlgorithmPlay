using AlgoLab.Core.Utilities;
using Xunit;

namespace AlgoLab.Tests;

public class DataGeneratorTests
{
    [Fact]
    public void GenerateRandomNumbers_ShouldReturnArrayWithCorrectSize()
    {
        var result = DataGenerator.GenerateRandomNumbers(20, 1, 100);

        Assert.Equal(20, result.Length);
    }

    [Fact]
    public void GenerateRandomNumbers_ShouldReturnValuesInRange()
    {
        var result = DataGenerator.GenerateRandomNumbers(50, 10, 20);

        Assert.All(result, x => Assert.InRange(x, 10, 19));
    }
}
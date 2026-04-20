using AlgoLab.Core.Algo.Search;
using Xunit;

namespace AlgoLab.Tests;

public class LinearSearchAlgoTests
{
    [Fact]
    public void Search_ShouldReturnCorrectIndex_WhenElementExists()
    {
        var algo = new LinearSearchAlgo();
        int[] source = [2, 4, 6, 8];

        var result = algo.Search(source, 6);

        Assert.Equal(2, result.Index);
    }

    [Fact]
    public void Search_ShouldReturnMinusOne_WhenElementDoesNotExist()
    {
        var algo = new LinearSearchAlgo();
        int[] source = [2, 4, 6, 8];

        var result = algo.Search(source, 5);

        Assert.Equal(-1, result.Index);
    }

    [Fact]
    public void SearchWithSteps_ShouldReturnSteps()
    {
        var algo = new LinearSearchAlgo();
        int[] source = [10, 20, 30, 40];

        var result = algo.SearchWithSteps(source, 30);

        Assert.NotEmpty(result.Steps);
        Assert.Equal(2, result.Index);
    }
}
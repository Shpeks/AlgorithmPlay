using AlgoLab.Core.Algo.Search;
using Xunit;

namespace AlgoLab.Tests;

public class BinarySearchAlgoTests
{
    [Fact]
    public void Search_ShouldReturnCorrectIndex_WhenElementExists()
    {
        var algo = new BinarySearchAlgo();
        int[] source = [1, 3, 5, 7, 9];

        var result = algo.Search(source, 7);

        Assert.Equal(3, result.Index);
    }

    [Fact]
    public void Search_ShouldReturnMinusOne_WhenElementDoesNotExist()
    {
        var algo = new BinarySearchAlgo();
        int[] source = [1, 3, 5, 7, 9];

        var result = algo.Search(source, 4);

        Assert.Equal(-1, result.Index);
    }

    [Fact]
    public void SearchWithSteps_ShouldReturnSteps()
    {
        var algo = new BinarySearchAlgo();
        int[] source = [1, 3, 5, 7, 9, 11, 13];

        var result = algo.SearchWithSteps(source, 11);

        Assert.NotEmpty(result.Steps);
        Assert.Equal(5, result.Index);
    }
}
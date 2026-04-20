using AlgoLab.Core.Algo.Sort;
using Xunit;

namespace AlgoLab.Tests;

public class QuickSortAlgoTests
{
    [Fact]
    public void Sort_ShouldSortArrayAscending()
    {
        var algo = new QuickSortAlgo();
        int[] source = [5, 3, 1, 4, 2];

        var result = algo.Sort(source);

        Assert.Equal([1, 2, 3, 4, 5], result.SortedArray);
    }

    [Fact]
    public void Sort_ShouldSortArrayWithDuplicates()
    {
        var algo = new QuickSortAlgo();
        int[] source = [4, 2, 4, 1, 2];

        var result = algo.Sort(source);

        Assert.Equal([1, 2, 2, 4, 4], result.SortedArray);
    }

    [Fact]
    public void SortWithSteps_ShouldReturnSortedArray()
    {
        var algo = new QuickSortAlgo();
        int[] source = [8, 3, 6, 1, 9, 2];

        var result = algo.SortWithSteps(source);

        Assert.Equal([1, 2, 3, 6, 8, 9], result.SortedArray);
    }
}
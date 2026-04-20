using Xunit;
using AlgoLab.Core.Algo.Sort;

namespace AlgoLab.Tests;

public class BubbleSortAlgoTests
{
    [Fact]
    public void Sort_ShouldSortArrayAscending()
    {
        var algo = new BubbleSortAlgo();
        int[] source = [5, 3, 1, 4, 2];
        
        var result = algo.Sort(source);
        
        Assert.Equal([1, 2, 3, 4, 5], result.SortedArray);
    }
    
    [Fact]
    public void Sort_ShouldKeepDuplicatesCorrectly()
    {
        var algo = new BubbleSortAlgo();
        int[] source = [4, 2, 4, 1, 2];

        var result = algo.Sort(source);

        Assert.Equal([1, 2, 2, 4, 4], result.SortedArray);
    }

    [Fact]
    public void SortWithSteps_ShouldReturnSteps_ForUnsortedArray()
    {
        var algo = new BubbleSortAlgo();
        int[] source = [5, 1, 4, 2, 3];

        var result = algo.SortWithSteps(source);

        Assert.NotEmpty(result.Steps);
        Assert.Equal([1, 2, 3, 4, 5], result.SortedArray);
    }
}
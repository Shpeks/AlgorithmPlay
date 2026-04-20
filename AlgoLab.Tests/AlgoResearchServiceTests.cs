using AlgoLab.Core.Algo.Sort;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Services;
using Xunit;

namespace AlgoLab.Tests;

public class AlgoResearchServiceTests
{
    [Fact]
    public void RunBenchmark_ShouldReturnResults_ForSelectedAlgorithms()
    {
        var service = new AlgoResearchService();

        var algorithms = new List<ISortingAlgo>
        {
            new BubbleSortAlgo(),
            new QuickSortAlgo()
        };

        var results = service.RunBenchmark(algorithms, runsCount: 5, arraySize: 10, minValue: 1, maxValue: 100); 
        
        Assert.Equal(2, results.Count);
        Assert.All(results, x => Assert.Equal(5, x.RunsCount));
    }

    [Fact]
    public void RunBenchmark_ShouldSortResultsByAverageTime()
    {
        var service = new AlgoResearchService();

        var algorithms = new List<ISortingAlgo>
        {
            new BubbleSortAlgo(),
            new QuickSortAlgo()
        };

        var results = service.RunBenchmark(algorithms, runsCount: 5, arraySize: 20, minValue: 1, maxValue: 100);

        for (int i = 0; i < results.Count - 1; i++)
        {
            Assert.True(results[i].AvgMs <= results[i + 1].AvgMs);
        }
    }
}
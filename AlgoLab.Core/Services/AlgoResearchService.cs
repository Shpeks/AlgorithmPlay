using AlgoLab.Core.Contracts;
using AlgoLab.Core.Models;
using AlgoLab.Core.Utilities;

namespace AlgoLab.Core.Services;

public class AlgoResearchService
{
    public IReadOnlyList<SortBenchmarkResult> RunBenchmark(
        IEnumerable<ISortingAlgo> algos,
        int runsCount,
        int arraySize,
        int minValue = 5,
        int maxValue = 10)
    {
        if(runsCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(runsCount));
        
        if(arraySize <= 0)
            throw new ArgumentOutOfRangeException(nameof(arraySize));

        var testArrays = new List<int[]>();
        
        for(int i = 0; i < runsCount; i++)
            testArrays.Add(DataGenerator.GenerateRandomNumbers(arraySize, minValue, maxValue));
        
        var results = new List<SortBenchmarkResult>();

        foreach (var algo in algos)
        {
            long totalComparisons = 0;
            long totalSwaps = 0;
            double totalMs = 0;
            double minMs = double.MaxValue;
            double maxMs = 0;

            foreach (var testArray in testArrays)
            {
                var result = algo.Sort(testArray);
                
                totalComparisons += result.Comparisons;
                totalSwaps += result.Swaps;
                totalMs += result.Ms;
                
                if(result.Ms < minMs)
                    minMs = result.Ms;
                
                if(result.Ms > maxMs)
                    maxMs = result.Ms;
            }
            
            results.Add(new SortBenchmarkResult(
                algo.Name,
                runsCount,
                totalComparisons / (double)runsCount,
                totalMs / runsCount,
                totalSwaps / (double)runsCount,
                minMs,
                maxMs,
                totalMs));
        }

        return results
            .OrderBy(x => x.AvgMs)
            .ToList();
    }
    public IReadOnlyList<SortResult> RunSortResearch(int[] source, IEnumerable<ISortingAlgo> algo)
    {
        return algo
            .Select(algo => algo.Sort(source))
            .ToList();
    }

    public IReadOnlyList<SearchResult> RunSearchResearch(int[] source, int target, IEnumerable<ISearchingAlgo> algo)
    {
        return algo
            .Select(algo => algo.Search(source, target))
            .ToList();
    }
}
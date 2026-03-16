using System.Diagnostics;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Models;

namespace AlgoLab.Core.Algo.Search;

public class LinearSearchAlgo : ISearchingAlgo
{
    public string Name { get; } = "LinearSearch";

    public SearchResult Search(int[] source, int target)
    {
        var result = SearchWithSteps(source, target);
        
        return new SearchResult(result.Name, result.Target, result.Index, result.Comparisons, result.Ms);
    }

    public SearchVisualResult SearchWithSteps(int[] source, int target)
    {
        var steps = new List<SearchStep>();
        
        int comparisons = 0;
        int index = -1;
        
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < source.Length; i++)
        {
            comparisons++;

            if (source[i] == target)
            {
                index = i;
                steps.Add(new SearchStep(i, FoundIndex: i));
                break;
            }
            
            steps.Add(new SearchStep(i));
        }
        
        stopwatch.Stop();
        
        return new SearchVisualResult(
            Name,
            target,
            index,
            steps,
            comparisons,
            stopwatch.Elapsed.TotalMilliseconds);
    }
}
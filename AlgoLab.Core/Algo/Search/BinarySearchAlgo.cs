using System.Diagnostics;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Models;

namespace AlgoLab.Core.Algo.Search;

public class BinarySearchAlgo : ISearchingAlgo
{
    public string Name => "Binary Search";

    public SearchResult Search(int[] source, int target)
    {
        var result = SearchWithSteps(source, target);   
        
        return new SearchResult(
            result.Name, 
            result.Target, 
            result.Index, 
            result.Comparisons, 
            result.Ms);
    }

    public SearchVisualResult SearchWithSteps(int[] source, int target)
    {
        int left = 0;
        int right = source.Length - 1;
        int index = -1;
        int comparisons = 0;

        var steps = new List<SearchStep>();
        var stopwatch = Stopwatch.StartNew();

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            
            comparisons++;

            if (source[middle] == target)
            {
                index = middle;
                steps.Add(new SearchStep(middle, left, right, middle));
                break;
            }
            
            steps.Add(new SearchStep(middle, left, right));

            comparisons++;
            
            if(source[middle] < target)
                left = middle + 1;
            else
                right = middle - 1;
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
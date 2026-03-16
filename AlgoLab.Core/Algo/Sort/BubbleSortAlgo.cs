using System.Diagnostics;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Models;

namespace AlgoLab.Core.Algo.Sort;

public class BubbleSortAlgo : ISortingAlgo
{
    public string Name => "Bubble Sort";

    public SortResult Sort(int[] source)
    {
        var result = SortWithSteps(source);

        return new SortResult(
            result.Name,
            result.SortedArray,
            result.Comparisons,
            result.Swaps,
            result.Ms);
    }

    public SortVisualResult SortWithSteps(int[] source)
    {
        var array = source.ToArray();
        var steps = new List<SortStep>();
        
        int comparisons = 0;
        int swaps = 0;
        
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < array.Length - 1; i++)
        {
            bool swapped = false;

            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                comparisons++;

                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swaps++;
                    swapped = true;
                    
                    steps.Add(new SortStep(j, j + 1));
                }
            }

            if (!swapped)
            {
                for (int k = 0; k < array.Length - i; k++)
                    steps.Add(new SortStep(-1, -1, k));
                
                break;
            }
            
            steps.Add(new SortStep(-1, -1, array.Length - 1 - i));
        }
        
        if (array.Length > 0)
            steps.Add(new SortStep(-1, -1, 0));
        
        stopwatch.Stop();
        
        return new SortVisualResult(
            Name, 
            array, 
            steps, 
            comparisons, 
            swaps, 
            stopwatch.Elapsed.TotalMilliseconds); 
    }
}
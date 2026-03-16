using System.Diagnostics;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Models;

namespace AlgoLab.Core.Algo.Sort;

public class QuickSortAlgo : ISortingAlgo
{
    public string Name => "Quick Sort";

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
        
        if(array.Length > 1)
            QuickSort(array, 0, array.Length - 1, ref comparisons, ref swaps, steps);
        else if (array.Length == 1)
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

    private static void QuickSort(
        int[] array, 
        int low, 
        int high, 
        ref int comparisons, 
        ref int swaps, 
        List<SortStep> steps)
    {
        if (low >= high)
            return;

        if (low == high)
        {
            steps.Add(new SortStep(-1, -1, low));
            return;
        }
        
        int pivotIndex = Partition(array, low, high, ref comparisons, ref swaps, steps);
        
        steps.Add(new SortStep(-1, -1, pivotIndex));
        
        QuickSort(array, low, pivotIndex - 1, ref comparisons, ref swaps, steps);
        QuickSort(array, pivotIndex + 1, high, ref comparisons, ref swaps, steps);
    }

    private static int Partition(int[] array, int low, int high, ref int comparisons, ref int swaps, List<SortStep> steps)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;

            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j, ref swaps, steps);
            }
        }
        
        Swap(array, i + 1, high, ref swaps, steps);

        return i + 1;
    }

    private static void Swap(int[] array, int i, int j, ref int swaps, List<SortStep> steps)
    {
        if (i == j)
            return;
        
        (array[i], array[j]) = (array[j], array[i]);
        swaps++;
        
        steps.Add(new SortStep(i, j));
    }
}
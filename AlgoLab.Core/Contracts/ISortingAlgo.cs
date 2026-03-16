namespace AlgoLab.Core.Contracts;

using AlgoLab.Core.Models;

public interface ISortingAlgo
{
    string Name { get; }
    SortResult Sort(int[] source);
    SortVisualResult SortWithSteps(int[] source);
}
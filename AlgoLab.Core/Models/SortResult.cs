namespace AlgoLab.Core.Models;

public record SortResult(
    string Name,
    int[] SortedArray,
    int Comparisons,
    int Swaps,
    double Ms);
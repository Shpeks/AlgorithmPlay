namespace AlgoLab.Core.Models;

public record SortVisualResult(
    string Name,
    int[] SortedArray,
    IReadOnlyList<SortStep> Steps,
    int Comparisons, 
    int Swaps,
    double Ms);
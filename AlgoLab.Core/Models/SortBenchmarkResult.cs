namespace AlgoLab.Core.Models;

public record SortBenchmarkResult(
    string Name,
    int RunsCount,
    double AvgComparisons,
    double AvgMs,
    double AvgSwaps,
    double MinMs,
    double MaxMs,
    double TotalMs);
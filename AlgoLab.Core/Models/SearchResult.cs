namespace AlgoLab.Core.Models;

public record SearchResult(
    string Name,
    int Target,
    int Index,
    int Comparisons,
    double Ms);
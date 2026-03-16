namespace AlgoLab.Core.Models;

public record SearchVisualResult(
    string Name,
    int Target,
    int Index,
    IReadOnlyList<SearchStep> Steps,
    int Comparisons,
    double Ms);
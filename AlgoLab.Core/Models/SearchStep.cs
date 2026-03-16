namespace AlgoLab.Core.Models;

public record SearchStep(
    int CurrentIndex,
    int LeftIndex = -1,
    int RightIndex = -1,
    int FoundIndex = -1);
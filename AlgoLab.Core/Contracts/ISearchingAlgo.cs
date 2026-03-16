namespace AlgoLab.Core.Contracts;

using AlgoLab.Core.Models;

public interface ISearchingAlgo
{
    string Name { get; }
    SearchResult Search(int[] source, int target);
    SearchVisualResult SearchWithSteps(int[] source, int target);
}
using AlgoLab.Core.Algo.Search;
using AlgoLab.Core.Algo.Sort;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Services;
using AlgoLab.Core.Utilities;


var service = new AlgoResearchService();

int[] sourceArray = DataGenerator.GenerateRandomNumbers(size: 20, minValue: 1, maxValue: 100);

Console.WriteLine($"Исходный массив:");
PrintArray(sourceArray);

Console.WriteLine();
Console.WriteLine("=== ИССЛЕДОВАНИЕ СОРТИРОВОК ===");

var sortingAlgo = new List<ISortingAlgo>
{
    new BubbleSortAlgo(),
    new QuickSortAlgo()
};

var sortResult = service.RunSortResearch(sourceArray, sortingAlgo);

foreach (var result in sortResult)
{
    Console.WriteLine();
    Console.WriteLine($"Алгоритм: {result.Name}");
    Console.WriteLine($"Сравнений: {result.Comparisons}");
    Console.WriteLine($"Перестановок: {result.Swaps}");
    Console.WriteLine($"Время: {result.Ms} мс");
    Console.Write("Результат: ");
    PrintArray(result.SortedArray);
}

var sortedArrayForSearch = new QuickSortAlgo().Sort(sourceArray).SortedArray;
int target = sortedArrayForSearch[sortedArrayForSearch.Length / 2];

Console.WriteLine();
Console.WriteLine("=== ИССЛЕДОВАНИЕ ПОИСКА ===");
Console.WriteLine($"Ищем число: {target}");
Console.Write("Массив для поиска: ");
PrintArray(sortedArrayForSearch);

var searchAlgo = new List<ISearchingAlgo>
{
    new LinearSearchAlgo(),
    new BinarySearchAlgo(),
};

var searchResults = service.RunSearchResearch(sortedArrayForSearch, target, searchAlgo);

foreach (var result in searchResults)
{
    Console.WriteLine();
    Console.WriteLine($"Алгоритм: {result.Name}");
    Console.WriteLine($"Индекс найденного элемента: {result.Index}");
    Console.WriteLine($"Сравнений: {result.Comparisons}");
    Console.WriteLine($"Время: {result.Ms} мс");
}

static void PrintArray(int[] array)
{
    Console.WriteLine($"[{string.Join(", ", array)}]");
}
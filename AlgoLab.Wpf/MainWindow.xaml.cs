using System.Collections.ObjectModel;
using System.Windows;
using AlgoLab.Core.Algo.Sort;
using AlgoLab.Core.Contracts;
using AlgoLab.Core.Utilities;
using AlgoLab.Wpf.Models;
using AlgoLab.Core.Services;
using AlgoLab.Core.Algo.Search;
using AlgoLab.Core.Models;

namespace AlgoLab.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const double ChartHeight = 360;

    private readonly ObservableCollection<BarItem> _bars = new();
    private readonly Dictionary<string, ISortingAlgo> _algo = new();
    private readonly AlgoResearchService _researchService = new();
    private readonly ObservableCollection<BarItem> _searchBars = new();
    private readonly Dictionary<string, ISearchingAlgo> _searchAlgo = new();

    private int[] _searchArray = [];
    
    private int[] _sourceArray = [];
    
    public MainWindow()
    {
        InitializeComponent();

        BarsItemsControl.ItemsSource = _bars;
        
        _algo.Add("Bubble Sort", new BubbleSortAlgo());
        _algo.Add("Quick Sort", new QuickSortAlgo());
        
        AlgorithmComboBox.ItemsSource = _algo.Keys;
        AlgorithmComboBox.SelectedIndex = 0;

        GenerateArray();
        
        SearchBarsItemsControl.ItemsSource = _searchBars;

        _searchAlgo.Add("Linear Search", new LinearSearchAlgo());
        _searchAlgo.Add("Binary Search", new BinarySearchAlgo());

        SearchAlgorithmComboBox.ItemsSource = _searchAlgo.Keys;
        SearchAlgorithmComboBox.SelectedIndex = 0;

        GenerateSearchArray();
    }

    private async void BenchmarkButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedAlgo = GetSelectedBenchmarkAlgorithms();

        if (selectedAlgo.Count == 0)
        {
            MessageBox.Show("Выберите хотябы один алгоритм");
            return;
        }

        if (!int.TryParse(RunsTextBox.Text, out int runsCount))
            runsCount = 50;
        
        if (runsCount < 1)
            runsCount = 1;
        
        if (runsCount > 500)
            runsCount = 500;
        
        RunsTextBox.Text = runsCount.ToString();

        int size = GetArraySize();
        
        ToggleControls(false);
        InfoTextBlock.Text = "Выполняется исследование...";

        try
        {
            var results = await Task.Run(() =>
                _researchService.RunBenchmark(selectedAlgo, runsCount, size, 5, 100));
            
            BenchmarkDataGrid.ItemsSource = results;

            var fastest = results
                .OrderBy(x => x.AvgMs).First();
            
            InfoTextBlock.Text =
                $"Исследование завершено | Повторов: {runsCount} | Быстрее всего: {fastest.Name} | Среднее время: {fastest.AvgMs:F4} мс";
        }
        finally
        {
            ToggleControls(true);
        }
    }
    
    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        GenerateArray();
    }

    private async void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if (_sourceArray.Length == 0)
            return;
        
        ToggleControls(false);

        try
        {
            RenderBars(_sourceArray);

            string selectedName = AlgorithmComboBox.SelectedItem!.ToString();
            ISortingAlgo algo = _algo[selectedName];

            var result = algo.SortWithSteps(_sourceArray);

            InfoTextBlock.Text =
                $"Алгоритм: {result.Name} | Сравнений: {result.Comparisons} | Перестановок: {result.Swaps}";

            foreach (var step in result.Steps)
            {
                if (step.FirstIndex >= 0 && step.SecondIndex >= 0)
                    SwapBars(step.FirstIndex, step.SecondIndex);

                if (step.FixedIndex.HasValue)
                    MarkBarAsSorted(step.FixedIndex.Value);

                await Task.Delay((int)DelaySlider.Value);
            }

            MarkAllBarsAsSorted();

            InfoTextBlock.Text =
                $"Готово | {result.Name} | Сравнений: {result.Comparisons} | Перестановок: {result.Swaps} | Время: {result.Ms:F4} мс";
        }
        finally
        {
            ToggleControls(true);
        }
    }

    private void DelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (DelayValueText != null)
            DelayValueText.Text = $"{(int)DelaySlider.Value} мс";
    }
    
    private void GenerateArray()
    {
        int size = GetArraySize();

        _sourceArray = DataGenerator.GenerateRandomNumbers(size, 5, 100);
        
        RenderBars(_sourceArray);
        InfoTextBlock.Text = "Новый массив создан";
    }

    private void RenderBars(int[] array)
    {
        _bars.Clear();
        
        int maxValue = array.Max();

        foreach (var value in array)
        {
            double height = value / (double)maxValue * ChartHeight;
            _bars.Add(new BarItem(value, height));
        }
    }

    private void SwapBars(int first, int second)
    {
        if (first == second) return;
        
        var temp = _bars[first];
        _bars[first] = _bars[second];
        _bars[second] = temp;
    }

    private void ToggleControls(bool isEnable)
    {
        GenerateButton.IsEnabled = isEnable;
        StartButton.IsEnabled = isEnable;
        BenchmarkButton.IsEnabled = isEnable;

        SizeTextBox.IsEnabled = isEnable;
        RunsTextBox.IsEnabled = isEnable;

        AlgorithmComboBox.IsEnabled = isEnable;
        DelaySlider.IsEnabled = isEnable;

        BubbleBenchmarkCheckBox.IsEnabled = isEnable;
        QuickBenchmarkCheckBox.IsEnabled = isEnable;
    }

    private List<ISortingAlgo> GetSelectedBenchmarkAlgorithms()
    {
        var algorithms = new List<ISortingAlgo>();
        
        if(BubbleBenchmarkCheckBox.IsChecked == true)
            algorithms.Add(_algo["Bubble Sort"]);
        
        if(QuickBenchmarkCheckBox.IsChecked == true)
            algorithms.Add(_algo["Quick Sort"]);
        
        return algorithms;
    }

    private int GetArraySize()
    {
        if(!int.TryParse(SizeTextBox.Text, out int size))
            size = 20;
        
        if (size < 5)
            size = 5;

        if (size > 300)
            size = 300;
        
        SizeTextBox.Text = size.ToString();
        
        return size;
    }
    
    private void MarkBarAsSorted(int index)
    {
        if (index < 0 || index >= _bars.Count)
            return;

        _bars[index].SetSorted();
    }
    
    private void MarkAllBarsAsSorted()
    {
        foreach (var bar in _bars)
            bar.SetSorted();
    }
    
    private void GenerateSearchButton_Click(object sender, RoutedEventArgs e)
{
    GenerateSearchArray();
}

private void RandomTargetButton_Click(object sender, RoutedEventArgs e)
{
    SetRandomExistingTarget();
}

private async void StartSearchButton_Click(object sender, RoutedEventArgs e)
{
    if (_searchArray.Length == 0)
        return;

    if (!int.TryParse(SearchTargetTextBox.Text, out int target))
    {
        MessageBox.Show("Введите корректное число для поиска.");
        return;
    }

    ToggleSearchControls(false);

    try
    {
        RenderSearchBars(_searchArray);

        string selectedName = SearchAlgorithmComboBox.SelectedItem!.ToString()!;
        ISearchingAlgo algo = _searchAlgo[selectedName];

        var result = algo.SearchWithSteps(_searchArray, target);

        SearchInfoTextBlock.Text =
            $"Алгоритм: {result.Name} | Цель: {result.Target} | Сравнений: {result.Comparisons}";

        foreach (var step in result.Steps)
        {
            ApplySearchStep(selectedName, step);
            await Task.Delay((int)SearchDelaySlider.Value);
        }

        if (result.Index >= 0)
        {
            MarkFoundBar(result.Index);

            SearchInfoTextBlock.Text =
                $"Найдено | {result.Name} | Значение: {result.Target} | Индекс: {result.Index} | Сравнений: {result.Comparisons} | Время: {result.Ms:F4} мс";
        }
        else
        {
            SearchInfoTextBlock.Text =
                $"Не найдено | {result.Name} | Значение: {result.Target} | Сравнений: {result.Comparisons} | Время: {result.Ms:F4} мс";
        }
    }
    finally
    {
        ToggleSearchControls(true);
    }
}

private void SearchDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
{
    if (SearchDelayValueText != null)
        SearchDelayValueText.Text = $"{(int)SearchDelaySlider.Value} мс";
}

private void GenerateSearchArray()
{
    int size = GetSearchArraySize();

    _searchArray = DataGenerator.GenerateRandomNumbers(size, 5, 100)
        .OrderBy(x => x)
        .ToArray();

    RenderSearchBars(_searchArray);
    SetRandomExistingTarget();

    SearchInfoTextBlock.Text = "Отсортированный массив для поиска создан";
}

private void SetRandomExistingTarget()
{
    if (_searchArray.Length == 0)
        return;

    int target = _searchArray[Random.Shared.Next(_searchArray.Length)];
    SearchTargetTextBox.Text = target.ToString();
}

private int GetSearchArraySize()
{
    if (!int.TryParse(SearchSizeTextBox.Text, out int size))
        size = 20;

    if (size < 5)
        size = 5;

    if (size > 40)
        size = 40;

    SearchSizeTextBox.Text = size.ToString();

    return size;
}

private void RenderSearchBars(int[] array)
{
    _searchBars.Clear();

    int maxValue = array.Max();

    foreach (int value in array)
    {
        double height = value / (double)maxValue * ChartHeight;
        _searchBars.Add(new BarItem(value, height));
    }
}

private void ApplySearchStep(string algorithmName, SearchStep step)
{
    if (algorithmName == "Linear Search")
        ApplyLinearSearchStep(step);
    else
        ApplyBinarySearchStep(step);
}

private void ApplyLinearSearchStep(SearchStep step)
{
    for (int i = 0; i < _searchBars.Count; i++)
    {
        if (i == step.FoundIndex)
            _searchBars[i].SetFound();
        else if (i == step.CurrentIndex)
            _searchBars[i].SetCurrent();
        else if (i < step.CurrentIndex)
            _searchBars[i].SetInactive();
        else
            _searchBars[i].SetDefault();
    }
}

private void ApplyBinarySearchStep(SearchStep step)
{
    for (int i = 0; i < _searchBars.Count; i++)
    {
        if (i == step.FoundIndex)
            _searchBars[i].SetFound();
        else if (i == step.CurrentIndex)
            _searchBars[i].SetCurrent();
        else if (i < step.LeftIndex || i > step.RightIndex)
            _searchBars[i].SetInactive();
        else
            _searchBars[i].SetDefault();
    }
}

private void MarkFoundBar(int index)
{
    if (index < 0 || index >= _searchBars.Count)
        return;

    _searchBars[index].SetFound();
}

private void ToggleSearchControls(bool isEnabled)
{
    GenerateSearchButton.IsEnabled = isEnabled;
    StartSearchButton.IsEnabled = isEnabled;
    RandomTargetButton.IsEnabled = isEnabled;

    SearchSizeTextBox.IsEnabled = isEnabled;
    SearchAlgorithmComboBox.IsEnabled = isEnabled;
    SearchTargetTextBox.IsEnabled = isEnabled;
    SearchDelaySlider.IsEnabled = isEnabled;
}
}
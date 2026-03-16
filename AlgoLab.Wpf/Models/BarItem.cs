using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace AlgoLab.Wpf.Models;

public class BarItem : INotifyPropertyChanged
{
    private static readonly Brush DefaultBrush = CreateBrush("#4F46E5");
    private static readonly Brush SortedBrush = CreateBrush("#22C55E");
    private static readonly Brush CurrentBrush = CreateBrush("#F59E0B");
    private static readonly Brush InactiveBrush = CreateBrush("#6B7280");
    private static readonly Brush FoundBrush = CreateBrush("#22C55E");

    private Brush _fill = DefaultBrush;

    public int Value { get; }
    public double Height { get; }

    public Brush Fill
    {
        get => _fill;
        private set
        {
            _fill = value;
            OnPropertyChanged();
        }
    }

    public BarItem(int value, double height)
    {
        Value = value;
        Height = height;
    }

    public void SetDefault() => Fill = DefaultBrush;
    public void SetSorted() => Fill = SortedBrush;
    public void SetCurrent() => Fill = CurrentBrush;
    public void SetInactive() => Fill = InactiveBrush;
    public void SetFound() => Fill = FoundBrush;

    private static Brush CreateBrush(string hex)
    {
        var brush = (SolidColorBrush)new BrushConverter().ConvertFromString(hex)!;
        brush.Freeze();
        return brush;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
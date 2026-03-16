namespace AlgoLab.Core.Utilities;

public static class DataGenerator
{
    public static int[] GenerateRandomNumbers(int size, int minValue = 0, int maxValue = 1000)
    {
        if(size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Размер массива должен быть больше 0.");

        if (minValue >= maxValue)
            throw new ArgumentException("minValue должен быть меньше maxValue.");
        
        var array = new int[size];
        
        for (int i = 0; i < size; i++)
            array[i] = Random.Shared.Next(minValue, maxValue);
        
        return array;
    }
}
var sumOfExtrapolatedValues = 0;

const int firstSequenceValuesCount = 21;

var subSequenceIndexToItsValues = new Dictionary<int, int[]>
{
    { 0, new int[firstSequenceValuesCount - 1] },
    { 1, new int[firstSequenceValuesCount - 2] },
    { 2, new int[firstSequenceValuesCount - 3] },
    { 3, new int[firstSequenceValuesCount - 4] },
    { 4, new int[firstSequenceValuesCount - 5] },
    { 5, new int[firstSequenceValuesCount - 6] },
    { 6, new int[firstSequenceValuesCount - 7] },
    { 7, new int[firstSequenceValuesCount - 8] },
    { 8, new int[firstSequenceValuesCount - 9] },
    { 9, new int[firstSequenceValuesCount - 10] },
    { 10, new int[firstSequenceValuesCount - 11] },
    { 11, new int[firstSequenceValuesCount - 12] },
    { 12, new int[firstSequenceValuesCount - 13] },
    { 13, new int[firstSequenceValuesCount - 14] },
    { 14, new int[firstSequenceValuesCount - 15] },
    { 15, new int[firstSequenceValuesCount - 16] },
    { 16, new int[firstSequenceValuesCount - 17] },
    { 17, new int[firstSequenceValuesCount - 18] },
    { 18, new int[firstSequenceValuesCount - 19] },
    { 19, new int[firstSequenceValuesCount - 20] }
};

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    var firstSequenceValues = line.Split(' ').Select(int.Parse).ToArray();

    sumOfExtrapolatedValues += GetExtrapolatedValue(firstSequenceValues, 0);
}

Console.WriteLine($"Sum of the extrapolated values: {sumOfExtrapolatedValues}.");
Console.ReadLine();

int GetExtrapolatedValue(int[] sequenceValues, int subSequenceIndex)
{
    var subSequenceValues = subSequenceIndexToItsValues[subSequenceIndex];

    for (var i = 0; i < subSequenceValues.Length; i++)
    {
        subSequenceValues[i] = sequenceValues[i + 1] - sequenceValues[i];
    }

    var subExtrapolatedValue = 0;

    if (subSequenceValues.Any(subSequenceValue => subSequenceValue != 0))
    {
        subExtrapolatedValue = GetExtrapolatedValue(subSequenceValues, subSequenceIndex + 1);
    }

    return sequenceValues[0] - subExtrapolatedValue;
}
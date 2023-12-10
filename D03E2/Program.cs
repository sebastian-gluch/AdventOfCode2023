var sumOfAllGearRatios = 0;

var gearIndexToPartNumbers = new Dictionary<string, List<string>>();

var lines = File.ReadAllLines("input.txt");

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    for (var j = 0; j < line.Length; j++)
    {
        var currentChar = line[j];

        if (char.IsDigit(currentChar))
        {
            var partNumber = string.Empty;
            var partNumberStartIndex = j;

            do
            {
                partNumber += currentChar;

                j++;

                if (j == line.Length)
                {
                    break;
                }

                currentChar = line[j];
            }
            while (char.IsDigit(currentChar));

            var partNumberEndIndex = partNumberStartIndex + partNumber.Length - 1;

            FindGearsAdjacentToPartNumber(lines, i, partNumberStartIndex, partNumberEndIndex, AddPartNumberToGearIndex);

            void AddPartNumberToGearIndex(string gearIndex)
            {
                if (gearIndexToPartNumbers.TryGetValue(gearIndex, out var partNumbers))
                {
                    partNumbers.Add(partNumber);
                }
                else
                {
                    gearIndexToPartNumbers.Add(gearIndex, new List<string> { partNumber });
                }
            }
        }
    }
}

foreach (var partNumbers in gearIndexToPartNumbers.Values)
{
    if (partNumbers.Count == 2)
    {
        sumOfAllGearRatios += int.Parse(partNumbers[0]) * int.Parse(partNumbers[1]);
    }
}

Console.WriteLine($"Sum of all of the gear ratios in the engine schematic: {sumOfAllGearRatios}.");
Console.ReadLine();

static void FindGearsAdjacentToPartNumber(string[] lines, int partNumberLineIndex, int partNumberStartIndex, int partNumberEndIndex, Action<string> addPartNumberToGearIndex)
{
    var partNumberLine = lines[partNumberLineIndex];

    if (partNumberStartIndex != 0 && IsGear(partNumberLine[partNumberStartIndex - 1]))
    {
        addPartNumberToGearIndex(GetGearIndex(partNumberLineIndex, partNumberStartIndex - 1));
    }

    if (partNumberEndIndex != partNumberLine.Length - 1 && IsGear(partNumberLine[partNumberEndIndex + 1]))
    {
        addPartNumberToGearIndex(GetGearIndex(partNumberLineIndex, partNumberEndIndex + 1));
    }

    if (partNumberLineIndex != 0)
    {
        FindGearsInAdjacentLine(partNumberLineIndex - 1);
    }

    if (partNumberLineIndex != lines.Length - 1)
    {
        FindGearsInAdjacentLine(partNumberLineIndex + 1);
    }

    void FindGearsInAdjacentLine(int partNumberAdjacentLineIndex)
    {
        var partNumberAdjacentLine = lines[partNumberAdjacentLineIndex];

        for (var i = partNumberStartIndex - 1; i <= partNumberEndIndex + 1; i++)
        {
            if (i > -1 && i < partNumberAdjacentLine.Length && IsGear(partNumberAdjacentLine[i]))
            {
                addPartNumberToGearIndex(GetGearIndex(partNumberAdjacentLineIndex, i));
            }
        }
    }

    string GetGearIndex(int lineIndex, int gearCharIndex)
    {
        const char gearIndexSeparatorChar = '-';

        return string.Concat(lineIndex, gearIndexSeparatorChar, gearCharIndex);
    }
}

static bool IsGear(char charToCheck)
{
    const char gearChar = '*';

    return charToCheck == gearChar;
}
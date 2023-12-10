var sumOfAllPartNumbers = 0;

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

            if (IsPartNumberAdjacentToSymbol(lines, i, partNumberStartIndex, partNumberEndIndex))
            {
                sumOfAllPartNumbers += int.Parse(partNumber);
            }
        }
    }
}

Console.WriteLine($"Sum of all of the part numbers in the engine schematic: {sumOfAllPartNumbers}.");
Console.ReadLine();

static bool IsPartNumberAdjacentToSymbol(string[] lines, int partNumberLineIndex, int partNumberStartIndex, int partNumberEndIndex)
{
    var partNumberLine = lines[partNumberLineIndex];

    if (partNumberStartIndex != 0 && IsSymbol(partNumberLine[partNumberStartIndex - 1]))
    {
        return true;
    }

    if (partNumberEndIndex != partNumberLine.Length - 1 && IsSymbol(partNumberLine[partNumberEndIndex + 1]))
    {
        return true;
    }

    if (partNumberLineIndex != 0 && TryFindSymbolInAdjacentLine(lines[partNumberLineIndex - 1]))
    {
        return true;
    }

    if (partNumberLineIndex != lines.Length - 1 && TryFindSymbolInAdjacentLine(lines[partNumberLineIndex + 1]))
    {
        return true;
    }

    bool TryFindSymbolInAdjacentLine(string partNumberAdjacentLine)
    {
        for (var i = partNumberStartIndex - 1; i <= partNumberEndIndex + 1; i++)
        {
            if (i > -1 && i < partNumberAdjacentLine.Length && IsSymbol(partNumberAdjacentLine[i]))
            {
                return true;
            }
        }

        return false;
    }

    return false;
}

static bool IsSymbol(char charToCheck) => !IsDot(charToCheck) && !char.IsDigit(charToCheck);

static bool IsDot(char charToCheck)
{
    const char dotChar = '.';

    return charToCheck == dotChar;
}
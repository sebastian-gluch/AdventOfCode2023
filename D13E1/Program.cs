var summaryOfAllPatternNotes = 0;

const int numberOfRowsMultiplier = 100;

var currentPattern = new List<string>();

var lines = File.ReadAllLines("input.txt");

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    if (line.Length != 0 && i != lines.Length - 1)
    {
        currentPattern.Add(line);
    }
    else
    {
        FindAllVerticalLinesOfReflectionAndExtendSummary();
        FindAllHorizontalLinesOfReflectionAndExtendSummary();

        currentPattern.Clear();
    }
}

Console.WriteLine($"Number got after summarizing all of the pattern notes: {summaryOfAllPatternNotes}.");
Console.ReadLine();

void FindAllVerticalLinesOfReflectionAndExtendSummary()
{
    var baseRow = currentPattern.First();

    for (var i = 0; i < baseRow.Length - 1; i++)
    {
        var currColumnIndex = i;
        var nextColumnIndex = i + 1;

        var areColumnsEqual = AreColumnsEqual(currColumnIndex, nextColumnIndex);
        if (areColumnsEqual)
        {
            while (currColumnIndex != 0 &&
                   nextColumnIndex != baseRow.Length - 1)
            {
                currColumnIndex--;
                nextColumnIndex++;

                if (!AreColumnsEqual(currColumnIndex, nextColumnIndex))
                {
                    areColumnsEqual = false;
                    break;
                }
            }

            if (areColumnsEqual)
            {
                summaryOfAllPatternNotes += i + 1;
            }
        }
    }
}

bool AreColumnsEqual(int currColumnIndex, int nextColumnIndex)
{
    foreach (var row in currentPattern)
    {
        if (row[currColumnIndex] != row[nextColumnIndex])
        {
            return false;
        }
    }

    return true;
}

void FindAllHorizontalLinesOfReflectionAndExtendSummary()
{
    var baseRow = currentPattern.First();

    for (var i = 0; i < currentPattern.Count - 1; i++)
    {
        var currRowIndex = i;
        var nextRowIndex = i + 1;

        var areRowsEqual = AreRowsEqual(currRowIndex, nextRowIndex, baseRow);
        if (areRowsEqual)
        {
            while (currRowIndex != 0 &&
                   nextRowIndex != currentPattern.Count - 1)
            {
                currRowIndex--;
                nextRowIndex++;

                if (!AreRowsEqual(currRowIndex, nextRowIndex, baseRow))
                {
                    areRowsEqual = false;
                    break;
                }
            }

            if (areRowsEqual)
            {
                summaryOfAllPatternNotes += (i + 1) * numberOfRowsMultiplier;
            }
        }
    }
}

bool AreRowsEqual(int currRowIndex, int nextRowIndex, string baseRow)
{
    var currRow = currentPattern[currRowIndex];
    var nextRow = currentPattern[nextRowIndex];

    for (var columnIndex = 0; columnIndex < baseRow.Length; columnIndex++)
    {
        if (currRow[columnIndex] != nextRow[columnIndex])
        {
            return false;
        }
    }

    return true;
}
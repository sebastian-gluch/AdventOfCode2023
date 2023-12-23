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

        var numberOfDifferencesBetweenColumns = GetNumberOfDifferencesBetweenColumns(currColumnIndex, nextColumnIndex);
        if (numberOfDifferencesBetweenColumns <= 1)
        {
            while (currColumnIndex != 0 &&
                   nextColumnIndex != baseRow.Length - 1)
            {
                currColumnIndex--;
                nextColumnIndex++;

                numberOfDifferencesBetweenColumns += GetNumberOfDifferencesBetweenColumns(currColumnIndex, nextColumnIndex);
                if (numberOfDifferencesBetweenColumns > 1)
                {
                    break;
                }
            }

            if (numberOfDifferencesBetweenColumns == 1)
            {
                summaryOfAllPatternNotes += i + 1;
            }
        }
    }
}

int GetNumberOfDifferencesBetweenColumns(int currColumnIndex, int nextColumnIndex)
{
    var foundDifferences = 0;

    foreach (var row in currentPattern)
    {
        if (row[currColumnIndex] != row[nextColumnIndex])
        {
            foundDifferences++;
        }
    }

    return foundDifferences;
}

void FindAllHorizontalLinesOfReflectionAndExtendSummary()
{
    var baseRow = currentPattern.First();

    for (var i = 0; i < currentPattern.Count - 1; i++)
    {
        var currRowIndex = i;
        var nextRowIndex = i + 1;

        var numberOfDifferencesBetweenRows = GetNumberOfDifferencesBetweenRows(currRowIndex, nextRowIndex, baseRow);
        if (numberOfDifferencesBetweenRows <= 1)
        {
            while (currRowIndex != 0 &&
                   nextRowIndex != currentPattern.Count - 1)
            {
                currRowIndex--;
                nextRowIndex++;

                numberOfDifferencesBetweenRows += GetNumberOfDifferencesBetweenRows(currRowIndex, nextRowIndex, baseRow);
                if (numberOfDifferencesBetweenRows > 1)
                {
                    break;
                }
            }

            if (numberOfDifferencesBetweenRows == 1)
            {
                summaryOfAllPatternNotes += (i + 1) * numberOfRowsMultiplier;
            }
        }
    }
}

int GetNumberOfDifferencesBetweenRows(int currRowIndex, int nextRowIndex, string baseRow)
{
    var foundDifferences = 0;

    var currRow = currentPattern[currRowIndex];
    var nextRow = currentPattern[nextRowIndex];

    for (var columnIndex = 0; columnIndex < baseRow.Length; columnIndex++)
    {
        if (currRow[columnIndex] != nextRow[columnIndex])
        {
            foundDifferences++;
        }
    }

    return foundDifferences;
}
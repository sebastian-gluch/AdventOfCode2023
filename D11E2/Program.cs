var sumOfLengthsOfShortestPath = 0L;

const char galaxySymbol = '#';

const int valueToExpandImageWith = 999999;

var lines = File.ReadAllLines("input.txt");

var image = BuildImage();

var rowsWithEmptySpacesOnlyIndices = GetRowsWithEmptySpacesOnlyIndices();
var columnsWithEmptySpacesOnlyIndices = GetColumnsWithEmptySpacesOnlyIndices();

var galaxies = CollectGalaxies();

for (var i = 0; i < galaxies.Count - 1; i++)
{
    var galaxy = galaxies[i];

    for (var j = i + 1; j < galaxies.Count; j++)
    {
        sumOfLengthsOfShortestPath += galaxy.GetLengthOfShortestPathTo(galaxies[j]);
    }
}

Console.WriteLine($"Sum of the lengths of the shortest path between every pair of galaxies: {sumOfLengthsOfShortestPath}.");
Console.ReadLine();

List<List<char>> BuildImage()
{
    var image = new List<List<char>>();

    foreach (var line in lines)
    {
        image.Add([.. line]);
    }

    return image;
}

List<int> GetRowsWithEmptySpacesOnlyIndices()
{
    var rowsWithEmptySpacesOnlyIndices = new List<int>();

    for (var i = 0; i < image.Count; i++)
    {
        var imageRow = image[i];
        var doesRowContainOnlyEmptySpaces = true;

        foreach (var imageRowChar in imageRow)
        {
            if (imageRowChar == galaxySymbol)
            {
                doesRowContainOnlyEmptySpaces = false;
                break;
            }
        }

        if (doesRowContainOnlyEmptySpaces)
        {
            rowsWithEmptySpacesOnlyIndices.Add(i);
        }
    }

    return rowsWithEmptySpacesOnlyIndices;
}

List<int> GetColumnsWithEmptySpacesOnlyIndices()
{
    var imageFirstRow = image.First();
    var columnsWithEmptySpacesOnlyIndices = new List<int>();

    for (var i = 0; i < imageFirstRow.Count; i++)
    {
        if (imageFirstRow[i] == galaxySymbol)
        {
            continue;
        }

        var doesColumnContainOnlyEmptySpaces = true;

        for (var j = 1; j < image.Count; j++)
        {
            if (image[j][i] == galaxySymbol)
            {
                doesColumnContainOnlyEmptySpaces = false;
                break;
            }
        }

        if (doesColumnContainOnlyEmptySpaces)
        {
            columnsWithEmptySpacesOnlyIndices.Add(i);
        }
    }

    return columnsWithEmptySpacesOnlyIndices;
}

List<Galaxy> CollectGalaxies()
{
    var galaxies = new List<Galaxy>();

    for (var i = 0; i < image.Count; i++)
    {
        var imageRow = image[i];

        for (var j = 0; j < imageRow.Count; j++)
        {
            if (imageRow[j] == galaxySymbol)
            {
                var rowIndex = i + GetNumberOfIndicesSmallerThan(i, rowsWithEmptySpacesOnlyIndices) * valueToExpandImageWith;
                var columnIndex = j + GetNumberOfIndicesSmallerThan(j, columnsWithEmptySpacesOnlyIndices) * valueToExpandImageWith;

                galaxies.Add(new Galaxy(rowIndex, columnIndex));
            }
        }
    }

    return galaxies;
}

int GetNumberOfIndicesSmallerThan(int indexToCheck, IEnumerable<int> indices)
{
    var numberOfSmallerIndices = 0;

    foreach (var index in indices)
    {
        if (index < indexToCheck)
        {
            numberOfSmallerIndices++;
            continue;
        }

        break;
    }

    return numberOfSmallerIndices;
}

internal class Galaxy(int rowIndex, int columnIndex)
{
    private readonly int rowIndex = rowIndex;
    private readonly int columnIndex = columnIndex;

    public int GetLengthOfShortestPathTo(Galaxy anotherGalaxy)
    {
        return Math.Abs(rowIndex - anotherGalaxy.rowIndex) +
               Math.Abs(columnIndex - anotherGalaxy.columnIndex);
    }
}
var sumOfLengthsOfShortestPath = 0;

const char emptySpaceSymbol = '.';
const char galaxySymbol = '#';

var lines = File.ReadAllLines("input.txt");

var image = BuildImage();

ExpandImageByAnalyzingItsRows();
ExpandImageByAnalyzingItsColumns();

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

void ExpandImageByAnalyzingItsRows()
{
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
            image.Insert(i, [.. imageRow]);
            i++;
        }
    }
}

void ExpandImageByAnalyzingItsColumns()
{
    var imageFirstRow = image.First();
    var columnsWithEmptySpacesOnlyIndices = new List<int>();

    for (var i = imageFirstRow.Count - 1; i >= 0; i--)
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

    foreach (var columnWithEmptySpacesOnlyIndex in columnsWithEmptySpacesOnlyIndices)
    {
        foreach (var imageRow in image)
        {
            imageRow.Insert(columnWithEmptySpacesOnlyIndex, emptySpaceSymbol);
        }
    }
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
                galaxies.Add(new Galaxy(i, j));
            }
        }
    }

    return galaxies;
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
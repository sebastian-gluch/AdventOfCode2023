var sumOfPossibleGamesIds = 0;

const string gameIdPartPrefix = "Game ";

const string redCubesCountSuffix = "red";
const string greenCubesCountSuffix = "green";
const string blueCubesCountSuffix = "blue";

const int availableRedCubesCount = 12;
const int availableGreenCubesCount = 13;
const int availableBlueCubesCount = 14;

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    var lineSplitByColon = line.Split(':');

    var gameId = int.Parse(lineSplitByColon[0][gameIdPartPrefix.Length..]);
    var subsetsOfCubes = lineSplitByColon[1];

    var isGamePossible = true;

    foreach (var subsetOfCubes in subsetsOfCubes.Split(';'))
    {
        foreach (var oneColorCubesCount in subsetOfCubes.Split(','))
        {
            if (DoesItExceedAvailableOneColorCubesCount(redCubesCountSuffix, availableRedCubesCount) ||
                DoesItExceedAvailableOneColorCubesCount(greenCubesCountSuffix, availableGreenCubesCount) ||
                DoesItExceedAvailableOneColorCubesCount(blueCubesCountSuffix, availableBlueCubesCount))
            {
                isGamePossible = false;
                break;
            }

            bool DoesItExceedAvailableOneColorCubesCount(string oneColorCubesCountSuffix, int availableOneColorCubesCount)
            {
                var indexOfOneColorCubesCountSuffix = oneColorCubesCount.IndexOf(oneColorCubesCountSuffix, StringComparison.Ordinal);
                if (indexOfOneColorCubesCountSuffix != -1)
                {
                    if (int.Parse(oneColorCubesCount[..indexOfOneColorCubesCountSuffix].Trim()) > availableOneColorCubesCount)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        if (!isGamePossible)
        {
            break;
        }
    }

    if (isGamePossible)
    {
        sumOfPossibleGamesIds += gameId;
    }
}

Console.WriteLine($"Sum of the possible games IDs: {sumOfPossibleGamesIds}.");
Console.ReadLine();
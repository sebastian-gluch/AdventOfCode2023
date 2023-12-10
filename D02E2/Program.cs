var sumOfPowerOfMinimumSetsOfCubes = 0;

const string redCubesCountSuffix = "red";
const string greenCubesCountSuffix = "green";
const string blueCubesCountSuffix = "blue";

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    var subsetsOfCubes = line.Split(':')[1];

    var minimumSetOfRedCubes = 0;
    var minimumSetOfGreenCubes = 0;
    var minimumSetOfBlueCubes = 0;

    foreach (var subsetOfCubes in subsetsOfCubes.Split(';'))
    {
        foreach (var oneColorCubesCount in subsetOfCubes.Split(','))
        {
            UpdateMinimumSetOfOneColorCubesIfRequired(redCubesCountSuffix, ref minimumSetOfRedCubes);
            UpdateMinimumSetOfOneColorCubesIfRequired(greenCubesCountSuffix, ref minimumSetOfGreenCubes);
            UpdateMinimumSetOfOneColorCubesIfRequired(blueCubesCountSuffix, ref minimumSetOfBlueCubes);

            void UpdateMinimumSetOfOneColorCubesIfRequired(string oneColorCubesCountSuffix, ref int minimumSetOfOneColorCubes)
            {
                var indexOfOneColorCubesCountSuffix = oneColorCubesCount.IndexOf(oneColorCubesCountSuffix, StringComparison.Ordinal);
                if (indexOfOneColorCubesCountSuffix != -1)
                {
                    var minimumSetOfOneColorCubesInSubset = int.Parse(oneColorCubesCount[..indexOfOneColorCubesCountSuffix].Trim());
                    if (minimumSetOfOneColorCubesInSubset > minimumSetOfOneColorCubes)
                    {
                        minimumSetOfOneColorCubes = minimumSetOfOneColorCubesInSubset;
                    }
                }
            }
        }
    }

    sumOfPowerOfMinimumSetsOfCubes += minimumSetOfRedCubes * minimumSetOfGreenCubes * minimumSetOfBlueCubes;
}

Console.WriteLine($"Sum of the power of the minimum sets of cubes: {sumOfPowerOfMinimumSetsOfCubes}.");
Console.ReadLine();
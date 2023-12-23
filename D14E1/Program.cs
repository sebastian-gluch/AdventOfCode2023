var totalLoadOnNorthSupportBeams = 0;

const char emptySpaceSymbol = '.';
const char roundedRockSymbol = 'O';
const char cubeShapedRockSymbol = '#';

var platform = File.ReadAllLines("input.txt").Select(line => line.ToCharArray()).ToArray();

var columnsCount = platform.First().Length;

for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
{
    var numberOfNonCubeShapedRocksInSeries = 0;
    var numberOfRoundedRocksInSeries = 0;

    for (var rowIndex = 0; rowIndex <= platform.Length; rowIndex++)
    {
        if (rowIndex == platform.Length)
        {
            SlideRoundedRocksFromSeriesNorth();
            break;
        }

        switch (platform[rowIndex][columnIndex])
        {
            case emptySpaceSymbol:
                numberOfNonCubeShapedRocksInSeries++;
                break;

            case roundedRockSymbol:
                numberOfNonCubeShapedRocksInSeries++;
                numberOfRoundedRocksInSeries++;
                break;

            case cubeShapedRockSymbol:
                SlideRoundedRocksFromSeriesNorth();
                break;

            default:
                throw new ArgumentException("Unsupported symbol!");
        }

        void SlideRoundedRocksFromSeriesNorth()
        {
            if (numberOfNonCubeShapedRocksInSeries != 0)
            {
                if (numberOfRoundedRocksInSeries != 0)
                {
                    for (var i = 0; i < numberOfRoundedRocksInSeries; i++)
                    {
                        platform[rowIndex - numberOfNonCubeShapedRocksInSeries + i][columnIndex] = roundedRockSymbol;
                    }

                    for (var i = numberOfNonCubeShapedRocksInSeries - numberOfRoundedRocksInSeries; i > 0; i--)
                    {
                        platform[rowIndex - i][columnIndex] = emptySpaceSymbol;
                    }

                    numberOfRoundedRocksInSeries = 0;
                }

                numberOfNonCubeShapedRocksInSeries = 0;
            }
        }
    }
}

for (var rowIndex = 0; rowIndex < platform.Length; rowIndex++)
{
    for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
    {
        if (platform[rowIndex][columnIndex] == roundedRockSymbol)
        {
            totalLoadOnNorthSupportBeams += platform.Length - rowIndex;
        }
    }
}

Console.WriteLine($"Total load on the north support beams: {totalLoadOnNorthSupportBeams}.");
Console.ReadLine();
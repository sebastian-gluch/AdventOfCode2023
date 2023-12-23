var totalLoadOnNorthSupportBeams = 0;

const char emptySpaceSymbol = '.';
const char roundedRockSymbol = 'O';
const char cubeShapedRockSymbol = '#';

const int numberOfCyclesToBePerformed = 1000000000;

var platform = File.ReadAllLines("input.txt").Select(line => line.ToCharArray()).ToArray();
var columnsCount = platform.First().Length;

var numberOfPerformedCycles = 0;
var platformPreviousStates = new List<string[]>();

while (numberOfPerformedCycles < numberOfCyclesToBePerformed)
{
    PerformCycle();

    numberOfPerformedCycles++;

    var platformCurrentState = platform.Select(chars => new string(chars)).ToArray();
    var wasCyclesLoopDetected = false;

    for (var i = 0; i < platformPreviousStates.Count; i++)
    {
        if (platformPreviousStates[i].SequenceEqual(platformCurrentState))
        {
            var sizeOfCyclesLoop = numberOfPerformedCycles - i - 1;
            var indexOfPlatformTargetStateInCyclesLoop = (numberOfCyclesToBePerformed - i - 1) % sizeOfCyclesLoop;
            var platformTargetState = platformPreviousStates[i + indexOfPlatformTargetStateInCyclesLoop];

            platform = platformTargetState.Select(line => line.ToCharArray()).ToArray();
            wasCyclesLoopDetected = true;
            break;
        }
    }

    if (wasCyclesLoopDetected)
    {
        break;
    }

    platformPreviousStates.Add(platformCurrentState);
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

File.WriteAllLines("output.txt", platform.Select(chars => new string(chars)));

Console.WriteLine($"Total load on the north support beams: {totalLoadOnNorthSupportBeams}.");
Console.ReadLine();

void PerformCycle()
{
    SlideRoundedRocksVertically(true);
    SlideRoundedRocksHorizontally(true);
    SlideRoundedRocksVertically(false);
    SlideRoundedRocksHorizontally(false);
}

void SlideRoundedRocksVertically(bool slideNorth)
{
    for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
    {
        var numberOfNonCubeShapedRocksInSeries = 0;
        var numberOfRoundedRocksInSeries = 0;

        for (var rowIndex = 0; rowIndex <= platform.Length; rowIndex++)
        {
            if (rowIndex == platform.Length)
            {
                SlideRoundedRocksFromSeriesVertically();
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
                    SlideRoundedRocksFromSeriesVertically();
                    break;

                default:
                    throw new ArgumentException("Unsupported symbol!");
            }

            void SlideRoundedRocksFromSeriesVertically()
            {
                if (numberOfNonCubeShapedRocksInSeries != 0)
                {
                    if (numberOfRoundedRocksInSeries != 0)
                    {
                        if (slideNorth)
                        {
                            for (var i = 0; i < numberOfRoundedRocksInSeries; i++)
                            {
                                platform[rowIndex - numberOfNonCubeShapedRocksInSeries + i][columnIndex] = roundedRockSymbol;
                            }

                            for (var i = numberOfNonCubeShapedRocksInSeries - numberOfRoundedRocksInSeries; i > 0; i--)
                            {
                                platform[rowIndex - i][columnIndex] = emptySpaceSymbol;
                            }
                        }
                        else
                        {
                            for (var i = 0; i < numberOfNonCubeShapedRocksInSeries - numberOfRoundedRocksInSeries; i++)
                            {
                                platform[rowIndex - numberOfNonCubeShapedRocksInSeries + i][columnIndex] = emptySpaceSymbol;
                            }

                            for (var i = numberOfRoundedRocksInSeries; i > 0; i--)
                            {
                                platform[rowIndex - i][columnIndex] = roundedRockSymbol;
                            }
                        }

                        numberOfRoundedRocksInSeries = 0;
                    }

                    numberOfNonCubeShapedRocksInSeries = 0;
                }
            }
        }
    }
}

void SlideRoundedRocksHorizontally(bool slideWest)
{
    for (var rowIndex = 0; rowIndex < platform.Length; rowIndex++)
    {
        var numberOfNonCubeShapedRocksInSeries = 0;
        var numberOfRoundedRocksInSeries = 0;

        for (var columnIndex = 0; columnIndex <= columnsCount; columnIndex++)
        {
            if (columnIndex == columnsCount)
            {
                SlideRoundedRocksFromSeriesHorizontally();
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
                    SlideRoundedRocksFromSeriesHorizontally();
                    break;

                default:
                    throw new ArgumentException("Unsupported symbol!");
            }

            void SlideRoundedRocksFromSeriesHorizontally()
            {
                if (numberOfNonCubeShapedRocksInSeries != 0)
                {
                    if (numberOfRoundedRocksInSeries != 0)
                    {
                        if (slideWest)
                        {
                            for (var i = 0; i < numberOfRoundedRocksInSeries; i++)
                            {
                                platform[rowIndex][columnIndex - numberOfNonCubeShapedRocksInSeries + i] = roundedRockSymbol;
                            }

                            for (var i = numberOfNonCubeShapedRocksInSeries - numberOfRoundedRocksInSeries; i > 0; i--)
                            {
                                platform[rowIndex][columnIndex - i] = emptySpaceSymbol;
                            }
                        }
                        else
                        {
                            for (var i = 0; i < numberOfNonCubeShapedRocksInSeries - numberOfRoundedRocksInSeries; i++)
                            {
                                platform[rowIndex][columnIndex - numberOfNonCubeShapedRocksInSeries + i] = emptySpaceSymbol;
                            }

                            for (var i = numberOfRoundedRocksInSeries; i > 0; i--)
                            {
                                platform[rowIndex][columnIndex - i] = roundedRockSymbol;
                            }
                        }

                        numberOfRoundedRocksInSeries = 0;
                    }

                    numberOfNonCubeShapedRocksInSeries = 0;
                }
            }
        }
    }
}
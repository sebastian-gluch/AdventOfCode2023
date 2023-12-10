var stepsItTakesToGetToFarthestPoint = 0;

const char startingPointSymbol = 'S';

var pipeSymbolToDirectionInfo = new Dictionary<char, DirectionInfo>
{
    { '.', new GroundDirectionInfo() },
    { '|', new VerticalPipeDirectionInfo() },
    { '-', new HorizontalPipeDirectionInfo() },
    { 'L', new NinetyDegreeBendNorthAndEastDirectionInfo() },
    { 'J', new NinetyDegreeBendNorthAndWestDirectionInfo() },
    { 'F', new NinetyDegreeBendSouthAndEastDirectionInfo() },
    { '7', new NinetyDegreeBendSouthAndWestDirectionInfo() }
};

var lines = File.ReadAllLines("input.txt");

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    var indexOfStartingPoint = line.IndexOf(startingPointSymbol);
    if (indexOfStartingPoint == -1)
    {
        continue;
    }

    var pointToStepsFromStartingPoint = new int?[line.Length, lines.Length];

    pointToStepsFromStartingPoint[indexOfStartingPoint, i] = 0;

    var pointsToGoNextWithDirectionFromWhichWeCome = new List<(int NextXCoordinate, int NextYCoordinate, Direction DirectionFromWhichWeCome)>
    {
        (indexOfStartingPoint, i - 1, Direction.South),
        (indexOfStartingPoint + 1, i, Direction.West),
        (indexOfStartingPoint, i + 1, Direction.North),
        (indexOfStartingPoint - 1, i, Direction.East)
    };

    while (true)
    {
        for (var j = 0; j < pointsToGoNextWithDirectionFromWhichWeCome.Count; j++)
        {
            var pointToGoNextWithDirectionFromWhichWeCome = pointsToGoNextWithDirectionFromWhichWeCome[j];

            var nextPointXCoordinate = pointToGoNextWithDirectionFromWhichWeCome.NextXCoordinate;
            var nextPointYCoordinate = pointToGoNextWithDirectionFromWhichWeCome.NextYCoordinate;

            if (pointToStepsFromStartingPoint[nextPointXCoordinate, nextPointYCoordinate] != null)
            {
                pointsToGoNextWithDirectionFromWhichWeCome.RemoveAt(j);
                j--;

                continue;
            }

            var directionInfo = pipeSymbolToDirectionInfo[lines[nextPointYCoordinate][nextPointXCoordinate]];
            if (directionInfo.TryGetDirectionToGoNext(pointToGoNextWithDirectionFromWhichWeCome.DirectionFromWhichWeCome, out var directionToGoNext))
            {
                pointToStepsFromStartingPoint[nextPointXCoordinate, nextPointYCoordinate] = stepsItTakesToGetToFarthestPoint + 1;

                var pointToGoNext = GetNextPointBasedOnDirection(nextPointXCoordinate, nextPointYCoordinate, directionToGoNext);

                pointsToGoNextWithDirectionFromWhichWeCome[j] = (pointToGoNext.NextXCoordinate, pointToGoNext.NextYCoordinate, GetOppositeDirection(directionToGoNext));
            }
            else
            {
                pointsToGoNextWithDirectionFromWhichWeCome.RemoveAt(j);
                j--;
            }
        }

        if (pointsToGoNextWithDirectionFromWhichWeCome.Count != 0)
        {
            stepsItTakesToGetToFarthestPoint++;
        }
        else
        {
            break;
        }
    }

    break;
}

Console.WriteLine($"Steps along the loop it takes to get from the starting position to the point farthest from the starting position: {stepsItTakesToGetToFarthestPoint}.");
Console.ReadLine();

(int NextXCoordinate, int NextYCoordinate) GetNextPointBasedOnDirection(int currentXCoordinate, int currentYCoordinate, Direction direction)
{
    switch (direction)
    {
        case Direction.North:
            return (currentXCoordinate, currentYCoordinate - 1);
        case Direction.East:
            return (currentXCoordinate + 1, currentYCoordinate);
        case Direction.South:
            return (currentXCoordinate, currentYCoordinate + 1);
        case Direction.West:
            return (currentXCoordinate - 1, currentYCoordinate);
        default:
            throw new ArgumentOutOfRangeException();
    }
}

Direction GetOppositeDirection(Direction direction)
{
    switch (direction)
    {
        case Direction.North:
            return Direction.South;
        case Direction.East:
            return Direction.West;
        case Direction.South:
            return Direction.North;
        case Direction.West:
            return Direction.East;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

internal abstract class DirectionInfo
{
    public abstract bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext);
}

internal class GroundDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        directionToGoNext = Direction.North;
        return false;
    }
}

internal class VerticalPipeDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.North:
                directionToGoNext = Direction.South;
                return true;
            case Direction.South:
                directionToGoNext = Direction.North;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal class HorizontalPipeDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.East:
                directionToGoNext = Direction.West;
                return true;
            case Direction.West:
                directionToGoNext = Direction.East;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal class NinetyDegreeBendNorthAndEastDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.North:
                directionToGoNext = Direction.East;
                return true;
            case Direction.East:
                directionToGoNext = Direction.North;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal class NinetyDegreeBendNorthAndWestDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.North:
                directionToGoNext = Direction.West;
                return true;
            case Direction.West:
                directionToGoNext = Direction.North;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal class NinetyDegreeBendSouthAndEastDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.South:
                directionToGoNext = Direction.East;
                return true;
            case Direction.East:
                directionToGoNext = Direction.South;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal class NinetyDegreeBendSouthAndWestDirectionInfo : DirectionInfo
{
    public override bool TryGetDirectionToGoNext(Direction directionFromWhichWeCome, out Direction directionToGoNext)
    {
        switch (directionFromWhichWeCome)
        {
            case Direction.South:
                directionToGoNext = Direction.West;
                return true;
            case Direction.West:
                directionToGoNext = Direction.South;
                return true;
            default:
                directionToGoNext = Direction.North;
                return false;
        }
    }
}

internal enum Direction
{
    North,
    East,
    South,
    West
}
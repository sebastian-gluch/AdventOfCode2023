var lines = File.ReadAllLines("input.txt");

var timeAllowed = long.Parse(lines[0].Split(':')[1].Replace(" ", ""));
var distanceToBeat = long.Parse(lines[1].Split(':')[1].Replace(" ", ""));

var numberOfWaysToBeatRecord = 0L;

for (var timeOfCharging = 1; timeOfCharging < timeAllowed; timeOfCharging++)
{
    var distanceReached = (timeAllowed - timeOfCharging) * timeOfCharging;
    if (distanceReached > distanceToBeat)
    {
        numberOfWaysToBeatRecord = timeAllowed + 1 - timeOfCharging * 2;
        break;
    }
}

Console.WriteLine($"Number of ways to beat the record in this one much longer race: {numberOfWaysToBeatRecord}.");
Console.ReadLine();
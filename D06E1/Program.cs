int? productOfNumbersOfWaysToBeatRecords = null;

var lines = File.ReadAllLines("input.txt");

var timesAllowed = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
var distancesToBeat = lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

for (var i = 0; i < timesAllowed.Length; i++)
{
    var timeAllowed = timesAllowed[i];
    var distanceToBeat = distancesToBeat[i];

    var numberOfWaysToBeatRecord = 0;

    for (var timeOfCharging = 1; timeOfCharging < timeAllowed; timeOfCharging++)
    {
        var distanceReached = (timeAllowed - timeOfCharging) * timeOfCharging;
        if (distanceReached > distanceToBeat)
        {
            numberOfWaysToBeatRecord++;
        }
    }

    if (!productOfNumbersOfWaysToBeatRecords.HasValue)
    {
        productOfNumbersOfWaysToBeatRecords = numberOfWaysToBeatRecord;
    }
    else
    {
        productOfNumbersOfWaysToBeatRecords *= numberOfWaysToBeatRecord;
    }
}

Console.WriteLine($"Product of the numbers of ways to beat the record in each race: {productOfNumbersOfWaysToBeatRecords!.Value}.");
Console.ReadLine();
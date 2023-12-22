var sumOfDifferentArrangements = 0;

const char operationalSpringSymbol = '.';
const char damagedSpringSymbol = '#';
const char unknownStateSpringSymbol = '?';

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    var lineSplit = line.Split(' ');

    var springsConditionRecords = lineSplit[0].ToArray();
    var groupsOfDamagedSpringsCounts = lineSplit[1].Split(',').Select(int.Parse).ToArray();

    var differentArrangements = 0;

    RecordDifferentArrangements(springsConditionRecords, groupsOfDamagedSpringsCounts, 0, ref differentArrangements);

    sumOfDifferentArrangements += differentArrangements;
}

Console.WriteLine($"Sum of the different arrangements of operational and broken springs that meet the given criteria: {sumOfDifferentArrangements}.");
Console.ReadLine();

static void RecordDifferentArrangements(char[] springsConditionRecords, int[] groupsOfDamagedSpringsCounts, int nextSpringToAnalyzeIndex, ref int differentArrangements)
{
    for (var i = nextSpringToAnalyzeIndex; i < springsConditionRecords.Length; i++)
    {
        if (springsConditionRecords[i] == unknownStateSpringSymbol)
        {
            springsConditionRecords[i] = operationalSpringSymbol;
            RecordDifferentArrangements(springsConditionRecords, groupsOfDamagedSpringsCounts, i + 1, ref differentArrangements);

            springsConditionRecords[i] = damagedSpringSymbol;
            RecordDifferentArrangements(springsConditionRecords, groupsOfDamagedSpringsCounts, i + 1, ref differentArrangements);

            springsConditionRecords[i] = unknownStateSpringSymbol;
            return;
        }
    }

    var currentGroupOfDamagedSpringsIndex = 0;
    var currentGroupOfDamagedSpringsCount = 0;

    for (var i = 0; i <= springsConditionRecords.Length; i++)
    {
        if (i != springsConditionRecords.Length && springsConditionRecords[i] == damagedSpringSymbol)
        {
            currentGroupOfDamagedSpringsCount++;
        }
        else if (currentGroupOfDamagedSpringsCount != 0)
        {
            if (currentGroupOfDamagedSpringsIndex == groupsOfDamagedSpringsCounts.Length ||
                currentGroupOfDamagedSpringsCount != groupsOfDamagedSpringsCounts[currentGroupOfDamagedSpringsIndex])
            {
                return;
            }

            currentGroupOfDamagedSpringsIndex++;
            currentGroupOfDamagedSpringsCount = 0;
        }
    }

    if (currentGroupOfDamagedSpringsIndex == groupsOfDamagedSpringsCounts.Length)
    {
        differentArrangements++;
    }
}
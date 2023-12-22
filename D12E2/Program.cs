using System.Text;

var sumOfDifferentArrangements = 0L;

const char operationalSpringSymbol = '.';
const char damagedSpringSymbol = '#';
const char unknownStateSpringSymbol = '?';

const char groupsOfDamagedSpringsSeparator = ',';

const int unfoldingMultiplier = 5;

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    var lineSplit = line.Split(' ');

    var springsConditionRecordsStr = UnfoldSpringsConditionRecordsString(lineSplit[0]);
    var groupsOfDamagedSpringsCountsStr = UnfoldGroupsOfDamagedSpringsCountsString(lineSplit[1]);

    sumOfDifferentArrangements += GetDifferentArrangements(springsConditionRecordsStr, groupsOfDamagedSpringsCountsStr);
}

Console.WriteLine($"Sum of the different arrangements of operational and broken springs that meet the given criteria: {sumOfDifferentArrangements}.");
Console.ReadLine();

static string UnfoldSpringsConditionRecordsString(string springsConditionRecordsStrToUnfold)
{
    var springsConditionRecordsStr = springsConditionRecordsStrToUnfold;

    for (var i = 0; i < unfoldingMultiplier - 1; i++)
    {
        springsConditionRecordsStr += unknownStateSpringSymbol;
        springsConditionRecordsStr += springsConditionRecordsStrToUnfold;
    }

    return springsConditionRecordsStr;
}

static string UnfoldGroupsOfDamagedSpringsCountsString(string groupsOfDamagedSpringsCountsStrToUnfold)
{
    var groupsOfDamagedSpringsCountsStr = groupsOfDamagedSpringsCountsStrToUnfold;

    for (var i = 0; i < unfoldingMultiplier - 1; i++)
    {
        groupsOfDamagedSpringsCountsStr += groupsOfDamagedSpringsSeparator;
        groupsOfDamagedSpringsCountsStr += groupsOfDamagedSpringsCountsStrToUnfold;
    }

    return groupsOfDamagedSpringsCountsStr;
}

static long GetDifferentArrangements(string springsConditionRecordsStr, string groupsOfDamagedSpringsCountsStr)
{
    var differentArrangements = 0L;

    var springsConditionSubRecordsStrs = CollectSpringsConditionSubRecordsStrs(springsConditionRecordsStr);
    var springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos = new Dictionary<string, IReadOnlyList<SubGroupsOfDamagedSpringsInfo>>();

    if (springsConditionSubRecordsStrs.Count == 1)
    {
        RecordDifferentArrangementsInSimpleCase(springsConditionSubRecordsStrs[0].ToCharArray(), groupsOfDamagedSpringsCountsStr.Split(groupsOfDamagedSpringsSeparator).Select(int.Parse).ToArray(), 0, 0, 0, ref differentArrangements);
    }
    else
    {
        foreach (var springsConditionSubRecordsStr in springsConditionSubRecordsStrs)
        {
            if (!springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos.ContainsKey(springsConditionSubRecordsStr))
            {
                var subGroupsOfDamagedSpringsCountsStrs = new List<SubGroupsOfDamagedSpringsInfo>();
                RecordSubGroupsOfDamagedSpringsInfos(springsConditionSubRecordsStr.ToCharArray(), groupsOfDamagedSpringsCountsStr, 0, subGroupsOfDamagedSpringsCountsStrs);
                springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos.Add(springsConditionSubRecordsStr, subGroupsOfDamagedSpringsCountsStrs);
            }
        }

        RecordDifferentArrangementsInComplexCase(springsConditionSubRecordsStrs, springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos, 1, string.Empty, groupsOfDamagedSpringsCountsStr, 0, ref differentArrangements);
    }

    return differentArrangements;
}

static IReadOnlyList<string> CollectSpringsConditionSubRecordsStrs(string springsConditionRecordsStr)
{
    var springsConditionSubRecordsStrs = new List<string>();
    var currSpringsConditionSubRecordStrBuilder = new StringBuilder();

    for (var i = 0; i <= springsConditionRecordsStr.Length; i++)
    {
        if (i != springsConditionRecordsStr.Length && springsConditionRecordsStr[i] != operationalSpringSymbol)
        {
            currSpringsConditionSubRecordStrBuilder.Append(springsConditionRecordsStr[i]);
        }
        else if (currSpringsConditionSubRecordStrBuilder.Length != 0)
        {
            springsConditionSubRecordsStrs.Add(currSpringsConditionSubRecordStrBuilder.ToString());
            currSpringsConditionSubRecordStrBuilder.Clear();
        }
    }

    return springsConditionSubRecordsStrs;
}

static void RecordDifferentArrangementsInSimpleCase(
    char[] springsConditionSubRecords,
    int[] groupsOfDamagedSpringsCounts,
    int nextSpringToAnalyzeIndex,
    int currentGroupOfDamagedSpringsIndex,
    int currentGroupOfDamagedSpringsCount,
    ref long differentArrangements)
{
    for (var i = nextSpringToAnalyzeIndex; i < springsConditionSubRecords.Length; i++)
    {
        if (springsConditionSubRecords[i] == unknownStateSpringSymbol)
        {
            if (currentGroupOfDamagedSpringsCount == 0)
            {
                if (currentGroupOfDamagedSpringsIndex == groupsOfDamagedSpringsCounts.Length)
                {
                    springsConditionSubRecords[i] = operationalSpringSymbol;
                    RecordDifferentArrangementsInSimpleCase(springsConditionSubRecords, groupsOfDamagedSpringsCounts, i + 1, currentGroupOfDamagedSpringsIndex, currentGroupOfDamagedSpringsCount, ref differentArrangements);
                }
                else
                {
                    springsConditionSubRecords[i] = damagedSpringSymbol;
                    RecordDifferentArrangementsInSimpleCase(springsConditionSubRecords, groupsOfDamagedSpringsCounts, i + 1, currentGroupOfDamagedSpringsIndex, 1, ref differentArrangements);

                    if (springsConditionSubRecords.Length - i > groupsOfDamagedSpringsCounts.Skip(currentGroupOfDamagedSpringsIndex).Sum() + groupsOfDamagedSpringsCounts.Length - currentGroupOfDamagedSpringsIndex - 1)
                    {
                        springsConditionSubRecords[i] = operationalSpringSymbol;
                        RecordDifferentArrangementsInSimpleCase(springsConditionSubRecords, groupsOfDamagedSpringsCounts, i + 1, currentGroupOfDamagedSpringsIndex, currentGroupOfDamagedSpringsCount, ref differentArrangements);
                    }
                }
            }
            else
            {
                if (currentGroupOfDamagedSpringsCount == groupsOfDamagedSpringsCounts[currentGroupOfDamagedSpringsIndex])
                {
                    springsConditionSubRecords[i] = operationalSpringSymbol;
                    RecordDifferentArrangementsInSimpleCase(springsConditionSubRecords, groupsOfDamagedSpringsCounts, i + 1, currentGroupOfDamagedSpringsIndex + 1, 0, ref differentArrangements);
                }
                else
                {
                    springsConditionSubRecords[i] = damagedSpringSymbol;
                    RecordDifferentArrangementsInSimpleCase(springsConditionSubRecords, groupsOfDamagedSpringsCounts, i + 1, currentGroupOfDamagedSpringsIndex, currentGroupOfDamagedSpringsCount + 1, ref differentArrangements);
                }
            }

            springsConditionSubRecords[i] = unknownStateSpringSymbol;
            return;
        }

        if (currentGroupOfDamagedSpringsIndex == groupsOfDamagedSpringsCounts.Length ||
            currentGroupOfDamagedSpringsCount == groupsOfDamagedSpringsCounts[currentGroupOfDamagedSpringsIndex])
        {
            return;
        }

        currentGroupOfDamagedSpringsCount++;
    }

    differentArrangements++;
}

static void RecordSubGroupsOfDamagedSpringsInfos(
    char[] springsConditionSubRecords,
    string groupsOfDamagedSpringsCountsStr,
    int nextSpringToAnalyzeIndex,
    List<SubGroupsOfDamagedSpringsInfo> subGroupsOfDamagedSpringsInfos)
{
    for (var i = nextSpringToAnalyzeIndex; i < springsConditionSubRecords.Length; i++)
    {
        if (springsConditionSubRecords[i] == unknownStateSpringSymbol)
        {
            springsConditionSubRecords[i] = operationalSpringSymbol;
            RecordSubGroupsOfDamagedSpringsInfos(springsConditionSubRecords, groupsOfDamagedSpringsCountsStr, i + 1, subGroupsOfDamagedSpringsInfos);

            springsConditionSubRecords[i] = damagedSpringSymbol;
            RecordSubGroupsOfDamagedSpringsInfos(springsConditionSubRecords, groupsOfDamagedSpringsCountsStr, i + 1, subGroupsOfDamagedSpringsInfos);

            springsConditionSubRecords[i] = unknownStateSpringSymbol;
            return;
        }
    }

    var subGroupsOfDamagedSpringsCountsStr = string.Empty;
    var currentGroupOfDamagedSpringsCount = 0;

    for (var i = 0; i <= springsConditionSubRecords.Length; i++)
    {
        if (i != springsConditionSubRecords.Length && springsConditionSubRecords[i] == damagedSpringSymbol)
        {
            currentGroupOfDamagedSpringsCount++;
        }
        else if (currentGroupOfDamagedSpringsCount != 0)
        {
            if (subGroupsOfDamagedSpringsCountsStr.Length != 0)
            {
                subGroupsOfDamagedSpringsCountsStr += groupsOfDamagedSpringsSeparator;
            }

            subGroupsOfDamagedSpringsCountsStr += currentGroupOfDamagedSpringsCount;
            currentGroupOfDamagedSpringsCount = 0;
        }
    }

    var subGroupsOfDamagedSpringsInfo = subGroupsOfDamagedSpringsInfos.FirstOrDefault(subGroupsOfDamagedSpringsInfo => subGroupsOfDamagedSpringsInfo.CountsStr == subGroupsOfDamagedSpringsCountsStr);
    if (subGroupsOfDamagedSpringsInfo != null)
    {
        subGroupsOfDamagedSpringsInfo.Occurrences++;
        return;
    }

    if (groupsOfDamagedSpringsCountsStr.StartsWith(subGroupsOfDamagedSpringsCountsStr))
    {
        subGroupsOfDamagedSpringsInfos.Add(new SubGroupsOfDamagedSpringsInfo(subGroupsOfDamagedSpringsCountsStr));
        return;
    }

    for (int i = 1, numberOfDifferentStartPoints = groupsOfDamagedSpringsCountsStr.Split(groupsOfDamagedSpringsSeparator).Length / unfoldingMultiplier; i < numberOfDifferentStartPoints; i++)
    {
        groupsOfDamagedSpringsCountsStr = groupsOfDamagedSpringsCountsStr[(groupsOfDamagedSpringsCountsStr.IndexOf(groupsOfDamagedSpringsSeparator) + 1)..];

        if (groupsOfDamagedSpringsCountsStr.StartsWith(subGroupsOfDamagedSpringsCountsStr))
        {
            subGroupsOfDamagedSpringsInfos.Add(new SubGroupsOfDamagedSpringsInfo(subGroupsOfDamagedSpringsCountsStr));
            break;
        }
    }
}

static void RecordDifferentArrangementsInComplexCase(
    IReadOnlyList<string> springsConditionSubRecordsStrs,
    IReadOnlyDictionary<string, IReadOnlyList<SubGroupsOfDamagedSpringsInfo>> springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos,
    long valueToExtendDifferentArrangementsBy,
    string groupsOfDamagedSpringsCountsStrActualCurrently,
    string groupsOfDamagedSpringsCountsStrExpected,
    int nextSpringsConditionSubRecordsStrToAnalyzeIndex,
    ref long differentArrangements)
{
    var springsConditionSubRecordsStr = springsConditionSubRecordsStrs[nextSpringsConditionSubRecordsStrToAnalyzeIndex];
    var subGroupsOfDamagedSpringsInfos = springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos[springsConditionSubRecordsStr];

    foreach (var subGroupsOfDamagedSpringsInfo in subGroupsOfDamagedSpringsInfos)
    {
        var groupsOfDamagedSpringsCountsStrActualExtended = groupsOfDamagedSpringsCountsStrActualCurrently.Length != 0
                ? subGroupsOfDamagedSpringsInfo.CountsStr.Length != 0 ? string.Join(groupsOfDamagedSpringsSeparator, groupsOfDamagedSpringsCountsStrActualCurrently, subGroupsOfDamagedSpringsInfo.CountsStr) : groupsOfDamagedSpringsCountsStrActualCurrently
                : subGroupsOfDamagedSpringsInfo.CountsStr;

        if (nextSpringsConditionSubRecordsStrToAnalyzeIndex == springsConditionSubRecordsStrs.Count - 1)
        {
            if (groupsOfDamagedSpringsCountsStrActualExtended == groupsOfDamagedSpringsCountsStrExpected)
            {
                differentArrangements += valueToExtendDifferentArrangementsBy * subGroupsOfDamagedSpringsInfo.Occurrences;
            }
        }
        else if (groupsOfDamagedSpringsCountsStrExpected.StartsWith(groupsOfDamagedSpringsCountsStrActualExtended))
        {
            RecordDifferentArrangementsInComplexCase(
                springsConditionSubRecordsStrs,
                springsConditionSubRecordStrToSubGroupsOfDamagedSpringsInfos,
                valueToExtendDifferentArrangementsBy * subGroupsOfDamagedSpringsInfo.Occurrences,
                groupsOfDamagedSpringsCountsStrActualExtended,
                groupsOfDamagedSpringsCountsStrExpected,
                nextSpringsConditionSubRecordsStrToAnalyzeIndex + 1,
                ref differentArrangements);
        }
    }
}

internal class SubGroupsOfDamagedSpringsInfo(string countsStr)
{
    public string CountsStr { get; } = countsStr;
    public int Occurrences { get; set; } = 1;
}
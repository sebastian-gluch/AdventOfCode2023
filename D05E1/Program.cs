const string seedName = "seed";
const string soilName = "soil";
const string fertilizerName = "fertilizer";
const string waterName = "water";
const string lightName = "light";
const string temperatureName = "temperature";
const string humidityName = "humidity";
const string locationName = "location";

var categoriesPairs = new List<(string SrcCategoryName, string DstCategoryName)>
{
    (seedName, soilName),
    (soilName, fertilizerName),
    (fertilizerName, waterName),
    (waterName, lightName),
    (lightName, temperatureName),
    (temperatureName, humidityName),
    (humidityName, locationName)
};

var lines = File.ReadAllLines("input.txt");

var seedsToBePlanted = lines[0].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse);

var categoriesPairToTranslationMaps = new Dictionary<(string SrcCategoryName, string DstCategoryName), List<TranslationMap>>();
categoriesPairs.ForEach(categoryPair => categoriesPairToTranslationMaps.Add(categoryPair, new List<TranslationMap>()));

(string SrcCategoryName, string DstCategoryName)? currCategoriesPair = null;

for (var i = 2; i < lines.Length; i++)
{
    var line = lines[i];

    if (!currCategoriesPair.HasValue)
    {
        var categoriesPairString = line.Split(' ')[0].Split('-');

        currCategoriesPair = (categoriesPairString[0], categoriesPairString[2]);
    }
    else if (line.Length == 0)
    {
        currCategoriesPair = null;
    }
    else
    {
        var translationMapString = line.Split(" ");
        var translationMap = new TranslationMap(long.Parse(translationMapString[1]), long.Parse(translationMapString[0]), long.Parse(translationMapString[2]));

        categoriesPairToTranslationMaps[currCategoriesPair.Value].Add(translationMap);
    }
}

var lowestLocationNumber = long.MaxValue;

foreach (var seedToBePlanted in seedsToBePlanted)
{
    var currCategoryNumber = seedToBePlanted;

    foreach (var categoryPair in categoriesPairs)
    {
        TranslationMap? translationMapToUse = null;

        foreach (var translationMap in categoriesPairToTranslationMaps[categoryPair])
        {
            if (translationMap.SrcRangeStart <= currCategoryNumber && currCategoryNumber < translationMap.SrcRangeStart + translationMap.RangeLength)
            {
                translationMapToUse = translationMap;
                break;
            }
        }

        if (translationMapToUse != null)
        {
            var delta = currCategoryNumber - translationMapToUse.SrcRangeStart;

            currCategoryNumber = translationMapToUse.DstRangeStart + delta;
        }
    }

    if (currCategoryNumber < lowestLocationNumber)
    {
        lowestLocationNumber = currCategoryNumber;
    }
}

Console.WriteLine($"Lowest location number that corresponds to any of the initial seed numbers: {lowestLocationNumber}.");
Console.ReadLine();

internal class TranslationMap(long srcRangeStart, long dstRangeStart, long rangeLength)
{
    public long SrcRangeStart { get; } = srcRangeStart;
    public long DstRangeStart { get; } = dstRangeStart;
    public long RangeLength { get; } = rangeLength;
}
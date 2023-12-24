using System.Text;

var focusingPowerOfResultingLensConfiguration = 0;

const int numberOfBoxes = 256;

const char initializationSequenceStepsSeparator = ',';

const char lensAdditionSymbol = '=';
const char lensRemovalSymbol = '-';

const int hashAlgorithmMultiplier = 17;
const int hashAlgorithmDivider = 256;

var boxes = new List<LensInfo>?[numberOfBoxes];

var initializationSequence = File.ReadAllLines("input.txt").First();
var initializationSequenceSteps = initializationSequence.Split(initializationSequenceStepsSeparator);

foreach (var initializationSequenceStep in initializationSequenceSteps)
{
    var operationSymbolIndex = initializationSequenceStep.IndexOf(lensAdditionSymbol);
    if (operationSymbolIndex == -1)
    {
        operationSymbolIndex = initializationSequenceStep.IndexOf(lensRemovalSymbol);
        if (operationSymbolIndex == -1)
        {
            continue;
        }
    }

    var hashAlgorithmResult = 0;

    var lensLabel = initializationSequenceStep[..operationSymbolIndex];
    var lensLabelAsciiBytes = Encoding.ASCII.GetBytes(lensLabel);

    foreach (var lensLabelAsciiByte in lensLabelAsciiBytes)
    {
        hashAlgorithmResult += lensLabelAsciiByte;
        hashAlgorithmResult *= hashAlgorithmMultiplier;
        hashAlgorithmResult %= hashAlgorithmDivider;
    }

    if (initializationSequenceStep[operationSymbolIndex] == lensAdditionSymbol)
    {
        var lensFocalLength = int.Parse(initializationSequenceStep[(operationSymbolIndex + 1)..]);

        var lensesInfos = boxes[hashAlgorithmResult];
        if (lensesInfos == null)
        {
            lensesInfos = new List<LensInfo>();
            boxes[hashAlgorithmResult] = lensesInfos;
        }

        var lensInfoToReplaceIndex = lensesInfos.FindIndex(lensInfo => lensInfo.Label == lensLabel);
        if (lensInfoToReplaceIndex == -1)
        {
            lensesInfos.Add(new LensInfo(lensLabel, lensFocalLength));
        }
        else
        {
            lensesInfos[lensInfoToReplaceIndex].FocalLength = lensFocalLength;
        }
    }
    else
    {
        var lensesInfos = boxes[hashAlgorithmResult];
        if (lensesInfos == null)
        {
            continue;
        }

        var lensInfoToRemoveIndex = lensesInfos.FindIndex(lensInfo => lensInfo.Label == lensLabel);
        if (lensInfoToRemoveIndex != -1)
        {
            lensesInfos.RemoveAt(lensInfoToRemoveIndex);
        }
    }
}

for (var boxIndex = 0; boxIndex < boxes.Length; boxIndex++)
{
    var lensesInfos = boxes[boxIndex];
    if (lensesInfos == null)
    {
        continue;
    }

    for (var lensInfoIndex = 0; lensInfoIndex < lensesInfos.Count; lensInfoIndex++)
    {
        focusingPowerOfResultingLensConfiguration += (boxIndex + 1) * (lensInfoIndex + 1) * lensesInfos[lensInfoIndex].FocalLength;
    }
}

Console.WriteLine($"Focusing power of the resulting lens configuration: {focusingPowerOfResultingLensConfiguration}.");
Console.ReadLine();

internal class LensInfo(string label, int focalLength)
{
    public string Label { get; } = label;
    public int FocalLength { get; set; } = focalLength;
}
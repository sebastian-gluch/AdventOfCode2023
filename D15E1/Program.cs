using System.Text;

var sumOfHashAlgorithmResults = 0;

const char initializationSequenceStepsSeparator = ',';

const int hashAlgorithmMultiplier = 17;
const int hashAlgorithmDivider = 256;

var initializationSequence = File.ReadAllLines("input.txt").First();
var initializationSequenceSteps = initializationSequence.Split(initializationSequenceStepsSeparator);

foreach (var initializationSequenceStep in initializationSequenceSteps)
{
    var hashAlgorithmResult = 0;

    var initializationSequenceStepAsciiBytes = Encoding.ASCII.GetBytes(initializationSequenceStep);

    foreach (var initializationSequenceStepAsciiByte in initializationSequenceStepAsciiBytes)
    {
        hashAlgorithmResult += initializationSequenceStepAsciiByte;
        hashAlgorithmResult *= hashAlgorithmMultiplier;
        hashAlgorithmResult %= hashAlgorithmDivider;
    }

    sumOfHashAlgorithmResults += hashAlgorithmResult;
}

Console.WriteLine($"Sum of the HASH algorithm results run on each of the steps: {sumOfHashAlgorithmResults}.");
Console.ReadLine();
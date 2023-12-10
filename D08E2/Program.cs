const char aChar = 'A';
const char zChar = 'Z';

const char leftInstructionSymbol = 'L';
const char rightInstructionSymbol = 'R';

var lines = File.ReadAllLines("input.txt");

var instructionsSequence = lines[0].ToCharArray();

var currentElements = new List<string>();

var elementToItsLeftNeighbor = new Dictionary<string, string>();
var elementToItsRightNeighbor = new Dictionary<string, string>();

for (var i = 2; i < lines.Length; i++)
{
    var line = lines[i];

    var element = line[..3];
    if (element.Last() == aChar)
    {
        currentElements.Add(element);
    }

    var leftNeighbor = line[7..10];
    var rightNeighbor = line[12..15];

    elementToItsLeftNeighbor.Add(element, leftNeighbor);
    elementToItsRightNeighbor.Add(element, rightNeighbor);
}

var iterationsCount = 0;

var stepsRequiredToReachElementThatEndWithZPerFirstElement = new long?[currentElements.Count];
var stepsRequiredToReachThisElementOnceAgainPerFirstElement = new long?[currentElements.Count];

while (stepsRequiredToReachThisElementOnceAgainPerFirstElement.Any(stepsRequiredToReachThisElementOnceAgain => stepsRequiredToReachThisElementOnceAgain == null))
{
    var nextInstructionSymbol = instructionsSequence[iterationsCount % instructionsSequence.Length];
    if (nextInstructionSymbol == leftInstructionSymbol)
    {
        for (var i = 0; i < currentElements.Count; i++)
        {
            var currentElement = elementToItsLeftNeighbor[currentElements[i]];
            if (currentElement.Last() == zChar)
            {
                if (stepsRequiredToReachElementThatEndWithZPerFirstElement[i] == null)
                    stepsRequiredToReachElementThatEndWithZPerFirstElement[i] = iterationsCount + 1;
                else if (stepsRequiredToReachThisElementOnceAgainPerFirstElement[i] == null)
                    stepsRequiredToReachThisElementOnceAgainPerFirstElement[i] = iterationsCount + 1 - stepsRequiredToReachElementThatEndWithZPerFirstElement[i];
            }

            currentElements[i] = currentElement;
        }
    }
    else if (nextInstructionSymbol == rightInstructionSymbol)
    {
        for (var i = 0; i < currentElements.Count; i++)
        {
            var currentElement = elementToItsRightNeighbor[currentElements[i]];
            if (currentElement.Last() == zChar)
            {
                if (stepsRequiredToReachElementThatEndWithZPerFirstElement[i] == null)
                    stepsRequiredToReachElementThatEndWithZPerFirstElement[i] = iterationsCount + 1;
                else if (stepsRequiredToReachThisElementOnceAgainPerFirstElement[i] == null)
                    stepsRequiredToReachThisElementOnceAgainPerFirstElement[i] = iterationsCount + 1 - stepsRequiredToReachElementThatEndWithZPerFirstElement[i];
            }

            currentElements[i] = currentElement;
        }
    }
    else
    {
        throw new ArgumentException("Unsupported instruction!");
    }

    iterationsCount++;
}

for (var i = 0; i < currentElements.Count; i++)
{
    if (stepsRequiredToReachElementThatEndWithZPerFirstElement[i] != stepsRequiredToReachThisElementOnceAgainPerFirstElement[i])
    {
        throw new ArgumentException("It makes it impossible to use the LCM method!");
    }
}

var stepsItTakesToBeOnlyOnElementsThatEndWithZ = GetLeastCommonMultiple(stepsRequiredToReachThisElementOnceAgainPerFirstElement.Select(stepsRequiredToReachThisElementOnceAgain => stepsRequiredToReachThisElementOnceAgain!.Value));

Console.WriteLine($"Steps it takes to be only on elements that end with \"Z\": {stepsItTakesToBeOnlyOnElementsThatEndWithZ}.");
Console.ReadLine();

static long GetLeastCommonMultiple(IEnumerable<long> numbers) => numbers.Aggregate(GetLeastCommonMultipleForTwoNumbers);

static long GetLeastCommonMultipleForTwoNumbers(long number1, long number2) => Math.Abs(number1 * number2) / GetGreatestCommonDivisor(number1, number2);

static long GetGreatestCommonDivisor(long number1, long number2) => number2 == 0 ? number1 : GetGreatestCommonDivisor(number2, number1 % number2);
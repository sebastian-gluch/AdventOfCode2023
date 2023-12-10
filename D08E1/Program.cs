var stepsRequiredToReachLastElement = 0;

const string firstElement = "AAA";
const string lastElement = "ZZZ";

const char leftInstructionSymbol = 'L';
const char rightInstructionSymbol = 'R';

var lines = File.ReadAllLines("input.txt");

var instructionsSequence = lines[0].ToCharArray();

var elementToItsLeftNeighbor = new Dictionary<string, string>();
var elementToItsRightNeighbor = new Dictionary<string, string>();

for (var i = 2; i < lines.Length; i++)
{
    var line = lines[i];

    var element = line[..3];

    var leftNeighbor = line[7..10];
    var rightNeighbor = line[12..15];

    elementToItsLeftNeighbor.Add(element, leftNeighbor);
    elementToItsRightNeighbor.Add(element, rightNeighbor);
}

var currentElement = firstElement;

while (currentElement != lastElement)
{
    var nextInstructionSymbol = instructionsSequence[stepsRequiredToReachLastElement % instructionsSequence.Length];
    if (nextInstructionSymbol == leftInstructionSymbol)
    {
        currentElement = elementToItsLeftNeighbor[currentElement];
    }
    else if (nextInstructionSymbol == rightInstructionSymbol)
    {
        currentElement = elementToItsRightNeighbor[currentElement];
    }
    else
    {
        throw new ArgumentException("Unsupported instruction!");
    }

    stepsRequiredToReachLastElement++;
}

Console.WriteLine($"Steps required to reach the last element: {stepsRequiredToReachLastElement}.");
Console.ReadLine();
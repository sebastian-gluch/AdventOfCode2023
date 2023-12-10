var lines = File.ReadAllLines("input.txt");

var scratchcardsInstances = new int[lines.Length];

for (var i = 0; i < scratchcardsInstances.Length; i++)
{
    scratchcardsInstances[i] = 1;
}

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];

    var numbersStrings = line.Split(':')[1].Split('|');

    var winningNumbersString = numbersStrings[0];
    var numbersWeHaveString = numbersStrings[1];

    var winningNumbers = new List<int>(winningNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

    var howManyWinningNumbersWeHave = 0;

    foreach (var numberWeHave in numbersWeHaveString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
    {
        if (winningNumbers.Contains(numberWeHave))
        {
            howManyWinningNumbersWeHave++;
        }
    }

    for (var j = 1; j <= howManyWinningNumbersWeHave; j++)
    {
        scratchcardsInstances[i + j] += scratchcardsInstances[i];
    }
}

var sumOfScratchcardsInstances = scratchcardsInstances.Sum();

Console.WriteLine($"Sum of the scratchcards instances we end up with: {sumOfScratchcardsInstances}.");
Console.ReadLine();
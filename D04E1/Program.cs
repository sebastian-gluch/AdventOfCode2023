var sumOfPoints = 0;

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
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

    if (howManyWinningNumbersWeHave != 0)
    {
        sumOfPoints += Pow(2, howManyWinningNumbersWeHave - 1);
    }
}

Console.WriteLine($"Sum of points the scratchcards are worth in total: {sumOfPoints}.");
Console.ReadLine();

static int Pow(int baseNumber, int exponentNumber)
{
    if (exponentNumber == 0)
    {
        return 1;
    }

    var result = baseNumber;

    for (var i = 1; i < exponentNumber; i++)
    {
        result *= baseNumber;
    }

    return result;
}
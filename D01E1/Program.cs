var sum = 0;

var lines = File.ReadAllLines("input.txt");

foreach (var line in lines)
{
    if (TryGetFirstDigitCharFromLine(line, out var firstDigitChar) &&
        TryGetLastDigitCharFromLine(line, out var lastDigitChar))
    {
        sum += int.Parse(string.Concat(firstDigitChar, lastDigitChar));
    }
}

Console.WriteLine($"Sum of all of the calibration values: {sum}.");
Console.ReadLine();

static bool TryGetFirstDigitCharFromLine(string line, out char firstDigitChar)
{
    for (var i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            firstDigitChar = line[i];
            return true;
        }
    }

    firstDigitChar = '\0';
    return false;
}

static bool TryGetLastDigitCharFromLine(string line, out char lastDigitChar)
{
    for (var i = line.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(line[i]))
        {
            lastDigitChar = line[i];
            return true;
        }
    }

    lastDigitChar = '\0';
    return false;
}
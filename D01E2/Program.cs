var sum = 0;

IReadOnlyDictionary<string, char> stringDigitToDigitChar = new Dictionary<string, char>
{
    { "one", '1' },
    { "two", '2' },
    { "three", '3' },
    { "four", '4' },
    { "five", '5' },
    { "six", '6' },
    { "seven", '7' },
    { "eight", '8' },
    { "nine", '9' }
};

var minStringDigitLength = stringDigitToDigitChar.Keys.Select(stringDigit => stringDigit.Length).Min();

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

bool TryGetFirstDigitCharFromLine(string line, out char firstDigitChar)
{
    for (var i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
        {
            if (i != 0)
            {
                var charsBeforeFirstDigitChar = string.Concat(line.Take(i));
                if (charsBeforeFirstDigitChar.Length >= minStringDigitLength)
                {
                    var firstStringDigit = string.Empty;
                    var firstStringDigitStartIndex = -1;

                    foreach (var stringDigit in stringDigitToDigitChar.Keys)
                    {
                        var stringDigitStartIndex = charsBeforeFirstDigitChar.IndexOf(stringDigit, StringComparison.Ordinal);
                        if (stringDigitStartIndex != -1 && (firstStringDigitStartIndex == -1 ||
                                                            stringDigitStartIndex < firstStringDigitStartIndex))
                        {
                            firstStringDigit = stringDigit;
                            firstStringDigitStartIndex = stringDigitStartIndex;
                        }
                    }

                    if (firstStringDigit.Length != 0)
                    {
                        firstDigitChar = stringDigitToDigitChar[firstStringDigit];
                        return true;
                    }
                }
            }

            firstDigitChar = line[i];
            return true;
        }
    }

    firstDigitChar = '\0';
    return false;
}

bool TryGetLastDigitCharFromLine(string line, out char lastDigitChar)
{
    for (var i = line.Length - 1; i >= 0; i--)
    {
        if (char.IsDigit(line[i]))
        {
            if (i != line.Length - 1)
            {
                var charsAfterLastDigitChar = string.Concat(line.Skip(i + 1));
                if (charsAfterLastDigitChar.Length >= minStringDigitLength)
                {
                    var lastStringDigit = string.Empty;
                    var lastStringDigitStartIndex = -1;

                    foreach (var stringDigit in stringDigitToDigitChar.Keys)
                    {
                        var stringDigitStartIndex = charsAfterLastDigitChar.LastIndexOf(stringDigit, StringComparison.Ordinal);
                        if (stringDigitStartIndex != -1 && (lastStringDigitStartIndex == -1 ||
                                                            stringDigitStartIndex > lastStringDigitStartIndex))
                        {
                            lastStringDigit = stringDigit;
                            lastStringDigitStartIndex = stringDigitStartIndex;
                        }
                    }

                    if (lastStringDigit.Length != 0)
                    {
                        lastDigitChar = stringDigitToDigitChar[lastStringDigit];
                        return true;
                    }
                }
            }

            lastDigitChar = line[i];
            return true;
        }
    }

    lastDigitChar = '\0';
    return false;
}
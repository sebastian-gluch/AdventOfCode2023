var totalWinnings = 0;

var cardLabelToItsStrength = new Dictionary<char, int>
{
    { '2', 1 },
    { '3', 2 },
    { '4', 3 },
    { '5', 4 },
    { '6', 5 },
    { '7', 6 },
    { '8', 7 },
    { '9', 8 },
    { 'T', 9 },
    { 'J', 10 },
    { 'Q', 11 },
    { 'K', 12 },
    { 'A', 13 }
};

var lines = File.ReadAllLines("input.txt");

var hands = new Hand[lines.Length];

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var lineSplit = line.Split(' ');

    var cardsLabels = lineSplit[0];
    var bid = int.Parse(lineSplit[1]);

    var hand = new Hand(
        cardLabelToItsStrength[cardsLabels[0]],
        cardLabelToItsStrength[cardsLabels[1]],
        cardLabelToItsStrength[cardsLabels[2]],
        cardLabelToItsStrength[cardsLabels[3]],
        cardLabelToItsStrength[cardsLabels[4]],
        bid);

    var cardStrengthToItsAmount = new Dictionary<int, int>();

    CountCardStrength(hand.Card1Strength);
    CountCardStrength(hand.Card2Strength);
    CountCardStrength(hand.Card3Strength);
    CountCardStrength(hand.Card4Strength);
    CountCardStrength(hand.Card5Strength);

    void CountCardStrength(int cardStrength)
    {
        if (cardStrengthToItsAmount.TryGetValue(cardStrength, out var cardAmount))
        {
            cardStrengthToItsAmount[cardStrength] = cardAmount + 1;
        }
        else
        {
            cardStrengthToItsAmount.Add(cardStrength, 1);
        }
    }

    if (IsFiveOfKind(cardStrengthToItsAmount))
    {
        hand.Type = HandType.FiveOfKind;
    }
    else if (IsFourOfKind(cardStrengthToItsAmount))
    {
        hand.Type = HandType.FourOfKind;
    }
    else if (IsFullHouse(cardStrengthToItsAmount))
    {
        hand.Type = HandType.FullHouse;
    }
    else if (IsThreeOfKind(cardStrengthToItsAmount))
    {
        hand.Type = HandType.ThreeOfKind;
    }
    else if (IsTwoPair(cardStrengthToItsAmount))
    {
        hand.Type = HandType.TwoPair;
    }
    else if (IsOnePair(cardStrengthToItsAmount))
    {
        hand.Type = HandType.OnePair;
    }
    else if (IsHighCard(cardStrengthToItsAmount))
    {
        hand.Type = HandType.HighCard;
    }

    hands[i] = hand;
}

var currentRank = hands.Length;

foreach (var hand in hands.OrderBy(hand => hand, HandsComparer.Instance))
{
    totalWinnings += currentRank * hand.Bid;
    currentRank -= 1;
}

Console.WriteLine($"Total winnings: {totalWinnings}.");
Console.ReadLine();

static bool IsFiveOfKind(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 1;
}

static bool IsFourOfKind(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 2 && cardStrengthToItsAmount.Values.Any(cardAmount => cardAmount == 4);
}

static bool IsFullHouse(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 2 && cardStrengthToItsAmount.Values.Any(cardAmount => cardAmount == 3);
}

static bool IsThreeOfKind(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 3 && cardStrengthToItsAmount.Values.Any(cardAmount => cardAmount == 3);
}

static bool IsTwoPair(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 3 && cardStrengthToItsAmount.Values.Count(cardAmount => cardAmount == 2) == 2;
}

static bool IsOnePair(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 4;
}

static bool IsHighCard(IReadOnlyDictionary<int, int> cardStrengthToItsAmount)
{
    return cardStrengthToItsAmount.Count == 5;
}

internal class Hand(int card1Strength, int card2Strength, int card3Strength, int card4Strength, int card5Strength, int bid)
{
    public int Card1Strength { get; } = card1Strength;
    public int Card2Strength { get; } = card2Strength;
    public int Card3Strength { get; } = card3Strength;
    public int Card4Strength { get; } = card4Strength;
    public int Card5Strength { get; } = card5Strength;

    public int Bid { get; } = bid;

    public HandType Type { get; set; } = HandType.Unknown;
}

internal enum HandType
{
    Unknown,
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfKind,
    FullHouse,
    FourOfKind,
    FiveOfKind
}

internal class HandsComparer : IComparer<Hand>
{
    public static HandsComparer Instance = new();

    public int Compare(Hand? hand1, Hand? hand2)
    {
        if (hand1 == null) throw new ArgumentNullException(nameof(hand1));
        if (hand2 == null) throw new ArgumentNullException(nameof(hand2));

        var result = hand2.Type - hand1.Type;
        if (result != 0)
        {
            return result;
        }

        result = hand2.Card1Strength - hand1.Card1Strength;
        if (result != 0)
        {
            return result;
        }

        result = hand2.Card2Strength - hand1.Card2Strength;
        if (result != 0)
        {
            return result;
        }

        result = hand2.Card3Strength - hand1.Card3Strength;
        if (result != 0)
        {
            return result;
        }

        result = hand2.Card4Strength - hand1.Card4Strength;
        if (result != 0)
        {
            return result;
        }

        return hand2.Card5Strength - hand1.Card5Strength;
    }
}
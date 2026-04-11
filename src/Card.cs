namespace PokerHandEvaluator
{
    public readonly struct Card
    {
        public static Dictionary<char, byte> rankMap = new Dictionary<char, byte>()
        {
            { '2', 0}, { '3', 1}, { '4', 2}, { '5', 3},
            { '6', 4}, { '7', 5}, { '8', 6}, { '9', 7},
            { 'T', 8}, { 'J', 9}, { 'Q', 10}, { 'K', 11}, { 'A', 12}
        };
        public static Dictionary<char, byte> suitMap = new Dictionary<char, byte>()
        {
            { 'c', 0}, { 'd', 1}, { 'h', 2}, { 's', 3}
        };
        static char[] rankReverseArray = {
          '2', '3', '4', '5',
          '6', '7', '8', '9',
          'T', 'J', 'Q', 'K', 'A',
        };
        static char[] suitReverseArray = { 'c', 'd', 'h', 's' };

        public readonly byte id;

        public Card(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length != 2)
            {
                throw new ArgumentException("Card text must contain exactly two characters.", nameof(name));
            }

            if (!rankMap.TryGetValue(char.ToUpperInvariant(name[0]), out var rank))
            {
                throw new ArgumentException($"Invalid card rank '{name[0]}'.", nameof(name));
            }

            if (!suitMap.TryGetValue(char.ToLowerInvariant(name[1]), out var suit))
            {
                throw new ArgumentException($"Invalid card suit '{name[1]}'.", nameof(name));
            }

            id = (byte)(rank * 4 + suit);
        }

        public Card(byte id_)
        {
            if (id_ >= 52)
            {
                throw new ArgumentOutOfRangeException(nameof(id_), "Card id must be in range 0-51.");
            }

            id = id_;
        }

        public static Card[] Cards(string hand)
        {
            if (hand is null)
            {
                throw new ArgumentNullException(nameof(hand));
            }

            if ((hand.Length % 2) != 0)
            {
                throw new ArgumentException("Hand text must contain an even number of characters.", nameof(hand));
            }

            Card[] result = new Card[hand.Length / 2];
            var i = 0;
            while(i+1 < hand.Length)
            {
                result[i/2] = new Card(hand.Substring(i, 2));
                i += 2;
            }
            return result;
        }

        public static Card[] Cards(byte[] ids) {
            if (ids is null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return ids.Select(id => new Card(id)).ToArray();
        }

        char describeRank() { return rankReverseArray[id / 4]; }
        char describeSuit() { return suitReverseArray[id % 4]; }

        public override string ToString() {
            char[] chars = {describeRank(), describeSuit()};
            return new string(chars);
        }

        public static string CardsToString(Card[] cards) {
            var result = "";
            foreach(Card card in cards) {
                result += card.ToString();
            }
            return result;
        }
    }
}

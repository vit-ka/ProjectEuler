using System;
using System.Collections.Generic;
using System.IO;

namespace Problem_054
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IList<Round> rounds = LoadRounds("poker.txt");

            int player1WinsCount = 0;

            foreach (Round round in rounds)
            {
                RoundResult player1Result = GetPlayerResult(round.P1);
                RoundResult player2Result = GetPlayerResult(round.P2);

                if (player1Result > player2Result)
                {
                    Console.WriteLine("Player1: {0} vs {1}. {2}", player1Result, player2Result, round);
                    ++player1WinsCount;
                }
                else if (player1Result < player2Result)
                    Console.WriteLine("Player2: {0} vs {1}. {2}", player1Result, player2Result, round);
                else
                {
                    Console.WriteLine(
                        "Twin: {0} vs {1}. {2}. Detecting highest card...", player1Result, player2Result, round);

                    var comparer = new CardComparer();

                    for (int i = 0; i < 5; i++)
                    {
                        int comparationResult = comparer.Compare(round.P1.Cards[i], round.P2.Cards[i]);
                        if (comparationResult < 0)
                        {
                            Console.WriteLine("  * Player1: {0} vs {1}.", round.P1.Cards[i], round.P2.Cards[i]);
                            ++player1WinsCount;
                            break;
                        }
                        if (comparationResult > 0)
                        {
                            Console.WriteLine("  * Player2: {0} vs {1}.", round.P1.Cards[i], round.P2.Cards[i]);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("Player 1 wins {0} times", player1WinsCount);
        }

        private static RoundResult GetPlayerResult(Player player)
        {
            if (HasRoyalFlush(player.Cards))
                return RoundResult.RoyalFlush;

            if (HasStraightFlush(player.Cards))
                return RoundResult.StraightFlush;

            if (HasFourOfAKind(player.Cards))
                return RoundResult.FourOfAKind;

            if (HasFullHouse(player.Cards))
                return RoundResult.FullHouse;

            if (HasFlush(player.Cards))
                return RoundResult.Flush;

            if (HasStraight(player.Cards))
                return RoundResult.Straight;

            if (HasThreeOfAKind(player.Cards))
                return RoundResult.ThreeOfAKind;

            if (HasTwoPairs(player.Cards))
                return RoundResult.TwoPairs;

            if (HasOnePair(player.Cards))
                return RoundResult.OnePair;

            return RoundResult.HighCard;
        }

        private static bool HasRoyalFlush(Card[] cards)
        {
            return HasStraightFlush(cards) && cards[0].Value == Value.Ace;
        }

        private static bool HasStraightFlush(Card[] cards)
        {
            return HasStraight(cards) && HasFlush(cards);
        }

        private static bool HasFlush(Card[] cards)
        {
            return cards[0].Suite == cards[1].Suite &&
                   cards[1].Suite == cards[2].Suite &&
                   cards[2].Suite == cards[3].Suite &&
                   cards[3].Suite == cards[4].Suite;
        }

        private static bool HasStraight(Card[] cards)
        {
            return cards[1].Value + 1 == cards[0].Value &&
                   cards[2].Value + 1 == cards[1].Value &&
                   cards[3].Value + 1 == cards[2].Value &&
                   cards[4].Value + 1 == cards[3].Value;
        }

        private static bool HasFourOfAKind(Card[] cards)
        {
            IDictionary<Value, int> values = GetCardValues(cards);

            foreach (var value in values)
            {
                if (value.Value == 4)
                {
                    Array.Sort(
                        cards,
                        new ProtectedCardComparer(
                            new List<Value>
                                {
                                    value.Key
                                }));

                    return true;
                }
            }

            return false;
        }

        private static bool HasThreeOfAKind(Card[] cards)
        {
            IDictionary<Value, int> values = GetCardValues(cards);

            foreach (var value in values)
            {
                if (value.Value == 3)
                {
                    Array.Sort(
                        cards,
                        new ProtectedCardComparer(
                            new List<Value>
                                {
                                    value.Key
                                }));

                    return true;
                }
            }

            return false;
        }

        private static bool HasFullHouse(Card[] cards)
        {
            IDictionary<Value, int> values = GetCardValues(cards);

            bool hasTwo = false;
            bool hasThree = false;

            foreach (var value in values)
            {
                if (value.Value == 2)
                    hasTwo = true;

                if (value.Value == 3)
                    hasThree = true;
            }

            return hasTwo && hasThree;
        }

        private static bool HasTwoPairs(Card[] cards)
        {
            IDictionary<Value, int> values = GetCardValues(cards);

            int hasTwo = 0;

            var sortValue = new List<Value>();

            foreach (var value in values)
            {
                if (value.Value == 2)
                {
                    ++hasTwo;
                    sortValue.Add(value.Key);
                }
            }

            if (sortValue.Count == 2)
            {
                if (sortValue[0] < sortValue[1])
                {
                    Value temp = sortValue[0];
                    sortValue[0] = sortValue[1];
                    sortValue[1] = temp;
                }

                Array.Sort(
                    cards,
                    new ProtectedCardComparer(
                        new List<Value>
                            {
                                sortValue[1]
                            }));

                Array.Sort(
                    cards,
                    new ProtectedCardComparer(
                        new List<Value>
                            {
                                sortValue[0]
                            }));
            }

            return hasTwo == 2;
        }

        private static bool HasOnePair(Card[] cards)
        {
            IDictionary<Value, int> values = GetCardValues(cards);

            int hasTwo = 0;

            foreach (var value in values)
            {
                if (value.Value == 2)
                {
                    ++hasTwo;

                    Array.Sort(
                        cards,
                        new ProtectedCardComparer(
                            new List<Value>
                                {
                                    value.Key
                                }));
                }
            }

            return hasTwo == 1;
        }

        private static IDictionary<Value, int> GetCardValues(IEnumerable<Card> cards)
        {
            IDictionary<Value, int> values = new Dictionary<Value, int>();

            foreach (Card card in cards)
            {
                if (!values.ContainsKey(card.Value))
                    values.Add(card.Value, 0);

                ++values[card.Value];
            }

            return values;
        }

        private static IList<Round> LoadRounds(string path)
        {
            string[] strings = File.ReadAllLines(path);

            IList<Round> result = new List<Round>();

            foreach (string roundInStr in strings)
            {
                Round round = Round.Parse(roundInStr);
                result.Add(round);
            }

            return result;
        }

        #region Nested type: Card

        private class Card
        {
            public Suite Suite { get; set; }
            public Value Value { get; set; }

            public static Card Parse(string card)
            {
                char valueInChar = card[0];
                char suiteInChar = card[1];

                var result = new Card();

                switch (suiteInChar)
                {
                    case 'H':
                        result.Suite = Suite.Hearts;
                        break;
                    case 'D':
                        result.Suite = Suite.Diamonds;
                        break;
                    case 'S':
                        result.Suite = Suite.Spades;
                        break;
                    case 'C':
                        result.Suite = Suite.Clubs;
                        break;
                }

                switch (valueInChar)
                {
                    case '2':
                        result.Value = Value.C2;
                        break;
                    case '3':
                        result.Value = Value.C3;
                        break;
                    case '4':
                        result.Value = Value.C4;
                        break;
                    case '5':
                        result.Value = Value.C5;
                        break;
                    case '6':
                        result.Value = Value.C6;
                        break;
                    case '7':
                        result.Value = Value.C7;
                        break;
                    case '8':
                        result.Value = Value.C8;
                        break;
                    case '9':
                        result.Value = Value.C9;
                        break;
                    case 'T':
                        result.Value = Value.C10;
                        break;
                    case 'J':
                        result.Value = Value.Jack;
                        break;
                    case 'Q':
                        result.Value = Value.Queen;
                        break;
                    case 'K':
                        result.Value = Value.King;
                        break;
                    case 'A':
                        result.Value = Value.Ace;
                        break;
                }

                return result;
            }

            public override string ToString()
            {
                return string.Format(
                    "{1}{0}",
                    Suite.ToString().Substring(0, 1),
                    Value < Value.Jack
                        ? (Value == Value.C10 ? "T" : Value.ToString().Substring(1, 1))
                        : Value.ToString().Substring(0, 1));
            }
        }

        #endregion

        #region Nested type: CardComparer

        private class CardComparer : IComparer<Card>
        {
            #region IComparer<Card> Members

            public int Compare(Card x, Card y)
            {
                return y.Value - x.Value;
            }

            #endregion
        }

        #endregion

        #region Nested type: Player

        private class Player
        {
            public Card[] Cards { get; set; }

            public override string ToString()
            {
                string cards = "";
                foreach (Card card in Cards)
                    cards += " " + card;

                return string.Format("{0}", cards);
            }
        }

        #endregion

        #region Nested type: ProtectedCardComparer

        private class ProtectedCardComparer : IComparer<Card>
        {
            private readonly IList<Value> _protectedValues;

            public ProtectedCardComparer(IList<Value> protectedValues)
            {
                _protectedValues = protectedValues;
            }

            #region IComparer<Card> Members

            public int Compare(Card x, Card y)
            {
                if (_protectedValues.Contains(x.Value) && !_protectedValues.Contains(y.Value))
                    return -1;
                if (_protectedValues.Contains(y.Value) && !_protectedValues.Contains(x.Value))
                    return 1;
                return y.Value - x.Value;
            }

            #endregion
        }

        #endregion

        #region Nested type: Round

        private class Round
        {
            public Player P1 { get; set; }
            public Player P2 { get; set; }

            public static Round Parse(string roundInStr)
            {
                string[] cards = roundInStr.Split(
                    new[]
                        {
                            ' '
                        });

                var result = new Round
                    {
                        P1 = new Player(),
                        P2 = new Player()
                    };

                result.P1.Cards = new Card[5];
                result.P2.Cards = new Card[5];

                for (int i = 0; i < 5; i++)
                    result.P1.Cards[i] = Card.Parse(cards[i]);

                for (int i = 0; i < 5; i++)
                    result.P2.Cards[i] = Card.Parse(cards[i + 5]);

                Array.Sort(result.P1.Cards, new CardComparer());
                Array.Sort(result.P2.Cards, new CardComparer());

                return result;
            }

            public override string ToString()
            {
                return string.Format("P1: [{0}], P2: [{1}]", P1, P2);
            }
        }

        #endregion

        #region Nested type: RoundResult

        private enum RoundResult
        {
            HighCard,
            OnePair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        #endregion

        #region Nested type: Suite

        private enum Suite
        {
            Hearts,
            Diamonds,
            Spades,
            Clubs
        }

        #endregion

        #region Nested type: Value

        private enum Value
        {
            C2,
            C3,
            C4,
            C5,
            C6,
            C7,
            C8,
            C9,
            C10,
            Jack,
            Queen,
            King,
            Ace
        }

        #endregion
    }
}
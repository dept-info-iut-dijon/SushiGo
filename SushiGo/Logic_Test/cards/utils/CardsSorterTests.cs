using Logic_Layer.cards;
using LogicTest.datas_generators;
using Logic_Layer.cards.utils;
using LogicTest.datas_generators.cards.utils;

namespace LogicTest.cards.utils;

public class CardsSorterTests
{
    [Theory]
    [ClassData(typeof(CardsSorterDatasGenerator))]
    public void TypeSort_ShouldReturnSortedCards(Card[] cards, Card[] expected, Type type)
    {
        var actual = CardsSorter.TypeSort(type, cards.ToList());
        Assert.Equal(expected.Length, actual.Count);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i].GetType(), actual[i].GetType());
            Assert.True(expected[i].Equals(actual[i]));
        }
    }
}
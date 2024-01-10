using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.factories;
using System.Collections;

namespace LogicTest.datas_generators.cards.utils;

public class CardsSorterDatasGenerator : IEnumerable<object[]>
{
    private CardFactory cardFactory = new();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new Card[]
            {
                new TempuraCard(), new SashimiCard(), new ChopstickCard(), new DessertCard(),
                new SushiCard(SushiTypes.SALMON)
            },
            new Card[]
            {
                new TempuraCard()
            },
            typeof(TempuraCard)
        };
        yield return new object[]
        {
            new Card[] { new TempuraCard(), new SashimiCard(), new ChopstickCard(), new DessertCard() },
            new Card[] { new SashimiCard() },
            typeof(SashimiCard)
        };
        yield return new object[]
        {
            new Card[] { new TempuraCard(), new SashimiCard(), new ChopstickCard(), new DessertCard() },
            new Card[] { new ChopstickCard() },
            typeof(ChopstickCard)
        };
        yield return new object[]
        {
            new Card[]
            {
                new TempuraCard(), new SashimiCard(), new ChopstickCard(), new DessertCard(),
                new SushiCard(SushiTypes.CALAMARI), new SushiCard(SushiTypes.SALMON)
            },
            new Card[] { new SushiCard(SushiTypes.CALAMARI), new SushiCard(SushiTypes.SALMON) },
            typeof(SushiCard)
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
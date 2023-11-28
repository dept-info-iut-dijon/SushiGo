using Logic_Layer.cards.cards_implementation;
using Logic_Layer.factories;

namespace LogicTest.cards.cards_implementation;

public class MakiCardTests
{
    [Theory]
    [InlineData("maki 1")]
    [InlineData("maki 2")]
    [InlineData("maki 3")]
    public void Equals_ComparisonByValueTrue(string generationParameters)
    {
        CardFactory cardFactory = new CardFactory();
        var makiCards = cardFactory.CreateCard(generationParameters, 2);
        Assert.True(makiCards.First().Equals(makiCards.Last()));
    }
    
    [Theory]
    [InlineData("maki 1", "maki 2")]
    [InlineData("maki 2" , "maki 3")]
    [InlineData("maki 3", "maki 1")]
    public void Equals_ComparisonByValueFalse(string generationParameters, string generationParametersDifferentCard)
    {
        CardFactory cardFactory = new CardFactory();
        var makiCards = cardFactory.CreateCard(generationParameters);
        var differentMakiCards = cardFactory.CreateCard(generationParametersDifferentCard);
        Assert.False(makiCards.First().Equals(differentMakiCards.First()));
    }
}
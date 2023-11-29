using Logic_Layer.factories;

namespace LogicTest.cards.cards_implementation;

public class SushiCardTests
{
    private CardFactory CardFactory => new CardFactory();
    
    [Theory]
    [InlineData("sushi saumon")]
    [InlineData("sushi calamar")]
    [InlineData("sushi omelette")]
    public void Equals_ComparisonByValueTrue(string generationParameter)
    {
        var sushiCards = CardFactory.CreateCard(generationParameter, 2);
        Assert.True(sushiCards.First().Equals(sushiCards.Last()));
    }
    
    [Theory]
    [InlineData("sushi saumon", "sushi calamar")]
    [InlineData("sushi calamar" , "sushi omelette")]
    [InlineData("sushi omelette", "sushi saumon")]
    public void Equals_ComparisonByValueFalse(string generationParameter, string generationParameterDifferentCard)
    {
        var sushiCards = CardFactory.CreateCard(generationParameter);
        var differentSushiCards = CardFactory.CreateCard(generationParameterDifferentCard);
        Assert.False(sushiCards.First().Equals(differentSushiCards.First()));
    }
}
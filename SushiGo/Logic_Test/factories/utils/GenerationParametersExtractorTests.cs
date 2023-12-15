using Logic_Layer.factories.utils;

namespace LogicTest.factories.utils;

public class GenerationParametersExtractorTests
{
    [Fact]
    public void GetParameters_ReturnsEnoughCardsToPlay()
    {
        Dictionary<string, int> parameters = new GenerationParametersExtractor().GetParameters();

        int cardsSum = parameters.Values.Sum();

        Assert.True(cardsSum >= 35);
    }
}
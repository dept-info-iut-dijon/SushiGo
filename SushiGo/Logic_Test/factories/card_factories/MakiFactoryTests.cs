using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.factories.card_factories;
using Logic_Layer.logic_exceptions;

namespace LogicTest.factories.card_factories;

public class MakiFactoryTests
{
    private MakiFactory factory = new MakiFactory();

    [Fact]
    public void CreateCard_ReturnMaki()
    {
        Card maki = factory.CreateCard("maki 2");

        Assert.IsType<MakiCard>(maki);
        Assert.Equal(2, ((MakiCard)maki).Quantity);
    }

    [Fact]
    public void CreateCard_ThrowsExceptionWhenInvalidMakiQuantity()
    {
        Assert.Throws<InvalidParameterException>(() => factory.CreateCard("maki 4"));
    }
}
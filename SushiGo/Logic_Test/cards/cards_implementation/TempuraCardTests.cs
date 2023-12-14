using Logic_Layer.cards.cards_implementation;

namespace LogicTest.cards.cards_implementation;

public class TempuraCardTests
{
    [Fact]
    public void TempuraCard_NameIsCorrect()
    {
        // Arrange
        var tempuraCard = new TempuraCard();

        // Act
        string name = tempuraCard.Name;

        // Assert
        Assert.Equal("Tempura", name);
    }

    [Fact]
    public void TempuraCard_EqualByValuesTrue()
    {
        var tempuraCard = new TempuraCard();
        var tempuraCard2 = new TempuraCard();

        Assert.True(tempuraCard.Equals(tempuraCard2));
    }
}

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
}

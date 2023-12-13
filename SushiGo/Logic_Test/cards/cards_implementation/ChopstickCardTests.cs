using Logic_Layer.cards.cards_implementation;

namespace LogicTest.cards.cards_implementation;

public class ChopstickCardTests
{
    [Fact]
    public void ChopstickCard_NameIsCorrect()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        string name = chopstickCard.Name;

        // Assert
        Assert.Equal("Baguettes", name);
    }

    [Fact]
    public void ChopstickCard_PlayerTurn_ReturnsTrueWhenAvailable()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        bool result = chopstickCard.PlayerTurn();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ChopstickCard_EndRound_ReturnsFalse()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        bool result = chopstickCard.EndRound();

        // Assert
        Assert.False(result);
    }
}

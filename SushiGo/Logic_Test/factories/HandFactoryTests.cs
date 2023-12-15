using Logic_Layer;
using Logic_Layer.factories;
using Logic_Layer.logic_exceptions;

namespace LogicTest.factories;

public class HandFactoryTests
{
    [Fact]
    public void CreateHands_ReturnsListOfHands()
    {
        // Arrange
        int playersNumber = 2;
        var factory = new HandFactory();

        // Act
        List<Hand> hands = factory.CreateHands(playersNumber);

        // Assert
        Assert.Equal(playersNumber, hands.Count);

        // Vérifiez que chaque main a le nombre correct de cartes
        int expectedCardsPerPlayer = GetExpectedCardsPerPlayer(playersNumber);
        foreach (var hand in hands)
        {
            Assert.Equal(expectedCardsPerPlayer, hand.Cards.Count);
        }
    }

    [Fact]
    public void CreateHands_InvalidPlayersNumber_ThrowsException()
    {
        // Arrange
        int invalidPlayersNumber = 6;
        var factory = new HandFactory();

        // Act and Assert
        var exception = Assert.Throws<WrongPlayersNumberException>(() => factory.CreateHands(invalidPlayersNumber));

        // Vérifiez le message d'exception (facultatif)
        Assert.Equal("Le nombre de joueur devrait être inclus entre 2 et 5", exception.Message);
    }

    private int GetExpectedCardsPerPlayer(int playersNumber)
    {
        switch (playersNumber)
        {
            case 2:
                return 10;
            case 3:
                return 9;
            case 4:
                return 8;
            case 5:
                return 7;
            default:
                throw new ArgumentOutOfRangeException("Le nombre de joueurs doit être entre 2 et 5 pour ce test.");
        }
    }

}
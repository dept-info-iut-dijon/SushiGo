using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.logic_exceptions;

namespace LogicTest.cards.cards_implementation;

public class WasabiCardTests
{
    [Fact]
    public void WasabiCard_NameIsCorrect()
    {
        // Arrange
        var wasabiCard = new WasabiCard();

        // Act
        string name = wasabiCard.Name;

        // Assert
        Assert.Equal("Wasabi", name);
    }

    [Fact]
    public void WasabiCard_AssociateSushi_Success()
    {
        // Arrange
        var wasabiCard = new WasabiCard();
        var sushi = new SushiCard(SushiTypes.SALMON); // Vous devrez créer une instance de SushiCard ici

        // Act
        wasabiCard.AssociateSushi(sushi);

        // Assert
        Assert.Same(sushi, wasabiCard.Sushi);
    }

    [Fact]
    public void WasabiCard_AssociateSushi_ThrowsExceptionWhenAlreadyAssociated()
    {
        // Arrange
        var wasabiCard = new WasabiCard();
        var sushi1 = new SushiCard(SushiTypes.SALMON);
        var sushi2 = new SushiCard(SushiTypes.CALAMARI);

        // Act
        wasabiCard.AssociateSushi(sushi1);

        // Assert
        Assert.Throws<ValueAlreadySetException>(() => wasabiCard.AssociateSushi(sushi2));
    }

    [Fact]
    public void WasabiCard_PlayerTurn_ReturnsTrueWhenNotAssociatedWithSushi()
    {
        // Arrange
        var wasabiCard = new WasabiCard();

        // Act
        bool result = wasabiCard.PlayerTurn();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WasabiCard_PlayerTurn_ReturnsFalseWhenAssociatedWithSushi()
    {
        // Arrange
        var wasabiCard = new WasabiCard();
        var sushi = new SushiCard(SushiTypes.SALMON);

        // Act
        wasabiCard.AssociateSushi(sushi);
        bool result = wasabiCard.PlayerTurn();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WasabiCard_EndRound_ReturnsFalse()
    {
        // Arrange
        var wasabiCard = new WasabiCard();

        // Act
        bool result = wasabiCard.EndRound();

        // Assert
        Assert.False(result);
    }

    
    [Fact]
    public void WasabiCard_AssociatedWithNextSushiPutOnTheBoardAndNotOthersSushi()
    {
        // Arrange
        var board = new Board();
        var wasabiCard = new WasabiCard();
        var sushiCardOnTheBoard = new SushiCard(SushiTypes.OMELETTE);
        var sushiCardInTheHand = new SushiCard(SushiTypes.SALMON);

        //Act
        board.AddCard(sushiCardOnTheBoard);
        board.AddCard(wasabiCard);
        //Assert
        Assert.Null(wasabiCard.Sushi);
        //Assert
        board.AddCard(sushiCardInTheHand);
        Assert.Equal(wasabiCard.Sushi, sushiCardInTheHand);
    }
}

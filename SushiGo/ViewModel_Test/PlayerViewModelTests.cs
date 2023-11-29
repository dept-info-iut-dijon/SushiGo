using Logic_Layer;
using Logic_Layer.cards;
using UI_Layer.ViewModels;
using ViewModel_Test.DatasGenerators;

namespace ViewModel_Test;

public class PlayerViewModelTests
{
    [Theory]
    [InlineData(false, true, PlayerType.PLAYER)]
    [InlineData(true, false, PlayerType.PLAYER)]
    [InlineData(false, true, PlayerType.ROBOT)]
    public void IsTurnFinished_GetNotification(bool initValue, bool newValue, PlayerType playerType)
    {
        var playerVM = new PlayerViewModel(new Player(0, new Board(), new Hand(0, new List<Card>()), "Joueur test"), playerType);
        playerVM.IsTurnFinished = initValue;
        Assert.PropertyChanged(playerVM, nameof(playerVM.IsTurnFinished), () => playerVM.IsTurnFinished = newValue);
    }
    
    [Theory]
    [ClassData(typeof(PlayerDatasGenerator))]
    public void PlayCard_GetNotification(int playerId, Board board, Hand hand, string pseudo, PlayerType playerType, Card card)
    {
        var playerVM = new PlayerViewModel(new Player(playerId, board, hand, pseudo), playerType);
        Assert.PropertyChanged(playerVM, nameof(playerVM.Player.Hand), () => playerVM.PlayCard(card));
    }
}
using Logic_Layer;
using Logic_Layer.cards;
using UI_Layer.ViewModels;

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
}
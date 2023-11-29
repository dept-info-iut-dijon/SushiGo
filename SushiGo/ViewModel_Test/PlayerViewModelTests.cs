using System.Reflection;
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
    public void PlayCard_GetNotification_Hand(int playerId, Board board, Hand hand, string pseudo, PlayerType playerType, Card card)
    {
        var playerVM = new PlayerViewModel(new Player(playerId, board, hand, pseudo), playerType);
        Assert.PropertyChanged(playerVM, nameof(playerVM.Player.Hand), () => playerVM.PlayCard(card));
    }
    
    [Theory]
    [ClassData(typeof(PlayerDatasGenerator))]
    public void PlayCard_GetNotification_BoardCards(int playerId, Board board, Hand hand, string pseudo, PlayerType playerType, Card card)
    {
        var playerVM = new PlayerViewModel(new Player(playerId, board, hand, pseudo), playerType);
        Assert.PropertyChanged(playerVM, nameof(playerVM.Player.Board.Cards), () => playerVM.PlayCard(card));
    }
    
    [Theory]
    [ClassData(typeof(PlayerDatasGenerator))]
    public void PlayCard_GetNotification_HavePlayed(int playerId, Board board, Hand hand, string pseudo, PlayerType playerType, Card card)
    {
        var playerVM = new PlayerViewModel(new Player(playerId, board, hand, pseudo), playerType);
        Assert.PropertyChanged(playerVM, nameof(playerVM.Player.HavePlayed), () => playerVM.PlayCard(card));
    }
    
    [Theory]
    [ClassData(typeof(PlayerDatasGenerator))]
    public void EndRound_GetNotification_BoardCards(int playerId, Board board, Hand hand, string pseudo, PlayerType playerType, Card card)
    {
        var player = new Player(playerId, board, hand, pseudo); 
        var playerVM = new PlayerViewModel(player, playerType);
        var table = new Table(new List<Player> {player, new Player(1, board, hand, pseudo)});
        playerVM.Table = new GameTableViewModel(table);
        
        player.Hand.Cards.Add(card);
        playerVM.PlayCard(card);

        // on fait une réflection de la méthode NextRound pour pouvoir tester la notification
        var method = table.GetType().GetMethod("NextRound", BindingFlags.NonPublic | BindingFlags.Instance);
        
        Assert.PropertyChanged(playerVM, nameof(playerVM.Player.Board.Cards), () => method.Invoke(table, null));
    }
}
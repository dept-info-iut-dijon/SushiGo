using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic_Layer.factories;
using Logic_Layer.logic_exceptions;
using Logic_Layer.score;

namespace Logic_Layer;

/// <summary>
/// Table de jeu
/// </summary>
public class Table : INotifyPropertyChanged
{
    private const int MAX_PLAYERS = 5;
    private const int MIN_PLAYERS = 2;
    
    private int roundNumber;
    private readonly TableScoreCalculator scoreCalculator;

    /// <summary>
    /// Numéro de la manche de la partie
    /// </summary>
    public int RoundNumber
    {
        get => roundNumber;
        private set
        {
            roundNumber = value;
            NotifyPropertyChanged();
        }
    }

    /// <summary>
    /// Retourne le de rotation des mains pour la manche actuelle
    /// </summary>
    public GameOrderEnum GameOrder => RoundNumber % 2 == 0 ? GameOrderEnum.REGRESSIVE : GameOrderEnum.PROGRESSIVE;

    /// <summary>
    /// Les joueurs participant à la partie
    /// </summary>
    public List<Player> Players { get; }

    /// <summary>
    /// Permet de d'abonner un objet à la notification de changement de propriété
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    #region Méthodes publiques

    /// <summary>
    /// Initialise la table de jeu
    /// </summary>
    /// <param name="players">liste des joueurs</param>
    public Table(List<Player> players)
    {
        roundNumber = 1;
        this.Players = players;
        InitPlayersValue();
        scoreCalculator = new TableScoreCalculator(this);
        StartPlayersTurns();
    }

    /// <summary>
    /// Permet de récupérer le score d'un joueur
    /// </summary>
    /// <param name="player">joueur dont on veut le score</param>
    /// <returns>score entier</returns>
    public int GetScoreOfPlayer(Player player)
    {
        return scoreCalculator.GetScoreOfPlayer(player);
    }

    /// <summary>
    /// Effectue les opérations de changement de tour
    /// </summary>
    public void NextTurn()
    {
        ActualizeHands();
        StartPlayersTurns();
        // On passe à la manche suivante si les joueurs n'ont plus de cartes dans leur main
        if (NoMoreCards())
            NextRound();
    }

    #endregion

    #region Méthodes privées

    private void StartPlayersTurns()
    {
        foreach (var player in Players) player.PlayerTurn();
    }

    // Retourne true si toutes les cartes distribuées aux joueurs ont été posées
    private bool NoMoreCards()
    {
        return !Players.Any(player => player.HaveCards);
    }

    // Passe à la manche suivante
    private void NextRound()
    {
        scoreCalculator.CalculateScore();
        foreach (var player in Players) EndPlayerRound(player);
        RoundNumber = roundNumber + 1;
    }

    // Doit être appelé sur chaque joueur à la fin de chaque manche
    private void EndPlayerRound(Player player)
    {
        var specialCards = player.EndRound();
        //throw new NotImplementedException("Il faut encore implémenter la gestion des cartes spéciales !");
    }

    // Initialiser les joueurs
    private void InitPlayersValue()
    {
        if (Players.Count is < MIN_PLAYERS or > MAX_PLAYERS)
            throw new WrongPlayersNumberException($"Le nombre de joueur doit être inclus entre {MIN_PLAYERS} et {MAX_PLAYERS}");


        var hands = new HandFactory().CreateHands(Players.Count);


        for (var i = 0; i < Players.Count; i++)
        {
            Players[i].Id = i;
            Players[i].Hand = hands[i];
            Players[i].PropertyChanged += PlayerNotification;
        }
    }

    private void PlayerNotification(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Player.HavePlayed):
                // On passe au tour suivant si tout le monde a joué
                if (Players.All(gamePlayer => gamePlayer.HavePlayed))
                    NextTurn();
                break;
        }
    }

    // Actualise les mains des joueurs en effectuant une rotation
    private void ActualizeHands()
    {
        var rotatedHands = RotateHands();
        for (var index = 0; index < rotatedHands.Count; index++) 
            Players[index].Hand = rotatedHands[index];
    }

    // Créée la liste des mains pour la rotation
    private List<Hand> RotateHands()
    {
        var hands = Enumerable.Repeat((Hand)null, Players.Count).ToList();


        // Make the hands rotate position positively if the game order is progressive, opposite order if regressive
        var rotation = GameOrder == GameOrderEnum.PROGRESSIVE ? 1 : -1;
        
        for (var i = 0; i < Players.Count; i++)
        {
            var index = (i + rotation) % Players.Count;
            if (index < 0) index += Players.Count;
            hands[index] = Players[i].Hand;
        }
        

        if (hands is null || hands.Count != Players.Count || hands.Contains(null))
            throw new HandsRotationException("La nouvelle liste de mains est invalide");

        return hands;
    }

    #endregion
}
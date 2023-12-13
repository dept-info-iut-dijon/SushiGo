using Logic_Layer.cards;
using Logic_Layer.factories;
using Logic_Layer.logic_exceptions;
using Logic_Layer.score;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic_Layer;

/// <summary>
/// Table de jeu
/// </summary>
public class Table : INotifyPropertyChanged
{
    private readonly List<Player> players;
    private int roundNumber;
    private TableScoreCalculator scoreCalculator;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Numéro de la manche de la partie
    /// </summary>
    public int RoundNumber
    {
        get
        {
            return roundNumber;
        }
        private set
        {
            roundNumber = value;
            NotifyPropertyChanged();
        }

    }

    public List<Player> Players => players;



    #region Méthodes publiques


    /// <summary>
    /// Initialise la table de jeu
    /// </summary>
    /// <param name="players">liste des joueurs</param>
    public Table(List<Player> players)
    {
        this.players = players;
        InitPlayersValue();
        this.scoreCalculator = new TableScoreCalculator(this);
        this.StartPlayersTurns();
    }

    /// <summary>
    /// Permet de récupérer le score d'un joueur
    /// </summary>
    /// <param name="player">joueur dont on veut le score</param>
    /// <returns>score entier</returns>
    public int GetScoreOfPlayer(Player player)
    {
        return this.scoreCalculator.GetScoreOfPlayer(player);
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
        foreach (Player player in this.players)
        {
            player.PlayerTurn();
        }
    }

    // Retourne true si toutes les cartes distribuées aux joueurs ont été posées
    private bool NoMoreCards()
    {
        return !players.Any(player => player.HaveCards);
    }

    // Passe à la manche suivante
    private void NextRound()
    {
        this.scoreCalculator.CalculateScore();
        foreach (var player in players)
        {
            EndPlayerRound(player);
        }
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
        if (this.players.Count is < 2 or > 5)
        {
            throw new WrongPlayersNumberException("Le nombre de joueur doit être inclus entre 2 et 5");
        }


        List<Hand> hands = new HandFactory().CreateHands(this.players.Count);


        for (int i = 0; i < this.players.Count; i++)
        {
            players[i].Id = i;
            players[i].Hand = hands[i];
            players[i].PropertyChanged += PlayerNotification;
        }
    }

    private void PlayerNotification(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Player.HavePlayed):
                // On passe au tour suivant si tout le monde a joué
                if (players.All(gamePlayer => gamePlayer.HavePlayed))
                    NextTurn();
                break;
        }

    }

    // Actualise les mains des joueurs en effectuant une rotation
    private void ActualizeHands()
    {
        var rotatedHands = RotateHands();
        for (var index = 0; index < rotatedHands.Count; index++)
        {
            players[index].Hand = rotatedHands[index];
        }
    }

    // Créée la liste des mains pour la rotation
    private List<Hand> RotateHands()
    {
        List<Hand?> hands = Enumerable.Repeat((Hand)null, players.Count).ToList();
        for (var index = 0; index < players.Count; index++)
        {
            var player = players[index];
            if (index + 1 < players.Count)
                hands[index + 1] = player.Hand;
            else hands[0] = player.Hand;
        }

        if (hands is null || hands.Count != players.Count || hands.Contains(null))
        {
            throw new HandsRotationException("La nouvelle liste de mains est invalide");
        }

        return hands;
    }
    #endregion
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic_Layer.cards;
using Logic_Layer.factories;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer;

/// <summary>
/// Table de jeu
/// </summary>
public sealed class Table : INotifyPropertyChanged
{
    private readonly List<Player> players;
    private int roundNumber;
    
    /// <summary>
    /// Numéro de la manche de la partie
    /// </summary>
    public int RoundNumber
    {
        get => roundNumber;
        private set
        {
            roundNumber = value;
            OnPropertyChanged(nameof(roundNumber));
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
    }


    /// <summary>
    /// Effectue les opérations de changement de tour
    /// </summary>
    public void NextTurn()
    {
        ActualizeHands();
        // On passe à la manche suivante si les joueurs n'ont plus de cartes dans leur main
        if (NoMoreCards()) NextRound();
    }
    
    /// <summary>
    /// Fait jouer une carte à un joueur
    /// </summary>
    /// <param name="player">Le joueur que l'on veut faire jouer</param>
    /// <param name="card">La carte à jouer</param>
    /// <exception cref="PlayerImpossibleToFindException">Lancée quand le joueur demandé n'est pas en jeu</exception>
    public void PlayCard(Player player, Card card)
    {
        Player? myPlayer = players.Find(myPlayer => myPlayer.Equals(player));

        if (myPlayer is null)
        {
            throw new PlayerImpossibleToFindException("Le joueur n'est pas dans la partie");
        }

        // On joue la carte si le joueur n'est pas null et n'a pas encore joué sur ce tour
        if (!myPlayer.HavePlayed) myPlayer.PlayCard(card);
        
        // On passe au tour suivant si tout le monde a joué
        if (players.All(gamePlayer => gamePlayer.HavePlayed)) NextTurn();
    }
    
    #endregion

    #region Méthodes privées
    // Retourne true si toutes les cartes distribuées aux joueurs ont été posées
    private bool NoMoreCards()
    {
        return !players.Any(player => player.HaveCards);
    }

    // Passe à la manche suivante
    private void NextRound()
    {
        foreach (var player in players)
        {
            EndPlayerRound(player);
        }
        RoundNumber++;
    }
    
    // Doit être appelé sur chaque joueur à la fin de chaque manche
    private void EndPlayerRound(Player player)
    {
        var specialCards = player.EndRound();
        //TODO Il faut encore implémenter la gestion des cartes spéciales
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
                hands[index+1] = player.Hand;
            else hands[0] = player.Hand;
        }

        if (hands is null || hands.Count != players.Count || hands.Contains(null))
        {
            throw new HandsRotationException("La nouvelle liste de mains est invalide");
        }
        
        return hands;
    }
    #endregion

    #region Notify
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

}
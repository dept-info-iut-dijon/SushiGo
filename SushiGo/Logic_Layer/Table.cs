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
    private const int ROUND_MAX = 3;
    private const int MAX_PLAYERS = 5;
    private const int MIN_PLAYERS = 2;

    #region Attributs

    private readonly List<Player> players;
    private int roundNumber;
    private readonly TableScoreCalculator scoreCalculator;
    #endregion

    #region Propriétés
    /// <summary>
    /// Numéro de la manche de la partie
    /// </summary>
    public int RoundNumber
    {
        get => roundNumber;
        private set
        {
            roundNumber = value;
            this.NotifyPropertyChanged();
        }
    }

    /// <summary>
    /// Retourne le de rotation des mains pour la manche actuelle
    /// </summary>
    public GameOrderEnum GameOrder => RoundNumber % 2 == 0 ? GameOrderEnum.REGRESSIVE : GameOrderEnum.PROGRESSIVE;
    /// <summary>
    /// Liste des joueurs.
    /// </summary>
    public List<Player> Players => players;

    #endregion

    #region Events
    /// <summary>
    /// Permet de d'abonner un objet à la notification de changement de propriété
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    /// <summary>
    /// Initialise la table de jeu
    /// </summary>
    /// <param name="players">liste des joueurs</param>
    public Table(List<Player> players)
    {
        this.players = players;
        this.InitPlayersValue();
        this.scoreCalculator = new TableScoreCalculator(this);
        this.StartPlayersTurns();
        this.RoundNumber = 1;
    }


    #region Méthodes publiques

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
        this.ActualizeHands();
        this.StartPlayersTurns();

        // On passe à la manche suivante si les joueurs n'ont plus de cartes dans leur main
        if (this.NoMoreCards())
        {
            this.NextRound();
        }
    }

    #endregion

    #region Méthodes privées

    private void StartPlayersTurns()
    {
        foreach (var player in Players) player.PlayerTurn();
    }

    /// <summary>
    /// Permet de savoir s'il reste des cartes.
    /// </summary>
    /// <returns>Retourne true si toutes les cartes distribuées aux joueurs ont été posées. Faux sinon.</returns>
    private bool NoMoreCards()
    {
        return !Players.Any(player => player.HaveCards);
    }

    /// <summary>
    /// Passe à la manche suivante.
    /// </summary>
    private void NextRound()
    {
        this.scoreCalculator.CalculateScore();

        foreach (var player in players)
        {
            this.EndPlayerRound(player);
        }

        // Si toutes les manches n'ont pas été faites
        if (RoundNumber < ROUND_MAX)
        {
            RoundNumber = roundNumber + 1;
            this.GiveNewHands();
            this.StartPlayersTurns();
        }
        else
        {
            // TODO : Fin de partie
        }
    }

    /// <summary>
    /// Donne une nouvelle main à chaque joueur.
    /// </summary>
    private void GiveNewHands()
    {
        // Créé autant de mains qu'il y a de joueurs
        List<Hand> hands = new HandFactory().CreateHands(this.players.Count);

        for (int i = 0; i < this.players.Count; i++)
        {
            players[i].Hand = hands[i];
        }
    }

    /// <summary>
    /// Doit être appelé sur chaque joueur à la fin de chaque manche.
    /// </summary>
    /// <param name="player">Joueur</param>
    private void EndPlayerRound(Player player)
    {
        var specialCards = player.EndRound();
        //throw new NotImplementedException("Il faut encore implémenter la gestion des cartes spéciales !");
    }

    /// <summary>
    /// Initialiser les joueurs.
    /// </summary>
    /// <exception cref="WrongPlayersNumberException">Levée si le nombre de joueur est incorrecte.</exception>
    private void InitPlayersValue()
    {
        if (Players.Count is < MIN_PLAYERS or > MAX_PLAYERS)
            throw new WrongPlayersNumberException($"Le nombre de joueur doit être inclus entre {MIN_PLAYERS} et {MAX_PLAYERS}");

        for (int i = 0; i < this.players.Count; i++)
        {
            players[i].Id = i;
            players[i].PropertyChanged += this.PlayerNotification;
        }

        this.GiveNewHands();
    }

    private void PlayerNotification(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Player.HavePlayed):
                // On passe au tour suivant si tout le monde a joué
                if (players.All(gamePlayer => gamePlayer.HavePlayed))
                {
                    this.NextTurn();
                }

                break;
        }
    }

    /// <summary>
    /// Actualise les mains des joueurs en effectuant une rotation
    /// </summary>
    private void ActualizeHands()
    {
        var rotatedHands = this.RotateHands();
        for (var index = 0; index < rotatedHands.Count; index++)
        {
            players[index].Hand = rotatedHands[index];
        }
    }

    /// <summary>
    /// Créée la liste des mains pour la rotation.
    /// </summary>
    /// <exception cref="HandsRotationException">Levée si la nouvelle liste de mains est invalide.</exception>
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
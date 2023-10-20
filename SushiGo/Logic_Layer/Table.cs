using Logic_Layer.cards;
using Logic_Layer.factories;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer;

/// <summary>
/// Table de jeu
/// </summary>
public class Table
{
    private List<Player> players;
    private int currentPlayerIndex;

    private int CurrentPlayerIndex
    {
        get => currentPlayerIndex;
        set => currentPlayerIndex = value;
    }

    /// <summary>
    /// Joueur en train d'effectuer son tour
    /// </summary>
    public Player CurrentPlayer => players[currentPlayerIndex];

    public List<Player> Players => players;

    #region Méthodes publiques
    /// <summary>
    /// Initialise la table de jeu
    /// </summary>
    /// <param name="playersNumber">Nombre de joueurs participants</param>
    public Table(int playersNumber)
    {
        players = new List<Player>();
        InitPlayersValue(playersNumber);

        currentPlayerIndex = 0;
    }

    /// <summary>
    /// Effectue les opérations de changement de joueur
    /// </summary>
    /// <returns>Nouveau joueur courant</returns>
    public Player NextPlayerTurn()
    {
        // On passe à la manche suivante si les joueurs n'ont plus de cartes dans leur main
        if (NoMoreCards()) NextRound();
        
        return NextPlayer();
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

        // On joue la carte si le joueur n'est pas null
        myPlayer.PlayCard(card);
    }
    
    #endregion

    #region Méthodes privées
    /// <summary>
    /// Arrive après un tour complet de la table
    /// </summary>
    private void NextTableTurn()
    {
        ActualizeHands();
    }
    
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
            EndPlayerTurn(player);
        }
    }
    
    // Doit être appelé sur chaque joueur à la fin de chaque manche
    private void EndPlayerTurn(Player player)
    {
        var specialCards = player.EndTurn();
        throw new NotImplementedException("Il faut encore implémenter la gestion des cartes spéciales !");
    }
    
    /// <summary>
    /// Actualise le joueur courant
    /// </summary>
    /// <returns>Le nouveau joueur courant</returns>
    private Player NextPlayer()
    {
        if (currentPlayerIndex < players.Count)
        {
            CurrentPlayerIndex++;
        }
        else
        {
            CurrentPlayerIndex = 0;
            NextTableTurn();
        }

        return CurrentPlayer;
    }
    
    // Initialise les joueurs participants
    private void InitPlayersValue(int playersNumber)
    {
        if (playersNumber is < 2 or > 5)
        {
            throw new WrongPlayersNumberException("Le nombre de joueur doit être inclus entre 2 et 5");
        }

        players = new List<Player>();
        List<Hand> hands = new HandFactory().CreateHands(playersNumber);

        for (int i = 0; i < playersNumber; i++)
        {
            players.Add(new Player(i, new Board(), hands[i]));
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

    // Retourne les mains des joueurs après rotation
    private List<Hand> RotateHands()
    {
        List<Hand> hands = new List<Hand>();
        for (var index = 0; index < players.Count; index++)
        {
            var player = players[index];
            if (index + 1 < players.Count) hands.Insert(index + 1, player.Hand);
            else hands.Insert(0, player.Hand);
        }
        return hands;
    }
    #endregion
}
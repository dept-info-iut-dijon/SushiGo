using Logic_Layer.cards;

namespace Logic_Layer;

public class Player
{
    private int id;
    private string pseudo;
    private readonly Board board;
    private Hand hand;
    private bool havePlayed;

    /// <summary>
    /// Indique si le joueur a terminé de jouer dans le tour courant
    /// </summary>
    public bool HavePlayed => havePlayed;

    /// <summary>
    /// Identifiant du joueur dans la partie
    /// </summary>
    public int Id 
    {
        get => id;
        set => id = value;
    }

    /// <summary>
    /// Indique si le joueur a encore des cartes dans sa main
    /// </summary>
    public bool HaveCards => hand.Cards.Count > 0;

    /// <summary>
    /// Main de cartes du joueur
    /// </summary>
    public Hand Hand
    {
        get => hand;
        set => hand = value;
    }
    /// <summary>
    /// Pseudo du joueur
    /// </summary>
    public string Pseudo => pseudo;

    /// <summary>
    /// Plateau du joueur
    /// </summary>
    public Board Board => board;

    public Player(int id, Board board, Hand hand,string pseudo)
    {
        this.id = id;
        this.board = board;
        this.hand = hand;
        this.pseudo = pseudo;
        havePlayed = false;
    }

    /// <summary>
    /// Joue une carte
    /// </summary>
    /// <param name="card">La carte à jouer</param>
    public void PlayCard(Card card)
    {
        hand.PlayCard(card, board);
        havePlayed = true;
    }
    
    /// <summary>
    /// Effectue les actions nécessaires au début du tour du joueur possédant la main
    /// </summary>
    /// <see cref="ISpecialCard"/>
    /// <returns>Les cartes spéciales à prendre en compte</returns>
    public List<ISpecialCard> PlayerTurn()
    {
        return board.PlayerTurn();
    }
    
    /// <summary>
    /// Effectue les actions nécessaires à la fin de la manche
    /// </summary>
    /// <returns></returns>
    public List<ISpecialCard> EndRound()
    {
        return board.EndRound();
    }
}
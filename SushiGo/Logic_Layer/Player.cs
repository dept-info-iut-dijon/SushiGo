using Logic_Layer.cards;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic_Layer;

public class Player : INotifyPropertyChanged
{
    private int id;
    private string pseudo;
    private Board board;
    private Hand hand;
    private bool havePlayed;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Indique si le joueur a terminé de jouer dans le tour courant
    /// </summary>
    public virtual bool HavePlayed 
    { 
        get 
        { 
            return havePlayed; 
        } 
        protected set 
        { 
            havePlayed = value; 
        } 
    }

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
    public string Pseudo { get => pseudo; set => pseudo = value; }
    /// <summary>
    /// Board des cartes posées du joueur
    /// </summary>
    public Board Board { get => board; }

    public Player(int id, Board board, Hand hand, string pseudo)
    {
        this.id = id;
        this.board = board;
        this.hand = hand;
        this.pseudo = pseudo;
        havePlayed = false;
    }


    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Joue une carte
    /// </summary>
    /// <param name="card">La carte à jouer</param>
    public void PlayCard(Card card)
    {
        // On joue la carte si le joueur n'est pas null et n'a pas encore joué sur ce tour
        if (!HavePlayed)
        {
            hand.PlayCard(card, board);
            havePlayed = true;

            NotifyPropertyChanged(nameof(Player.HavePlayed));

        }
    }

    /// <summary>
    /// Joue deux cartes à la fois
    /// </summary>
    /// <param name="firstCard">Première carte à jouer</param>
    /// <param name="secondCard">Deuxième carte à jouer</param>
    public void PlayCard(Card firstCard, Card secondCard)
    {
        // On ignore la demande si le joueur a déjà joué ou si le board ne peut pas jouer deux cartes
        if (HavePlayed || !board.CanPlayTwoCards) return;
        
        hand.PlayCard(firstCard, secondCard, board);
        havePlayed = true;
    }

    /// <summary>
    /// Effectue les actions nécessaires au début du tour du joueur possédant la main
    /// </summary>
    /// <see cref="ISpecialCard"/>
    /// <returns>Les cartes spéciales à prendre en compte</returns>
    public List<ISpecialCard> PlayerTurn()
    {
        HavePlayed = false;
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
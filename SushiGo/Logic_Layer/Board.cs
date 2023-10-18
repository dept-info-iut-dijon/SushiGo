﻿using Logic_Layer.cards;

namespace Logic_Layer;

/// <summary>
/// Représente les cartes posées sur la table devant le joueur
/// </summary>
public class Board
{
    private List<Card> cards;

    public Board()
    {
        cards = new List<Card>();
    }

    /// <summary>
    /// Liste des cartes du joueur
    /// </summary>
    public List<Card> Cards => cards;

    /// <summary>
    /// Effectue les actions nécessaires au début du tour du joueur possédant la main
    /// </summary>
    /// <see cref="ISpecialCard"/>
    /// <returns>Les cartes spéciales à prendre en compte</returns>
    public List<ISpecialCard> PlayerTurn()
    {
        List<ISpecialCard> ret = new List<ISpecialCard>();
        
        // On rassemble les cartes spéciales devant être prises en compte
        foreach (Card c in Cards)
        {
            if (c is ISpecialCard card && card.PlayerTurn())
                ret.Add(card);
        }
        
        return ret;
    }

    /// <summary>
    /// Effectue les actions nécessaires à la fin de la manche
    /// </summary>
    /// <returns></returns>
    public List<ISpecialCard> EndTurn()
    {
        List<ISpecialCard> ret = new List<ISpecialCard>();

        // On rassemble les cartes spéciales devant être prises en compte
        foreach (Card c in Cards)
        {
            if (c is ISpecialCard card && card.EndRound())
                ret.Add(card);
        }

        return ret;
    }

    /// <summary>
    /// Ajoute une carte sur le plateau
    /// </summary>
    /// <param name="card">Carte à ajouter</param>
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}
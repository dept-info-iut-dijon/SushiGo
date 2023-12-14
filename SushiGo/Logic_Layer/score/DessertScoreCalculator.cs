using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.score;

/// <summary>
/// Calculateur de score pour les cartes desserts (ne doit être appelé qu'à la fin de la partie)
/// </summary>
public class DessertScoreCalculator : IScoreCalculator
{
    /// <inheritdoc />
    public virtual Dictionary<int, int> CalculateScore(List<Player> players)
    {
        var scores = new Dictionary<int, int>();
        var dessertCards = new Dictionary<int, List<Card>>();

        var playersWithMostDesserts = new List<Player>();
        var playersWithLeastDesserts = new List<Player>();
        int mostDesserts = 0;

        // Filtrer pour ne retrouver que les cartes desserts
        foreach (var player in players)
        {
            dessertCards[player.Id] = CardsSorter.TypeSort(typeof(DessertCard), player.Board.Cards);
            scores[player.Id] = 0;
        }

        // On donne 6 points au.x joueur.s avec le plus de cartes desserts
        mostDesserts = AddScoreToPlayersWithMostDesserts(players, dessertCards, mostDesserts, playersWithMostDesserts, scores);

        int leastDesserts = mostDesserts; // on initialise la variable ici pour pouvoir gérer correctement le cas où les joueurs avec le plus de cartes desserts sont aussi ceux avec le moins de cartes desserts

        // On retire 6 points au.x joueur.s avec le moins de cartes desserts
        RemoveScoreFromPlayersWithLeastDesserts(players, dessertCards, leastDesserts, playersWithLeastDesserts, scores);

        return scores;
    }

    private static void RemoveScoreFromPlayersWithLeastDesserts(List<Player> players, Dictionary<int, List<Card>> dessertCards, int leastDesserts,
        List<Player> playersWithLeastDesserts, Dictionary<int, int> scores)
    {
        // Définir le/les joueurs avec le moins de cartes desserts
        GetPlayerWithLeastDesserts(players, dessertCards, leastDesserts, playersWithLeastDesserts);

        // Leur retirer 6 points (à partager si égalité)
        foreach (var player in playersWithLeastDesserts)
        {
            scores[player.Id] -= 6 / playersWithLeastDesserts.Count;
        }
    }

    private static int AddScoreToPlayersWithMostDesserts(List<Player> players, Dictionary<int, List<Card>> dessertCards, int mostDesserts,
        List<Player> playersWithMostDesserts, Dictionary<int, int> scores)
    {
        // Définir le/les joueurs avec le plus de cartes desserts
        mostDesserts = GetPlayerWithMostDesserts(players, dessertCards, mostDesserts, playersWithMostDesserts);

        // Leur ajouter 6 points (à partager si égalité)
        foreach (var player in playersWithMostDesserts)
        {
            scores[player.Id] += 6 / playersWithMostDesserts.Count;
        }
        return mostDesserts;
    }

    // Définir le/les joueurs avec le plus de cartes desserts
    private static int GetPlayerWithMostDesserts(List<Player> players, Dictionary<int, List<Card>> dessertCards, int mostDesserts,
        List<Player> playersWithMostDesserts)
    {
        foreach (var player in players)
        {
            if (dessertCards[player.Id].Count > mostDesserts)
            {
                mostDesserts = dessertCards[player.Id].Count;
                playersWithMostDesserts.Clear();
                playersWithMostDesserts.Add(player);
            }
            else if (dessertCards[player.Id].Count == mostDesserts)
            {
                playersWithMostDesserts.Add(player);
            }
        }

        return mostDesserts;
    }

    // Définir le/les joueurs avec le moins de cartes desserts
    private static void GetPlayerWithLeastDesserts(List<Player> players, Dictionary<int, List<Card>> dessertCards, int leastDesserts,
        List<Player> playersWithLeastDesserts)
    {
        foreach (var player in players)
        {
            if (dessertCards[player.Id].Count < leastDesserts)
            {
                leastDesserts = dessertCards[player.Id].Count;
                playersWithLeastDesserts.Clear();
                playersWithLeastDesserts.Add(player);
            }
            else if (dessertCards[player.Id].Count == leastDesserts)
            {
                playersWithLeastDesserts.Add(player);
            }
        }
    }
}
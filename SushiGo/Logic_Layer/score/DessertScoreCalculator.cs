using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.score;

public class DessertScoreCalculator : IScoreCalculator
{
    public Dictionary<int, int> CalculateScore(List<Player> players)
    {
        var scores = new Dictionary<int, int>();
        var dessertCards = new Dictionary<Player, List<Card>>();
        
        var playersWithMostDesserts = new List<Player>();
        var playersWithLeastDesserts = new List<Player>();
        int mostDesserts = 0;
        int leastDesserts = 0;
        
        // Filtrer pour ne retrouver que les cartes desserts
        foreach (var player in players)
        {
            dessertCards[player] = CardsSorter.TypeSort(typeof(DessertCard), player.Board.Cards);
        }
        
        // On donne 6 points au.x joueur.s avec le plus de cartes desserts
        AddScoreToPlayersWithMostDesserts(players, dessertCards, mostDesserts, playersWithMostDesserts, scores);

        // On retire 6 points au.x joueur.s avec le moins de cartes desserts
        RemoveScoreFromPlayersWithLeastDesserts(players, dessertCards, leastDesserts, playersWithLeastDesserts, scores);

        return scores;
    }

    private static void RemoveScoreFromPlayersWithLeastDesserts(List<Player> players, Dictionary<Player, List<Card>> dessertCards, int leastDesserts,
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

    private static void AddScoreToPlayersWithMostDesserts(List<Player> players, Dictionary<Player, List<Card>> dessertCards, int mostDesserts,
        List<Player> playersWithMostDesserts, Dictionary<int, int> scores)
    {
        // Définir le/les joueurs avec le plus de cartes desserts
        GetPlayerWithMostDesserts(players, dessertCards, mostDesserts, playersWithMostDesserts);

        // Leur ajouter 6 points (à partager si égalité)
        foreach (var player in playersWithMostDesserts)
        {
            scores[player.Id] += 6 / playersWithMostDesserts.Count;
        }
    }

    // Définir le/les joueurs avec le plus de cartes desserts
    private static void GetPlayerWithMostDesserts(List<Player> players, Dictionary<Player, List<Card>> dessertCards, int mostDesserts,
        List<Player> playersWithMostDesserts)
    {
        foreach (var player in players)
        {
            if (dessertCards[player].Count > mostDesserts)
            {
                mostDesserts = dessertCards[player].Count;
                playersWithMostDesserts.Clear();
                playersWithMostDesserts.Add(player);
            }
            else if (dessertCards[player].Count == mostDesserts)
            {
                playersWithMostDesserts.Add(player);
            }
        }
    }

    // Définir le/les joueurs avec le moins de cartes desserts
    private static void GetPlayerWithLeastDesserts(List<Player> players, Dictionary<Player, List<Card>> dessertCards, int leastDesserts,
        List<Player> playersWithLeastDesserts)
    {
        foreach (var player in players)
        {
            if (dessertCards[player].Count < leastDesserts)
            {
                leastDesserts = dessertCards[player].Count;
                playersWithLeastDesserts.Clear();
                playersWithLeastDesserts.Add(player);
            }
            else if (dessertCards[player].Count == leastDesserts)
            {
                playersWithLeastDesserts.Add(player);
            }
        }
    }
}
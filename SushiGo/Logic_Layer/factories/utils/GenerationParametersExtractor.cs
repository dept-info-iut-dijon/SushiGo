namespace Logic_Layer.factories.utils;

public class GenerationParametersExtractor
{
    /// <summary>
    /// Extrait toutes les données de générations de la config
    /// </summary>
    /// <returns>Données de génération</returns>
    public Dictionary<string, int> GetParameters()
    {
        // Temporairement on génèrera les données ici, le but final est de les stocker dans un fichier externe
        return CreateParameters();
    }

    //TODO à externaliser, c'est vraiment sale de hardcoder comme ça
    private Dictionary<string, int> CreateParameters()
    {
        return new Dictionary<string, int>()
        {
            { "tempura", 14 },
            { "sashimi", 14 },
            { "ravioli", 14 },
            { "maki 1", 6 },
            { "maki 2", 12 },
            { "maki 3", 8 },
            { "sushi saumon", 10 },
            { "sushi calamar", 5 },
            { "sushi omelette", 10 },
            { "dessert", 10 },
            { "wasabi", 6 },
            // { "baguette", 4 }
        };
        //TODO La génération des cartes spéciales est désactivée pour l'instant car leur gestion n'est pas encore totalement implémentée
    }
}
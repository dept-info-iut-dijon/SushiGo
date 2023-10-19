namespace Logic_Layer.cards.utils;

/// <summary>
/// Convertis les SushiTypes depuis ou vers d'autres types
/// </summary>
public class SushiTypesConverter
{
    private Dictionary<string, SushiTypes> translationDictionnary = new Dictionary<string, SushiTypes>()
    {
        { "saumon", SushiTypes.SALMON },
        { "calamar", SushiTypes.CALAMARI },
        { "omelette", SushiTypes.OMELETTE }
    };

    /// <summary>
    /// Convertis une chaîne de caractère en SushiTypes
    /// </summary>
    /// <param name="sushi">Chaîne à convertir</param>
    /// <returns>Élément de SushiTypes</returns>
    public SushiTypes StringToSushi(string sushi)
    {
        return translationDictionnary[sushi];
    }
}
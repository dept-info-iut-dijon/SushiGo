using Logic_Layer.logic_exceptions;

namespace Logic_Layer.cards.utils;

/// <summary>
/// Convertis les SushiTypes depuis ou vers d'autres types
/// </summary>
public class SushiTypesConverter
{
    private readonly Dictionary<string, SushiTypes> translationDictionnary = new()
    {
        { "saumon", SushiTypes.SALMON },
        { "calamar", SushiTypes.CALAMARI },
        { "omelette", SushiTypes.OMELETTE }
    };

    private static readonly Dictionary<SushiTypes, String> typeToString = new()
    {
        { SushiTypes.SALMON, "Saumon" },
        { SushiTypes.CALAMARI, "Calamar" },
        { SushiTypes.OMELETTE, "Omelette" }
    };

    /// <summary>
    /// Convertis une chaîne de caractère en SushiTypes
    /// </summary>
    /// <param name="sushi">Chaîne à convertir</param>
    /// <returns>Élément de SushiTypes</returns>
    public SushiTypes StringToSushi(string sushi)
    {
        if (translationDictionnary.TryGetValue(sushi, out var toSushi))
            return toSushi;
        throw new SushiTypeDoesNotExist($"Le type de sushi {sushi} n'existe pas");
    }

    /// <summary>
    /// Convertis un SushiType en string destiné à être lu par un humain
    /// </summary>
    /// <param name="type">Le type à convertir</param>
    /// <returns>Le string</returns>
    public String SushiToString(SushiTypes type)
    {
        return typeToString[type];
    }
}
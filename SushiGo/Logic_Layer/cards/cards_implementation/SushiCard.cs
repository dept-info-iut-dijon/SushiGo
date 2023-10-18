namespace Logic_Layer.cards.cards_implementation;

public class SushiCard : Card
{
    private SushiTypes type;

    public SushiTypes Type => type;
    
    public override string Name => $"Sushi {TypeToString}";

    private static readonly Dictionary<SushiTypes, String> typeToString = new Dictionary<SushiTypes, string>()
    {
        { SushiTypes.SALMON, "Saumon" },
        { SushiTypes.CALAMARI, "Calamar" },
        { SushiTypes.OMELETTE, "Omelette" }
    };

    private String TypeToString
    {
        get => typeToString[type];
    }

    /// <summary>
    /// Créée une carte sushi
    /// </summary>
    /// <param name="type">Type de sushi de la carte</param>
    public SushiCard(SushiTypes type)
    {
        this.type = type;
    }
}
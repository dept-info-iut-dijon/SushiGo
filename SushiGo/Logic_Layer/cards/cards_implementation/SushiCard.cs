using Logic_Layer.cards.utils;

namespace Logic_Layer.cards.cards_implementation;

public class SushiCard : Card
{
    private SushiTypes type;
    private SushiTypesConverter converter;

    public SushiTypes Type => type;
    public override string Name => $"Sushi{TypeToString}";

    private string TypeToString => converter.SushiToString(type);

    /// <summary>
    /// Créée une carte sushi
    /// </summary>
    /// <param name="type">Type de sushi de la carte</param>
    public SushiCard(SushiTypes type)
    {
        this.type = type;
        converter = new SushiTypesConverter();
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SushiCard card &&
               Name == card.Name &&
               type == card.type &&
               Type == card.Type &&
               TypeToString == card.TypeToString;
    }
}
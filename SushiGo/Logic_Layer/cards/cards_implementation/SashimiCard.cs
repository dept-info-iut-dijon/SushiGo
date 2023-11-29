namespace Logic_Layer.cards.cards_implementation;

public class SashimiCard : Card
{
    public override string Name => "Sashimi";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is SashimiCard card &&
               Name == card.Name;
    }
}
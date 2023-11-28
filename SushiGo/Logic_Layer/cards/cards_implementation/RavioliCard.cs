namespace Logic_Layer.cards.cards_implementation;

public class RavioliCard : Card
{
    public RavioliCard()
    {
    }

    public override string Name => "Ravioli";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is RavioliCard card &&
               Name == card.Name;
    }
}
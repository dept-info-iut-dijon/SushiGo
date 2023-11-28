namespace Logic_Layer.cards.cards_implementation;

public class TempuraCard : Card
{
    public override string Name => "Tempura";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is TempuraCard card &&
               Name == card.Name;
    }
}
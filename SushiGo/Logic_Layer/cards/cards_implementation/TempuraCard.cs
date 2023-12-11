namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Implémentation de la carte Tempura
/// </summary>
public class TempuraCard : Card
{
    /// <inheritdoc/>
    public override string Name => "Tempura";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is TempuraCard card &&
               Name == card.Name;
    }
}
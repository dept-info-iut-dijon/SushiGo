namespace Logic_Layer.cards.cards_implementation;

public class MakiCard : Card
{
    protected bool Equals(MakiCard other)
    {
        return quantity == other.quantity;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return quantity;
    }

    private readonly int quantity;
    
    /// <summary>
    /// Quantité de makis présents sur la carte
    /// </summary>
    public int Quantity => quantity;

    /// <summary>
    /// Créée une instance de carte maki
    /// </summary>
    /// <param name="quantity">Quantité de makis présents sur la carte</param>
    public MakiCard(int quantity)
    {
        this.quantity = quantity;
    }

    /// <summary>
    /// Nom de la carte, inclus la quantité de makis présents sur la carte
    /// </summary>
    public override string Name => $"Maki{quantity}";

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is MakiCard card &&
               Name == card.Name &&
               quantity == card.quantity &&
               Quantity == card.Quantity;
    }
}
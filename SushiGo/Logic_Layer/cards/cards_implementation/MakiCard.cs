namespace Logic_Layer.cards.cards_implementation;

public class MakiCard : Card
{
    private int quantity;
    
    public int Quantity => quantity;

    /// <summary>
    /// Créée une instance de carte maki
    /// </summary>
    /// <param name="quantity">Quantité de makis présents sur la carte</param>
    public MakiCard(int quantity)
    {
        this.quantity = quantity;
    }

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
namespace Logic_Layer.cards.cards_implementation;

public class MakiCard : Card
{
    protected bool Equals(MakiCard other)
    {
        return quantity == other.quantity;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        if ((obj as MakiCard)!.Quantity.Equals(Quantity) && (obj as MakiCard)!.Name.Equals(Name)) return true;
        return Equals((MakiCard)obj);
    }

    public override int GetHashCode()
    {
        return quantity;
    }

    private readonly int quantity;
    
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
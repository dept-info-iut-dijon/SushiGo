namespace Logic_Layer.cards.cards_implementation;

public class MakiCard : Card
{
    private int quantity;
    
    public int Quantity => quantity;

    /// <summary>
    /// Créée une instance de carte maki
    /// </summary>
    /// <param name="name">Nom de la carte</param>
    /// <param name="quantity">Quantité de makis présents sur la carte</param>
    public MakiCard(string name, int quantity) : base(name)
    {
        this.quantity = quantity;
    }
}
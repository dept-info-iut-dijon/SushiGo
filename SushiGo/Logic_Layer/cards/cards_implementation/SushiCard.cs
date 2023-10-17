namespace Logic_Layer.cards.cards_implementation;

public class SushiCard : Card
{
    private SushiTypes type;

    public SushiTypes Type => type;

    /// <summary>
    /// Créée une carte sushi
    /// </summary>
    /// <param name="name">Nom de la carte</param>
    /// <param name="type">Type de sushi de la carte</param>
    public SushiCard(string name, SushiTypes type) : base(name)
    {
        this.type = type;
    }
}
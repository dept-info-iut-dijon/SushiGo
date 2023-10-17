namespace Logic_Layer.cards;

/// <summary>
/// Représente une carte générale
/// </summary>
public abstract class Card
{
    private string name;

    /// <summary>
    /// Nom de la carte
    /// </summary>
    public string Name
    {
        get => name;
    }

    /// <summary>
    /// Créée les éléments les plus généraux de la carte
    /// </summary>
    /// <param name="name">Nom de la carte</param>
    protected Card(string name)
    {
        this.name = name;
    }
}
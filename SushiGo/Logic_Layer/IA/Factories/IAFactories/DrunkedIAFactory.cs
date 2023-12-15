using Logic_Layer.IA.IAImplementation;

namespace Logic_Layer.IA.Factories.IAFactories
{
    /// <summary>
    /// Fabrique d'IA de diffculté Facile.
    /// </summary>
    public class DrunkedIAFactory : ISpecificIAFactory
    {
        /// <inheritdoc/>
        public IA CreateIA(int id, Board board, Hand hand)
        {
            return new DrunkenIA(id, board, hand, $"IAFacile{id}");
        }
    }
}

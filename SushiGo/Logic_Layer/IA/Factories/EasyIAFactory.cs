using Logic_Layer.IA.IAImplementation;

namespace Logic_Layer.IA.Factories
{
    /// <summary>
    /// Fabrique d'IA de diffculté Facile.
    /// </summary>
    public class EasyIAFactory : IIAFactory
    {
        /// <inheritdoc/>
        public IA CreateIA(int id, Board board, Hand hand)
        {
            return new DrunkenIA(id, board, hand, $"IAFacile{id}");
        }
    }
}

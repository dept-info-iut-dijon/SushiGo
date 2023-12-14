namespace Logic_Layer.IA.Factories
{
    /// <summary>
    /// Interface d'une fabrique d'IA.
    /// </summary>
    public interface IIAFactory
    {
        /// <summary>
        /// Créé une IA selon les paramètres demandés.
        /// </summary>
        /// <param name="id">Id de l'IA.</param>
        /// <param name="board">Table de l'IA.</param>
        /// <param name="hand">Main de l'IA.</param>
        /// <returns>L'IA slon les caractéristiques envoyées en paramètre.</returns>
        public IA CreateIA(int id, Board board, Hand hand);
    }
}

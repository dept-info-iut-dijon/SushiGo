using Logic_Layer.IA.IAImplementation;

namespace Logic_Layer.IA
{
    /// <summary>
    /// Fabrique d'IA.
    /// </summary>
    public class IAFactory
    {
        /// <summary>
        /// Créé une IA selon les paramètres demandés.
        /// </summary>
        /// <param name="difficulty">Difficulté de l'IA.</param>
        /// <param name="id">Id de l'IA.</param>
        /// <param name="board">Table de l'IA.</param>
        /// <param name="hand">Main de l'IA.</param>
        /// <returns>L'IA slon les caractéristiques envoyées en paramètre.</returns>
        public IA CreateIA(IADifficultyEnum difficulty, int id, Board board, Hand hand)
        {
            IA IACreated;

            switch (difficulty)
            {
                // Ajouter vos IAs ici
                case IADifficultyEnum.FACILE: IACreated = new DrunkenIA(id, board, hand, $"IAFacile{id}"); break;

                // On renvoie par défaut une IA Facile
                default: IACreated = new DrunkenIA(id, board, hand, $"IAFacile{id}"); break;
            }

            return IACreated;
        }
    }
}

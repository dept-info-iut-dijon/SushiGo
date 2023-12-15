namespace Logic_Layer.IA.IAImplementation
{
    /// <summary>
    /// IA bourrée : elle pose la premiere carte de sa main
    /// </summary>
    public class DrunkenIA : IA
    {
        /// <inheritdoc/>
        public DrunkenIA(int id, Board board, Hand hand, string pseudo) : base(id, board, hand, pseudo)
        {
        }

        /// <summary>
        /// Joue la première carte de sa main et termine son tour
        /// </summary>
        public override void Play()
        {
            if (HavePlayed == false)
            {
                if (Hand.Cards.Count > 0)
                {
                    PlayCard(Hand.Cards.First());
                }
            }
        }
    }
}

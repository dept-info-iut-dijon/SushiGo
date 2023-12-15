namespace Logic_Layer.IA
{
    /// <summary>
    /// Base de toutes les IA
    /// Une IA est un joueur
    /// </summary>
    public abstract class IA : Player
    {

        /// <inheritdoc/>
        protected IA(int id, Board board, Hand hand, string pseudo) : base(id, board, hand, pseudo)
        {
        }

        /// <summary>
        /// Actions à réaliser au moment du tour de celle-ci
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// Propriété qui dit à l'IA de jouer quand elle a pas joué 
        /// (quand l'attribut passe à false au début d'un tour)
        /// </summary>
        public override bool HavePlayed
        {
            get { return base.HavePlayed; }
            protected set
            {
                base.HavePlayed = value;
                if (!this.HavePlayed)
                {
                    this.Play();
                }
            }
        }
    }
}

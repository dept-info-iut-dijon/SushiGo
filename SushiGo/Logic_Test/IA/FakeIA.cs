using Logic_Layer;

namespace LogicTest.IA
{
    public class FakeIA : Logic_Layer.IA.IA
    {
        /// <inheritdoc/>
        public FakeIA(int id, Board board, Hand hand, string pseudo) : base(id, board, hand, pseudo)
        {
            testPlay = false;
        }
        private bool testPlay;
        /// <summary>
        /// Propriété d'un attribut qui est set seulement quand on passe dans la fonction play
        /// </summary>
        public bool TestPlay { get => testPlay; }

        public override void Play()
        {
            testPlay = true;
        }
    }
}

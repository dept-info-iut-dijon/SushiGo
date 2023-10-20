using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.IA
{
    /// <summary>
    /// IA bourrée : elle pose la premiere carte de sa main
    /// </summary>
    public class DrunkenIA : IA
    {

        public DrunkenIA(int id, Board board, Hand hand) : base(id, board, hand)
        {
        }

        public DrunkenIA(int id, Board board, Hand hand,string pseudo) : base(id, board, hand, pseudo)
        {
        }

        /// <summary>
        /// Joue la première carte de sa main et termine son tour
        /// </summary>
        public override void Play()
        {
            this.PlayCard(Hand.Cards.First());
            this.EndTurn();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.IA
{
    /// <summary>
    /// Base de toutes les IA
    /// Une IA est un joueur
    /// </summary>
    public abstract class IA : Player
    {

        /// <summary>
        /// Reprend le constructeur du joueur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="board"></param>
        /// <param name="hand"></param>
        protected IA(int id, Board board, Hand hand, string pseudo) : base(id, board, hand, pseudo)
        {
        }

        /// <summary>
        /// Actions à réaliser au moment du tour de celle-ci
        /// </summary>
        public abstract void Play();

        public override bool HavePlayed
        {
            get { return base.HavePlayed; }
            protected set
            {
                base.HavePlayed = value;
                if (this.HavePlayed == false)
                {
                    this.Play();
                }
            }
        }
    }
}

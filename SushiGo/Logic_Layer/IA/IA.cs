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
        protected IA(int id, Board board, Hand hand) : base(id, board, hand)
        {
        }

        /// <summary>
        /// Actions à réaliser au moment du tour de celle-ci
        /// </summary>
        public abstract void Play();
    }
}

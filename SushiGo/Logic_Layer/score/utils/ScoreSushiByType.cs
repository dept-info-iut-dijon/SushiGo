using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.score.utils
{
    public class ScoreSushiByType
    {
        private Dictionary<SushiTypes, int> scoreBySushiTypes;
        /// <summary>
        /// Dictionnaire qui donne la correspondance entre le type de sushi et la quantité de points à octroyer
        /// </summary>
        public Dictionary<SushiTypes, int> ScoreBySushiTypes { get => scoreBySushiTypes; }
        /// <summary>
        /// Constructeur qui instancie toutes les valeurs du dictionnaire
        /// </summary>
        public ScoreSushiByType()
        {
            scoreBySushiTypes = new Dictionary<SushiTypes, int>()
            {
                { SushiTypes.OMELETTE, 1 },
                { SushiTypes.SALMON, 2 },
                { SushiTypes.CALAMARI, 3 }
            };
        }
    }
}

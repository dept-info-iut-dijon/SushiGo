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
        public Dictionary<SushiTypes, int> ScoreBySushiTypes { get => scoreBySushiTypes; }

        public ScoreSushiByType()
        {
            scoreBySushiTypes = new Dictionary<SushiTypes, int>();
            scoreBySushiTypes.Add(SushiTypes.OMELETTE, 1);
            scoreBySushiTypes.Add(SushiTypes.SALMON, 2);
            scoreBySushiTypes.Add(SushiTypes.CALAMARI, 3);
        }
    }
}

using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards;
using Logic_Layer;
using LogicTest.datas_generators.score;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest.datas_generators.Score
{
    public class WasabiScoreCalculatorDatasGenerator : IEnumerable<object[]>
    {
        private CreatePlayerUtils createPlayerUtils = new();

        public IEnumerator<object[]> GetEnumerator()
        {
            //Wasabi avec saumon
            yield return new object[]
            {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), new DessertCard(), CreateWasabiCardWithSushi(SushiTypes.SALMON),
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new DessertCard(), CreateWasabiCardWithSushi(SushiTypes.SALMON),
                    new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 4 },
                { 1, 4 }
            }
            };

            //Wasabi avec calamar, 2 wasabi avec calamars
            yield return new object[]
            {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new MakiCard(2), CreateWasabiCardWithSushi(SushiTypes.CALAMARI)
                }),
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new MakiCard(1), new SushiCard(SushiTypes.CALAMARI), new SushiCard(SushiTypes.CALAMARI)
                })
            },
            new Dictionary<int, int>
            {
                { 1, 6 },
                { 0, 0 }
            }
            };
            //Wasabi avec omelette (et 2 wasabi avec omelette)
            yield return new object[]
            {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), CreateWasabiCardWithSushi(SushiTypes.OMELETTE), CreateWasabiCardWithSushi(SushiTypes.OMELETTE)
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), CreateWasabiCardWithSushi(SushiTypes.OMELETTE), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 4 },
                { 1, 2 }
            }
            };
            //2 wasabi avec sushi
            //Sushi sans wasabi + wasabi saumon
            //Aucun wasabi
            yield return new object[]
            {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), CreateWasabiCardWithSushi(SushiTypes.SALMON), CreateWasabiCardWithSushi(SushiTypes.CALAMARI)
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.OMELETTE), new SushiCard(SushiTypes.CALAMARI),
                    CreateWasabiCardWithSushi(SushiTypes.SALMON)
                }),
                createPlayerUtils.CreatePlayer(2, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON),
                    new SushiCard(SushiTypes.SALMON), new DessertCard(), new SushiCard(SushiTypes.CALAMARI)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 10 },
                { 1, 4 },
                { 2, 0 }
            }
            };
            //Wasabi vide
            yield return new object[]
            {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), new WasabiCard()
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.OMELETTE), new SushiCard(SushiTypes.CALAMARI),
                    CreateWasabiCardWithSushi(SushiTypes.SALMON)
                }),
                createPlayerUtils.CreatePlayer(2, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON),
                    new SushiCard(SushiTypes.SALMON), new DessertCard(), CreateWasabiCardWithSushi(SushiTypes.CALAMARI)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 0 },
                { 1, 4 },
                { 2, 6 }
            }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private WasabiCard CreateWasabiCardWithSushi(SushiTypes sushiType)
        {
            WasabiCard wasabicard = new WasabiCard();
            wasabicard.AssociateSushi(new SushiCard(sushiType));
            return wasabicard;
        }
    }
}


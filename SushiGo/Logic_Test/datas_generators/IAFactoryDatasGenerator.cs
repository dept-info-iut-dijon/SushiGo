using Logic_Layer;
using System.Collections;
using Logic_Layer.cards;
using Logic_Layer.IA;

namespace LogicTest.datas_generators
{
    internal class IAFactoryDatasGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                IADifficultyEnum.FACILE, 3, new Board(), new Hand(3, new List<Card>())
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

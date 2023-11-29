using System.Collections;
using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.factories;
using Logic_Layer.factories.card_factories;
using UI_Layer.ViewModels;

namespace ViewModel_Test.DatasGenerators;

public class PlayerDatasGenerator : IEnumerable<object[]>
{
    private CardFactory cardFactory = new();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            // Valeurs simples permettant de valider un fonctionnement de base 
            0, new Board(), new Hand(0, cardFactory.CreateCard("maki 2")), "Joueur test",
            PlayerType.PLAYER, cardFactory.CreateCard("maki 2").First()
        };
        yield return new object[]
        {
            0, new Board(), new Hand(0, cardFactory.CreateCard("tempura", 3)), "Joueur test",
            PlayerType.ROBOT, cardFactory.CreateCard("tempura").First()
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
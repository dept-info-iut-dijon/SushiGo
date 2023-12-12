using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Windows.Threading;
using UI_Layer.UserControls;
using UI_Layer.ViewModels;

namespace ViewModel_Test
{
    public class GameTableViewModelTests
    {
        [Fact]
        public void ButtonValidateEnable_ShouldBeFalse_WhenCardSelectedIsNull()
        {
            // Arrange
            var viewModel = new GameTableViewModel();

            // Act
            bool buttonEnable = viewModel.IsButtonValidateShown;

            // Assert
            Assert.False(buttonEnable);
        }

        [Fact]
        public void ButtonValidateEnable_ShouldBeTrue_WhenCardSelectedIsNotNull()
        {
            // Act
            Thread thread = new Thread(() =>
            {
                // Arrange
                var gameTableVM = new GameTableViewModel();
                Player player = new Player(2, new Board(), new Hand(2, new List<Card>()), "Spartacus");
                PlayerViewModel playerVM = new PlayerViewModel(player, PlayerType.PLAYER);
                var card = new CardComponent(playerVM, new ChopstickCard());

                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

                gameTableVM.CardSelected = card;
                bool buttonEnable = gameTableVM.IsButtonValidateShown;

                // Assert
                Assert.True(buttonEnable);

                Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        [Fact]
        public void CardSelected_ShouldRaisePropertyChangedEvent()
        {
            // Act
            Thread thread = new Thread(() =>
            {
                // Arrange
                var gameTableVM = new GameTableViewModel();
                Player player = new Player(2, new Board(), new Hand(2, new List<Card>()), "Spartacus");
                PlayerViewModel playerVM = new PlayerViewModel(player, PlayerType.PLAYER);

                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

                // Assert
                Assert.PropertyChanged(gameTableVM, "CardSelected", () =>  gameTableVM.CardSelected = new CardComponent(playerVM, new SushiCard(SushiTypes.OMELETTE)));

                Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}


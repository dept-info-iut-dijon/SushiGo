using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Couche_IHM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Écoute l'événement PreviewMouseDown au niveau de l'application pour toutes les fenêtres
            EventManager.RegisterClassHandler(typeof(Window), UIElement.PreviewMouseDownEvent, new MouseButtonEventHandler(App_PreviewMouseDown));

            // Charge le curseur initial
            Cursor customCursor = (Cursor)FindResource("MainCursor");
            Mouse.OverrideCursor = customCursor;
        }

        private async void App_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = (Cursor)FindResource("MainCursorOn");
            await Task.Delay(300);
            Mouse.OverrideCursor = (Cursor)FindResource("MainCursor");
        }
    }
}

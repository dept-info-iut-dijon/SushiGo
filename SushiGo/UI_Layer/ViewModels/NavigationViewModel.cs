using System.Windows;

namespace UI_Layer.ViewModels
{
    public class NavigationViewModel
    {
        private Window currentWindow;


        /// <summary>
        /// Permet de quitter le jeu
        /// </summary>
        public DelegateCommand QuitGame => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });

        /// <summary>
        /// Permet de retourner au menu du jeu
        /// </summary>
        public DelegateCommand BackToMenu => new DelegateCommand(() =>
        {
            ReturnToMenu();
        });

        /// <summary>
        /// Permet de minimiser l'application
        /// </summary>
        public DelegateCommand MinimizeApplication => new DelegateCommand(() =>
        {

            currentWindow.WindowState = WindowState.Minimized;
        });

        /// <summary>
        /// Représente la window affiché
        /// </summary>
        public Window CurrentWindow { get => currentWindow; set => currentWindow = value; }


        /// <summary>
        /// Permet de retourner au menu
        /// </summary>
        public void ReturnToMenu()
        {
            HomeView home = new HomeView();
            home.Show();
            currentWindow.Close();
        }
    }
}

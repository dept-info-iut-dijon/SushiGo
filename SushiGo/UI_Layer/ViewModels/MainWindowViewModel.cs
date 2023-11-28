using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class MainWindowViewModel
    {
        #region singleton
        private static MainWindowViewModel instance;
        /// <summary>
        /// Singleton du mainwindowviewmodel
        /// </summary>
        public static MainWindowViewModel Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new MainWindowViewModel();
                }
                return instance;
            }   
        }

        #endregion

        #region viewModels
        private NavigationViewModel navigationViewModel;

        public NavigationViewModel NavigationViewModel { get => navigationViewModel; set => navigationViewModel = value; }
        public GameCreationViewModel GameCreationViewModel { get => gamecreationViewModel; set => gamecreationViewModel = value; }

        private GameCreationViewModel gamecreationViewModel;

        #endregion


        private MainWindowViewModel()
        {
            navigationViewModel = new NavigationViewModel();
            gamecreationViewModel = new GameCreationViewModel();
        }



    }
}

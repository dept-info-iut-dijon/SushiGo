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
        private GameCreationViewModel gamecreationViewModel;
        private GameTableViewModel gameTableViewModel;

        public NavigationViewModel NavigationViewModel { get => navigationViewModel; set => navigationViewModel = value; }
        public GameCreationViewModel GameCreationViewModel { get => gamecreationViewModel; set => gamecreationViewModel = value; }
        public GameTableViewModel GameTableViewModel { get => gameTableViewModel; set => gameTableViewModel = value; }

        #endregion


        private MainWindowViewModel()
        {
            navigationViewModel = new NavigationViewModel();
            gamecreationViewModel = new GameCreationViewModel();
            gameTableViewModel = new GameTableViewModel();
        }



    }
}

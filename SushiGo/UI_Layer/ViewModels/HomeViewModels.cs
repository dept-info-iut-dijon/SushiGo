using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class HomeViewModels : INotifyPropertyChanged
    {

        #region Constructeur

        public HomeViewModels()
        {
            
        }

        #endregion Constructeur

        #region Evenement

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Evenement
    }
}

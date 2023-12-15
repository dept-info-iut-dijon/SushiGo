using System;
using System.Globalization;
using System.Windows.Data;
using UI_Layer.ViewModels;

namespace UI_Layer.UserControls
{
    public class LeaderboardRetriever : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String? valeurRetour = null;
            if (value is PlayerViewModel player)
            {
                int position = MainWindowViewModel.Instance.GameTableViewModel.LeaderBoard.IndexOf(player) + 1;
                valeurRetour = position.ToString();
            }
            return valeurRetour;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

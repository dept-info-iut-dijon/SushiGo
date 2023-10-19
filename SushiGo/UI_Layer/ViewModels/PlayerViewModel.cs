using Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI_Layer.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private Player player;
        private string nom;
        private string role;


        public PlayerViewModel(Player player,string nomTemp,string role)
        {
            this.player = player;
            this.nom = nomTemp;
            this.role = role;
        }
        #endregion

        #region properties
        public List<string> IaDifficulties
        {
            get
            {
                return new List<string>() { "Facile", "Moyen", "Difficile" };
            }
        }
        public string Nom { get => nom; set => nom = value; }
        public int Id { get => player.Id; }
        public string Role { get => role; set => role = value; }
        #endregion

    }
}

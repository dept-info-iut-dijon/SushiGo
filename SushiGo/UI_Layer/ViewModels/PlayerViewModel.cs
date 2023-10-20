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
        private PlayerType role;
        private bool isReady = false;
        private IADifficulty difficulty = IADifficulty.MOYEN;
        #endregion

        /// <summary>
        /// Constructeur du playerviewmodel associé au joueur ou a l'ia
        /// </summary>
        /// <param name="player">objet player metier</param>
        /// <param name="role">type de joueur (ia ou robot ou non-determiné)</param>
        public PlayerViewModel(Player player, PlayerType role)
        {
            this.player = player;
            this.role = role;
        }
        

        #region properties

        /// <summary>
        /// Nom du joueur
        /// </summary>
        public string Nom { get => player.Pseudo;  }

        /// <summary>
        /// Id du joueur
        /// </summary>
        public int Id { get => player.Id; }

        /// <summary>
        /// Role du joueur
        /// </summary>
        public PlayerType Role { get => role; set => role = value; }

        /// <summary>
        /// Est ce que le joueur est prêt
        /// </summary>
        public bool IsReady { get => isReady; set => isReady = value; }
        /// <summary>
        /// Difficulte de l'ia
        /// </summary>
        public IADifficulty Difficulty 
        { 
            get => difficulty; 
            set => difficulty = value; }
        #endregion

    }
}

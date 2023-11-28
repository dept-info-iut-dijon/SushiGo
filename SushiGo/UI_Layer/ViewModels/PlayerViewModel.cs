using Logic_Layer;
using Logic_Layer.IA;
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
        private bool isReady;
        private GameCreationViewModel gameCreationViewModel;
        private bool isTurnFinished;
        #endregion

        /// <summary>
        /// Constructeur du playerviewmodel associé au joueur ou a l'ia
        /// </summary>
        /// <param name="player">objet player metier</param>
        /// <param name="role">type de joueur (ia ou robot ou non-determiné)</param>
        public PlayerViewModel(Player player, PlayerType role,GameCreationViewModel creationViewModel)
        {
            this.player = player;
            this.role = role;
            this.gameCreationViewModel = creationViewModel;
        }

        /// <summary>
        /// Constructeur du playerviewmodel associé au joueur ou a l'ia, en version isolée du VM du jeu
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
        /// Est ce que le joueur est prêt à démarrer la partie
        /// </summary>
        public bool IsReady 
        { 
            get => isReady;
            set 
            { 
                isReady = value; 
                NotifyPropertyChanged(nameof(gameCreationViewModel.MessageWaitingStart));
            }
        }

        /// <summary>
        /// Modele du joueur
        /// </summary>
        public Player Player { get => player; set => player = value; }
        
        /// <summary>
        /// Indique si le joueur a terminé son tour
        /// </summary>
        public bool IsTurnFinished
        {
            get => isTurnFinished;
            set
            {
                isTurnFinished = value;
                NotifyPropertyChanged(nameof(isTurnFinished));
            }
        }

        #endregion

    }
}

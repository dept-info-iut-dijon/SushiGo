using Logic_Layer;
using Logic_Layer.cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI_Layer.ViewModels
{
    public class GameCreationViewModel : INotifyPropertyChanged
    {

        #region inotify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region attributes
        private ObservableCollection<PlayerViewModel> players = new ObservableCollection<PlayerViewModel>();
        private int playerCount = 3;
        private bool isModeJvJ = true;
        private bool isLobbyShowed = false;
        private bool startButtonShow = false;
        private Window view;
        public GameCreationViewModel(Window view)
        {
            this.view = view;

            // Initialisation des events
            this.AddPlayer = new RelayCommand(x =>
            {
                if (playerCount < 5)
                {
                    this.PlayerCount++;
                }
            });
            this.RemovePlayer = new RelayCommand(x =>
            {
                if (playerCount > 2)
                {
                    this.PlayerCount--;
                }
            });
            this.CreateGame = new RelayCommand(x => this.CreateLobby());
            this.BackToHome = new RelayCommand(x =>
            {
                HomeView homeView = new HomeView();
                homeView.Show();
                this.view.Close();

            });
        }



        #endregion
        #region events
        /// <summary>
        /// Permet de retourner à l'écran d'accueil
        /// </summary>
        public RelayCommand BackToHome { get; set; }
        /// <summary>
        /// Permet d'ajouter un joueur dans la partie
        /// </summary>
        public RelayCommand AddPlayer { get; set; }
        /// <summary>
        /// Permet d'enlever un joueur dans la partie
        /// </summary>
        public RelayCommand RemovePlayer { get; set; }
        /// <summary>
        /// Permet de créer le lobby de la partie
        /// </summary>
        public RelayCommand CreateGame { get; set; }

        #endregion
        #region properties

        /// <summary>
        /// Nom de la partie
        /// </summary>
       public string TitleLobby
        {
            get
            {
                string title = "Partie contre des robots";
                if (isModeJvJ)
                {
                    title = $"Partie en multijoueur";
                }
                return title;
            }
        }

        /// <summary>
        /// Message qui s'affiche avant de lancer la partie
        /// </summary>
        public string MessageWaitingStart
        {
            get
            {
                string message = "Prêt à lancer !";
                int compteur = 0;
                compteur = this.players.ToList().FindAll(x => x.IsReady == false).Count;
                if (compteur != 0)
                {
                    message = $"En attente de {compteur} joueur(s) pour lancer la partie";
                }
                return message;
            }
        }
        /// <summary>
        /// Représente le nombre de joueurs
        /// </summary>
        public int PlayerCount
        {
            get => playerCount;
            set 
            { 
                playerCount = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Est ce que le mode de jeu est Joueur vs Joueur / Si non alors c'est Joueur vs Robots
        /// </summary>
        public bool IsModeJvJ
        {
            get => isModeJvJ;
            set 
            { 
                isModeJvJ = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Permet d'afficher le bouton pour lancer la partie
        /// </summary>
        public bool StartButtonShow
        {
            get => startButtonShow;
            set 
            { 
                startButtonShow = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Permet d'afficher le lobby et la creation/join de partie
        /// </summary>
        public bool IsLobbyShowed 
        { 
            get => isLobbyShowed;
            set
            {
                isLobbyShowed = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// Liste des joueurs de la partie
        /// </summary>
        public ObservableCollection<PlayerViewModel> Players { get => players; set => players = value; }

        #endregion

        #region methods

        /// <summary>
        /// Permet de créer le lobby de la partie
        /// </summary>
        private void CreateLobby()
        {
            if (isModeJvJ)
            {
                this.Players.Add(new PlayerViewModel(new Player(1, new Board(), new Hand(1, new List<Card>()), "Player 1"), PlayerType.PLAYER) { IsReady = true});
                for (int i = 1; i < playerCount; i++)
                {
                    this.Players.Add(new PlayerViewModel(new Player(i + 1, new Board(), new Hand(i + 1, new List<Card>()), $"Waiting for player..."), PlayerType.WAITING));
                }
                NotifyPropertyChanged(nameof(MessageWaitingStart));
            }
            else
            {
                this.Players.Add(new PlayerViewModel(new Player(1, new Board(), new Hand(1, new List<Card>()),"Player"), PlayerType.PLAYER) { IsReady = true});
                for (int i = 1; i < playerCount; i++)
                {
                    this.Players.Add(new PlayerViewModel(new Player(i+1,new Board(),new Hand(i+1,new List<Card>()),$"Robot {i}"), PlayerType.ROBOT));
                }
                this.StartButtonShow = true;
            }

            this.IsLobbyShowed = true;
        }
        #endregion
    }
}

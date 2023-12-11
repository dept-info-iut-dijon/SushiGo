﻿using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.IA;
using Logic_Layer.IA.IAImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI_Layer.Views;

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
        private IADifficultyEnum difficulty = IADifficultyEnum.FACILE;
        private ObservableCollection<PlayerViewModel> players = new ObservableCollection<PlayerViewModel>();
        private int playerCount = 3;
        private bool isModeJvJ = false;
        private bool isLobbyShowed = false;
        private bool startButtonShow = false;
        private string idParty;
        private Window view;

        #endregion



        #region events
        /// <summary>
        /// Permet d'indiquer a la vue de fermer la page
        /// </summary>
        public event EventHandler CloseRequested;

        /// <summary>
        /// Permet d'ajouter un joueur dans la partie
        /// </summary>
        public DelegateCommand AddPlayer => new DelegateCommand(() =>
        {
            if (playerCount < 5)
            {
                this.PlayerCount++;
            }
        });

        /// <summary>
        /// Permet d'enlever un joueur dans la partie
        /// </summary>
        public DelegateCommand RemovePlayer => new DelegateCommand(() =>
        {
            if (playerCount > 2)
            {
                this.PlayerCount--;
            }
        });

        /// <summary>
        /// Permet de créer le lobby de la partie
        /// </summary>
        public DelegateCommand CreateGame => new DelegateCommand(() => 
            this.CreateLobby()
        );

        /// <summary>
        /// Permet de lancer  la partie
        /// </summary>
        public DelegateCommand StartGame => new DelegateCommand(() => 
            this.Start()
        );

        #endregion

        #region properties

        /// <summary>
        /// Difficulté de l'ia sélectionnée
        /// </summary>
        public IADifficultyEnum Difficulty
        {
            get
            {
                if (!isModeJvJ)
                {
                    return difficulty;
                }
                else
                {
                    throw new Exception("Mode de jeu non compatible");
                }
            }
            set 
            { 
                difficulty = value; 
            }
        }

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
                NotifyPropertyChanged(nameof(TitleLobby));
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

        /// <summary>
        /// Id de la partie à rejoindre en multijoueur
        /// </summary>
        public string IdParty 
        { 
            get => idParty;
            set 
            { 
                idParty = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Indique à la vue de close la page
        /// </summary>
        protected virtual void OnCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Permet de créer le lobby de la partie
        /// </summary>
        private void CreateLobby()
        {
            // Création du joueur qui créer la partie
            this.Players.Add(new PlayerViewModel(new Player(1, new Board(), new Hand(1, new List<Card>()), "Moi"), PlayerType.PLAYER) { IsReady = true });

            // Création des joueurs en multijoueurs
            if (isModeJvJ)
            {
                for (int i = 1; i < playerCount; i++)
                {
                    this.Players.Add(new PlayerViewModel(new Player(i + 1, new Board(), new Hand(i + 1, new List<Card>()), $"Waiting for player..."), PlayerType.WAITING));
                }
                NotifyPropertyChanged(nameof(MessageWaitingStart));
                GenerateIDParty();
            }
            // Création des IAs
            else
            {
                IAFactory iAFactory = new IAFactory();

                for (int i = 1; i < playerCount; i++)
                {
                    // Création de l'IA
                    IA ia = iAFactory.CreateIA(this.difficulty, i + 1, new Board(), new Hand(i + 1, new List<Card>()));

                    // Création de la vueModel à partir de l'IA créée
                    PlayerViewModel playerViewModel = new PlayerViewModel(ia, PlayerType.ROBOT);

                    // Ajout de l'IA aux joueurs
                    this.Players.Add(playerViewModel);
                }
                
                this.StartButtonShow = true;
            }

            this.IsLobbyShowed = true;
        }

        /// <summary>
        /// Permet de réinitialiser l'ihm à chaque chargement de la page
        /// </summary>
        public void ResetChanges()
        {
            this.IsLobbyShowed = false;
            this.StartButtonShow = false;
            this.PlayerCount = 5;
            this.IsModeJvJ = false;
            this.Players.Clear();
        }

        /// <summary>
        /// Permet de lancer la partie
        /// </summary>
        public void Start()
        {
            // Création de la logique
            List<Player> list = new List<Player>();
            foreach (var player in this.Players)
            {
                list.Add(player.Player);
            }
            Table t = new Table(list);


            GameTableView gameTableView = new GameTableView(t);
            MainWindowViewModel.Instance.GameTableViewModel.Init(t);
            gameTableView.Show();
            OnCloseRequested();
        }

        /// <summary>
        /// Permet de générer un identifiant de partie pour s'y connecter
        /// </summary>
        private void GenerateIDParty()
        {
            Random random = new Random();
            int nbCar = random.Next(6, 9);
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] uniqueId = new char[nbCar];
            for (int i = 0; i < nbCar; i++)
            {
                uniqueId[i] = characters[random.Next(characters.Length)];
            }
            this.IdParty = new string(uniqueId);
            
        }
        #endregion
    }
}

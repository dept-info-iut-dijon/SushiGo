using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using UI_Layer.ViewModels;

namespace UI_Layer.UserControls;

/// <summary>
/// Logique d'interaction pour BoardComponent.xaml
/// </summary>
public partial class BoardComponent : UserControl, INotifyPropertyChanged
{

    /// <summary>
    /// Instancie un BoardComponent
    /// </summary>
    /// <param name="player">Le joueur ayant la carte en main</param>
    public BoardComponent()
    {
        InitializeComponent();

    }

    private static void GetNotification(object sender, PropertyChangedEventArgs e, BoardComponent boardComponent)
    {
        switch (e.PropertyName)
        {
            case nameof(boardComponent.Player.Board):
                SetupCards(boardComponent);
                break;
        }
    }

    #region dependenciesProperties


    /// <summary>
    ///     Lie la propriété CardName à l'event correspondant
    /// </summary>
    public static readonly DependencyProperty PlayerProperty =
        DependencyProperty.Register("Player", typeof(PlayerViewModel), typeof(BoardComponent),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure, ChangePlayer));

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region properties

    /// <summary>
    /// Représente le playerviewmodel bind au board
    /// </summary>
    public PlayerViewModel Player
    {
        get => (PlayerViewModel)GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }



    #endregion


    /// <summary>
    /// Permet de changer le joueur
    /// </summary>
    private static void ChangePlayer(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BoardComponent BoardComponent && BoardComponent.Player != null)
        {
            BoardComponent.PlayerName.Text = BoardComponent.Player.Nom;
            BoardComponent.Player.PropertyChanged += (sender, args) => GetNotification(sender, args, BoardComponent);
        }
    }

    /// <summary>
    /// Permet de remplir le board avec les différentes cartes
    /// </summary>
    private static void SetupCards(BoardComponent boardComponent)
    {
        List<CardComponent> cartesJoueurs = boardComponent.Player.Board;
        boardComponent.Board.ItemsSource = cartesJoueurs;


    }
}
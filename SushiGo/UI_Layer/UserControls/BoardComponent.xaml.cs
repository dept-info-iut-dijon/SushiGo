using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Logic_Layer;
using Logic_Layer.cards;
using UI_Layer.ViewModels;

namespace UI_Layer.UserControls;


public partial class BoardComponent : UserControl
{

    /// <summary>
    /// Instancie un BoardComponent
    /// </summary>
    /// <param name="player">Le joueur ayant la carte en main</param>
    public BoardComponent()
    {
        InitializeComponent();
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
        if (d is BoardComponent BoardComponent)
        {
            BoardComponent.PlayerName.Text = BoardComponent.Player.Nom;
            SetupCards(BoardComponent.Player);
        }
    }

    /// <summary>
    /// Permet de remplir le board avec les différentes cartes
    /// </summary>
    private static void SetupCards(PlayerViewModel Player)
    {
        List<CardComponent> cartesJoueurs = Player.Deck;
        // TODO : afficher les cartes

    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Logic_Layer.cards;
using UI_Layer.ViewModels;

namespace UI_Layer.UserControls;

/// <summary>
/// Logique d'interaction pour CardComponent.xaml
/// </summary>
public partial class CardComponent : UserControl
{
    private Card card;
    private bool isSelected;
    private bool isPut;

    /// <summary>
    /// Instancie un CardComponent
    /// </summary>
    /// <param name="player">Le joueur ayant la carte en main</param>
    public CardComponent(Card card)
    {
        this.card = card;
        this.isSelected = false;
        this.isPut = false;
        InitializeComponent();
        this.CardName = card.Name;
    }

    #region dependenciesProperties

    /// <summary>
    /// Lie la propriété IsSelecting à l'event correspondant
    /// </summary>
    public static readonly DependencyProperty IsMouseOverCardProperty =
        DependencyProperty.Register("IsSelecting", typeof(bool), typeof(CardComponent),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure, HighlightBorders));

    /// <summary>
    /// Lie la propriété CardName à l'event correspondant
    /// </summary>
    public static readonly DependencyProperty CardNameProperty =
        DependencyProperty.Register("CardName", typeof(string), typeof(CardComponent),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure, ChangeCardName));
    
    #endregion

    #region properties

    /// <summary>
    /// Permet de modifier si l'utilisateur a la souris sur la carte
    /// </summary>
    public bool IsMouseOverCard
    {
        get => (bool)GetValue(IsMouseOverCardProperty);
        set => SetValue(IsMouseOverCardProperty, value);
    }

    /// <summary>
    /// Permet de modifier le nom de la carte
    /// </summary>
    public string CardName
    {
        get => (string)GetValue(CardNameProperty);
        set => SetValue(CardNameProperty, value);
    }

    /// <summary>
    /// Si la carte est sélectionnée ou non.
    /// </summary>
    public bool IsSelected
    {
        get
        {
            return this.isSelected;
        }
        set
        {
            this.isSelected = value;
            if (this.isSelected == true)
            {
                this.Margin = new Thickness(0,0,0,40);
                
            }
            else
            {
                this.Margin = new Thickness(0,0,0,0);
            }
        }
    }


    /// <summary>
    /// Représente l'objet métier de la carte
    /// </summary>
    public Card Card { get => card; }
    /// <summary>
    /// Est ce que la carte a été posée
    /// </summary>
    public bool IsPut 
    { 
        get => isPut;
        set 
        { 
            isPut = value;
            if (this.isPut == true)
            {
                this.Width = 80;
                this.Height = 110;

            }
            else
            {
                this.Width = 140;
                this.Height = 200;
            }
        }
    }

    #endregion

    #region Méthode Publique

    /// <summary>
    /// Sélectionne ou déselectionne la carte lorsque l'on clic dessus.
    /// </summary>
    public void ClickOnCard()
    {
        this.IsSelected = !this.IsSelected;
    }


    #endregion Méthode Publique

    #region event

    /// <summary>
    /// Réagit quand la souris de l'utilisateur passe dessus ->  la carte est en surbrillance
    /// </summary>
    private void MouseOverCard(object sender, MouseEventArgs e)
    {
        IsMouseOverCard = true;
    }

    /// <summary>
    /// Réagit quand la souris de l'utilisateur sort de la carte ->  la carte n'est plus en surbrillance
    /// </summary>
    private void MouseNotOverCard(object sender, MouseEventArgs e)
    {
        IsMouseOverCard = false;
    }

    /// <summary>
    /// Permet de mettre en surbrillance la carte.
    /// </summary>
    private static void HighlightBorders(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CardComponent CardComponent && CardComponent.IsPut == false)
            if (CardComponent.border != null)
            {
                if (CardComponent.IsMouseOverCard)
                    CardComponent.border.BorderThickness = new Thickness(3);
                else
                    CardComponent.border.BorderThickness = new Thickness(0);
            }
    }

    /// <summary>
    /// Permet de changer le nom de la carte.
    /// </summary>
    private static void ChangeCardName(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CardComponent CardComponent)
        {
            if (CardComponent.image != null)
            {
                CardComponent.image.Source = new BitmapImage(new Uri($"../Assets/Cartes/{CardComponent.CardName}.png", UriKind.Relative));
            }
        }
    }

    #endregion
}
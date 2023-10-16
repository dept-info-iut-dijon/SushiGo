using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Couche_IHM.Views
{
    /// <summary>
    /// Logique d'interaction pour CarteComponent.xaml
    /// </summary>
    public partial class CarteComponent : UserControl
    {

        #region dependenciesProperties
        /// <summary>
        /// Lie la propriété IsSelecting à l'event correspondant
        /// </summary>
        public static readonly DependencyProperty IsSelectingProperty =
         DependencyProperty.Register("IsSelecting", typeof(bool), typeof(CarteComponent),
         new FrameworkPropertyMetadata(false,
             FrameworkPropertyMetadataOptions.AffectsRender |
             FrameworkPropertyMetadataOptions.AffectsParentMeasure, HighlightBorders));

        /// <summary>
        /// Lie la propriété CardName à l'event correspondant
        /// </summary>
        public static readonly DependencyProperty CardNameProperty =
         DependencyProperty.Register("CardName", typeof(string), typeof(CarteComponent),
         new FrameworkPropertyMetadata(string.Empty,
             FrameworkPropertyMetadataOptions.AffectsRender |
             FrameworkPropertyMetadataOptions.AffectsParentMeasure, ChangeCardName));
        #endregion

        #region properties
        /// <summary>
        /// Permet de modifier si l'utilisateur a la souris sur la carte
        /// </summary>
        public bool IsSelecting
        {
            get { return (bool)GetValue(IsSelectingProperty); }
            set 
            { 
                SetValue(IsSelectingProperty, value);
            }
        }

        /// <summary>
        /// Permet de modifier le nom de la carte
        /// </summary>
        public string CardName
        {
            get { return (string)GetValue(CardNameProperty); }
            set
            {
                SetValue(CardNameProperty, value);
            }
        }
        #endregion

        #region event

        /// <summary>
        /// Réagit quand la souris de l'utilisateur passe dessus ->  la carte est en surbrillance
        /// </summary>
        private void MouseOverCard(object sender, MouseEventArgs e)
        {
            IsSelecting = true;
        }

        /// <summary>
        /// Réagit quand la souris de l'utilisateur sort de la carte ->  la carte n'est plus en surbrillance
        /// </summary>
        private void MouseNotOverCard(object sender, MouseEventArgs e)
        {
            IsSelecting = false;
        }

        /// <summary>
        /// Permet de mettre en surbrillance la carte quand la property IsSelecting est modifiée
        /// </summary>
        private static void HighlightBorders(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarteComponent carteComponent)
            {
                if (carteComponent.border != null)
                {
                    if (carteComponent.IsSelecting)
                    {
                        carteComponent.border.BorderThickness = new Thickness(3);
                    }
                    else
                    {
                        carteComponent.border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        /// <summary>
        /// Permet de changer le nom de la carte
        /// </summary>
        private static void ChangeCardName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarteComponent carteComponent)
            {
                if (carteComponent.cardName != null)
                {
                    // TO DO : changer l'image à partir du texte quand images pretes
                    carteComponent.cardName.Text = carteComponent.CardName;
                    
                }
            }
        }
        #endregion

        public CarteComponent()
        {
            InitializeComponent();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Couche_IHM.Views
{
    /// <summary>
    /// Logique d'interaction pour CardComponent.xaml
    /// </summary>
    public partial class CardComponent : UserControl
    {

        #region dependenciesProperties
        /// <summary>
        /// Lie la propriété IsSelecting à l'event correspondant
        /// </summary>
        public static readonly DependencyProperty IsSelectingProperty =
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
            if (d is CardComponent CardComponent)
            {
                if (CardComponent.border != null)
                {
                    if (CardComponent.IsSelecting)
                    {
                        CardComponent.border.BorderThickness = new Thickness(3);
                    }
                    else
                    {
                        CardComponent.border.BorderThickness = new Thickness(0);
                    }
                }
            }
        }

        /// <summary>
        /// Permet de changer le nom de la carte
        /// </summary>
        private static void ChangeCardName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CardComponent CardComponent)
            {
                if (CardComponent.image != null)
                {
                    CardComponent.image.Source = new BitmapImage(new System.Uri($"../Assets/Cartes/{CardComponent.CardName}.png", System.UriKind.Relative));            
                }
            }
        }
        #endregion

        public CardComponent()
        {
            InitializeComponent();
        }
    }
}

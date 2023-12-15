using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UI_Layer.Views
{
    /// <summary>
    /// Logique d'interaction pour CreditsView.xaml
    /// </summary>
    public partial class CreditsView : Window
    {
        /// <summary>
        /// Constructeur de la classe <see cref="CreditsView"/>
        /// </summary>
        public CreditsView()
        {
            this.InitializeComponent();

            this.Loaded += CreditsView_Loaded;
            this.PreviewMouseDown += Credits_PreviewMouseDown;
        }

        private void CreditsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.StartCreditsAnimation();
            this.CenterCredits();
        }

        private void StartCreditsAnimation()
        {
            DoubleAnimation verticalAnimation = new DoubleAnimation
            {
                From = CanvasCredits.ActualHeight,
                To = -Credits.ActualHeight - 100,
                Duration = TimeSpan.FromSeconds(80),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Credits.BeginAnimation(Canvas.TopProperty, verticalAnimation);
        }

        private void CenterCredits()
        {
            double canvasCenterX = CanvasCredits.ActualWidth / 2;
            double creditsCenterX = Credits.ActualWidth / 2;

            double canvasCenterY = CanvasCredits.ActualHeight / 2;
            double creditsCenterY = Credits.ActualHeight / 2;

            double leftOffset = canvasCenterX - creditsCenterX;
            double topOffset = canvasCenterY - creditsCenterY;

            Canvas.SetLeft(Credits, leftOffset);
            Canvas.SetTop(Credits, topOffset);
        }

        private void Credits_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HomeView creditsView = new HomeView();
            creditsView.Show();
            this.Close();
        }
    }
}

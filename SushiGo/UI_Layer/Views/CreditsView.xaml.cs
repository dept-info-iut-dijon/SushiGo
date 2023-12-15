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
        public CreditsView()
        {
            this.InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartCreditsAnimation();
        }

        private void StartCreditsAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = canvasCredits.ActualWidth,   // Début à la largeur du canvas
                To = 200,      // Fin à l'opposé (gauche)
                Duration = TimeSpan.FromSeconds(5),  // Durée de l'animation
                RepeatBehavior = RepeatBehavior.Forever  // Répéter indéfiniment
            };

            Credits.BeginAnimation(Canvas.LeftProperty, animation);
        }
    }
}

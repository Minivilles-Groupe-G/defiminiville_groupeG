using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minivilles_Graphique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        { 
            Card.CreateDeck();
            this.Content = new Jeu();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Label_music.Visibility = Visibility.Visible;
            Label_sound.Visibility = Visibility.Visible;
            Slider_music.Visibility = Visibility.Visible;
            Slider_sound.Visibility = Visibility.Visible;
            Btn_retour.Visibility = Visibility.Visible;

            Btn_play.Visibility = Visibility.Collapsed;
            Btn_settings.Visibility = Visibility.Collapsed;
            Btn_quit.Visibility = Visibility.Collapsed;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Slider_music_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            Btn_play.Visibility = Visibility.Visible;
            Btn_settings.Visibility = Visibility.Visible;
            Btn_quit.Visibility = Visibility.Visible;

            Label_music.Visibility = Visibility.Collapsed;
            Label_sound.Visibility = Visibility.Collapsed;
            Slider_music.Visibility = Visibility.Collapsed;
            Slider_sound.Visibility = Visibility.Collapsed;
            Btn_retour.Visibility = Visibility.Collapsed;
        }
    }
}

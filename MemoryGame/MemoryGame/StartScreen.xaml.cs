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

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Page
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PlayerSelect());
        }

        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Scorebord());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SavedGames());
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Themas());
        }
    }
}

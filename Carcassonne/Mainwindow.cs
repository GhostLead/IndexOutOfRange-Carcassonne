using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Carcassonne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SoundPlayer mediaPlayer = new SoundPlayer("Carcassonne-OST.wav");
        public MainWindow()
        {
            InitializeComponent();

            SetBackground();
            PlayAudio();

        }

        private void PlayAudio()
        {
            
            mediaPlayer.Load();
            mediaPlayer.PlayLooping();
        }

        private void SetBackground()
        {
            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"Backgrounds\Background2.png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();
            this.Background = new ImageBrush(bitimg);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            
            CarcassoneGame cgw = new CarcassoneGame();
            mediaPlayer.Stop();
            cgw.Show();
            this.Close();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}

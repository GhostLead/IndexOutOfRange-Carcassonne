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
            BitmapImage bitimgEnabled = new BitmapImage();
            bitimgEnabled.BeginInit();
            bitimgEnabled.UriSource = new Uri(@"Egyeb\enable.png", UriKind.RelativeOrAbsolute);
            bitimgEnabled.DecodePixelWidth = 60;
            bitimgEnabled.DecodePixelHeight = 60;
            bitimgEnabled.EndInit();
            btnEnabled.Background = new ImageBrush(bitimgEnabled);

            BitmapImage bitimgDisabled = new BitmapImage();
            bitimgDisabled.BeginInit();
            bitimgDisabled.UriSource = new Uri(@"Egyeb\mute.png", UriKind.RelativeOrAbsolute);
            bitimgDisabled.EndInit();
            btnDisabled.Background = new ImageBrush(bitimgDisabled);

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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEnabled_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            btnEnabled.Visibility = Visibility.Hidden;
            btnDisabled.Visibility = Visibility.Visible;
        }

        private void btnDisabled_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.PlayLooping();
            btnDisabled.Visibility = Visibility.Hidden;
            btnEnabled.Visibility = Visibility.Visible;
        }

        private void btnWebpage_Click(object sender, RoutedEventArgs e)
        {
			var p = new Process();
			p.StartInfo = new ProcessStartInfo("Website\\MainMenu.html")
			{
				UseShellExecute = true
			};
			p.Start();
		}
	}
}

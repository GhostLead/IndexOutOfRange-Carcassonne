using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
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
    public partial class CarcassoneGame : Window
    {
        
        bool isNavbarDropped = false;
        public CarcassoneGame()
        {
            InitializeComponent();

            SetCardPack();

            SetBackground();

            NavbarButtonsHidden();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pushedButton = ((Button)(FrameworkElement)sender);

            List<Button> buttons = new List<Button>();
            buttons.Append(pushedButton);

            pushedButton.Background = rctFelforditottKartya.Fill;

            Random rnd = new Random();
            string folder = @"Kepek";
            string[] files = Directory.GetFiles(folder);


            string generaltNev = files[rnd.Next(files.Length)];


            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@$"{generaltNev}", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();


            CardPlacementSound();
            rctFelforditottKartya.Fill = new ImageBrush(bitimg);
            pushedButton.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
            pushedButton.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OccupiedTileErrorMessage));

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            CreatePlayground();

            Random rnd = new Random();
            string folder = @"Kepek";
            string[] files = Directory.GetFiles(folder);

            string generaltNev = files[rnd.Next(files.Length)];


            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@$"{generaltNev}", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();


            rctFelforditottKartya.Fill = new ImageBrush(bitimg);

            ((Button)(FrameworkElement)sender).Visibility = Visibility.Hidden;

        }

        private void btnExpander_Click(object sender, RoutedEventArgs e)
        {
            
            if (isNavbarDropped == false)
            {
                eclCircle.Stroke = Brushes.Red;
                rctLine1.Fill = Brushes.Red;
                rctLine2.Fill = Brushes.Red;
                rctLine3.Fill = Brushes.Red;
                NavbarButtonsVisible();
                isNavbarDropped = true;
            }
            else
            {
                eclCircle.Stroke = Brushes.White;
                rctLine1.Fill = Brushes.White;
                rctLine2.Fill = Brushes.White;
                rctLine3.Fill = Brushes.White;
                NavbarButtonsHidden();
                isNavbarDropped = false;
            }
            
        }

        private void OccupiedTileErrorMessage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ez a mező már foglalt! Válassz másikat!", "Foglalt mező", MessageBoxButton.OK,MessageBoxImage.Error);
        }

        private void CardPlacementSound()
        {             
            SoundPlayer soundPlayer = new SoundPlayer("Card-Flip-Sound.wav");
            soundPlayer.Load();
            soundPlayer.Play();   
        }

        private void SetCardPack()
        {
            BitmapImage bitBackground = new BitmapImage();
            bitBackground.BeginInit();
            bitBackground.UriSource = new Uri(@"Egyeb\Null0.png", UriKind.RelativeOrAbsolute);
            bitBackground.EndInit();
            rctLeforditottKartya.Fill = new ImageBrush(bitBackground);
            rctFelforditottKartyaFent.Fill = new ImageBrush(bitBackground);
            rctFelforditottKartyaKozep.Fill = new ImageBrush(bitBackground);

            var fileCount = (from file in Directory.EnumerateFiles(@"Kepek\", "*.png", SearchOption.AllDirectories)
                             select file).Count();
        }

        private void SetBackground()
        {
            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"Backgrounds\Background3.png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();
            grid.Background = new ImageBrush(bitimg);
        }

        private void CreatePlayground()
        {
            ugTabla.Opacity = 1;
            ugTabla.Rows = 8;
            ugTabla.Columns = 5;

            for (int i = 0; i < ugTabla.Rows * ugTabla.Columns; i++)
            {
                Button gomb1 = new Button();
                gomb1.Background = Brushes.ForestGreen;
                gomb1.Name = $"btnTeruletek{i}";
                gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
                ugTabla.Children.Add(gomb1);

            }
        }

        private void NavbarButtonsHidden()
        {
            btnSave.Visibility = Visibility.Hidden;
            btnSettings.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
        }

        private void NavbarButtonsVisible()
        {
            btnSave.Visibility = Visibility.Visible;
            btnSettings.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
        }

    }

}

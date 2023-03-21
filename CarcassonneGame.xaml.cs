using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
using Path = System.IO.Path;

namespace Carcassonne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CarcassoneGame : Window
    {
        
        private ObservableCollection<kartya> classKartyak = new ObservableCollection<kartya>();
        public CarcassoneGame()
        {
            InitializeComponent();

            BitmapImage bitBackground = new BitmapImage();
            bitBackground.BeginInit();
            bitBackground.UriSource = new Uri(@"Egyeb\Null0.png", UriKind.RelativeOrAbsolute);
            bitBackground.EndInit();
            rctLeforditottKartya.Fill = new ImageBrush(bitBackground);

            var fileCount = (from file in Directory.EnumerateFiles(@"Kepek\", "*.png", SearchOption.AllDirectories)
                             select file).Count();

            // Típuskártyák felvétele CLASS-ba
            KartyakFelveteleOsztalyba();
            

            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"Backgrounds\Background3.png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();
            grid.Background = new ImageBrush(bitimg);

            ugTabla.Rows = 8;
            ugTabla.Columns = 5;

            for (int i = 0; i < 40; i++)
            {
                Button gomb1 = new Button();
                gomb1.Background = Brushes.ForestGreen;
                gomb1.Name = $"btnGomb{i}";
                gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
                ugTabla.Children.Add(gomb1);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            List<Button> buttons = new List<Button>();
            buttons.Append(((Button)(FrameworkElement)sender));

            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"Kepek\CastleEdge3.png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();
            ((Button)(FrameworkElement)sender).Background = new ImageBrush(bitimg);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        public void KartyakFelveteleOsztalyba()
        {
            


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapImage bitBackground = new BitmapImage();
            bitBackground.BeginInit();
            bitBackground.UriSource = new Uri(@"Kepek\CastleCenter0.png", UriKind.RelativeOrAbsolute);
            bitBackground.EndInit();
            rctFelforditottKartya.Fill = new ImageBrush(bitBackground);
        }
    }
}

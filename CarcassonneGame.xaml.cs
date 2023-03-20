using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public CarcassoneGame()
        {
            InitializeComponent();



            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"Kepek\Background3.png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();
            grid.Background = new ImageBrush(bitimg);

            ugTabla.Rows = 8;
            ugTabla.Columns = 5;

            for (int i = 0; i < 40; i++)
            {
                Button gomb1 = new Button();
                gomb1.Background = Brushes.ForestGreen;
                gomb1.Name = $"btnGomb{i}";
                //gomb1.Width = 100;
                //gomb1.Height = 100;
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
    }
}

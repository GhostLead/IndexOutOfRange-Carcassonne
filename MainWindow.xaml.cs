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

namespace CarcassonneVerseny
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			ugTabla.Rows = 8;
			ugTabla.Columns = 5;

			for (int i = 0; i < 40; i++)
			{
				Button gomb1 = new Button();
				gomb1.Background = Brushes.Green;
				gomb1.Name = $"btnGomb{i}";
				gomb1.Width = 50;
				gomb1.Height = 50;
				gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
				Canvas rajzLap = new Canvas();
				Thickness margo = rajzLap.Margin;
				margo.Left = 3;
				rajzLap.Margin = margo;
				rajzLap.Children.Add(gomb1);
				ugTabla.Children.Add(rajzLap);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			szoveg.Content = ((FrameworkElement)e.Source).Name;
			((FrameworkElement)sender).Visibility = Visibility.Hidden;

			var valami = ((FrameworkElement)sender).Parent.GetType();
		}
	}




}

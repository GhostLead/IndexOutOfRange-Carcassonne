using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Carcassonne
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CarcassoneGame : Window
	{
		List<kartya> lista = new List<kartya>();

		string[,] KartyaTomb = new string[8, 5];
		int sorSzam = 8;
		int oszlopSzam = 5;
		string JelenlegiKartya = "";
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
			Ellenoriz(((Button)(FrameworkElement)sender));

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
			string[] tombElso = generaltNev.Split('\\');
			string[] tombMasodik = tombElso[1].Split('.');
			JelenlegiKartya = tombMasodik[0];

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
			MessageBox.Show("Ez a mező már foglalt! Válassz másikat!", "Foglalt mező", MessageBoxButton.OK, MessageBoxImage.Error);
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
			ugTabla.Rows = sorSzam;
			ugTabla.Columns = oszlopSzam;

			for (int i = 0; i < KartyaTomb.GetLength(0); i++)
			{
				for (int j = 0; j < KartyaTomb.GetLength(1); j++)
				{

					KartyaTomb[i, j] = '0'.ToString();
				}
			}
			for (int i = 0; i < ugTabla.Rows * ugTabla.Columns; i++)
			{
				Button gomb1 = new Button();
				gomb1.Background = Brushes.ForestGreen;
				gomb1.Name = $"btnTeruletek{i}";
				gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
				if (i == 17)
				{
					BitmapImage bitimg = new BitmapImage();
					bitimg.BeginInit();
					bitimg.UriSource = new Uri(@"Kepek\U_R_U_R_S.png", UriKind.RelativeOrAbsolute);
					bitimg.EndInit();
					gomb1.Background = new ImageBrush(bitimg);
					KartyaTomb[3, 2] = "U_R_U_R_S";
					gomb1.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
					gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OccupiedTileErrorMessage));

				}

				ugTabla.Children.Add(gomb1);

			}
		}

		private void NavbarButtonsHidden()
		{
			btnSave.Visibility = Visibility.Hidden;
			btnSettings.Visibility = Visibility.Hidden;
			btnExit.Visibility = Visibility.Hidden;
			btnBetolt.Visibility = Visibility.Hidden;
		}

		private void NavbarButtonsVisible()
		{
			btnSave.Visibility = Visibility.Visible;
			btnSettings.Visibility = Visibility.Visible;
			btnExit.Visibility = Visibility.Visible;
			btnBetolt.Visibility = Visibility.Visible;
		}

		private void btnJobbraFordit_Click(object sender, RoutedEventArgs e)
		{
			string[] tomb = JelenlegiKartya.Split("_");
			kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya);
			string uj_nev = kartya.Fordit(generaltKartya, true);


			BitmapImage bitimg = new BitmapImage();
			bitimg.BeginInit();
			bitimg.UriSource = new Uri(@$"Kepek\{uj_nev}.png", UriKind.RelativeOrAbsolute);
			bitimg.EndInit();


			rctFelforditottKartya.Fill = new ImageBrush(bitimg);

			JelenlegiKartya = uj_nev;
		}

		private void btnBalraFordit_Click(object sender, RoutedEventArgs e)
		{
			string[] tomb = JelenlegiKartya.Split("_");
			kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya);
			string uj_nev = kartya.Fordit(generaltKartya, false);


			BitmapImage bitimg = new BitmapImage();
			bitimg.BeginInit();
			bitimg.UriSource = new Uri(@$"Kepek\{uj_nev}.png", UriKind.RelativeOrAbsolute);
			bitimg.EndInit();


			rctFelforditottKartya.Fill = new ImageBrush(bitimg);

			JelenlegiKartya = uj_nev;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			/*
			string[] sorok = new string[KartyaTomb.GetLength(0)];

			for (int i = 0; i < KartyaTomb.GetLength(0); i++)
			{
				for (int j = 0; j < KartyaTomb.GetLength(1); j++)
				{
					sorok[i] += KartyaTomb[i, j];
				}
			}
			File.WriteAllLines("nem.txt", sorok);
			*/
			TextWriter tw = new StreamWriter("new.txt");
			for (int i = 0; i < KartyaTomb.GetLength(0); i++)
			{
				for (int j = 0; j < KartyaTomb.GetLength(1); j++)
				{
					tw.WriteLine(KartyaTomb[i, j]);
				}
			}
			tw.Close();

		}


		private void btnBetolt_Click(object sender, RoutedEventArgs e)
		{

			lista.Clear();
			string[] lines = File.ReadAllLines("new.txt");
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i] != "0".ToString())
				{

					BitmapImage bitimg = new BitmapImage();
					bitimg.BeginInit();
					bitimg.UriSource = new Uri(@$"Kepek\{lines[i]}.png", UriKind.RelativeOrAbsolute);
					bitimg.EndInit();
					int oszlopindex = i % oszlopSzam;
					int sorindex = i / oszlopSzam;
					KartyaTomb[sorindex, oszlopindex] = lines[i];
					((Button)ugTabla.Children[i]).Background = new ImageBrush(bitimg);

					string[] tomb = lines[i].Split("_");

					kartya beolvasottKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), lines[i]);
					lista.Add(beolvasottKartya);

				}
			}




		}

		public void LeRakas(Button gomb)
		{
			gomb.Background = rctFelforditottKartya.Fill;
			Random rnd = new Random();
			string folder = @"Kepek";
			string[] files = Directory.GetFiles(folder);


			string generaltNev = files[rnd.Next(files.Length)];

			string[] segedTombElso = generaltNev.Split('\\');
			string[] segedTombMasodik = segedTombElso[1].Split('.');



			BitmapImage bitimg = new BitmapImage();
			bitimg.BeginInit();
			bitimg.UriSource = new Uri(@$"{generaltNev}", UriKind.RelativeOrAbsolute);
			bitimg.EndInit();



			CardPlacementSound();
			rctFelforditottKartya.Fill = new ImageBrush(bitimg);
			gomb.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
			gomb.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OccupiedTileErrorMessage));


			JelenlegiKartya = segedTombMasodik[0];
		}

		public void Ellenoriz(Button gomb)
		{

			string[] tomb = JelenlegiKartya.Split('_');

			kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya);
			lista.Add(generaltKartya);

			string[] tombKetto = gomb.Name.Split("k");
			int index = Convert.ToInt32(tombKetto[1]);
			int oszlopindex = index % oszlopSzam;
			int sorindex = index / oszlopSzam;

			KartyaTomb[sorindex, oszlopindex] = generaltKartya.Nev;


			bool vanFent = false;
			bool vanJobb = false;
			bool vanLent = false;
			bool vanBal = false;
			
			bool fentEllenorzes = true;
			bool jobbOldaliEllenorzes = true;
			bool lenitEllenorzes = true;
			bool balOldaliEllenorzes = true;


			if (sorindex - 1 > 0)
			{
				if (KartyaTomb[sorindex - 1, oszlopindex] != "0")
				{
					vanFent = true;
					if (vanFent && KartyaTomb[sorindex, oszlopindex][0] != KartyaTomb[sorindex - 1, oszlopindex][4])
					{
						fentEllenorzes = false;
					}

				}
				
			}
			if (oszlopindex + 1 < KartyaTomb.GetLength(1))
			{
				if (KartyaTomb[sorindex, oszlopindex + 1] != "0")
				{

					vanJobb = true;
					if (vanJobb && KartyaTomb[sorindex, oszlopindex][2] != KartyaTomb[sorindex, oszlopindex + 1][6])
					{
						jobbOldaliEllenorzes = false;
					}
				}

			}
			if (sorindex+1 < KartyaTomb.GetLength(0))
			{
				if (KartyaTomb[sorindex + 1, oszlopindex] != "0")
				{
					vanLent = true;
					if ( vanLent && KartyaTomb[sorindex, oszlopindex][4] != KartyaTomb[sorindex + 1, oszlopindex][0])
					{
						lenitEllenorzes = false;
					}

				}

			}

			if (oszlopindex -1 > 0)
			{
				if (KartyaTomb[sorindex, oszlopindex - 1] != "0")
				{
					vanBal = true;
					if (vanBal && KartyaTomb[sorindex, oszlopindex][6] != KartyaTomb[sorindex, oszlopindex - 1][2])
					{
						balOldaliEllenorzes = false;
					}

				}

			}

			if (vanFent == false && vanJobb == false && vanLent == false && vanBal == false)
			{
				KartyaTomb[sorindex, oszlopindex] = "0";
				lista.Remove(generaltKartya);
				MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
			}

			if (fentEllenorzes == false || jobbOldaliEllenorzes == false || lenitEllenorzes == false || balOldaliEllenorzes == false)
			{
				KartyaTomb[sorindex, oszlopindex] = "0";
				lista.Remove(generaltKartya);
				MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
			}
			else
			{
				LeRakas(gomb);
			}


		}

	}
}

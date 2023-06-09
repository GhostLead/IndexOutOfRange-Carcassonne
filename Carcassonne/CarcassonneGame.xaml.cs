﻿using Microsoft.Win32;
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
		const byte ESZAK = 0;
		const byte KELET = 2;
		const byte DEL = 4;
		const byte NYUGAT = 6;
		const byte KOZEP = 8;
		kartya[,] osztalyTomb = new kartya[8,5];
        SoundPlayer soundPlayer = new SoundPlayer("Card-Flip-Sound.wav");
		string pont = "";
		int UtHossz = 0;
		int VarosHossz = 0;
		int beteltPalyaBonusz = 0;

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


			BitmapImage jobbraForgatHatter = new BitmapImage();
			jobbraForgatHatter.BeginInit();
			jobbraForgatHatter.UriSource = new Uri(@"Egyeb\RotateButtonRight.jpg", UriKind.RelativeOrAbsolute);
			jobbraForgatHatter.EndInit();
			btnJobbraFordit.Background = new ImageBrush(jobbraForgatHatter);

			BitmapImage balraForgatHatter = new BitmapImage();
			balraForgatHatter.BeginInit();
			balraForgatHatter.UriSource = new Uri(@"Egyeb\RotateButtonLeft.jpg", UriKind.RelativeOrAbsolute);
			balraForgatHatter.EndInit();
			btnBalraFordit.Background = new ImageBrush(balraForgatHatter);
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

			for (int sorIndex = 0; sorIndex < KartyaTomb.GetLength(0); sorIndex++)
			{
				for (int oszlopIndex = 0; oszlopIndex < KartyaTomb.GetLength(1); oszlopIndex++)
				{

					KartyaTomb[sorIndex, oszlopIndex] = '0'.ToString();
				}
			}
			for (int index = 0; index < ugTabla.Rows * ugTabla.Columns; index++)
			{
				Button gomb1 = new Button();
				gomb1.Background = Brushes.ForestGreen;
				gomb1.Name = $"btnTeruletek{index}";
				gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
				ugTabla.Children.Add(gomb1);

			}


		}

		private void NavbarButtonsHidden()
		{
			btnSave.Visibility = Visibility.Hidden;
			btnExit.Visibility = Visibility.Hidden;
			btnLoad.Visibility = Visibility.Hidden;
			btnFinish.Visibility = Visibility.Hidden;
		}

		private void NavbarButtonsVisible()
		{
			btnSave.Visibility = Visibility.Visible;
			btnExit.Visibility = Visibility.Visible;
			btnLoad.Visibility = Visibility.Visible;
			btnFinish.Visibility = Visibility.Visible;
		}

		private void btnJobbraFordit_Click(object sender, RoutedEventArgs e)
		{
			if (btnStartGame.Visibility == Visibility.Visible)
			{
				MessageBox.Show("A játék még nincsen elindítva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

			}

			else
			{

				string[] tomb = JelenlegiKartya.Split("_");
				kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya, Convert.ToChar(tomb[5]));
				string uj_nev = kartya.Fordit(generaltKartya, true);


				BitmapImage bitimg = new BitmapImage();
				bitimg.BeginInit();
				bitimg.UriSource = new Uri(@$"Kepek\{uj_nev}.png", UriKind.RelativeOrAbsolute);
				bitimg.EndInit();


				rctFelforditottKartya.Fill = new ImageBrush(bitimg);

				JelenlegiKartya = uj_nev;
				
			}

		}

		private void btnBalraFordit_Click(object sender, RoutedEventArgs e)
		{

			if (btnStartGame.Visibility == Visibility.Visible)
			{
				MessageBox.Show("A játék még nincsen elindítva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

			}

			else
			{
				string[] tomb = JelenlegiKartya.Split("_");
				kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya, Convert.ToChar(tomb[5]));
				string uj_nev = kartya.Fordit(generaltKartya, false);


				BitmapImage bitimg = new BitmapImage();
				bitimg.BeginInit();
				bitimg.UriSource = new Uri(@$"Kepek\{uj_nev}.png", UriKind.RelativeOrAbsolute);
				bitimg.EndInit();


				rctFelforditottKartya.Fill = new ImageBrush(bitimg);

				JelenlegiKartya = uj_nev;

			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			/// <summary>
			/// Elmenti a jelenlegi pálya állapotát
			/// </summary>

			if (btnStartGame.Visibility == Visibility.Visible)
			{
				MessageBox.Show("A játék még nincsen elindítva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

			}

			else
			{
				TextWriter tw = new StreamWriter("new.txt");
				for (int sorIndex = 0; sorIndex < KartyaTomb.GetLength(0); sorIndex++)
				{
					for (int oszlopIndex = 0; oszlopIndex < KartyaTomb.GetLength(1); oszlopIndex++)
					{
						tw.WriteLine(KartyaTomb[sorIndex, oszlopIndex]);
					}
				}
				tw.Close();
				MessageBox.Show("A pálya mentése sikeres!!", "Sikeres mentés!", MessageBoxButton.OK, MessageBoxImage.Information);

			}

		}
		
		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			/// <summary>
			/// Betölti az utoljára elmentett pályát:
			/// </summary>
			if (btnStartGame.Visibility == Visibility.Visible)
			{
				MessageBox.Show("A játék még nincsen elindítva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

			}

			else
			{
				for (int sorIndex = 0; sorIndex < osztalyTomb.GetLength(0); sorIndex++)
				{
					for (int oszlopIndex = 0; oszlopIndex < osztalyTomb.GetLength(1); oszlopIndex++)
					{
						osztalyTomb[sorIndex, oszlopIndex] = new kartya();
					}
				}

				lista.Clear();
				
				string[] lines = File.ReadAllLines("new.txt");
				for (int index = 0; index < lines.Length; index++)
				{
					if (lines[index] != "0".ToString())
					{

						BitmapImage bitimg = new BitmapImage();
						bitimg.BeginInit();
						bitimg.UriSource = new Uri(@$"Kepek\{lines[index]}.png", UriKind.RelativeOrAbsolute);
						bitimg.EndInit();
						int oszlopindex = index % oszlopSzam;
						int sorindex = index / oszlopSzam;
						KartyaTomb[sorindex, oszlopindex] = lines[index];
						((Button)ugTabla.Children[index]).Background = new ImageBrush(bitimg);
						((Button)ugTabla.Children[index]).RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
						((Button)ugTabla.Children[index]).AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OccupiedTileErrorMessage));
						string[] tomb = lines[index].Split("_");

						kartya beolvasottKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), lines[index], Convert.ToChar(tomb[5]));
						lista.Add(beolvasottKartya);
						osztalyTomb[sorindex,oszlopindex] = beolvasottKartya;
					}
				}
				MessageBox.Show("A pálya betöltése sikeres!!", "Sikeres betöltés!", MessageBoxButton.OK, MessageBoxImage.Information);
			}

		}

        private void Lerak(Button gomb)
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

		private void Ellenoriz(Button gomb)
		{

			string[] tomb = JelenlegiKartya.Split('_');


			string[] tombKetto = gomb.Name.Split("k");
			int index = Convert.ToInt32(tombKetto[1]);
			int oszlopindex = index % oszlopSzam;
			int sorindex = index / oszlopSzam;
			if (lista.Count == 0)
			{
				kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya, Convert.ToChar(tomb[5]));
				lista.Add(generaltKartya);
				osztalyTomb[sorindex, oszlopindex] = generaltKartya;
				KartyaTomb[sorindex, oszlopindex] = generaltKartya.Nev;
				Lerak(gomb);
			}

			else
			{

				kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya, Convert.ToChar(tomb[5]));
				lista.Add(generaltKartya);
				osztalyTomb[sorindex, oszlopindex] = generaltKartya;
				KartyaTomb[sorindex, oszlopindex] = generaltKartya.Nev;



				bool vanFent = false;
				bool vanJobb = false;
				bool vanLent = false;
				bool vanBal = false;

				bool fentEllenorzes = true;
				bool jobbOldaliEllenorzes = true;
				bool lenitEllenorzes = true;
				bool balOldaliEllenorzes = true;


				if (sorindex - 1 >= 0)
				{
					if (KartyaTomb[sorindex - 1, oszlopindex] != "0")
					{
						vanFent = true;
						if (vanFent && KartyaTomb[sorindex, oszlopindex][ESZAK] != KartyaTomb[sorindex - 1, oszlopindex][DEL])
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
						if (vanJobb && KartyaTomb[sorindex, oszlopindex][KELET] != KartyaTomb[sorindex, oszlopindex + 1][NYUGAT])
						{
							jobbOldaliEllenorzes = false;
						}
					}

				}
				if (sorindex + 1 < KartyaTomb.GetLength(0))
				{
					if (KartyaTomb[sorindex + 1, oszlopindex] != "0")
					{
						vanLent = true;
						if (vanLent && KartyaTomb[sorindex, oszlopindex][DEL] != KartyaTomb[sorindex + 1, oszlopindex][ESZAK])
						{
							lenitEllenorzes = false;
						}

					}

				}

				if (oszlopindex - 1 >= 0)
				{
					if (KartyaTomb[sorindex, oszlopindex - 1] != "0")
					{
						vanBal = true;
						if (vanBal && KartyaTomb[sorindex, oszlopindex][NYUGAT] != KartyaTomb[sorindex, oszlopindex - 1][KELET])
						{
							balOldaliEllenorzes = false;
						}

					}

				}

				if ((fentEllenorzes == false || jobbOldaliEllenorzes == false || lenitEllenorzes == false || balOldaliEllenorzes == false) ||
					(vanFent == false && vanJobb == false && vanLent == false && vanBal == false))
				{
					KartyaTomb[sorindex, oszlopindex] = "0";
					lista.Remove(generaltKartya);
					osztalyTomb[sorindex, oszlopindex] = new kartya();
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna, vagy nem tud csatlakozni a körülötte lévő kártyákhoz","Nem rakható le a kártya!", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
				{
					Lerak(gomb);
				}
			}
		}
		
		private void btnFinish_Click(object sender, RoutedEventArgs e)
		{
            if (btnStartGame.Visibility == Visibility.Visible)
            {
                MessageBox.Show("A játék még nincsen elindítva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }
			else
			{

			
				UtHossz = 0;
				string[,] MatrixUt = MatrixMasolat(KartyaTomb);
				for (int sorIndexMasolatUt = 0; sorIndexMasolatUt < MatrixUt.GetLength(0); sorIndexMasolatUt++)
				{
					for (int oszlopIndexMasolatUt = 0; oszlopIndexMasolatUt < MatrixUt.GetLength(1); oszlopIndexMasolatUt++)
					{
						if (MatrixUt[sorIndexMasolatUt, oszlopIndexMasolatUt].Contains('U'))
						{

							int sorIndexUt = sorIndexMasolatUt;
							int oszlopIndexUt = oszlopIndexMasolatUt;
							UtHossz += UtHossza(MatrixUt, sorIndexUt, oszlopIndexUt);
							MatrixUt[sorIndexMasolatUt, oszlopIndexMasolatUt] = "0";

						}
					}
				}

				VarosHossz = 0;
				string[,] MatrixVaros = MatrixMasolat(KartyaTomb);
				for (int sorIndexMasolatVaros = 0; sorIndexMasolatVaros < MatrixVaros.GetLength(0); sorIndexMasolatVaros++)
				{
					for (int oszlopIndexMasolatVaros = 0; oszlopIndexMasolatVaros < MatrixVaros.GetLength(1); oszlopIndexMasolatVaros++)
					{
						if (MatrixVaros[sorIndexMasolatVaros, oszlopIndexMasolatVaros].Contains('V'))
						{
							int sorIndexVaros = sorIndexMasolatVaros;
							int oszlopIndexVaros = oszlopIndexMasolatVaros;
							if (MatrixVaros[sorIndexMasolatVaros, oszlopIndexMasolatVaros][KOZEP] == 'V')
							{
								VarosHossz += VarosHossza(MatrixVaros, sorIndexVaros, oszlopIndexVaros);
								MatrixVaros[sorIndexMasolatVaros, oszlopIndexMasolatVaros] = "0";
							}
							else
							{
								VarosHossz += (VarosHossza(MatrixVaros, sorIndexVaros,oszlopIndexVaros))*2;
								MatrixVaros[sorIndexMasolatVaros,oszlopIndexMasolatVaros] = "0";

							}
						
					
						}
					}
				}
				beteltPalyaBonusz = BeteltPalya(MatrixMasolat(KartyaTomb));

				lblNameSend.Visibility = Visibility.Visible;
				txtNameSend.Visibility = Visibility.Visible;
				btnNameSend.Visibility = Visibility.Visible;

					/*
					CarcassoneGame ujAblak = new CarcassoneGame();
					Application.Current.MainWindow = ujAblak;
					ujAblak.Show();
					this.Close();
					*/

            }
        }

		private string[,] MatrixMasolat(string[,] matrix)
		{
			string[,] masolatTomb = new string[matrix.GetLength(0), matrix.GetLength(1)];
			for (int sorIndex = 0; sorIndex < masolatTomb.GetLength(0); sorIndex++)
			{
				for (int oszlopIndex = 0; oszlopIndex < masolatTomb.GetLength(1); oszlopIndex++)
				{
					masolatTomb[sorIndex, oszlopIndex] = matrix[sorIndex, oszlopIndex];
				}
			}
			return masolatTomb;
		}
		private int UtHossza(string[,] jelenlegiTerkep, int sorIndex, int oszlopIndex)
		{
			int mertUthossz = 0;
			
			string masolat = jelenlegiTerkep[sorIndex, oszlopIndex];
			jelenlegiTerkep[sorIndex, oszlopIndex] = "0";

			

			if (sorIndex > 0 && masolat[ESZAK] == 'U' && jelenlegiTerkep[sorIndex - 1, oszlopIndex] != "0")
			{
				mertUthossz += UtHossza(jelenlegiTerkep, sorIndex - 1, oszlopIndex);
			}

			if (sorIndex < sorSzam - 1 && masolat[DEL] == 'U' && jelenlegiTerkep[sorIndex + 1, oszlopIndex] != "0")
			{
				mertUthossz += UtHossza(jelenlegiTerkep, sorIndex + 1, oszlopIndex);
			}

			if (oszlopIndex < oszlopSzam - 1 && masolat[KELET] == 'U' && jelenlegiTerkep[sorIndex, oszlopIndex + 1] != "0")
			{
				mertUthossz += UtHossza(jelenlegiTerkep, sorIndex, oszlopIndex + 1);
			}

			if (oszlopIndex > 0 && masolat[NYUGAT] == 'U' && jelenlegiTerkep[sorIndex, oszlopIndex - 1] != "0")
			{
				mertUthossz += UtHossza(jelenlegiTerkep, sorIndex, oszlopIndex - 1);
			}

			return mertUthossz + 1;
		}

		private int VarosHossza(string[,] jelenlegiTerkep, int sorIndex, int oszlopIndex)
		{
			int mertVaroshossz = 0;
			string masolat = jelenlegiTerkep[sorIndex, oszlopIndex];
			jelenlegiTerkep[sorIndex, oszlopIndex] = "0";

			if (masolat[KOZEP] == 'V')
			{
				return mertVaroshossz + 7;
			}

			if (masolat.Contains('C'))
			{
				return mertVaroshossz + 2;
			}

			else
			{
				if (sorIndex > 0 && masolat[ESZAK] == 'V' && jelenlegiTerkep[sorIndex - 1, oszlopIndex] != "0")
				{
					mertVaroshossz += VarosHossza(jelenlegiTerkep, sorIndex - 1, oszlopIndex);
				}

				if (sorIndex < sorSzam - 1 && masolat[DEL] == 'V' && jelenlegiTerkep[sorIndex + 1, oszlopIndex] != "0")
				{
					mertVaroshossz += VarosHossza(jelenlegiTerkep, sorIndex + 1, oszlopIndex);
				}

				if (oszlopIndex < oszlopSzam - 1 && masolat[KELET] == 'V' && jelenlegiTerkep[sorIndex, oszlopIndex + 1] != "0")
				{
					mertVaroshossz += VarosHossza(jelenlegiTerkep, sorIndex, oszlopIndex + 1);
				}

				if (oszlopIndex > 0 && masolat[NYUGAT] == 'V' && jelenlegiTerkep[sorIndex, oszlopIndex - 1] != "0")
				{
					mertVaroshossz += VarosHossza(jelenlegiTerkep, sorIndex, oszlopIndex - 1);
				}
				return mertVaroshossz + 1;

			}
		}

		private int KolostorPontozas(string[,] jelenlegiTerkep)
		{
			int pont = 0;
			for (int sorIndex = 0; sorIndex < jelenlegiTerkep.GetLength(0); sorIndex++)
			{
				for (int oszlopIndex = 0; oszlopIndex < jelenlegiTerkep.GetLength(1); oszlopIndex++)
				{
					if (jelenlegiTerkep[sorIndex,oszlopIndex] != "0" && jelenlegiTerkep[sorIndex, oszlopIndex][KOZEP] == 'K')
					{
						if (sorIndex > 0 && jelenlegiTerkep[sorIndex - 1, oszlopIndex] != "0")
						{
							pont++;
						}

						if (sorIndex < sorSzam - 1 && jelenlegiTerkep[sorIndex + 1, oszlopIndex] != "0")
						{
							pont++;
						}

						if (oszlopIndex > 0 && jelenlegiTerkep[sorIndex, oszlopIndex - 1] != "0")
						{
							pont++;
						}

						if (oszlopIndex < oszlopSzam - 1 && jelenlegiTerkep[sorIndex, oszlopIndex + 1] != "0")
						{
							pont++;
						}

						if ((sorIndex > 0 && oszlopIndex > 0) && jelenlegiTerkep[sorIndex - 1, oszlopIndex - 1] != "0")
						{
							pont++;
						}

						if ((sorIndex < sorSzam - 1 && oszlopIndex < oszlopSzam - 1) && jelenlegiTerkep[sorIndex + 1, oszlopIndex + 1] != "0")
						{
							pont++;
						}

						if ((sorIndex > 0 && oszlopIndex < oszlopSzam - 1) && jelenlegiTerkep[sorIndex - 1, oszlopIndex + 1] != "0")
						{
							pont++;
						}

						if ((sorIndex < sorSzam - 1 && oszlopIndex > 0) && jelenlegiTerkep[sorIndex + 1, oszlopIndex - 1] != "0")
						{
							pont++;
						}
					}
					
				}
			}
			return pont;
		}

		private int BeteltPalya(string[,] jelenlegiTerkep)
		{
			int pont = 10;	
			for (int sorIndex = 0; sorIndex < jelenlegiTerkep.GetLength(0); sorIndex++)
			{
				for (int oszlopIndex = 0; oszlopIndex < jelenlegiTerkep.GetLength(1); oszlopIndex++)
				{
					if (jelenlegiTerkep[sorIndex,oszlopIndex] == "0")
					{
						pont = 0;
						break;
					}
				}
			}
			return pont;
		}

		private void btnNameSend_Click(object sender, RoutedEventArgs e)
		{

			if (txtNameSend.Text == null)
			{
				MessageBox.Show("Kérem adjon meg egy nevet a mentés előtt!", "Sikertelen mentés", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			else
			{
				pont = $"{txtNameSend.Text};{UtHossz + VarosHossz + KolostorPontozas(MatrixMasolat(KartyaTomb)) + beteltPalyaBonusz};{UtHossz};{VarosHossz};" +
					$"{KolostorPontozas(MatrixMasolat(KartyaTomb))};{beteltPalyaBonusz}";

				List<string> pontLista = new List<string>
				{
					pont
				};
				string myFile = @"ScoreTableData.txt";

				using (StreamWriter sw = File.AppendText(myFile))
				{
					for (int i = 0; i < pontLista.Count; i++)
					{
						sw.WriteLine(pontLista[i]);
					}
					sw.Close();
				}
				lista.Clear();
				for (int sorIndex = 0; sorIndex < KartyaTomb.GetLength(0); sorIndex++)
				{
					for (int oszlopIndex = 0; oszlopIndex < KartyaTomb.GetLength(1); oszlopIndex++)
					{
						KartyaTomb[sorIndex, oszlopIndex] = "0";
					}
				}

				for (int sorIndex = 0; sorIndex < osztalyTomb.GetLength(0); sorIndex++)
				{
					for (int oszlopIndex = 0; oszlopIndex < osztalyTomb.GetLength(1); oszlopIndex++)
					{
						osztalyTomb[sorIndex, oszlopIndex] = new kartya();
					}
				}
				CarcassoneGame cgw = new CarcassoneGame();
				MessageBox.Show("A pontszám és a név mentése sikeres!!", "Sikeres mentés!", MessageBoxButton.OK, MessageBoxImage.Information); ;
				ScoreTable stw = new ScoreTable();
				this.Close();
				cgw.Show();
				stw.Show();

			}
		}

	}
}

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

		string[] KartyaTomb = new string[40];
		List<int> fent = new List<int> {0,1,2,3,4};
		List<int> jobb = new List<int> {4,9,14,19,24,29,34,39};
		List<int> lent = new List<int> {35,36,37,38,39 };
		List<int> bal = new List<int> {0,5,10,15,20,25,30,35};

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

			for (int i = 0; i < KartyaTomb.Length; i++)
			{
				KartyaTomb[i] = '0'.ToString();
			}
            for (int i = 0; i < ugTabla.Rows * ugTabla.Columns; i++)
            {
                Button gomb1 = new Button();
                gomb1.Background = Brushes.ForestGreen;
                gomb1.Name = $"btnTeruletek{i}";
                if (i == 17)
                {
					BitmapImage bitimg = new BitmapImage();
					bitimg.BeginInit();
					bitimg.UriSource = new Uri(@"Kepek\U_R_U_R_S.png", UriKind.RelativeOrAbsolute);
					bitimg.EndInit();
					gomb1.Background = new ImageBrush(bitimg);
                    KartyaTomb[i] = "U_R_U_R_S";

				}

                gomb1.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(Button_Click));
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
			File.WriteAllLines("new.txt", KartyaTomb);
			using StreamReader sr = new StreamReader("new.txt");
			string res = sr.ReadToEnd();
		}

		private void btnBetolt_Click(object sender, RoutedEventArgs e)
		{
            lista.Clear();
			string[] lines = File.ReadAllLines("new.txt");
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i] != '0'.ToString())
				{

					BitmapImage bitimg = new BitmapImage();
					bitimg.BeginInit();
					bitimg.UriSource = new Uri(@$"Kepek\{lines[i]}.png", UriKind.RelativeOrAbsolute);
					bitimg.EndInit();
                    KartyaTomb[i] = lines[i];   
					((Button)ugTabla.Children[i]).Background = new ImageBrush(bitimg);

                    string[] tomb = lines[i].Split("_");

                    kartya beolvasottKartya =new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), lines[i]);
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
			var pushedButton = gomb;

			string[] tomb = JelenlegiKartya.Split('_');

			kartya generaltKartya = new kartya(Convert.ToChar(tomb[0]), Convert.ToChar(tomb[1]), Convert.ToChar(tomb[2]), Convert.ToChar(tomb[3]), Convert.ToChar(tomb[4]), JelenlegiKartya);
			lista.Add(generaltKartya);

			string[] tombKetto = gomb.Name.Split("k");
			int index = Convert.ToInt32(tombKetto[1]);
			KartyaTomb[index] = generaltKartya.Nev;


			bool vanFent = false;
			bool vanJobb = false;
			bool vanLent = false;
			bool vanBal = false;

			bool FentiEllenorzes = false;
			bool JobbOldaliEllenorzes = false;
			bool LentiEllenorzes = false;
			bool BalOldaliEllenorzes = false;
			/// <summary>
			/// Ha nem szélen van
			/// </summary>
			if (!fent.Contains(index) && !jobb.Contains(index) && !lent.Contains(index) && !bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}
				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}
				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanFent == false && vanJobb == false && vanLent == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}
				/// <summary>
				/// Ha fent
				/// </summary>
				if (vanFent && !vanBal && !vanLent && !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanFent && !vanLent && !vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanFent && !vanJobb && !vanBal)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanFent && !vanLent && !vanJobb)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és jobb
				/// </summary>
				else if (vanFent && vanJobb && !vanLent && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és lent
				/// </summary>
				else if (vanFent && vanLent && !vanJobb && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha bal és fent
				/// </summary>
				else if (vanBal && vanFent && !vanLent && !vanJobb)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb és bal
				/// </summary>
				else if (vanJobb && vanBal && !vanFent && !vanLent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (JobbOldaliEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}

				}
				/// <summary>
				/// Ha jobb és lent
				/// </summary>
				else if (vanJobb && vanLent && !vanFent && !vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent és bal
				/// </summary>
				else if (vanLent && vanBal && !vanFent && !vanJobb)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és lent és jobb
				/// </summary>
				else if (vanFent && vanJobb && vanLent && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és jobb és bal
				/// </summary>
				else if (vanFent && vanJobb && vanBal && !vanLent)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (FentiEllenorzes && JobbOldaliEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és lent és bal
				/// </summary>
				else if (vanFent && vanLent && vanBal && !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (FentiEllenorzes && LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb és lent és bal
				/// </summary>
				else if (vanJobb && vanLent && vanBal && !vanFent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (JobbOldaliEllenorzes && LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és lent és bal és jobb
				/// </summary>
				else if (vanFent && vanLent && vanBal && vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes && BalOldaliEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
			}
			/// <summary>
			/// Ha a fenti szélen van
			/// </summary>
			else if (fent.Contains(index) && !jobb.Contains(index) && !lent.Contains(index) && !bal.Contains(index))
			{
				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}
				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanJobb == false && vanLent == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}

				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanLent && !vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanJobb && !vanBal)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanLent && !vanJobb)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha jobb és bal
				/// </summary>
				else if (vanJobb && vanBal && !vanLent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (JobbOldaliEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}

				}

				/// <summary>
				/// Ha jobb és lent
				/// </summary>
				else if (vanJobb && vanLent && !vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent és bal
				/// </summary>
				else if (vanLent && vanBal && !vanJobb)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha jobb és lent és bal
				/// </summary>
				else if (vanJobb && vanLent && vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (JobbOldaliEllenorzes && LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

			}
			/// <summary>
			/// Ha a jobb oldali szélen van
			/// </summary>
			else if (!fent.Contains(index) && jobb.Contains(index) && !lent.Contains(index) && !bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}

				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanFent == false && vanLent == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}
				/// <summary>
				/// Ha fent
				/// </summary>
				if (vanFent && !vanBal && !vanLent)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanFent && !vanBal)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanFent && !vanLent)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és lent
				/// </summary>
				else if (vanFent && vanLent && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha bal és fent
				/// </summary>
				else if (vanBal && vanFent && !vanLent)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha lent és bal
				/// </summary>
				else if (vanLent && vanBal && !vanFent)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és lent és bal
				/// </summary>
				else if (vanFent && vanLent && vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (FentiEllenorzes && LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

			}
			/// <summary>
			/// Ha a lenti szélen van
			/// </summary>
			else if (!fent.Contains(index) && !jobb.Contains(index) && lent.Contains(index) && !bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}
				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanFent == false && vanJobb == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}

				/// <summary>
				/// Ha fent
				/// </summary>
				if (vanFent && !vanBal && !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanFent && !vanBal)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanFent && !vanJobb)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és jobb
				/// </summary>
				else if (vanFent && vanJobb && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha jobb és bal
				/// </summary>
				else if (vanJobb && vanBal && !vanFent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (JobbOldaliEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}

				}
				/// <summary>
				/// Ha bal és fent
				/// </summary>
				else if (vanBal && vanFent && !vanJobb)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és jobb és bal
				/// </summary>
				else if (vanFent && vanJobb && vanBal && !vanLent)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (FentiEllenorzes && JobbOldaliEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
			}
			/// <summary>
			/// Ha a bal oldali szélen van
			/// </summary>
			else if (!fent.Contains(index) && !jobb.Contains(index) && !lent.Contains(index) && bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}
				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}
				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}

				if (vanFent == false && vanJobb == false && vanLent == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}
				/// <summary>
				/// Ha fent
				/// </summary>
				if (vanFent && !vanLent && !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanFent && !vanLent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanFent && !vanJobb)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és jobb
				/// </summary>
				else if (vanFent && vanJobb && !vanLent)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha fent és lent
				/// </summary>
				else if (vanFent && vanLent && !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha jobb és lent
				/// </summary>
				else if (vanJobb && vanLent && !vanFent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és lent és jobb
				/// </summary>
				else if (vanFent && vanJobb && vanLent)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (FentiEllenorzes && LentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}


			}
			/// <summary>
			/// Ha a bal felső sarokban van
			/// </summary>
			else if (fent.Contains(index) && !jobb.Contains(index) && !lent.Contains(index) && bal.Contains(index))
			{

				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}
				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}


				if (vanJobb == false && vanLent == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}
				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanLent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanJobb)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha jobb és lent
				/// </summary>
				else if (vanJobb && vanLent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
			}
			/// <summary>
			/// Ha a jobb felső sarokban van
			/// </summary>
			else if (fent.Contains(index) && jobb.Contains(index) && !lent.Contains(index) && !bal.Contains(index))
			{


				if (KartyaTomb[index + 5] != "0")
				{
					vanLent = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanLent == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}


				/// <summary>
				/// Ha lent
				/// </summary>
				else if (vanLent && !vanBal)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (LentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanLent)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha lent és bal
				/// </summary>
				else if (vanLent && vanBal)
				{
					if (generaltKartya.MiLe == KartyaTomb[index + 5][0])
					{
						LentiEllenorzes = true;

					}

					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (LentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
			}
			/// <summary>
			/// Ha a jobb alsó sarokban van
			/// </summary>
			else if (!fent.Contains(index) && jobb.Contains(index) && lent.Contains(index) && !bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}

				if (KartyaTomb[index - 1] != "0")
				{
					vanBal = true;
				}

				if (vanFent == false && vanBal == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}
				/// <summary>
				/// Ha van fent
				/// </summary>

				if (vanFent && !vanBal)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha bal
				/// </summary>
				else if (vanBal && !vanFent)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha bal és fent
				/// </summary>
				else if (vanBal && vanFent)
				{
					if (generaltKartya.MiBal == KartyaTomb[index - 1][2])
					{
						BalOldaliEllenorzes = true;
					}

					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes && BalOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

			}
			/// <summary>
			/// Ha a bal alsó sarokban van
			/// </summary>
			else if (!fent.Contains(index) && !jobb.Contains(index) && lent.Contains(index) && bal.Contains(index))
			{
				if (KartyaTomb[index - 5] != "0")
				{
					vanFent = true;
				}
				if (KartyaTomb[index + 1] != "0")
				{
					vanJobb = true;
				}


				if (vanFent == false && vanJobb == false)
				{
					KartyaTomb[index] = "0";
					lista.Remove(generaltKartya);
					MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nincs körülötte kártya amihez csatlakozni tudna!");
				}

				/// <summary>
				/// Ha fent
				/// </summary>
				if (vanFent &&  !vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (FentiEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
				/// <summary>
				/// Ha jobb
				/// </summary>
				else if (vanJobb && !vanFent)
				{
					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}

				/// <summary>
				/// Ha fent és jobb
				/// </summary>
				else if (vanFent && vanJobb)
				{
					if (generaltKartya.MiFel == KartyaTomb[index - 5][4])
					{
						FentiEllenorzes = true;

					}

					if (generaltKartya.MiJobb == KartyaTomb[index + 1][6])
					{
						JobbOldaliEllenorzes = true;

					}

					if (FentiEllenorzes && JobbOldaliEllenorzes)
					{
						LeRakas(pushedButton);
					}
					else
					{
						KartyaTomb[index] = "0";
						lista.Remove(generaltKartya);
						MessageBox.Show("A kártyát nem lehet lehelyezni, mivel nem tud csatlakozni a körülötte lévő kártyákhoz!");
					}
				}
			}


		}

	}
}

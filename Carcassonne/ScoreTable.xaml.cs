using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.IO;
using System.CodeDom.Compiler;
using Carcassonne;


namespace Carcassonne
{
    /// <summary>
    /// Interaction logic for ScoreTable.xaml
    /// </summary>
    public partial class ScoreTable : Window
    {

		List<ScoreClass> pontozas = new List<ScoreClass>();
		public ScoreTable()
        {
            InitializeComponent();
            

            
            string[] lines = File.ReadAllLines("ScoreTableData.txt");
            

			for (int i = 0; i < lines.Length; i++)
            {
				string[] pontok = lines[i].Split(';');
				
                ScoreClass betoltpont = new ScoreClass(Convert.ToString(pontok[0]), Convert.ToInt32(pontok[1]), Convert.ToInt32(pontok[2]), Convert.ToInt32(pontok[3]), Convert.ToInt32(pontok[4]), Convert.ToInt32(pontok[5]));
                pontozas.Add(betoltpont);
			}

            PontTablazat.ItemsSource = pontozas;


		}

	}
}

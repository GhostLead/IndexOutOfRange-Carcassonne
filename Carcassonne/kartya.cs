using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    class kartya
    {
		string nev;
		char miFel;
		char miLe;
		char miJobb;
		char miBal;
		char miKozep;

		public kartya(char miFel, char miJobb, char miLe, char miBal, char miKozep, string nev)
		{
			this.MiFel = miFel;
			this.MiJobb = miJobb;
			this.MiLe = miLe;
			this.MiBal = miBal;
			this.MiKozep = miKozep;
			this.nev = nev;
		}

		public char MiFel { get => miFel; set => miFel = value; }
		public char MiLe { get => miLe; set => miLe = value; }
		public char MiJobb { get => miJobb; set => miJobb = value; }
		public char MiBal { get => miBal; set => miBal = value; }
		public char MiKozep { get => miKozep; set => miKozep = value; }
		public string Nev { get => nev; set => nev = value; }

		public static string Fordit(kartya kartyNev, bool jobbVagyBal)
		{

			if (jobbVagyBal == true)
			{
				string[] tombJobb = kartyNev.nev.Split('_');
				char felul = Convert.ToChar(tombJobb[0]);
				char jobbra = Convert.ToChar(tombJobb[1]);
				char lent = Convert.ToChar(tombJobb[2]);
				char balra = Convert.ToChar(tombJobb[3]);


				kartyNev.miFel = balra;
				kartyNev.miJobb = felul;
				kartyNev.miLe = jobbra;
				kartyNev.miBal = lent;
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep;

				return kartyNev.nev;
			}
			else
			{

				string[] tombBal = kartyNev.nev.Split('_');
				char felul = Convert.ToChar(tombBal[0]);
				char jobbra = Convert.ToChar(tombBal[1]);
				char alul = Convert.ToChar(tombBal[2]);
				char balra = Convert.ToChar(tombBal[3]);


				kartyNev.miFel = jobbra;
				kartyNev.miJobb = alul;
				kartyNev.miLe = balra;
				kartyNev.miBal = felul;
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep;

				return kartyNev.nev;
			}
		}
	}
}

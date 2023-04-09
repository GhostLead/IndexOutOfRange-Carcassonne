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
		char cimer;
		bool uresKartya;
		const int SOROK_SZAMA = 8;
		const int OSZLOPOK_SZAMA = 5;
		public kartya(char miFel, char miJobb, char miLe, char miBal, char miKozep, string nev, char cimer)
		{
			this.miFel = miFel;
			this.miJobb = miJobb;
			this.miLe = miLe;
			this.miBal = miBal;
			this.miKozep = miKozep;
			this.nev = nev;
			this.cimer = cimer;

		}

		public kartya()
		{
			this.uresKartya = true;
		}

		public char MiFel { get => miFel;}
		public char MiLe { get => miLe;}
		public char MiJobb { get => miJobb;}
		public char MiBal { get => miBal;}
		public char MiKozep { get => miKozep;}
		public string Nev { get => nev;}
		public bool UresKartya { get => uresKartya;}
		public char Cimer { get => cimer;}

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
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep + "_" + kartyNev.cimer;

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
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep + "_" + kartyNev.cimer;

				return kartyNev.nev;
			}
		}
		
		
		
	}
}

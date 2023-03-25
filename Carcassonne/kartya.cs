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

		public kartya(char miFel, char miLe, char miJobb, char miBal, char miKozep, string nev)
		{
			this.MiFel = miFel;
			this.MiLe = miLe;
			this.MiJobb = miJobb;
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

		public static string Fordit(kartya kartyNev, bool jobbVagyBall)
		{

			if (jobbVagyBall == true)
			{
				string[] tombJobb = kartyNev.nev.Split('_');
				char aJobb = Convert.ToChar(tombJobb[0]);
				char bJobb = Convert.ToChar(tombJobb[1]);
				char cJobb = Convert.ToChar(tombJobb[2]);
				char dJobb = Convert.ToChar(tombJobb[3]);


				kartyNev.miFel = dJobb;
				kartyNev.miJobb = aJobb;
				kartyNev.miLe = bJobb;
				kartyNev.miBal = cJobb;
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep;

				return kartyNev.nev;
			}
			else
			{

				string[] tombBall = kartyNev.nev.Split('_');
				char aBall = Convert.ToChar(tombBall[0]);
				char bBall = Convert.ToChar(tombBall[1]);
				char cBall = Convert.ToChar(tombBall[2]);
				char dBall = Convert.ToChar(tombBall[3]);


				kartyNev.miFel = bBall;
				kartyNev.miJobb = cBall;
				kartyNev.miLe = dBall;
				kartyNev.miBal = aBall;
				kartyNev.nev = kartyNev.miFel + "_" + kartyNev.miJobb + "_" + kartyNev.miLe + "_" + kartyNev.miBal + "_" + kartyNev.miKozep;

				return kartyNev.nev;
			}
		}
	}
}

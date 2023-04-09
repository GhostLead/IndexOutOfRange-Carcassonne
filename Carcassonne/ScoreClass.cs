using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne
{
    class ScoreClass
    {
        int sorszam = 0;
        int osszesPont = 0;
        int ut = 0;
        int varos = 0;
        int kolostor = 0;
        int bonusz = 0;
		public ScoreClass(int sorszam, int osszesPont, int ut, int varos, int kolostor, int bonusz)
		{
			this.sorszam = sorszam;
			this.osszesPont = osszesPont;
			this.ut = ut;
			this.varos = varos;
			this.kolostor = kolostor;
			this.bonusz = bonusz;
		}


		public int Sorszam { get => sorszam;}
		public int OsszesPont { get => osszesPont;}
		public int Ut { get => ut;}
		public int Varos { get => varos;}
		public int Kolostor { get => kolostor;}
		public int Bonusz { get => bonusz;}

	}
}

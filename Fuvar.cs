using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDOGA_2023_09_20
{
    internal class Fuvar
    {
        int taxiID;
        string indulas;
        int idotartam;
        double tavolsag;
        double vitelDij;
        double borravalo;
        string fozetesModja;

        public Fuvar(string sor)
        {
            string[] tomb = sor.Split(";");

            this.taxiID = Convert.ToInt32(tomb[0]);
            this.indulas = tomb[1];
            this.idotartam = Convert.ToInt32(tomb[2]);
            this.tavolsag = Convert.ToDouble(tomb[3]);
            this.vitelDij = Convert.ToDouble(tomb[4]);
            this.borravalo = Convert.ToDouble(tomb[5]);
            this.fozetesModja = tomb[6];
        }

        public int TaxiID { get => taxiID;}
        public string Indulas { get => indulas;}
        public int Idotartam { get => idotartam;}
        public double Tavolsag { get => tavolsag;}
        public double VitelDij { get => vitelDij;}
        public double Borravalo { get => borravalo;}
        public string FozetesModja { get => fozetesModja;}
    }
}

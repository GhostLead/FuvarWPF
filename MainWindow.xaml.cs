using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppDOGA_2023_09_20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// balra: BekeTamas
    /// jobbra: Gajdos Csanád
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Fuvar> fuvarLista = File.ReadAllLines("fuvar.csv").Skip(1).Select(x => new Fuvar(x)).ToList(); 
        public MainWindow()
        {
            InitializeComponent();
            OtodikFeladat();
            HetedikFeladat();

        }

        private void btnHarmadikFeladat_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Convert.ToString($"3. feladat: {fuvarLista.Count}"));
        }

        private void btnNegyedikFeladat_Click(object sender, RoutedEventArgs e)
        {
            int azonosito = 0;

            try
            {
                azonosito = Convert.ToInt32(txtNegyedikFeladat.Text);
            }
            catch (FormatException)
            {

                MessageBox.Show("Helytelen megadási formátum!");
            }

            if (fuvarLista.Exists(x => x.TaxiID == azonosito))
            {
                int fuvarSzam = fuvarLista.Count(x => x.TaxiID == azonosito);
                double fuvarBevetel = 0;
                foreach (var item in fuvarLista)
                {
                    if (item.TaxiID == azonosito)
                    {
                        fuvarBevetel += item.VitelDij;
                        fuvarBevetel += item.Borravalo;
                    }
                }

                MessageBox.Show($"4. feladat: {fuvarSzam} fuvar alatt: {fuvarBevetel}$");

            }
            else
            {
                MessageBox.Show("Nincs ilyen számú taxi!");
            }

            
        }

        private void OtodikFeladat()
        {
            int modBankkartyaCount = fuvarLista.Count(x => x.FozetesModja == "bankkártya");
            int modKeszpenzCount = fuvarLista.Count(x => x.FozetesModja == "készpénz");
            int modVitatottCount = fuvarLista.Count(x => x.FozetesModja == "vitatott");
            int modIngyenesCount = fuvarLista.Count(x => x.FozetesModja == "ingyenes");
            int modIsmeretlenCount = fuvarLista.Count(x => x.FozetesModja == "ismeretlen");

            lbOtodikFeladat.Items.Add($"Bankkártya: {modBankkartyaCount} fuvar");
            lbOtodikFeladat.Items.Add($"Készpénz: {modKeszpenzCount} fuvar");
            lbOtodikFeladat.Items.Add($"Vitatott: {modVitatottCount} fuvar");
            lbOtodikFeladat.Items.Add($"Ingyenes: {modIngyenesCount} fuvar");
            lbOtodikFeladat.Items.Add($"Ismeretlen: {modIsmeretlenCount} fuvar");
        }

        private void btnHatodikFeladat_Click(object sender, RoutedEventArgs e)
        {

            double osszesTavolsag = 0;

            foreach (var item in fuvarLista)
            {
                osszesTavolsag += item.Tavolsag;
            }

            MessageBox.Show($"{(osszesTavolsag*1.6):f2} km");

        }

        private void HetedikFeladat()
        {
            int leghosszabbIdo = 0;
            int leghosszabbIdotartamAzonosito = 0;
            double leghosszabbMegtettTav = 0;
            double leghosszabbViteldij = 0;


            foreach (var item in fuvarLista)
            {
                if (item.Idotartam > leghosszabbIdo)
                {
                    leghosszabbIdo = item.Idotartam;
                    leghosszabbIdotartamAzonosito = item.TaxiID;
                    leghosszabbMegtettTav = item.Tavolsag * 1.6;
                    leghosszabbViteldij = item.VitelDij + item.Borravalo;
                }
            }

            lbHetedikFeladat.Items.Add($"Fuvar hossza: {leghosszabbIdo} másodperc");
            lbHetedikFeladat.Items.Add($"Taxi azonosító: {leghosszabbIdotartamAzonosito}");
            lbHetedikFeladat.Items.Add($"Megtett távolság: {leghosszabbMegtettTav:f2} km");
            lbHetedikFeladat.Items.Add($"Viteldíj: {leghosszabbViteldij} $");
            

        }

        private void btnNyolcadikFeladat_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("hibak.txt", string.Empty);

            string irottSor = "taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja" + Environment.NewLine;
            File.WriteAllText("hibak.txt", irottSor);
            foreach (var item in fuvarLista)
            {
                if (item.Idotartam > 0 && item.VitelDij > 0 && item.Tavolsag == 0)
                {
                    irottSor = $"{item.TaxiID};{item.Indulas};{item.Idotartam};{item.Tavolsag};{item.VitelDij};{item.Borravalo};{item.FozetesModja}";
                    File.AppendAllText("hibak.txt", irottSor + Environment.NewLine);
                }
            }
            
        }
    }
}

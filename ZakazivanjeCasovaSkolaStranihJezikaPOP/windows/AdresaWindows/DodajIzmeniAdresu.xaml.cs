using System;
using System.Collections.Generic;
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
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.AdresaWindows
{
    /// <summary>
    /// Interaction logic for DodajIzmeniAdresu.xaml
    /// </summary>
    public partial class DodajIzmeniAdresu : Window
    {

        private Adresa _adresa;
        public DodajIzmeniAdresu(Adresa address)
        {
            InitializeComponent();
            _adresa = address;

            if (_adresa != null)
            {
                this.Title = "Izmeni adresu";
                TxtStreet.Text = _adresa.Ulica;
                TxtStreetNumber.Text = _adresa.Broj.ToString();
                TxtCity.Text = _adresa.Grad;
                TxtCountry.Text = _adresa.Drzava;
            }
            else
                this.Title = "Dodaj adresu";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_adresa == null)
            {
                Util.Instance.Adrese.Add(new Adresa
                {
                    ID = (Util.Instance.Adrese.Count() + 1).ToString(),
                    Ulica = TxtStreet.Text,
                    Broj = int.Parse(TxtStreetNumber.Text),
                    Grad = TxtCity.Text,
                    Drzava = TxtCountry.Text,
                    Aktivan = true
                });
            }
            else
            {
                Adresa oldAddress = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(_adresa.ID));
                oldAddress.Ulica = TxtStreet.Text;
                oldAddress.Broj = int.Parse(TxtStreetNumber.Text);
                oldAddress.Grad = TxtCity.Text;
                oldAddress.Drzava = TxtCountry.Text;
            }



            Util.Instance.sacuvajEnitete();
            this.Close();
        }
    }
}
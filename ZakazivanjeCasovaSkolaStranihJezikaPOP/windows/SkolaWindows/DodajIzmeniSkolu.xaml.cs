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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.SkolaWindows
{/// <summary>
 /// Interaction logic for AddEditSchool.xaml
 /// </summary>
    public partial class DodajIzmeniSkolu : Window
    {
        private Skola _skola;
        public DodajIzmeniSkolu(Skola skola)
        {
            InitializeComponent();
            _skola = skola;
            string initialCB = "";
            List<string> addressesCB = new List<string>();
            foreach (Adresa one in Util.Instance.Adrese)
            {
                addressesCB.Add(one.ID + "-" + one.Ulica + "/" + one.Broj);
                if (_skola != null)
                {
                    if (one.ID == _skola.Adresa.ID)
                    {
                        initialCB = one.ID + "-" + one.Ulica + "/" + one.Broj;
                    }
                }
            }


            CmbAddress.ItemsSource = addressesCB;

            if (_skola != null)
            {
                string languages = "";
                foreach (string language in _skola.Jezici)
                {
                    languages += language + ",";
                }
                this.Title = "Edit School";
                TxtName.Text = _skola.Naziv;
                CmbAddress.Text = initialCB;
                TxtLanguages.Text = languages.Substring(0, languages.Length - 1);
            }
            else
                this.Title = "Add School";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_skola == null)
            {
                Adresa adresaSkole = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));
                string[] jezici = TxtLanguages.Text.Split(',');
                Util.Instance.Skole.Add(new Skola
                {
                    ID = (Util.Instance.Skole.Count + 1).ToString(),
                    Naziv = TxtName.Text,
                    Adresa = adresaSkole,
                    Jezici = jezici.ToList(),
                    Aktivan = true
                });
            }
            else
            {
                Skola oldSchool = Util.Instance.Skole.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(_skola.ID.ToString()));
                oldSchool.Naziv = TxtName.Text;
                oldSchool.Adresa = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));
                oldSchool.Jezici = TxtLanguages.Text.Split(',').ToList();
            }



            Util.Instance.sacuvajEnitete();
            this.Close();
        }
    }
}
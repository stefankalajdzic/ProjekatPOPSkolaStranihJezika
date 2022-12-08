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
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows
{/// <summary>
 /// Interaction logic for AddEditProfessor.xaml
 /// </summary>
    public partial class DodajIzmeniProfesora : Window
    {
        private Profesor _professor;
        public DodajIzmeniProfesora(Profesor professor)
        {
            InitializeComponent();
            _professor = professor;

            string initialAddressesCB = "";
            string initialSchoolsCB = "";
            List<string> addressesCB = new List<string>();
            foreach (Adresa one in Util.Instance.Adrese)
            {
                addressesCB.Add(one.ID + "-" + one.Ulica + "/" + one.Broj);
                if (_professor != null)
                {
                    if (one.ID == _professor.Korisnik.Adresa.ID)
                    {
                        initialAddressesCB = one.ID + "-" + one.Ulica + "/" + one.Broj;
                    }
                }
            }
            CmbAddress.ItemsSource = addressesCB;

            List<string> schoolsCB = new List<string>();
            foreach (Skola one in Util.Instance.Skole)
            {
                schoolsCB.Add(one.ID + "-" + one.Naziv);
                if (_professor != null)
                {
                    if (one.ID == _professor.Skola.ID)
                    {
                        initialSchoolsCB = one.ID + "-" + one.Naziv;
                    }
                }
            }
            CmbSchool.ItemsSource = schoolsCB;

            if (_professor != null)
            {
                string languages = "";
                foreach (string language in _professor.Jezici)
                {
                    languages += language + ",";
                }

                this.Title = "Izmeni Profesora";
                TxtName.Text = _professor.Korisnik.Ime;
                TxtSurname.Text = _professor.Korisnik.Prezime;
                TxtJMBG.Text = _professor.Korisnik.JMBG;
                TxtSex.Text = _professor.Korisnik.Pol.ToString();
                CmbSchool.Text = initialSchoolsCB;
                CmbAddress.Text = initialAddressesCB;
                TxtEmail.Text = _professor.Korisnik.Email;
                TxtPassword.Text = _professor.Korisnik.Lozinka;
                TxtLanguages.Text = languages.Substring(0, languages.Length - 1);
            }
            else
            {

                this.Title = "Dodaj Profesora";
            }

        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_professor == null)
            {
                Adresa addressOfProfessor = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));
                Skola schoolOfProfessor = Util.Instance.Skole.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbSchool.SelectedItem.ToString().Split('-')[0]));

                RegistrovaniKorisnik user = new RegistrovaniKorisnik
                {
                    ID = (Util.Instance.Korisnici.Count + 1).ToString(),
                    Ime = TxtName.Text,
                    Prezime = TxtSurname.Text,
                    JMBG = TxtJMBG.Text,
                    Pol = (EPol)Enum.Parse(typeof(EPol), TxtSex.Text),
                    Adresa = addressOfProfessor,
                    Email = TxtEmail.Text,
                    Lozinka = TxtPassword.Text,
                    TipKorisnika = ETipKorisnika.STUDENT,
                    Aktivan = true
                };
                Util.Instance.Korisnici.Add(user);

                string[] langugaes = TxtLanguages.Text.Split(',');

                Util.Instance.Profesori.Add(new Profesor
                {
                    ID = (Util.Instance.Studenti.Count + 1).ToString(),
                    Korisnik = user,
                    Skola = schoolOfProfessor,
                    Jezici = langugaes.ToList(),
                    Casovi = new List<Cas>(),
                    Aktivan = true
                });
            }
            else
            {
                Profesor oldProfessor = Util.Instance.Profesori.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(_professor.ID.ToString()));
                oldProfessor.Korisnik.Ime = TxtName.Text;
                oldProfessor.Korisnik.Prezime = TxtSurname.Text;
                oldProfessor.Korisnik.JMBG = TxtJMBG.Text;
                oldProfessor.Korisnik.Email = TxtEmail.Text;
                oldProfessor.Korisnik.Lozinka = TxtPassword.Text;
                oldProfessor.Korisnik.Adresa = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));
                oldProfessor.Skola = Util.Instance.Skole.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbSchool.SelectedItem.ToString().Split('-')[0]));
                oldProfessor.Jezici = TxtLanguages.Text.Split(',').ToList();
            }



            Util.Instance.sacuvajEnitete();
            this.Close();
        }


    }
}

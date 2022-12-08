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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.StudentWindows
{/// <summary>
    /// Interaction logic for AddEditStudent.xaml
    /// </summary>
    public partial class DodajIzmeniStudenta : Window
    {
        private Student _student;
        public DodajIzmeniStudenta(Student student)
        {
            InitializeComponent();
            _student = student;

            string initialCB = "";
            List<string> adreseCB = new List<string>();
            foreach (Adresa one in Util.Instance.Adrese)
            {
                adreseCB.Add(one.ID + "-" + one.Ulica + "/" + one.Broj);
                if (_student != null)
                {
                    if (one.ID == _student.Korisnik.Adresa.ID)
                    {
                        initialCB = one.ID + "-" + one.Ulica + "/" + one.Broj;
                    }
                }
            }


            CmbAddress.ItemsSource = adreseCB;

            if (_student != null)
            {

                this.Title = "Izmeni Studenta";
                TxtName.Text = _student.Korisnik.Ime;
                TxtSurname.Text = _student.Korisnik.Prezime;
                TxtJMBG.Text = _student.Korisnik.JMBG;
                TxtSex.Text = _student.Korisnik.Pol.ToString();
                CmbAddress.Text = initialCB;
                TxtEmail.Text = _student.Korisnik.Email;
                TxtPassword.Text = _student.Korisnik.Lozinka;
            }
            else
                this.Title = "Dodaj Studenta";
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_student == null)
            {
                Adresa adresaStudenta = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));

                RegistrovaniKorisnik korisnik = new RegistrovaniKorisnik
                {
                    ID = (Util.Instance.Korisnici.Count + 1).ToString(),
                    Ime = TxtName.Text,
                    Prezime = TxtSurname.Text,
                    JMBG = TxtJMBG.Text,
                    Pol = (EPol)Enum.Parse(typeof(EPol), TxtSex.Text),
                    Adresa = adresaStudenta,
                    Email = TxtEmail.Text,
                    Lozinka = TxtPassword.Text,
                    TipKorisnika = ETipKorisnika.STUDENT,
                    Aktivan = true
                };
                Util.Instance.Korisnici.Add(korisnik);

                Util.Instance.Studenti.Add(new Student
                {
                    ID = (Util.Instance.Studenti.Count + 1).ToString(),
                    Korisnik = korisnik,
                    Casovi = new List<Cas>(),
                    Aktivan = true
                });
            }
            else
            {
                Student stariStudent = Util.Instance.Studenti.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(_student.ID.ToString()));
                stariStudent.Korisnik.Ime = TxtName.Text;
                stariStudent.Korisnik.Prezime = TxtSurname.Text;
                stariStudent.Korisnik.JMBG = TxtJMBG.Text;
                stariStudent.Korisnik.Email = TxtEmail.Text;
                stariStudent.Korisnik.Lozinka = TxtPassword.Text;
                stariStudent.Korisnik.Adresa = Util.Instance.Adrese.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbAddress.SelectedItem.ToString().Split('-')[0]));
            }



            Util.Instance.sacuvajEnitete();
            this.Close();
        }

    }
}
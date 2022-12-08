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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.CasWindows
{
    public partial class DodajIzmeniCas : Window
    {
        private Cas _cas;

        public DodajIzmeniCas(Cas cas)
        {
            InitializeComponent();
            _cas = cas;

            string initialProfessorsCB = "";
            string initialStudentsCB = "";
            List<string> studentsCB = new List<string>();
            foreach (Student one in Util.Instance.Studenti)
            {
                studentsCB.Add(one.ID + "-" + one.Korisnik.Email);
                if (_cas != null)
                {
                    if (one.ID == _cas.Student.ID)
                    {
                        initialStudentsCB = one.ID + "-" + one.Korisnik.Email;
                    }
                }
            }
            CmbStudent.ItemsSource = studentsCB;

            List<string> professorsCB = new List<string>();
            foreach (Profesor one in Util.Instance.Profesori)
            {
                professorsCB.Add(one.ID + "-" + one.Korisnik.Ime + " " + one.Korisnik.Prezime);
                if (_cas != null)
                {
                    if (one.ID == _cas.Profesor.ID)
                    {
                        initialProfessorsCB = one.ID + "-" + one.Korisnik.Ime + " " + one.Korisnik.Prezime;
                    }
                }
            }
            CmbProfessor.ItemsSource = professorsCB;

            if (_cas != null)
            {


                this.Title = "Izmeni Profesora";
                CmbProfessor.Text = initialProfessorsCB;
                TxtDate.Text = _cas.Datum;
                TxtStartTime.Text = _cas.VremePocetka;
                TxtDuration.Text = _cas.Trajanje;
                TxtStatus.Text = _cas.Status.ToString();
                CmbStudent.Text = initialStudentsCB;

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

            if (_cas == null)
            {
                Profesor profesor = Util.Instance.Profesori.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbProfessor.SelectedItem.ToString().Split('-')[0]));
                Student student = Util.Instance.Studenti.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbStudent.SelectedItem.ToString().Split('-')[0]));

                Util.Instance.Casovi.Add(new Cas
                {
                    ID = (Util.Instance.Casovi.Count + 1).ToString(),
                    Profesor = profesor,
                    Datum = TxtDate.Text,
                    VremePocetka = TxtStartTime.Text,
                    Trajanje = TxtDuration.Text,
                    Status = (EStatusLekcije)Enum.Parse(typeof(EStatusLekcije), "SLOBODAN"),
                    Student = student,
                    Aktivan = true
                });
            }
            else
            {
                Cas oldLesson = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(_cas.ID.ToString()));
                oldLesson.Profesor = Util.Instance.Profesori.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbProfessor.SelectedItem.ToString().Split('-')[0]));
                oldLesson.Datum = TxtDate.Text;
                oldLesson.VremePocetka = TxtStartTime.Text;
                oldLesson.Trajanje = TxtDuration.Text;
                oldLesson.Status = (EStatusLekcije)Enum.Parse(typeof(EStatusLekcije), TxtStatus.Text);
                oldLesson.Student = Util.Instance.Studenti.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(CmbStudent.SelectedItem.ToString().Split('-')[0]));
            }



            Util.Instance.sacuvajEnitete();
            this.Close();
        }
    }
}

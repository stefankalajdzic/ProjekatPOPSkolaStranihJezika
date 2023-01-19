using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PViewStudent.xaml
    /// </summary>
    public partial class PViewStudent : Window
    {
        private Student student;

        public PViewStudent(Student _student)
        {
            InitializeComponent();
            this.Title = "View Student";
            TxtName.Text = _student.Korisnik.Ime;
            TxtSurname.Text = _student.Korisnik.Prezime;
            TxtJMBG.Text = _student.Korisnik.JMBG;
            TxtSex.Text = _student.Korisnik.Pol.ToString();
            TxtAddress.Text = _student.Korisnik.Adresa.Grad + ": " + _student.Korisnik.Adresa.Ulica;
            TxtEmail.Text = _student.Korisnik.Email;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

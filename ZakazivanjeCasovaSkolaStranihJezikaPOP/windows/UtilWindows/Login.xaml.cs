using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UtilWindows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            Util.Instance.UlogovanKorisnik = Util.Instance.Login(TxtName.Text, TxtPassword.Password.ToString());

            if (Util.Instance.UlogovanKorisnik != null)
            {
                switch (Util.Instance.UlogovanKorisnik.TipKorisnika)
                {
                    case ETipKorisnika.ADMINISTRATOR:
                        AdminUI adminUI = new AdminUI();
                        this.Hide();
                        adminUI.Show();
                        break;
                    case ETipKorisnika.STUDENT:
                        StudentUI studentUI = new StudentUI();
                        this.Hide();
                        studentUI.Show();
                        break;
                    case ETipKorisnika.PROFESOR:
                        ProfessorUI professorUI = new ProfessorUI();
                        this.Hide();
                        professorUI.Show();
                        break;
                    default:
                        MessageBox.Show("Nesto je poslo po zlu sa tipom korisnika");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Podaci prilikom prijavljivanja se ne poklapaju");
            }

        }
    }
}

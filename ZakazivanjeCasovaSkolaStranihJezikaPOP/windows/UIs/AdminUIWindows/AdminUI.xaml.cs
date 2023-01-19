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
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.AdresaWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.CasWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.SkolaWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.StudentWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.AdminUIWindows;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs
{
    /// <summary>
    /// Interaction logic for AdminUI.xaml
    /// </summary>
    public partial class AdminUI : Window
    {
        public AdminUI()
        {
            InitializeComponent();
        }

        private void AddressesButton_Click(object sender, RoutedEventArgs e)
        {
            AdresaDisplay addressDisplay = new AdresaDisplay();
            addressDisplay.Show();
        }

        private void SchoolsButton_Click(object sender, RoutedEventArgs e)
        {
            SkolaDisplay schoolDisplay = new SkolaDisplay();
            schoolDisplay.Show();
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            StudentDisplay studentDisplay = new StudentDisplay();
            studentDisplay.Show();
        }

        private void ProfessorsButton_Click(object sender, RoutedEventArgs e)
        {
            ProfesorDisplay professorDisplay= new ProfesorDisplay();
            professorDisplay.Show();
        }

        private void LessonsButton_Click(object sender, RoutedEventArgs e)
        {
            CasDisplay lessonDisplay = new CasDisplay();
            lessonDisplay.Show();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            AAllUsers aAllUsers= new AAllUsers();
            aAllUsers.Show();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.AdresaWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.CasWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.SkolaWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.StudentWindows;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Util.Instance.Initialize();
        }

        private void AddressesButton_Click(object sender, RoutedEventArgs e)
        {
            AdresaDisplay addressesDisplayWindow = new AdresaDisplay();
            addressesDisplayWindow.Show();
        }

        private void SchoolsButton_Click(Object sender, RoutedEventArgs e)
        {
            SkolaDisplay schoolsDisplayWindow = new SkolaDisplay();
            schoolsDisplayWindow.Show();
        }

        private void StudentsButton_Click(Object sender, RoutedEventArgs e)
        {
            StudentDisplay studentsDisplayWindow = new StudentDisplay();
            studentsDisplayWindow.Show();
        }
        private void ProfessorsButton_Click(Object sender, RoutedEventArgs e)
        {
            ProfesorDisplay professorsDisplayWindow = new ProfesorDisplay();
            professorsDisplayWindow.Show();
        }
        private void LessonsButton_Click(Object sender, RoutedEventArgs e)
        {
            CasDisplay lessonDisplayWindow = new CasDisplay();
            lessonDisplayWindow.Show();
        }
    }
}

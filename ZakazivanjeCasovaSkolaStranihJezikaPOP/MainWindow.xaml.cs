﻿using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.NotRegisteredUserWindows;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UtilWindows;
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

        private void SchoolsButton_Click(Object sender, RoutedEventArgs e)
        {
            NRUSchoolsWindow nruSchoolsDisplayWindow = new NRUSchoolsWindow();
            nruSchoolsDisplayWindow.Show();
        }
        private void ProfessorsButton_Click(Object sender, RoutedEventArgs e)
        {
            NRUProfessorsWindow nruProfessorsDisplayWindow = new NRUProfessorsWindow();
            nruProfessorsDisplayWindow.Show();
        }
        private void RegistrationButton_Click(Object sender, RoutedEventArgs e)
        {
            DodajIzmeniStudenta studentsDisplayWindow = new DodajIzmeniStudenta(null);
            studentsDisplayWindow.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
        }

    }
}

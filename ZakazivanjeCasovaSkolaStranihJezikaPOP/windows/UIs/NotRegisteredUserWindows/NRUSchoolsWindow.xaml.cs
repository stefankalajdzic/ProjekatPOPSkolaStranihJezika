using Microsoft.VisualBasic;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.SkolaWindows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.NotRegisteredUserWindows
{
    /// <summary>
    /// Interaction logic for NRUSchoolsWindow.xaml
    /// </summary>
    public partial class NRUSchoolsWindow : Window
    {
        ICollectionView view;
        string city = "";
        string language = "";
        string schoolName = "";
        string schoolAddress = "";

        ObservableCollection<Skola> activeEntities = new ObservableCollection<Skola>();
        public NRUSchoolsWindow()
        {

            foreach (Skola skola in Util.Instance.Skole)
            {
                if (skola.Aktivan == true)
                {
                    activeEntities.Add(skola);
                }
            }

            InitializeComponent();
            UpdateView();
            initializeCBs();

        }

        private void initializeCBs()
        {
            List<string> citiesCB = new List<string>();
            List<string> languagesCB = new List<string>();

            citiesCB.Add("Izaberi");
            languagesCB.Add("Izaberi");

            foreach (Skola one in Util.Instance.Skole)
            {
                citiesCB.Add(one.Adresa.Grad);
                foreach (string jezik in one.Jezici)
                {
                    languagesCB.Add(jezik);
                }
            }

            CmbCity.ItemsSource = citiesCB.Distinct().ToList();
            CmbLanguage.ItemsSource = languagesCB.Distinct().ToList();

          CmbCity.SelectedIndex = 0;
          CmbLanguage.SelectedIndex = 0;
        }

        private void UpdateView()
        {

            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.Naziv,
                address = x.Adresa.Ulica,
                language = string.Join(",", x.Jezici.ToArray())
            }).ToList();


            DGSchools.ItemsSource = itemSource;

            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGSchools.IsSynchronizedWithCurrentItem = true;
            DGSchools.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void criteriaChanged()
        {
            activeEntities.Clear();
            ObservableCollection<Skola> prva = new ObservableCollection<Skola>();
            ObservableCollection<Skola> druga = new ObservableCollection<Skola>();
            ObservableCollection<Skola> treca = new ObservableCollection<Skola>();
            ObservableCollection<Skola> cetvrta = new ObservableCollection<Skola>();
            foreach (Skola skola in Util.Instance.Skole)
            {
                if (!city.Equals("Izaberi") && skola.Adresa.Grad.Equals(city))
                {
                    prva.Add(skola);
                }
                else if(city.Equals("Izaberi"))
                {
                    prva.Add(skola);
                    Console.WriteLine(skola.Naziv + " --- Dodata preko Izaberi city");
                }

                if (!language.Equals("Izaberi") && skola.Jezici.Contains(language))
                {
                    druga.Add(skola);
                }
                else if (language.Equals("Izaberi"))
                {
                    druga.Add(skola);
                    Console.WriteLine(skola.Naziv + " --- Dodata preko Izaberi language");
                }

                if (!schoolName.Equals("") && skola.Naziv.StartsWith(schoolName))
                {
                    treca.Add(skola);
                }
                else if (schoolName.Equals(""))
                {
                    treca.Add(skola);
                    Console.WriteLine(skola.Naziv + " --- Dodata preko praznog skolskog naziva");
                }


                if (!schoolAddress.Equals("") && skola.Adresa.Ulica.StartsWith(schoolAddress))
                {
                    cetvrta.Add(skola);
                }
                else if (schoolAddress.Equals(""))
                {
                    cetvrta.Add(skola);
                    Console.WriteLine(skola.Adresa.Ulica + " --- Dodata preko prazne skolske adrese");
                }
            }
            var commonItems = prva.Intersect(druga).Intersect(treca).Intersect(cetvrta).ToList();
            foreach(Skola sch in commonItems)
            {
                if (!activeEntities.Contains(sch))
                {
                activeEntities.Add(sch);

                }
            }
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            city = CmbCity.SelectedItem.ToString();
            criteriaChanged();
            UpdateView();
        }

        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            language = CmbLanguage.SelectedItem.ToString();
            criteriaChanged();
            UpdateView();
        }

        private void txtSchoolName_TextChanged(object sender, TextChangedEventArgs e)
        {
            schoolName = txtSchoolName.Text;
            criteriaChanged();
            UpdateView();
        }

        private void txtSchoolAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            schoolAddress = txtSchoolAddress.Text;
            criteriaChanged();
            UpdateView();
        }
    }
}

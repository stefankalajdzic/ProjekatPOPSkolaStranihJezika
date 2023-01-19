using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
    /// Interaction logic for NRUProfessorsWindow.xaml
    /// </summary>
    public partial class NRUProfessorsWindow : Window
    {
        ICollectionView view;
        int schoolId = 0;

        public NRUProfessorsWindow()
        {
            InitializeComponent();
            UpdateView();
            List<string> schoolsCB = new List<string>();
            schoolsCB.Add("0-Izaberi");
            foreach (Skola one in Util.Instance.Skole)
            {
                schoolsCB.Add(one.ID + '-' + one.Naziv);

            }
            CmbSchool.ItemsSource = schoolsCB;
            CmbSchool.SelectedIndex = 0;
        }

        private void UpdateView()
        {
            ObservableCollection<Profesor> activeEntities = new ObservableCollection<Profesor>();
            foreach (Profesor professor in Util.Instance.Profesori)
            {
                if (professor.Aktivan == true)
                {
                    activeEntities.Add(professor);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.Korisnik.Ime,
                surname = x.Korisnik.Prezime,
                jmbg = x.Korisnik.JMBG,
                sex = x.Korisnik.Pol,
                address = x.Korisnik.Adresa.Ulica,
                email = x.Korisnik.Email,
                school = x.Skola.Naziv,
                languages = string.Join(",", x.Jezici.ToArray())
            }).ToList();
            if(schoolId != 0)
            {
                itemSource = itemSource.Where(x => x.school.Equals(CmbSchool.SelectedItem.ToString().Split('-')[1])).ToList();
            }
            DGProfessors.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGProfessors.IsSynchronizedWithCurrentItem = true;
            DGProfessors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void CmbSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            schoolId = int.Parse(CmbSchool.SelectedItem.ToString().Split('-')[0]);
            UpdateView();
        }
    }
}

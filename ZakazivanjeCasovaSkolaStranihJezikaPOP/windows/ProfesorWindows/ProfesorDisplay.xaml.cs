using System;
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
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows
{
    public partial class ProfesorDisplay : Window
    {
        ICollectionView view;
        Profesor _selected;
        public ProfesorDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Profesor> activeEntities = new ObservableCollection<Profesor>();
            foreach (Profesor profesoir in Util.Instance.Profesori)
            {
                if (profesoir.Aktivan == true)
                {
                    activeEntities.Add(profesoir);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                ime = x.Korisnik.Ime,
                prezime = x.Korisnik.Prezime,
                jmbg = x.Korisnik.JMBG,
                pol = x.Korisnik.Pol,
                adresa = x.Korisnik.Adresa.Ulica,
                email = x.Korisnik.Email,
                skola = x.Skola.Naziv,
                jezici = string.Join(",", x.Jezici.ToArray())
            }).ToList();
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

        private void MIAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            DodajIzmeniProfesora addWindow = new DodajIzmeniProfesora(null);
            addWindow.Show();
        }


        private void MIEditProfessor_Click(object sender, RoutedEventArgs e)
        {
            object item = DGProfessors.SelectedItem;
            PropertyInfo[] props = DGProfessors.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Profesori.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            DodajIzmeniProfesora editWindow = new DodajIzmeniProfesora(_selected);
            editWindow.Show();

        }

        private void MIRemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            object item = DGProfessors.SelectedItem;
            PropertyInfo[] props = DGProfessors.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Profesori.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            Util.Instance.ObrisiEntitet(_selected);

            UpdateView();
            view.Refresh();

        }
    }
}
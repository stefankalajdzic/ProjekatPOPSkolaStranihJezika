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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.StudentWindows
{
    public partial class StudentDisplay : Window
    {
        ICollectionView view;
        Student _selected;
        public StudentDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Student> activeEntities = new ObservableCollection<Student>();
            foreach (Student student in Util.Instance.Studenti)
            {
                if (student.Aktivan == true)
                {
                    activeEntities.Add(student);
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
                email = x.Korisnik.Email
            }).ToList();
            DGStudenti.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGStudenti.IsSynchronizedWithCurrentItem = true;
            DGStudenti.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGStudenti_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
        private void MIDodajStudent_Click(object sender, RoutedEventArgs e)
        {
            DodajIzmeniStudenta addWindow = new DodajIzmeniStudenta(null);
            addWindow.Show();
        }


        private void MIIzmeniStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGStudenti.SelectedItem;
            PropertyInfo[] props = DGStudenti.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Studenti.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            DodajIzmeniStudenta editWindow = new DodajIzmeniStudenta(_selected);
            editWindow.Show();

        }
        private void MIObrisiStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGStudenti.SelectedItem;
            PropertyInfo[] props = DGStudenti.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Studenti.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            Util.Instance.ObrisiEntitet(_selected);

            UpdateView();
            view.Refresh();

        }

    }
}
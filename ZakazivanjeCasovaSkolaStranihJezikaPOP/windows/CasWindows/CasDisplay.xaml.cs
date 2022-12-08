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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.CasWindows
{
    public partial class CasDisplay : Window
    {
        ICollectionView view;
        Cas _selected;
        public CasDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            ObservableCollection<Cas> activeEntities = new ObservableCollection<Cas>();
            foreach (Cas lesson in Util.Instance.Casovi)
            {
                if (lesson.Aktivan == true)
                {
                    activeEntities.Add(lesson);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                profesor = x.Profesor.Korisnik.Email,
                datum = x.Datum,
                vremepocetka = x.VremePocetka,
                trajanje = x.Trajanje,
                status = x.Status,
                student = x.Student.Korisnik.Email
            }).ToList();
            DGLessons.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }


        private void MIAddLesson_Click(object sender, RoutedEventArgs e)
        {
            DodajIzmeniCas addWindow = new DodajIzmeniCas(null);
            addWindow.Show();
        }

        private void MIEditLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            DodajIzmeniCas editWindow = new DodajIzmeniCas(_selected);
            editWindow.Show();
        }
        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string studentID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(studentID));

            Util.Instance.ObrisiEntitet(_selected);

            UpdateView();
            view.Refresh();

        }
    }
}
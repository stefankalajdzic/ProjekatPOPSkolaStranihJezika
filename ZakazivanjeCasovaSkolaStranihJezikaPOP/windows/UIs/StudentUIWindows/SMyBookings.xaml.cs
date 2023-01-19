using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.StudentUIWindows
{
    /// <summary>
    /// Interaction logic for SMyBookings.xaml
    /// </summary>
    public partial class SMyBookings : Window
    {
        ICollectionView view;
        Cas _selected;

        public SMyBookings()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Cas> activeEntities = new ObservableCollection<Cas>();
            foreach (Cas lesson in Util.Instance.Casovi)
            {
                if (lesson.Aktivan == true && lesson.Student != null)
                {
                    if (lesson.Student.ID == Util.Instance.pronadjiStudentaPoKorisnickomId(Util.Instance.UlogovanKorisnik.ID).ID)
                    activeEntities.Add(lesson);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                professor = x.Profesor.Korisnik.Email,
                date = x.Datum,
                starttime = x.VremePocetka,
                duration = x.Trajanje,
                status = x.Status
            }).ToList();
            DGLessons.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
        }


        private void MIUnbookLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(lessonID));

            _selected.Status = EStatusLekcije.SLOBODAN;
            _selected.Student = null;

            Util.Instance.sacuvajEnitete();
            var currentWindow = Window.GetWindow(this);
            currentWindow.Close();
            var newWindow = new SMyBookings();
            newWindow.Show();
        }
    }
}

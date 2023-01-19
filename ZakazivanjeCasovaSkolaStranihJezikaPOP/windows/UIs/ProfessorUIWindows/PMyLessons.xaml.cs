using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.StudentUIWindows;
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
using System.Windows.Markup;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.ProfessorUIWindows;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PMyLessons.xaml
    /// </summary>
    public partial class PMyLessons : Window
    {

        ICollectionView view;
        Cas _selected;
        string selectedDate = "Izaberi";

        public PMyLessons()
        {
            InitializeComponent();
            initializeCBs();
            UpdateView();

        }

        private void initializeCBs()
        {
            List<string> datesCB = new List<string>();

            datesCB.Add("Izaberi");

            foreach (Cas one in Util.Instance.pronadjiProfesoraPoKorisnickomId(Util.Instance.UlogovanKorisnik.ID).Casovi)
            {
                datesCB.Add(one.Datum);
                Console.WriteLine("Cao");
            }

            CBDate.ItemsSource = datesCB.Distinct().ToList();

            CBDate.SelectedIndex = 0;
        }

        private void UpdateView()
        {
            ObservableCollection<Cas> activeEntities = new ObservableCollection<Cas>();
            foreach (Cas cas in Util.Instance.Casovi)
            {
                if (cas.Aktivan == true)
                {
                    if (cas.Profesor.ID == Util.Instance.pronadjiProfesoraPoKorisnickomId(Util.Instance.UlogovanKorisnik.ID).ID)
                        activeEntities.Add(cas);
                }
            }
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                professor = x.Profesor.Korisnik.Email,
                date = x.Datum,
                starttime = x.VremePocetka,
                duration = x.Trajanje,
                status = x.Status,
                student = x.Student?.Korisnik.Email ?? "To je besplatan čas"
            }).ToList();
            if (!selectedDate.Equals("Izaberi"))
            {
                itemSource = itemSource.Where(x => x.date.Equals(selectedDate)).ToList();
            }
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

        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(lessonID));

            if (_selected.Status == EStatusLekcije.SLOBODAN
                )
            {
                Util.Instance.ObrisiEntitet(_selected);

                var currentWindow = Window.GetWindow(this);
                currentWindow.Close();
                var newWindow = new PMyLessons();
                newWindow.Show();
            }
            else
            {
                MessageBox.Show("Lekcije koje nisu besplatne ne mogu biti obrisane", "Nije dozvoljeno obrisati lekciju", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MIViewStudent_Click(object sender, RoutedEventArgs e)
        {
            object item = DGLessons.SelectedItem;
            PropertyInfo[] props = DGLessons.SelectedItem.GetType().GetProperties();
            string lessonID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Casovi.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(lessonID));

            if (_selected.Student != null)
            {
                PViewStudent pViewStudent = new PViewStudent(_selected.Student);
                pViewStudent.Show();
            }
            else
            {
                MessageBox.Show("Choosen lesson has not been taken yet", "Lesson is free", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void MICreateLesson_Click(object sender, RoutedEventArgs e)
        {
            PCreateNewLesson pCreateNewLesson= new PCreateNewLesson();
            pCreateNewLesson.Show();
        }

        private void CBDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = CBDate.SelectedItem.ToString();
            UpdateView();
        }
    }
}

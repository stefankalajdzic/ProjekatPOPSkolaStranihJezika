using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.StudentWindows
{
    /// <summary>
    /// Interaction logic for StudentDisplay.xaml
    /// </summary>
    public partial class StudentDisplay : Window
    {
        ICollectionView view;
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
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGStudents.ItemsSource = view;
            DGStudents.IsSynchronizedWithCurrentItem = true;
            DGStudents.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            Student selected = view.CurrentItem as Student;
            Util.Instance.ObrisiEntitet(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

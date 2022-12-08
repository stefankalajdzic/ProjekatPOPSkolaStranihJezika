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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.CasWindows
{
    /// <summary>
    /// Interaction logic for CasDisplay.xaml
    /// </summary>
    public partial class CasDisplay : Window
    {
        ICollectionView view;
        public CasDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

         private void UpdateView()
        {
            ObservableCollection<Cas> activeEntities = new ObservableCollection<Cas>();
            foreach (Cas cas in Util.Instance.Casovi)
            {
                if (cas.Aktivan == true)
                {
                    activeEntities.Add(cas);
                }
            }
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGLessons.ItemsSource = view;
            DGLessons.IsSynchronizedWithCurrentItem = true;
            DGLessons.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGLessons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveLesson_Click(object sender, RoutedEventArgs e)
        {
            Cas selected = view.CurrentItem as Cas;
            Util.Instance.ObrisiEntitet(selected);

            UpdateView();
            view.Refresh();

        }

    }
}

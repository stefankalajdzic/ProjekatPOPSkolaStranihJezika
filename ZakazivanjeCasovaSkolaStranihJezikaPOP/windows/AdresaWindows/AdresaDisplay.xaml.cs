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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.AdresaWindows
{
    /// <summary>
    /// Interaction logic for AdresaDisplay.xaml
    /// </summary>
    public partial class AdresaDisplay : Window
    {
        ICollectionView view;

        public AdresaDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            ObservableCollection<Adresa> activeEntities = new ObservableCollection<Adresa>();
            foreach (Adresa Adresa in Util.Instance.Adrese)
            {
                if (Adresa.Aktivan == true)
                {
                    activeEntities.Add(Adresa);
                }
            }
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGAdrese.ItemsSource = view;
            DGAdrese.IsSynchronizedWithCurrentItem = true;
            DGAdrese.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGAdrese_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }


        private void MIDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            DodajIzmeniAdresu addWindow = new DodajIzmeniAdresu(null);
            addWindow.Show();
        }


        private void MIIzmeniAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa selected = view.CurrentItem as Adresa;

            if (selected != null)
            {
                DodajIzmeniAdresu editWindow = new DodajIzmeniAdresu(selected);
                editWindow.Show();
            }
        }


        private void MIObrisiAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa selected = view.CurrentItem as Adresa;
            Util.Instance.ObrisiEntitet(selected);

            UpdateView();
            view.Refresh();

        }
    }
}
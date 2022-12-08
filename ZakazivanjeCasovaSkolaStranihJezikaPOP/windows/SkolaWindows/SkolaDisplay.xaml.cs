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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.SkolaWindows
{
    public partial class SkolaDisplay : Window
    {
        ICollectionView view;
        Skola _selected;
        public SkolaDisplay()
        {
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            ObservableCollection<Skola> activeEntities = new ObservableCollection<Skola>();
            foreach (Skola skola in Util.Instance.Skole)
            {
                if (skola.Aktivan == true)
                {
                    activeEntities.Add(skola);
                }
            }

            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                naziv = x.Naziv,
                adresa = x.Adresa.Ulica,
                jezici = string.Join(",", x.Jezici.ToArray())
            }).ToList();
            DGSkola.ItemsSource = itemSource;

            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGSkola.IsSynchronizedWithCurrentItem = true;
            DGSkola.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGSkola_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }


        private void MIDodajSkolu_Click(object sender, RoutedEventArgs e)
        {
            DodajIzmeniSkolu addWindow = new DodajIzmeniSkolu(null);
            addWindow.Show();
        }


        private void MIIzmeniSkolu_Click(object sender, RoutedEventArgs e)
        {
            object item = DGSkola.SelectedItem;
            PropertyInfo[] props = DGSkola.SelectedItem.GetType().GetProperties();
            string schoolID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Skole.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(schoolID));

            DodajIzmeniSkolu editWindow = new DodajIzmeniSkolu(_selected);
            editWindow.Show();

        }


        private void MIObrisiSkolu_Click(object sender, RoutedEventArgs e)
        {
            object item = DGSkola.SelectedItem;
            PropertyInfo[] props = DGSkola.SelectedItem.GetType().GetProperties();
            string schoolID = props[0].GetValue(item, null).ToString();
            _selected = Util.Instance.Skole.FirstOrDefault(c => int.Parse(c.ID) == int.Parse(schoolID));
            Util.Instance.ObrisiEntitet(_selected);

            UpdateView();
            view.Refresh();

        }

    }
}
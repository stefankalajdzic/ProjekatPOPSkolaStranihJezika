﻿using System;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.ProfesorWindows
{
    /// <summary>
    /// Interaction logic for ProfesorDisplay.xaml
    /// </summary>
    public partial class ProfesorDisplay : Window
    {
        ICollectionView view;
        public ProfesorDisplay()
        {
            InitializeComponent();
            UpdateView();
        }
        private void UpdateView()
        {
            ObservableCollection<Profesor> activeEntities = new ObservableCollection<Profesor>();
            foreach (Profesor profesor in Util.Instance.Profesori)
            {
                if (profesor.Aktivan == true)
                {
                    activeEntities.Add(profesor);
                }
            }
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGProfessors.ItemsSource = view;
            DGProfessors.IsSynchronizedWithCurrentItem = true;
            DGProfessors.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DGProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MIRemoveProfessor_Click(object sender, RoutedEventArgs e)
        {
            Profesor selected = view.CurrentItem as Profesor;
            Util.Instance.ObrisiEntitet(selected);

            UpdateView();
            view.Refresh();

        }
    }
}

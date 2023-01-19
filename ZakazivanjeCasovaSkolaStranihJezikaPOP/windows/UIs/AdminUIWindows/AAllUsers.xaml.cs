using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.AdminUIWindows
{
    /// <summary>
    /// Interaction logic for AAllUsers.xaml
    /// </summary>
    public partial class AAllUsers : Window
    {
        ICollectionView view;
        string name = "";
        string surname = "";
        string email = "";
        string userType = "";
        ObservableCollection<RegistrovaniKorisnik> activeEntities = new ObservableCollection<RegistrovaniKorisnik>();
        public AAllUsers()
        {
            foreach (RegistrovaniKorisnik korisnik in Util.Instance.Korisnici)
            {
                if (korisnik.Aktivan == true)
                {
                    activeEntities.Add(korisnik);
                }
            }
            InitializeComponent();
            UpdateView();

            List<string> userTypesCB = new List<string>();
            userTypesCB.Add("Izaberi");
            userTypesCB.Add("ADMINISTRATOR");
            userTypesCB.Add("PROFESOR");
            userTypesCB.Add("STUDENT");
            cmbUserType.ItemsSource = userTypesCB;
            cmbUserType.SelectedIndex = 0;
        }

        private void UpdateView()
        {
            var itemSource = activeEntities.Select(x => new
            {
                id = x.ID,
                name = x.Ime,
                surname = x.Prezime,
                jmbg = x.JMBG,
                sex = x.Pol,
                address = x.Adresa.Ulica,
                email = x.Email,
                type = x.TipKorisnika.ToString()
            }).ToList();
            DGAllUsers.ItemsSource = itemSource;
            view = CollectionViewSource.GetDefaultView(activeEntities);
            DGAllUsers.IsSynchronizedWithCurrentItem = true;
            DGAllUsers.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void criteriaChanged()
        {
            activeEntities.Clear();
            ObservableCollection<RegistrovaniKorisnik> prva = new ObservableCollection<RegistrovaniKorisnik>();
            ObservableCollection<RegistrovaniKorisnik> druga = new ObservableCollection<RegistrovaniKorisnik>();
            ObservableCollection<RegistrovaniKorisnik> treca = new ObservableCollection<RegistrovaniKorisnik>();
            ObservableCollection<RegistrovaniKorisnik> cetvrta = new ObservableCollection<RegistrovaniKorisnik>();
            foreach (RegistrovaniKorisnik user in Util.Instance.Korisnici)
            {
                if (!userType.Equals("Izaberi") && user.TipKorisnika == (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), userType))
                {
                    prva.Add(user);
                }
                else if (userType.Equals("Izaberi"))
                {
                    prva.Add(user);
                }

                if (!email.Equals("") && user.Email.StartsWith(email))
                {
                    druga.Add(user);
                }
                else if (email.Equals(""))
                {
                    druga.Add(user);
                }


                if (!surname.Equals("") && user.Prezime.StartsWith(surname))
                {
                    treca.Add(user);
                }
                else if (surname.Equals(""))
                {
                    treca.Add(user);
                }


                if (!name.Equals("") && user.Ime.StartsWith(name))
                {
                    cetvrta.Add(user);
                }
                else if (name.Equals(""))
                {
                    cetvrta.Add(user);
                }
            }
            var commonItems = prva.Intersect(druga).Intersect(treca).Intersect(cetvrta).ToList();
            foreach (RegistrovaniKorisnik hcs in commonItems)
            {
                if (!activeEntities.Contains(hcs))
                {
                    activeEntities.Add(hcs);

                }
            }
        }


        private void DGAllUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = txtName.Text;
            criteriaChanged();
            UpdateView();
        }


        private void txtSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname = txtSurname.Text;
            criteriaChanged();
            UpdateView();
        }


        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = txtEmail.Text;
            criteriaChanged();
            UpdateView();
        }

        private void cmbUserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userType = cmbUserType.SelectedItem.ToString();
            criteriaChanged();
            UpdateView();
        }
    }
}

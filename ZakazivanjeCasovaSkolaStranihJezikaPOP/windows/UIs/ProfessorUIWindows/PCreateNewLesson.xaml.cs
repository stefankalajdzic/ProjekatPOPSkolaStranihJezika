using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using System;
using System.Collections.Generic;
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

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.windows.UIs.ProfessorUIWindows
{
    /// <summary>
    /// Interaction logic for PCreateNewLesson.xaml
    /// </summary>
    public partial class PCreateNewLesson : Window
    {
        public PCreateNewLesson()
        {
            InitializeComponent();
            datePicker.DisplayDateStart = DateTime.Now;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Profesor professor = Util.Instance.pronadjiProfesoraPoKorisnickomId(Util.Instance.UlogovanKorisnik.ID);
            Student student = null;

            Util.Instance.Casovi.Add(new Cas
            {
                ID = (Util.Instance.Casovi.Count + 2).ToString(),
                Profesor = professor,
                Datum = datePicker.Text,
                VremePocetka = TxtStartTime.Text,
                Trajanje = TxtDuration.Text,
                Status = (EStatusLekcije)Enum.Parse(typeof(EStatusLekcije), "SLOBODAN"),
                Student = student,
                Aktivan = true
            });

            Util.Instance.sacuvajEnitete();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

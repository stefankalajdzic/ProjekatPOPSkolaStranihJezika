using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    class Skola
    {

        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _naziv;

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private List<string> _jezici;

        public List<string> Jezici
        {
            get { return _jezici; }
            set { _jezici = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public string formatirajTxtFajlLiniju()
        {
            string jezici = "";
            foreach (string jezik in Jezici)
            {
                jezici += jezik + ",";
            }
            return ID.ToString() + ";" + Naziv + ";" + Adresa.ID + ";" + jezici.Substring(0, jezici.Length - 1) + ";" + Aktivan.ToString();
        }

    }
}

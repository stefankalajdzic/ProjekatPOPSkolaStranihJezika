using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    class Adresa
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _ulica;

        public string Ulica
        {
            get { return _ulica; }
            set { _ulica = value; }
        }

        private int _broj;

        public int Broj
        {
            get { return _broj; }
            set { _broj = value; }
        }

        private string _grad;

        public string Grad
        {
            get { return _grad; }
            set { _grad = value; }
        }

        private string _drzava;

        public string Drzava
        {
            get { return _drzava; }
            set { _drzava = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }
        
        public string formatirajTxtFajlLiniju()
        {
            return ID + ";" + Ulica + ";" + Broj.ToString() + ";" + Grad + ";" + Drzava + ";" + Aktivan.ToString();
        }

    }
}

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    class Student
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private RegistrovaniKorisnik _korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }

        private List<Cas> _casovi;

        public List<Cas> Casovi
        {
            get { return _casovi; }
            set { _casovi = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public string formatirajTxtFajlLiniju()
        {
            return ID + ";" + Korisnik.ID + ";" + Aktivan.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    public class Profesor
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

        private Skola _skola;

        public Skola Skola
        {
            get { return _skola; }
            set { _skola = value; }
        }

        private List<string> _jezici;

        public List<string> Jezici
        {
            get { return _jezici; }
            set { _jezici = value; }
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
            string jezici = "";
            foreach (string jezik in Jezici)
            {
                jezici += jezik + ",";
            }
            return ID + ";" + Korisnik.ID + ";" + Skola.ID + ";" + jezici.Substring(0, jezici.Length - 1) + ";" + Aktivan.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    class RegistrovaniKorisnik
    {

        private string _ime;

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        private string _prezime;

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        private string _jmbg;

        public string JMBG
        {
            get { return _jmbg; }
            set { _jmbg = value; }
        }

        private EPol _pol;

        public EPol Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _lozinka;

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        private ETipKorisnika _tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }

        private bool _aktivan;

        public bool Aktivan 
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public RegistrovaniKorisnik() { }

        public override string ToString()
        {
            return "Ja sam " + Ime + ". Prezivam se " + Prezime + "." + " Moj pol je: " + Pol + ". Ja sam tip korisnika: " + TipKorisnika + " moj email je:" + Email; // + " Moja adresa: " + Adresa.ToString(); 
        }

        public string KorisnikZaUpisUFajl()
        {
            return Ime + "; " + Prezime + ";" + Email + ";" + Lozinka + ";" + JMBG + ";" + Pol + ";" + TipKorisnika + ";" + Aktivan;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.services;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    class Util
    {
        private static readonly Util instance = new Util();

        private AdresaServis _adresaServis;

        private SkolaServis _skolaServis;

        private KorisnikServis _korisnikServis;

        private StudentServis _studentServis;

        private ProfesorServis _profesorServis;

        private CasServis _casServis;


        private Util()
        {
            _adresaServis = new AdresaServis();
            _skolaServis = new SkolaServis();
            _korisnikServis = new KorisnikServis();
            _studentServis = new StudentServis();
            _profesorServis = new ProfesorServis();
            _casServis = new CasServis();
        }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<Adresa> Adrese { get; set; }
        public ObservableCollection<Skola> Skole { get; set; }
        public ObservableCollection<RegistrovaniKorisnik> Korisnici { get; set; }
        public ObservableCollection<Student> Studenti { get; set; }
        public ObservableCollection<Profesor> Profesori { get; set; }
        public ObservableCollection<Cas> Casovi { get; set; }


        public void Initialize()
        {
            _adresaServis.CitajAdrese();
            _skolaServis.CitajSkole();
            _korisnikServis.CitajKorisnike();
            _studentServis.CitajStudente();
            _profesorServis.CitajProfesore();
            _casServis.CitajCasove();
        }

        public void sacuvajEnitete()
        {
            _adresaServis.SacuvajAdrese();
            _skolaServis.SacuvajSkole();
            _korisnikServis.SacuvajKorisnike();
            _studentServis.SacuvajStudente();
            _profesorServis.SacuvajProfesore();
            _casServis.SacuvajCasove(); 
        }

        public void ObrisiEntitet(object obj)
        {
            if (obj is Adresa)
            {
                Adresa a = (Adresa)obj;
                var pronadjen = Adrese.FirstOrDefault(c => c.ID == a.ID);
                pronadjen.Aktivan = false;

            }
            else if (obj is Skola)
            {
                Skola a = (Skola)obj;
                var pronadjen = Skole.FirstOrDefault(c => c.ID == a.ID);
                pronadjen.Aktivan = false;
            }
            else if (obj is Student)
            {
                Student a = (Student)obj;
                var pronadjen = Studenti.FirstOrDefault(c => c.ID == a.ID);
                pronadjen.Aktivan = false;
            }
            else if (obj is Profesor)
            {
                Profesor a = (Profesor)obj;
                var pronadjen = Profesori.FirstOrDefault(c => c.ID == a.ID);
                pronadjen.Aktivan = false;
            }
            else if (obj is Cas)
            {
                Cas a = (Cas)obj;
                var pronadjen = Casovi.FirstOrDefault(c => c.ID == a.ID);
                pronadjen.Aktivan = false;
            }
            sacuvajEnitete();
        }
    }
}
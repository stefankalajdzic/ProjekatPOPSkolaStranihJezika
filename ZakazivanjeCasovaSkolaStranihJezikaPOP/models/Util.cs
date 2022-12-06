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

        private Util()
        {
            _adresaServis = new AdresaServis();
            _skolaServis = new SkolaServis();
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


        public void Initialize()
        {
            _adresaServis.citajAdrese();
            _skolaServis.citajSkole();
        }

        public void sacuvajEnitete()
        {
            _adresaServis.sacuvajAdresu();
            _skolaServis.sacuvajSkole();
        }

        public void obrisiEntitet(object obj)
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
            sacuvajEnitete();
        }
    }
}
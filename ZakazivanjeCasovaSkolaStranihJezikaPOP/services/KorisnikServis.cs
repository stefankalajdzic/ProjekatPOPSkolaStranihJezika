using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.services
{
    class KorisnikServis
    {
        public void SacuvajKorisnike()
        {
            using (StreamWriter file = new StreamWriter(@"../../resources/korisnici.txt"))
            {
                foreach (RegistrovaniKorisnik korisnik in Util.Instance.Korisnici)
                {
                    file.WriteLine(korisnik.formatirajTxtFajlLiniju());
                }
            }
        }

        public void CitajKorisnike()
        {
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            StreamReader file = new StreamReader(@"../../resources/korisnici.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                Adresa adresaKorisnika = Util.Instance.Adrese.FirstOrDefault(c => c.ID == lajs[5]);
                Util.Instance.Korisnici.Add(new RegistrovaniKorisnik
                {
                    ID = lajs[0],
                    Ime = lajs[1],
                    Prezime = lajs[2],
                    JMBG = lajs[3],
                    Pol = (EPol)Enum.Parse(typeof(EPol), lajs[4]),
                    Adresa = adresaKorisnika,
                    Email = lajs[6],
                    Lozinka = lajs[7],
                    TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), lajs[8]),
                    Aktivan = bool.Parse(lajs[9])
                });

            }
            file.Close();
        }
    }
}

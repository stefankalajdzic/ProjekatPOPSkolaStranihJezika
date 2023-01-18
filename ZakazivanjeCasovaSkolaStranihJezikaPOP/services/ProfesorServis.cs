using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.models;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.services
{
    class ProfesorServis
    {
        public void SacuvajProfesore()
        {
            using (StreamWriter file = new StreamWriter(@"../../resources/profesori.txt"))
            {
                foreach (Profesor profesor in Util.Instance.Profesori)
                {
                    file.WriteLine(profesor.formatirajTxtFajlLiniju());
                }
            }
        }

        public void CitajProfesore()
        {
            Util.Instance.Profesori = new ObservableCollection<Profesor>();
            StreamReader file = new StreamReader(@"../../resources/profesori.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                string[] languagesArray = lajs[3].Split(',');

                List<string> languages = languagesArray.ToList();
                RegistrovaniKorisnik korisnik = Util.Instance.Korisnici.FirstOrDefault(c => c.ID == lajs[1]);
                Skola skola = Util.Instance.Skole.FirstOrDefault(c => c.ID == lajs[2]);

                Util.Instance.Profesori.Add(new Profesor
                {
                    ID = lajs[0],
                    Korisnik = korisnik,
                    Skola = skola,
                    Jezici = languages,
                    Casovi = new List<Cas>(),
                    Aktivan = bool.Parse(lajs[4])
                });

            }
            file.Close();
        }
    }
}

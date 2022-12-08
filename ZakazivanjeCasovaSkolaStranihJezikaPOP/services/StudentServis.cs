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
    class StudentServis
    {
        public void SacuvajStudente()
        {
            using (StreamWriter file = new StreamWriter(@"../../resources/studenti.txt"))
            {
                foreach (Student student in Util.Instance.Studenti)
                {
                    file.WriteLine(student.formatirajTxtFajlLiniju());
                }
            }
        }

        public void CitajStudente()
        {
            Util.Instance.Studenti = new ObservableCollection<Student>();
            StreamReader file = new StreamReader(@"../../resources/studenti.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');
                RegistrovaniKorisnik korisnik = Util.Instance.Korisnici.FirstOrDefault(c => c.ID == lajs[1]);
                Util.Instance.Studenti.Add(new Student
                {
                    ID = lajs[0],
                    Korisnik = korisnik,
                    Casovi = new List<Cas>(),
                    Aktivan = bool.Parse(lajs[2])
                });

            }
            file.Close();
        }
    }
}

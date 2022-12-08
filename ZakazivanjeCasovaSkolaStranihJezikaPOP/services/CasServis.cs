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
    class CasServis
    {
        public void SacuvajCasove()
        {
            using (StreamWriter file = new StreamWriter(@"../../resources/casovi.txt"))
            {
                foreach (Cas cas in Util.Instance.Casovi)
                {
                    file.WriteLine(cas.formatirajTxtFajlLiniju());
                }
            }
        }

        public void CitajCasove()
        {
            Util.Instance.Casovi = new ObservableCollection<Cas>();
            StreamReader file = new StreamReader(@"../../resources/casovi.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {

                //1;1;03-10-2022;18:43;1:45;FREE;1;True
                string[] lajs = line.Split(';');
                Profesor profesor = Util.Instance.Profesori.FirstOrDefault(c => c.ID == lajs[1]);
                Student student = Util.Instance.Studenti.FirstOrDefault(c => c.ID == lajs[6]);

                Util.Instance.Casovi.Add(new Cas
                {
                    ID =  lajs[0],
                    Profesor = profesor,
                    Datum = lajs[2],
                    VremePocetka = lajs[3],
                    Trajanje = lajs[4],
                    Status = (EStatusLekcije)Enum.Parse(typeof(EStatusLekcije), lajs[5]),
                    Student = student,
                    Aktivan = bool.Parse(lajs[7])
                });
                Console.WriteLine(line);

            }
            file.Close();
        }
    }
}

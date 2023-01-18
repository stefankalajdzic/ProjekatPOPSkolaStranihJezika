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
    class SkolaServis
    {
        public void SacuvajSkole()
        {
            using (StreamWriter file = new StreamWriter(@"../../resources/skole.txt"))
            {
                foreach (Skola skola in Util.Instance.Skole)
                {
                    file.WriteLine(skola.formatirajTxtFajlLiniju());
                }
            }
        }

        public void CitajSkole()
        {
            Util.Instance.Skole = new ObservableCollection<Skola>();
            StreamReader streamReader = new StreamReader(@"../../resources/skole.txt");
            StreamReader file = streamReader;
            string line;

            while ((line = file.ReadLine()) != null)
            {
                
                string[] lajs = line.Split(';');
                string[] listaJezika = lajs[3].Split(',');

                List<string> jezici = listaJezika.ToList();
                Adresa adresaSkole = Util.Instance.Adrese.FirstOrDefault(c => c.ID == lajs[2]);

                Util.Instance.Skole.Add(new Skola
                {
                    ID = lajs[0],
                    Naziv = lajs[1],
                    Adresa = adresaSkole,
                    Jezici = jezici,
                    Aktivan = bool.Parse(lajs[4])
                });

            }
            file.Close();
        }
    }
}

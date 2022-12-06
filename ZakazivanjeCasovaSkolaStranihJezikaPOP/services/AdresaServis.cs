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
    class AdresaServis
    {
        public void sacuvajAdresu()
        {
            using (StreamWriter file = new StreamWriter(@"../../../resources/adrese.txt"))
            {
                foreach (Adresa adresa in Util.Instance.Adrese)
                {
                    file.WriteLine(adresa.formatirajTxtFajlLiniju());
                }
            }
        }

        public void citajAdrese()
        {
            Util.Instance.Adrese = new ObservableCollection<Adresa>();
            StreamReader streamReader = new StreamReader(@"../../resources/adrese.txt");
            StreamReader file = streamReader;
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] lajs = line.Split(';');

                Util.Instance.Adrese.Add(new Adresa
                {
                    ID = lajs[0],
                    Ulica = lajs[1],
                    Broj = int.Parse(lajs[2]),
                    Grad = lajs[3],
                    Drzava = lajs[4],
                    Aktivan = bool.Parse(lajs[5])
                });



            }
            file.Close();
        }
    }
}

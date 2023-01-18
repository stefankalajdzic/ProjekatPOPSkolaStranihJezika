using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakazivanjeCasovaSkolaStranihJezikaPOP.enums;

namespace ZakazivanjeCasovaSkolaStranihJezikaPOP.models
{
    public class Cas
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private Profesor _profesor;

        public Profesor Profesor
        {
            get { return _profesor; }
            set { _profesor = value; }
        }

        private string _datum;

        public string Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        private string _vremePocetka;

        public string VremePocetka
        {
            get { return _vremePocetka; }
            set { _vremePocetka = value; }
        }

        private string _trajanje;

        public string Trajanje
        {
            get { return _trajanje; }
            set { _trajanje = value; }
        }

        private EStatusLekcije _status;

        public EStatusLekcije Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set { _student = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }

        public string formatirajTxtFajlLiniju()
        {
            if (Student != null)
            {
                return ID + ";" + Profesor.ID + ";" + Datum + ";" + VremePocetka + ";" + Trajanje + ";" + Status + ";" + Student.ID + ";" + Aktivan.ToString();
            }
            else
            {
                return ID + ";" + Profesor.ID + ";" + Datum + ";" + VremePocetka + ";" + Trajanje + ";" + Status + ";" + 0 + ";" + Aktivan.ToString();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenadżerZadań
{
    public class Task
    {
        private int id;
        private int userId;
        private string tytul;
        private DateTime dataroz;
        private DateTime datazak;
        private int priorytet;
        private bool ukonczono;
        private string tresc;

        public Task(int id, int userId)
        {
            this.id = id;
            this.userId = userId;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public int UserId
        {
            get => userId;
        }

        public string Tytul
        {
            get => tytul;
            set => tytul = value;
        }

        public DateTime DataRoz
        {
            get => dataroz;
            set => dataroz = value;
        }

        public DateTime DataZak
        {
            get => datazak;
            set => datazak = value;
        }

        public int Priorytet
        {
            get => priorytet;
            set => priorytet = value;
        }

        public bool Ukonczono
        {
            get => ukonczono;
            set => ukonczono = value;
        }

        public string Tresc
        {
            get => tresc;
            set => tresc = value;
        }

    }
}

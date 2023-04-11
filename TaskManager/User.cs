using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenadżerZadań
{
    public class User
    {
        private int id;
        private string name;
        private string surname;
        private string login;
        private string password;

        public User(int id)
        {
            this.id = id;
        }

        public int Id 
        {
            get => id;
        }
 
        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Surname
        {
            get => surname;
            set => surname = value;
        }

        public string Login
        {
            get => login;
            set => login = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

    }



}

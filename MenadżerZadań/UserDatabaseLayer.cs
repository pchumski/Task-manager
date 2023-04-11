using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenadżerZadań
{
    public class UserDatabaseLayer
    {
        static UserDatabaseLayer instance;
        private Database database;

        private UserDatabaseLayer()
        {
            database = new Database();
        }

        public static UserDatabaseLayer GetInstance()
        {
            if (instance == null)
            {
                instance = new UserDatabaseLayer();
            }
            return instance;
        }

        public bool Create(User user)
        {
            SqlCommand cmd = database.OpenConnection("SELECT COUNT(*) AS UsersCount FROM Users WHERE Login=@LOGIN");
            cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar);
            cmd.Parameters["@LOGIN"].Value = user.Login;
            SqlDataReader reader = cmd.ExecuteReader();

            int coutOfUsersInDatabase = 0;
            if (reader.Read() == true)
            {
                coutOfUsersInDatabase = reader.GetInt32(0); // pobieramy 0 kolumne jako string

            }
            reader.Close();

            if (coutOfUsersInDatabase > 0)
            {
                MessageBox.Show("Uzytkownik o podanym loginie już istnieje!");
                database.CloseConnection();
                return false;
            }
            else
            {
                cmd = database.GetCommand("INSERT INTO Users (Imie, Nazwisko, Login, Password) VALUES(@Imie,@Nazwisko,@Login,@Password)");

                cmd.Parameters.AddWithValue("@Imie", user.Name);
                cmd.Parameters.AddWithValue("@Nazwisko", user.Surname);
                cmd.Parameters.AddWithValue("@Login", user.Login);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                try
                {
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Rejestracja przebiegła pomyślnie");
                    database.CloseConnection();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Nie udalo sie dodać użytkownika");
                    database.CloseConnection();
                    return false;
                }
            }
            

        }

        public User Read(int id)
        {
            SqlCommand cmd = database.OpenConnection("SELECT Imie, Nazwisko, Login, Password FROM Users WHERE id=@ID");
            cmd.Parameters.AddWithValue("@ID", id);

            User user= new User(id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read() == true)
            {
                user.Name = reader.GetString(0);
                user.Surname = reader.GetString(1); 
                user.Login = reader.GetString(2);
                user.Password = reader.GetString(3);
            }

            database.CloseConnection();

            return user;
        }

        public void Update(User user)
        {
            SqlCommand cmd = database.OpenConnection("UPDATE Users SET Imie=@IMIE, Nazwisko = @NAZWISKO, Password=@PASSWORD WHERE id=@ID");
            cmd.Parameters.AddWithValue("@ID", user.Id);

            cmd.Parameters.AddWithValue("@IMIE", user.Name);
            cmd.Parameters.AddWithValue("@NAZWISKO", user.Surname);
            cmd.Parameters.AddWithValue("@PASSWORD", user.Password);

            cmd.ExecuteNonQuery();

            database.CloseConnection();
        }


        public bool LogIn(string user, string password, out int id)
        {
            id = -1;
            if (password.Length == 0)
            {
                MessageBox.Show("Haslo nie może być puste");
                return false;
            }
            if (user.Length == 0)
            {
                MessageBox.Show("Nazwa użytkownika nie może być posta");
                return false;
            }

            SqlCommand sqlCommand = database.OpenConnection("SELECT Id, Password FROM Users WHERE Login=@LOGIN");
            sqlCommand.Parameters.Add("@LOGIN", SqlDbType.VarChar);
            sqlCommand.Parameters["@LOGIN"].Value = user;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            string databasePassword = "";
            if (reader.Read() == true)
            {
                id = reader.GetInt32(0);
                databasePassword = reader.GetString(1); // pobieramy 1 kolumne jako string
            }
            reader.Close();
            database.CloseConnection();

            if (databasePassword == password)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Nie poprawny login lub haslo");
                return false;
            }

        }


    }
}

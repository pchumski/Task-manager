using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MenadżerZadań
{
    class TasksDatabaseLayer
    {
        static TasksDatabaseLayer instance;
        private Database database;

        private TasksDatabaseLayer()
        {
            database = new Database();
        }

        public static TasksDatabaseLayer GetInstance()
        {
            if (instance == null)
            {
                instance = new TasksDatabaseLayer();
            }
            return instance;
        }

        public void Create(Task tasks)
        {
            SqlCommand cmd = database.OpenConnection("INSERT INTO Tasks (UserId, Tytul, DataRoz, DataZak, Priorytet, Ukonczono, Tresc) VALUES(@UserId, @Tytul, @DataRoz, @DataZak, @Priorytet, @Ukonczono, @Tresc);SELECT CAST(scope_identity() AS int)");

            cmd.Parameters.AddWithValue("@UserId", tasks.UserId);
            cmd.Parameters.AddWithValue("@Tytul", tasks.Tytul);
            cmd.Parameters.AddWithValue("@DataRoz", tasks.DataRoz);
            cmd.Parameters.AddWithValue("@DataZak", tasks.DataZak);
            cmd.Parameters.AddWithValue("@Priorytet", tasks.Priorytet);
            cmd.Parameters.AddWithValue("@Ukonczono", tasks.Ukonczono);
            cmd.Parameters.AddWithValue("@Tresc", tasks.Tresc);

            tasks.Id = (int)cmd.ExecuteScalar();

            database.CloseConnection();
        }

        public void Update(Task tasks)
        {
            SqlCommand cmd = database.OpenConnection("UPDATE Tasks SET Tytul=@TYTUL, DataRoz=@DATAROZ, DataZak=@DATAZAK, Priorytet=@PRIORYTET, Ukonczono=@UKONCZONO, Tresc=@TRESC WHERE id=@ID;");
            cmd.Parameters.AddWithValue("@ID", tasks.Id);

            cmd.Parameters.AddWithValue("@TYTUL", tasks.Tytul);
            cmd.Parameters.AddWithValue("@DATAROZ", tasks.DataRoz);
            cmd.Parameters.AddWithValue("@DATAZAK", tasks.DataZak);
            cmd.Parameters.AddWithValue("@PRIORYTET", tasks.Priorytet);
            cmd.Parameters.AddWithValue("@UKONCZONO", tasks.Ukonczono);
            cmd.Parameters.AddWithValue("@TRESC", tasks.Tresc);

            cmd.ExecuteNonQuery();

            database.CloseConnection();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = database.OpenConnection("DELETE FROM Tasks WHERE id=@ID");
            cmd.Parameters.AddWithValue("@ID", id);

            cmd.ExecuteNonQuery();

            database.CloseConnection();
        }

        public Task Read(int id, int userId)
        {
            SqlCommand cmd = database.OpenConnection("SELECT Tytul, DataRoz, DataZak, Priorytet, Ukonczono, Tresc FROM Tasks WHERE id=@ID");
            cmd.Parameters.AddWithValue("@ID", id);

            Task tasks = new Task(id, userId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                tasks.Tytul = reader.GetString(0);
                tasks.DataRoz = reader.GetDateTime(1);
                tasks.DataZak = reader.GetDateTime(2);
                tasks.Priorytet = reader.GetInt32(3);
                tasks.Ukonczono = reader.GetBoolean(4);
                tasks.Tresc = reader.GetString(5);
            }

            database.CloseConnection();

            return tasks;
        }

        public List<Task> ReadList(int userId)
        {
            SqlCommand cmd = database.OpenConnection("SELECT Id, Tytul, DataRoz, DataZak, Priorytet, Ukonczono, Tresc FROM Tasks WHERE userId=@UserId");
            cmd.Parameters.AddWithValue("@UserId", userId);

            List<Task> list = new List<Task>();

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                int id = reader.GetInt32(0);
                Task task = new Task(id, userId);
                task.Tytul = reader.GetString(1);
                task.DataRoz = reader.GetDateTime(2);
                task.DataZak = reader.GetDateTime(3);
                task.Priorytet = reader.GetInt32(4);
                task.Ukonczono = reader.GetBoolean(5);
                task.Tresc = reader.GetString(6);
                list.Add(task);
            }

            database.CloseConnection();
            return list;
        }
    }
}

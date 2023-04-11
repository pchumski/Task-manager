using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace MenadżerZadań
{
    public partial class LogIn : Form
    {
        private int userId;


        public LogIn()
        {
            InitializeComponent();
            userId = -1;
        }

        public int UserId
        {
            get
            {
                return userId;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(UserDatabaseLayer.GetInstance().LogIn(Username.Text, this.Password.Text,out userId))
           {
                this.Close();
           }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Hide();
            UserForm registry = new UserForm();
            registry.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(userId == -1)
            {
                Application.Exit();
            }
        }
    }
}

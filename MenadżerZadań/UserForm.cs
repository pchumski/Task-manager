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

namespace MenadżerZadań
{
    public partial class UserForm : Form
    {
        
        User user;
        public UserForm()
        {

            InitializeComponent();
            this.user = new User(-1);
            this.labelType.Text = "Rejestracja:";
            this.txtLogin.ReadOnly = false;
            this.btnAction.Text = "Rejestruj";
         
        }

        public UserForm(int id)
        {
            InitializeComponent();
            this.user = new User(id);

            this.labelType.Text = "Zmiana danych";
            this.txtLogin.ReadOnly = true;
            this.btnAction.Text = "Zmień";

            user = UserDatabaseLayer.GetInstance().Read(id);
            txtName.Text = user.Name;
            txtSurname.Text = user.Surname;
            txtLogin.Text = user.Login;
            txtPassword.Text = user.Password;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(user.Id == -1)
            {
                user.Name = txtName.Text;
                user.Surname = txtSurname.Text;
                user.Login = txtLogin.Text;
                user.Password = txtPassword.Text;

                if(UserDatabaseLayer.GetInstance().Create(user))
                {
                    this.Close();
                }
            }
            else
            {
               
                user.Name = txtName.Text;
                user.Surname = txtSurname.Text;
                user.Password = txtPassword.Text;

                UserDatabaseLayer.GetInstance().Update(user);
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }
    }
}

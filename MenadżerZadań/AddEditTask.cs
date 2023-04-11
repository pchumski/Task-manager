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
    public partial class AddEditTask : Form
    {
        Task task;
        bool action;

        public Task Item
        {
            get
            {
                return task;
            }
        }

        public bool Action
        {
            get
            {
                return action;
            }
        }

        public AddEditTask(int userId)
        { 
            InitializeComponent();
            this.task = new Task(-1, userId);
            this.action = false;
            this.btnaction.Text = "Dodaj";
        }

        public AddEditTask(int id, int userId)
        {
            InitializeComponent();
            this.task = TasksDatabaseLayer.GetInstance().Read(id, userId);
            
            textBox1.Text = task.Tytul;
            dateTimePickerKon.Value = task.DataZak;
            dateTimePickerRozp.Value = task.DataRoz;
            comboBox1.SelectedIndex = task.Priorytet;
            checkBoxUkonczono.Checked = task.Ukonczono;
            textBox2.Text = task.Tresc;
            this.action = false;
            this.btnaction.Text = "Edytuj";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            task.Tytul = textBox1.Text;
            task.DataRoz = dateTimePickerRozp.Value;
            task.DataZak = dateTimePickerKon.Value;
            task.Priorytet = comboBox1.SelectedIndex;
            task.Ukonczono = checkBoxUkonczono.Checked;
            task.Tresc = textBox2.Text;

            if(task.Id == -1)
            {
                TasksDatabaseLayer.GetInstance().Create(task);
            }
            else
            {
                TasksDatabaseLayer.GetInstance().Update(task);
                
            }
            this.action = true;
            this.Close();
            //                MessageBox.Show(ex.Message);

        }

    

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }
    }
}

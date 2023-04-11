using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenadżerZadań
{
    public partial class MainPanel : Form
    {
        int userId;
        List<Task> tasks;
        public MainPanel()
        {
            InitializeComponent();

            userId = LogIn();
            this.tasks = TasksDatabaseLayer.GetInstance().ReadList(userId);
            RefreshTasks();

        }


        private ListViewItem ConvertToListViewItem(Task task)
        {
            ListViewItem item = new ListViewItem(task.Id.ToString());

            item.SubItems.Add(task.Tytul);
            item.SubItems.Add(task.DataRoz.ToShortDateString());
            item.SubItems.Add(task.DataZak.ToShortDateString());
            if (task.Priorytet == 0)
            {
                item.SubItems.Add("Normalny");
            }
            else
            {
                item.SubItems.Add("Pilny");
            }
            if (task.Ukonczono)
            {
                item.SubItems.Add("TAK");
            }
            else
            {
                item.SubItems.Add("NIE");
            }
            return item;
        }
        private void RefreshTasks()
        {
            this.listView1.Items.Clear();
            
            foreach(Task task in tasks)
            {
                this.listView1.Items.Add(ConvertToListViewItem(task));
            }
            
        }

        int LogIn()
        {
            LogIn log = new LogIn();
            log.ShowDialog();
            return log.UserId;
        }


        private void dodajZadanieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usunZadanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditTask taskWindow = new AddEditTask(this.userId);
            taskWindow.ShowDialog();
            if (taskWindow.Action)
            {
                tasks.Add(taskWindow.Item);
                this.listView1.Items.Add(ConvertToListViewItem(taskWindow.Item));
            }
        }

        private void edytujToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm(this.userId);
            userForm.ShowDialog();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                int index = this.listView1.SelectedIndices[0];
                DialogResult dialogResult = MessageBox.Show( "Czy na pewno usunąć task: "+tasks[index].Tytul, "Usuwanie", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    TasksDatabaseLayer.GetInstance().Delete(tasks[index].Id);
                    tasks.RemoveAt(index);
                    this.listView1.Items.RemoveAt(index);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            
        }



        private void edytujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.listView1.SelectedIndices.Count > 0)
            {
                int index = this.listView1.SelectedIndices[0];
                AddEditTask taskWindow = new AddEditTask(tasks[index].Id, this.userId);
                taskWindow.ShowDialog();
                if (taskWindow.Action)
                {
                    ListViewItem item = ConvertToListViewItem(taskWindow.Item);
                    //this.listView1.Items[index].SubItems.Clear();
                    //this.listView1.Items[index].SubItems.AddRange(item.SubItems);
                    tasks[index] = taskWindow.Item;
                    this.listView1.Items[index] = item;
                    this.listView1.SelectedIndices.Add(index);
                    //.Add(ConvertToListViewItem(taskWindow.Item));
                }
            }
            
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listView1.SelectedIndices.Count>0)
            {
                int index = this.listView1.SelectedIndices[0];
                this.textBoxOpis.Text = tasks[index].Tresc;

            }
            else
            {
                this.textBoxOpis.Text = "";
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainPanel_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'database1DataSet.Tasks' . Możesz go przenieść lub usunąć.
            this.tasksTableAdapter.Fill(this.database1DataSet.Tasks);

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace WindowsFormsCRUDFirebase
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "wTmzsFqEsU6DINHgFDnexP78BrbLGFAa96Zuqfio",
            BasePath = "https://crudapp-fe2020.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null) 
            {
                MessageBox.Show("connection is established");
            }

            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Age");

            dataGridView1.DataSource = dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            FirebaseResponse resp = await client.GetTaskAsync("Counter/node");
            Counter_class get = resp.ResultAs<Counter_class>();

            var data = new Data
            {
                ID = (Convert.ToInt32(get.cnt)+1).ToString(),
                Name = textBox2.Text,
                Address = textBox3.Text,
                Age = textBox4.Text,
            };

            SetResponse response = await client.SetTaskAsync("Information/" + data.ID, data);
            Data result = response.ResultAs<Data>();

            MessageBox.Show("data inserted" + result.ID);

            var obj = new Counter_class
            {
                cnt = data.ID
            };

            SetResponse response1 = await client.SetTaskAsync("Counter/node", obj);
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Information/"+ textBox1.Text);
            Data obj = response.ResultAs<Data>();

            textBox1.Text = obj.ID;
            textBox2.Text = obj.Name;
            textBox3.Text = obj.Address;
            textBox4.Text = obj.Age;

            MessageBox.Show("Data Retrieved Successfully"); 
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                ID = textBox1.Text,
                Name = textBox2.Text,
                Address = textBox3.Text,
                Age = textBox4.Text,
            };
            FirebaseResponse response = await client.UpdateTaskAsync("Information/" + textBox1.Text, data);
            Data result = response.ResultAs<Data>();
            MessageBox.Show("Message update at ID:"+result.ID);

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information/" + textBox1.Text);
            MessageBox.Show("Delete record of ID:" + textBox1.Text);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information");
            MessageBox.Show("All Item has been deleted!!!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            export();
        }

        private async void export()
        {
            int i = 0;
            FirebaseResponse resp1 = await client.GetTaskAsync("Counter/node");
            Counter_class obj1 = resp1.ResultAs<Counter_class>();
            int cnt = Convert.ToInt32(obj1.cnt);

            MessageBox.Show(cnt.ToString());

            while(true)
            {
                if (i == cnt)
                {
                    break;
                }
                i++;
                try
                {
                    FirebaseResponse resp2 = await client.GetTaskAsync("Information/"+i);
                    Data obj2 = resp2.ResultAs<Data>();

                    DataRow row = dt.NewRow();
                    row["ID"] = obj2.ID;
                    row["Name"] = obj2.Name;
                    row["Address"] = obj2.Address;
                    row["Age"] = obj2.Age;

                    dt.Rows.Add(row);
                }
                catch
                {

                }
            }
            MessageBox.Show("The data is fetched!!!");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

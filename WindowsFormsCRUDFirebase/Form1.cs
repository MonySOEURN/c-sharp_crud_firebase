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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                ID = textBox1.Text,
                Name = textBox2.Text,
                Address = textBox3.Text,
                Age = textBox4.Text,
            };

            SetResponse response = await client.SetTaskAsync("Information/" + textBox1.Text, data);
            Data result = response.ResultAs<Data>();

            MessageBox.Show("data inserted" + result.ID);
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
    }
}

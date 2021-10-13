using SR6.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR6.PresentationDesktop
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client;
        public Form1()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2131/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            HttpResponseMessage response = await client.GetAsync("api/brands");
            listView1.Items.Clear();
            if (response.IsSuccessStatusCode)
            {
                var brands = await response.Content.ReadAsAsync<List<Brand>>();
                
                foreach(var item in brands)
                {
                    var li = listView1.Items.Add(item.BrandId.ToString());
                    li.SubItems.Add(item.BrandName);
                    li.SubItems.Add(item.Description);
                }
            }
            else
            {
                var errormsg = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MessageBox.Show(errormsg);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Brand name field is required");
                return;
            }
            var brand = new Brand
            {
                BrandName=textBox1.Text,
                Description = textBox2.Text
            };
            HttpResponseMessage respone = await client.PostAsJsonAsync<Brand>("api/Brands", brand);
            if (respone.IsSuccessStatusCode)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                Form1_Load(sender, e);
            }
            else
            {
                var errormsg = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MessageBox.Show(errormsg);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to delete this record?", "confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var id = listView1.SelectedItems[0].Text;
                    HttpResponseMessage respone = await client.DeleteAsync("api/Brands/" + id);
                    if (respone.IsSuccessStatusCode)
                    {
                        MessageBox.Show("The record was deleted");
                        Form1_Load(sender, e);
                    }
                    else
                    {
                        var errormsg = respone.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        MessageBox.Show(errormsg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item before click on delete button");
            }
        }
    }
}

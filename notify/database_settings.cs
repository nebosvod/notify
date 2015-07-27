using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
namespace notify
{
    public partial class database_settings : Form
    {
        public database_settings()
        {
            InitializeComponent();
        }

        private void database_settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";


            textBox1.Text = notify.Properties.Settings.Default.database_ip;
            textBox2.Text = notify.Properties.Settings.Default.user;
            textBox3.Text = notify.Properties.Settings.Default.password;
            textBox4.Text = notify.Properties.Settings.Default.database_name;

   /*         string path2 = Application.StartupPath + @"\file.xml";
            XmlTextReader XmlReader = new XmlTextReader (path2);
           
            while (XmlReader.Read())
            {
                if (XmlReader.NodeType == XmlNodeType.Element)
                {
                    if (XmlReader.Name.Equals("Database_Settings"))
                    {
                        textBox1.Text = XmlReader.GetAttribute("ip");
                        textBox2.Text = XmlReader.GetAttribute("user");
                        textBox3.Text = XmlReader.GetAttribute("password");
                    }
                }
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notify.Properties.Settings.Default.database_ip = textBox1.Text;
            notify.Properties.Settings.Default.user = textBox2.Text;
            notify.Properties.Settings.Default.password = textBox3.Text;
            notify.Properties.Settings.Default.database_name = textBox4.Text;

            notify.Properties.Settings.Default.Save();
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

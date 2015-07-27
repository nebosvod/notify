using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Configuration;

namespace notify
{
    public partial class f_settings : Form
    {

        public f_settings()
        {
            InitializeComponent();
            
        }

        private void f_settings_Load(object sender, EventArgs e)
        {

            comboBox1.Text = notify.Properties.Settings.Default.COM_cb1;
            comboBox2.Text = notify.Properties.Settings.Default.COM_cb2;
            comboBox3.Text = notify.Properties.Settings.Default.COM_cb3;
            comboBox4.Text = notify.Properties.Settings.Default.COM_cb4;
            comboBox5.Text = notify.Properties.Settings.Default.COM_cb5;
            comboBox6.Text = notify.Properties.Settings.Default.COM_cb6;
            comboBox7.Text = notify.Properties.Settings.Default.COM_cb7;

            string[] portnames = SerialPort.GetPortNames();

            comboBox1.Items.AddRange(portnames);
            comboBox2.Items.AddRange(portnames);
            comboBox3.Items.AddRange(portnames);
            comboBox4.Items.AddRange(portnames);
            comboBox5.Items.AddRange(portnames);
            comboBox6.Items.AddRange(portnames);
            comboBox7.Items.AddRange(portnames);

            Public_Data.Value = comboBox1.Text;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            notify.Properties.Settings.Default.COM_cb1 = comboBox1.Text;
            notify.Properties.Settings.Default.COM_cb2 = comboBox2.Text;
            notify.Properties.Settings.Default.COM_cb3 = comboBox3.Text;
            notify.Properties.Settings.Default.COM_cb4 = comboBox4.Text;
            notify.Properties.Settings.Default.COM_cb5 = comboBox5.Text;
            notify.Properties.Settings.Default.COM_cb6 = comboBox6.Text;
            notify.Properties.Settings.Default.COM_cb7 = comboBox7.Text;

            notify.Properties.Settings.Default.Save();

            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }



}


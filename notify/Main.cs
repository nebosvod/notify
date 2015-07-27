using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace notify
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        
        void pokazaniya_och(object sender, EventArgs e)
        {
            string conn_str = "Database=" + notify.Properties.Settings.Default.database_name + ";Data Source=" + notify.Properties.Settings.Default.database_ip + ";User Id=" + notify.Properties.Settings.Default.user + ";Password=" + notify.Properties.Settings.Default.password;
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            MySqlLib.MySqlData.MySqlExecute.MyResult result2 = new MySqlLib.MySqlData.MySqlExecute.MyResult();

            result = MySqlLib.MySqlData.MySqlExecute.SqlScalar("SELECT stoki_tek FROM resources.`stoki_tek` ORDER BY stoki_tek_date DESC LIMIT 0,1", conn_str);
            result2 = MySqlLib.MySqlData.MySqlExecute.SqlScalar("SELECT stoki_tek_date FROM resources.`stoki_tek` ORDER BY stoki_tek_date DESC LIMIT 0,1", conn_str);

            label2.Text = result.ResultText;
            label3.Text = result2.ResultText;
            toolStripStatusLabel2.Text = Convert.ToString(DateTime.Now);
            statusStrip1.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool status_och = PingHost ("10.1.4.160");
            bool status_db = PingHost("10.1.1.50");

            if (status_och == true)
                {
                    label6.Text = "Доступен";
                    label6.ForeColor = System.Drawing.Color.ForestGreen;

                }
            else
                {
                    label6.Text = "Недоступен";
                    label6.ForeColor = System.Drawing.Color.OrangeRed;
                }

            if (status_db == true)
            {
                label5.Text = "Доступен";
                label5.ForeColor = System.Drawing.Color.ForestGreen;

            }
            else
            {
                label5.Text = "Недоступен";
                label5.ForeColor = System.Drawing.Color.OrangeRed;
            }

            string conn_str = "Database=" + notify.Properties.Settings.Default.database_name + ";Data Source=" + notify.Properties.Settings.Default.database_ip + ";User Id=" + notify.Properties.Settings.Default.user + ";Password=" + notify.Properties.Settings.Default.password;
            MySqlLib.MySqlData.MySqlExecute.MyResult result = new MySqlLib.MySqlData.MySqlExecute.MyResult();
            MySqlLib.MySqlData.MySqlExecute.MyResult result2 = new MySqlLib.MySqlData.MySqlExecute.MyResult();

            result = MySqlLib.MySqlData.MySqlExecute.SqlScalar("SELECT stoki_tek FROM resources.`stoki_tek` ORDER BY stoki_tek_date DESC LIMIT 0,1", conn_str);
            result2 = MySqlLib.MySqlData.MySqlExecute.SqlScalar("SELECT stoki_tek_date FROM resources.`stoki_tek` ORDER BY stoki_tek_date DESC LIMIT 0,1", conn_str);


            label2.Text = result.ResultText;
            label3.Text = result2.ResultText;

            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            this.Top = Screen.PrimaryScreen.Bounds.Height - this.Height;

            Timer timer = new Timer();
            timer.Interval = 60000;
            timer.Tick += new EventHandler(pokazaniya_och);

            toolStripStatusLabel2.Text = Convert.ToString(DateTime.Now);
            statusStrip1.Update();

            timer.Start();

        }


        private void настройкаCOMПортовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_settings form_settings = new f_settings();
            form_settings.ShowDialog();
        }

        private void настройкаБазыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            database_settings form_database_settings = new database_settings();
            form_database_settings.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show(notify.Properties.Settings.Default.COM_cb1);
           
        }
    }
}

namespace MySqlLib
{
    /// <summary>
    /// Набор компонент для простой работы с MySQL базой данных.
    /// </summary>
    public class MySqlData
    {

        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением одного параметра либо без параметров вовсе.
        /// </summary>
        public class MySqlExecute
        {

            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResult
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public string ResultText;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }

            /// <summary>
            /// Для выполнения запросов к MySQL с возвращением 1 параметра.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает значение при успешном выполнении запроса, текст ошибки - при ошибке.</returns>
            public static MyResult SqlScalar(string sql, string connection)
            {
                MyResult result = new MyResult();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        result.ResultText = commRC.ExecuteScalar().ToString();
                        result.HasError = false;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorText = ex.Message;
                        result.HasError = true;

                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }


            /// <summary>
            /// Для выполнения запросов к MySQL без возвращения параметров.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает True - ошибка или False - выполнено успешно.</returns>
            public static MyResult SqlNoneQuery(string sql, string connection)
            {
                MyResult result = new MyResult();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        commRC.ExecuteNonQuery();
                        result.HasError = false;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorText = ex.Message;
                        result.HasError = true;
                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }

        }
        /// <summary>
        /// Методы реализующие выполнение запросов с возвращением набора данных.
        /// </summary>
        public class MySqlExecuteData
        {
            /// <summary>
            /// Возвращаемый набор данных.
            /// </summary>
            public class MyResultData
            {
                /// <summary>
                /// Возвращает результат запроса.
                /// </summary>
                public DataTable ResultData;
                /// <summary>
                /// Возвращает True - если произошла ошибка.
                /// </summary>
                public string ErrorText;
                /// <summary>
                /// Возвращает текст ошибки.
                /// </summary>
                public bool HasError;
            }
            /// <summary>
            /// Выполняет запрос выборки набора строк.
            /// </summary>
            /// <param name="sql">Текст запроса к базе данных</param>
            /// <param name="connection">Строка подключения к базе данных</param>
            /// <returns>Возвращает набор строк в DataSet.</returns>
            public static MyResultData SqlReturnDataset(string sql, string connection)
            {
                MyResultData result = new MyResultData();
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection connRC = new MySql.Data.MySqlClient.MySqlConnection(connection);
                    MySql.Data.MySqlClient.MySqlCommand commRC = new MySql.Data.MySqlClient.MySqlCommand(sql, connRC);
                    connRC.Open();
                    try
                    {
                        MySql.Data.MySqlClient.MySqlDataAdapter AdapterP = new MySql.Data.MySqlClient.MySqlDataAdapter();
                        AdapterP.SelectCommand = commRC;
                        DataSet ds1 = new DataSet();
                        AdapterP.Fill(ds1);
                        result.ResultData = ds1.Tables[0];
                    }
                    catch (Exception ex)
                    {
                        result.HasError = true;
                        result.ErrorText = ex.Message;
                    }
                    connRC.Close();
                }
                catch (Exception ex)//Этот эксепшн на случай отсутствия соединения с сервером.
                {
                    result.ErrorText = ex.Message;
                    result.HasError = true;
                }
                return result;
            }
        }
    }
}
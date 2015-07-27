using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace notify
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }

    static class Public_Data
    {
        public static string Value { get; set; }
    }

}

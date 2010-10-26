﻿using System;
using System.Windows.Forms;

namespace DipDemo.Cataloguer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appContext = Bootstrapper.Configure();

            Application.Run(appContext);
        }
    }
}

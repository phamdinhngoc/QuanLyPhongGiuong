﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E11_PhongGiuong
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
            E00_System.cls_System.sys_UserID = "Khohap";// "knhiem";//"1";
            Application.Run(new frm_QuanLyGiuong());
           //Application.Run(new Form1());
            
        }
    }
}

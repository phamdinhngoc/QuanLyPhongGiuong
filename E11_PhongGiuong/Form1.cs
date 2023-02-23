using CrystalDecisions.CrystalReports.Engine;
using E00_Base;
using E00_Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E11_PhongGiuong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            E12_SuCo.frmChonSuCo frmChonSuCo = new E12_SuCo.frmChonSuCo();
            frmChonSuCo.ShowDialog();
            if (frmChonSuCo._LoaiSuCo == "-1") return;
            E12_SuCo.frmSuCoTiepNhan frmSuCo = new E12_SuCo.frmSuCoTiepNhan(frmChonSuCo._LoaiSuCo);
            frmSuCo.Name = "TiepNhanSuCo" + frmChonSuCo._LoaiSuCo;
            if (frmChonSuCo._LoaiSuCo == "0")
            {
                frmSuCo.Text = "Tiếp nhận sự cố thường";
            }
            else
            {
                frmSuCo.Text = "Tiếp nhận sự cố y khoa";
            }
            frmSuCo.Show();
            //AddWindows(frmSuCo);
            //; break;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            E12_SuCo.frmSuCo frmSuCo2 = new E12_SuCo.frmSuCo(true);
            frmSuCo2.Name = "HoiDapSuCo";
            frmSuCo2.Text = "Phản hồi sự cố";
            frmSuCo2.Show();
        }
    }
}

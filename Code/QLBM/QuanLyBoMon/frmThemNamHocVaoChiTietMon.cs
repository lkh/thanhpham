﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DTO;
using BUS;
using System.Data.SqlClient;

namespace QuanLyBoMon
{
    public partial class frmThemNamHocVaoChiTietMon : Form
    {
        public int maChiTietMon;
        public int maNamHoc;
        public IList ltNamHoc;
        public bool isFirstLoadSuccess = false;

        public frmThemNamHocVaoChiTietMon()
        {
            InitializeComponent();
        }

        private void frmThemNamHocVaoChiTietMon_Load(object sender, EventArgs e)
        {
            try
            {
                maChiTietMon = frmThemLop.gMaChiTietMon;
                maNamHoc = frmThemLop.gMaNamHoc;
                ltNamHoc = NamHocBUS.GetList();

                cmbNamHoc.DataSource = ltNamHoc;
                cmbNamHoc.DisplayMember = "TenNamHoc";
                cmbNamHoc.ValueMember = "MaNamHoc";
                cmbNamHoc.SelectedValue = maNamHoc;

                isFirstLoadSuccess = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmThemLop.isFrmThemNamHocVaoChiTietMonClosed = true;
        }

        private void cmbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isFirstLoadSuccess)
                {
                    maNamHoc = (cmbNamHoc.SelectedItem as NamHocDTO).MaNamHoc;
                    ChiTietMonBUS.CapNhatNamHoc(maChiTietMon, maNamHoc);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HIS
{
    public partial class امرتوريدخدمات : Form
    {
        Connection con = new Connection();
        //SqlConnection con = new SqlConnection(@"Server=DEMIANA\SQLEXPRESS;Database=PHIS;integrated security=true");
        //SqlDataAdapter da;
        //DataSet ds = new DataSet();
        اضافةامرتوريدخدمات c;
        public امرتوريدخدمات()
        {
            InitializeComponent();
        }
         public امرتوريدخدمات (اضافةامرتوريدخدمات f8)
        {
            InitializeComponent();
            c= f8;
        }

        private void Form36_Load(object sender, EventArgs e)
        {
            con.OpenConection();
            dataGridView1.DataSource = con.ShowDataInGridViewUsingStoredProc("fservice");
            con.CloseConnection();
            //dataGridView1.DataSource = null;
            //ds.Tables.Clear();
            //dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
            //SqlCommand cmd = new SqlCommand("fservice", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //da = new SqlDataAdapter(cmd);
            ////da = new SqlDataAdapter("select * from services", @"Server=DEMIANA\SQLEXPRESS;Database=PHIS;integrated security=true");
            //SqlCommandBuilder cb = new SqlCommandBuilder(da);
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            // f13 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            c.setV(row.Cells[0].Value.ToString());
            this.Close();
        }
    }
}

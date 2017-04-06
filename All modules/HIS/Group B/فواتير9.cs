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
    public partial class For17 : Form
    {
        SqlConnection con = new SqlConnection(@"Server=KERMENA\SQLEXPRESS;Database=PHIS;integrated security=true");
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        For15 c;
        public For17()
        {
            InitializeComponent();
        }
          public For17(For15 f6)
        {
            InitializeComponent();
            c= f6;
        }
        
        private void Form17_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            ds.Tables.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            SqlCommand cmd = new SqlCommand("bill_show", con);
            cmd.CommandType = CommandType.StoredProcedure; 
                                                                       
            da = new SqlDataAdapter(cmd);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            // f13 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            c.setVal(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString());
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

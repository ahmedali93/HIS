﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
namespace HIS
{
    public partial class deductions : Form
    {
        public deductions()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            con.OpenConection();
        }
        SqlDataReader dr;
        ArrayList code = new ArrayList();
        Connection con = new Connection();
        private void add_Click(object sender, EventArgs e)
        {
           
            con.OpenConection();

            SqlCommand cmd = new SqlCommand("add_deduction");
            try
            {
                cmd.Connection = con.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@t", comboBox1.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@v", float.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@note", textBox4.Text);
                cmd.Parameters.AddWithValue("@n", comboBox2.SelectedItem);
                cmd.ExecuteNonQuery();
                MessageBox.Show("تم اضافه الخصم");
               comboBox2.SelectedItem = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }

            catch (Exception)
            {
                MessageBox.Show("خطأ في الادخال");
            }
            con.CloseConnection();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            con.OpenConection();

            SqlCommand cmd = new SqlCommand("del_deduction");
            try
            {
                cmd.Connection = con.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@n", comboBox2.SelectedItem);
                cmd.Parameters.AddWithValue("@t", comboBox1.Text);


                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("تم حذف الخصم");
                    comboBox2.SelectedItem = "";
                    comboBox1.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    MessageBox.Show("خطأ في الادخال");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطأ في الادخال");
            }
            con.CloseConnection();
        }

        private void modify_Click(object sender, EventArgs e)
        {
            con.OpenConection();

            SqlCommand cmd = new SqlCommand("modify_deduction");
            try
            {
                cmd.Connection = con.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@t", comboBox1.Text);
                cmd.Parameters.AddWithValue("@d", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@v", float.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@note", textBox4.Text);
                cmd.Parameters.AddWithValue("@n", comboBox2.SelectedItem);


                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {

                    MessageBox.Show("تم تعديل الخصم");
                   comboBox2.SelectedItem = "";
                    comboBox1.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }

                else
                {
                    MessageBox.Show("خطأ في الادخال");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("خطأ في ادخال قيمة الخصم");
            }
            con.CloseConnection();
        }

       

        private void deductions_Load(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
                comboBox2.Items.Clear();

            dr = con.DataReader("select name  from employee  ");
            while (dr.Read())
                comboBox2.Items.Add(dr[0].ToString());
            dr.Close();
        }

      
        

       
    }
}

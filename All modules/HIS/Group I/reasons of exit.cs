﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace HIS
{
    public partial class reasons_of_exit : Form
    {
        Connection con1 = new Connection();
        
        public reasons_of_exit()
        {
            
            InitializeComponent();

        }
        //**************************************الخروج*******************************************************************
        private void btn_exit_Click(object sender, EventArgs e)
        {
            //perant.Show();
            this.Close();
        }
        //**************************************Display function*********************************************************
        public void dis_data()
        {
            con1.OpenConection();
            dataGridView2.DataSource = (DataTable)con1.ShowDataInGridViewUsingStoredProc("viewingreasons_of_exit");
           
        }
        //****************************************************************************************************************
        private void reasons_of_exit_Load(object sender, EventArgs e)
        {
            dis_data();
        }
        //**************************************الاضافة*******************************************************************
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                con1.OpenConection();
                SqlDataReader dr;
                //****************** check empty fields*************************
                if (txtId.Text == "" || txtarb_des.Text == "" || txteng_des.Text == "")
                {
                    MessageBox.Show("بعض الحقول فارغة الرجاء اعادة المحاولة");
                    txtId.Text = "";
                    txtarb_des.Text = "";
                    txteng_des.Text = "";
                    cb_patientstatus.Text = "";
                    cb_type.Text = "";
                    chb_defualt.Checked = false;

                }
                //**********************check dublicated ID*********************
                else
                {
                    con1.OpenConection();

                    dr = (SqlDataReader)con1.DataReader("select Id from reasons_of_exit where Id=" + txtId.Text);
                    if (dr.HasRows)
                    {
                        MessageBox.Show(" هذا الكود موجود بالفعل حاول مرة اخرى");
                        txtId.Text = "";
                        txtarb_des.Text = "";
                        txteng_des.Text = "";
                        cb_patientstatus.Text = "";
                        cb_type.Text = "";
                        chb_defualt.Checked = false;
                    }
                    //**********************insert into database****************
                    else
                    {
                        dr.Close();
                        string[] paramname = new string[] { "@param1", "@param2", "@param3", "@param4", "@param5", "@param6" };
                        string[] paramvalue = new string[] { txtId.Text, txtarb_des.Text, txteng_des.Text, cb_patientstatus.Text, cb_type.Text, chb_defualt.Checked.ToString() };
                        SqlDbType[] paramtype = new SqlDbType[] { SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Bit };
                        con1.ExecuteInsertOrUpdateOrDeleteUsingStoredProc("addingreasons_of_exit1", paramname, paramvalue, paramtype);
                        txtId.Text = "";
                        txtarb_des.Text = "";
                        txteng_des.Text = "";
                        cb_patientstatus.Text = "";
                        cb_type.Text = "";
                        chb_defualt.Checked = false;
                        dis_data();
                        MessageBox.Show("تمت الاضافة");
                    }
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        //**********************************************الحذف **********************************************************
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txtarb_des.Text == "")
            { MessageBox.Show("من فضلك اختر الصف المراد حذفه ", "تنبيه"); }
            else
            {
                DialogResult dialogResult = MessageBox.Show("هل انت متأكد من الحذف ؟ ", "تنبيه ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string id = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    string[] paramname = new string[] { "@param1" };
                    string[] paramvalue = new string[] { txtarb_des.Text };
                    SqlDbType[] paramtype = new SqlDbType[] { SqlDbType.NVarChar };
                    con1.ExecuteInsertOrUpdateOrDeleteUsingStoredProc("deletingreasons_of_exit", paramname, paramvalue, paramtype);
                    txtId.Text = "";
                    txtarb_des.Text = "";
                    txteng_des.Text = "";
                    cb_patientstatus.Text = "";
                    cb_type.Text = "";
                    chb_defualt.Checked = false;
                    dis_data();

                }
                if (dialogResult == DialogResult.No)
                {
                    dialogResult = DialogResult.Cancel;
                    txtId.Text = "";
                    txtarb_des.Text = "";
                    txteng_des.Text = "";
                    cb_patientstatus.Text = "";
                    cb_type.Text = "";
                    chb_defualt.Checked = false;
                }

            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
             try
            {
                int i = dataGridView2.CurrentCell.RowIndex;
                txtId.Text = dataGridView2.Rows[i].Cells["الكود"].Value.ToString();
                txteng_des.Text = dataGridView2.Rows[i].Cells["الاسم اللاتينى"].Value.ToString();
                txtarb_des.Text = dataGridView2.Rows[i].Cells["الاسم العربى"].Value.ToString();
                cb_patientstatus.Text=dataGridView2.Rows[i].Cells["حالة المريض"].Value.ToString();
                cb_type.Text=dataGridView2.Rows[i].Cells["النوع"].Value.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        //******************************validate numbers*****************************************************************
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar))
            {
                btn_add.Enabled = false;
                btn_delete.Enabled = false;
                
                int visabletime = 1000;
                ToolTip t1 = new ToolTip();
                t1.Show("الكود يجـب ان يكون رقم", txtId, 0, 0, visabletime);
                e.Handled = true;
            }
            else
            {
                btn_add.Enabled = true;
                btn_delete.Enabled = true;
                
            }
        }
        //******************************validate letters*****************************************************************
        private void txtarb_des_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                btn_add.Enabled = false;
                btn_delete.Enabled = false;
                
                int visabletime = 1000;
                ToolTip t1 = new ToolTip();
                t1.Show(" الاسم العربى يجـب ان يكون مكون من حروف فقط", txtarb_des, 0, 0, visabletime);
                e.Handled = true;
            }
            else
            {
                btn_add.Enabled = true;
                btn_delete.Enabled = true;
               
            }
        }
        //******************************validate letters*****************************************************************
        private void txteng_des_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                btn_add.Enabled = false;
                btn_delete.Enabled = false;
                
                int visabletime = 1000;
                ToolTip t1 = new ToolTip();
                t1.Show(" الاسم اللاتينى يجـب ان يكون مكون من حروف فقط", txteng_des, 0, 0, visabletime);
                e.Handled = true;
            }
            else
            {
                btn_add.Enabled = true;
                btn_delete.Enabled = true;

            }
        }
      
    }
}

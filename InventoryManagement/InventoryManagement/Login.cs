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

namespace InventoryManagement
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gonza\source\repos\InventoryManagement\InventoryManagement\Inv.mdf;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlCommand cmd=con.CreateCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "Select * from registration where username='"+ textuser.Text +"'and password ='" + textpass.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

                if(i == 0)
                {
                MessageBox.Show("This user and password  doesn't match! ");
                }
            else
            {
                this.Hide();
                MDIParent1 mdi = new MDIParent1();
                mdi.Show();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
                con.Open(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textpass.PasswordChar == '*')
            {
                textpass.PasswordChar = '\0';
            }
            else if (textpass.PasswordChar == '\0')
            {
                textpass.PasswordChar = '*';
            }




        }
    }
}

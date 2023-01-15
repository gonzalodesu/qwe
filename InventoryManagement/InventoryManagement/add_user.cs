using System;
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
    public partial class add_user : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gonza\source\repos\InventoryManagement\InventoryManagement\Inv.mdf;Integrated Security=True");
        public add_user()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            int i = 0;

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from registration where username='" + textBoxUname.Text + "'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into registration values('"+ textBoxFname.Text +"','"+ textBoxLname.Text +"','"+ textBoxUname.Text +"','"+ textBoxPass.Text +"','"+ textBoxEmail.Text +"','"+ textBoxPhone.Text +"')";
                cmd1.ExecuteNonQuery();


                textBoxFname.Text = ""; textBoxLname.Text = ""; textBoxUname.Text = "";
                textBoxPass.Text = ""; textBoxEmail.Text = ""; textBoxPhone.Text = "";
                display();
                MessageBox.Show("User has been successfully created!");
            } 
            else
            {
                MessageBox.Show("The username must be unique  ");
            }
        }

        private void add_user_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
                con.Open();
                display();
        }

        public void display()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from registration ";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int id;
            id=Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString()) ;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from registration where id ="+ id +"";
            cmd.ExecuteNonQuery();

            display();  
        }
    }
}

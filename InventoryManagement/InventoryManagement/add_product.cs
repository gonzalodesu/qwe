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
    
    public partial class add_product : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gonza\source\repos\InventoryManagement\InventoryManagement\Inv.mdf;Integrated Security=True");
        public add_product()
        {
            InitializeComponent();
        }

        private void add_product_Load(object sender, EventArgs e)
        {
            if(con.State== ConnectionState.Open)
            {
                con.Close();
            }
                con.Open();
            fill_dd();
            fill_dc();
        }
        public void fill_dd()
        {
            comboBoxU.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from units";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBoxU.Items.Add(dr["unit"].ToString());

            }

        }
        public void fill_dc()
        {
             
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into product_name values('"+ textBoxPname.Text +"','"+ comboBoxU.SelectedItem.ToString() +"')";
            cmd.ExecuteNonQuery();

            textBoxPname.Text = "";
            fill_dc();
            MessageBox.Show("Successfully Added!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            panel2.Visible = true;
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            comboBoxUs.Items.Clear();
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from units";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter d2a = new SqlDataAdapter(cmd2);
            d2a.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBoxUs.Items.Add(dr2["unit"].ToString());

            }

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from product_name where id=" + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBoxPm.Text = dr["product_name"].ToString();
                comboBoxUs.SelectedItem = dr["units"].ToString();

            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());


            MessageBox.Show(i.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update product_name set product_name='"+ textBoxPm.Text +"', units='"+ comboBoxUs.SelectedItem.ToString() +"' where id=" + i + "";
            cmd.ExecuteNonQuery();
            panel2.Visible = false;
            fill_dc();
        }
    }
}

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
using System.Configuration;

namespace Shaurya_Connections
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Employee values(@name,@salary)";//write query that needs to fire
                cmd = new SqlCommand(qry, con);//assign qry to command
                cmd.Parameters.AddWithValue("@name", txtName.Text);//assign parameters value
                cmd.Parameters.AddWithValue("@salary",Convert.ToDouble(txtSalary.Text));
                con.Open();//open connection to fire query
                int result = cmd.ExecuteNonQuery();//fire the query
                if (result == 1)
                {
                    MessageBox.Show("Success ! Record Inserted");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }


        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Employee set Name=@name, Salary=@salary where id=@id";//write query that needs to fire
                cmd = new SqlCommand(qry, con);//assign qry to command
                cmd.Parameters.AddWithValue("@name", txtName.Text);//assign parameters value
                cmd.Parameters.AddWithValue("@salary",Convert.ToDouble (txtSalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32( txtId.Text));
                con.Open();//open connection to fire query
                int result = cmd.ExecuteNonQuery();//fire the query
                if (result == 1)
                {
                    MessageBox.Show("Success ! Record Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete  from Employee  where id=@id";//write query that needs to fire
                cmd = new SqlCommand(qry, con);//assign qry to command
                
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();//open connection to fire query
                int result = cmd.ExecuteNonQuery();//fire the query
                if (result == 1)
                {
                    MessageBox.Show("Success ! Record Deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtId.Clear();
                txtName.Clear();
                txtSalary.Clear();
            }
            finally
            {
                con.Close();

            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from Employee ";
                cmd = new SqlCommand(qry, con);
                
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    EmpGridView.DataSource = dt;
                    
                   
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();

            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

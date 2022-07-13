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
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllEmps()
        {
            da = new SqlDataAdapter("select * from Employee", con);
           
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey; // assign PK to the col which in the DataSet
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // emp is a table name given which is in the DataSet
            da.Fill(ds, "emp");// Fill method fire the select qry
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].NewRow();
                row["Name"] = txtName.Text;
                row["Salary"] = txtSalary.Text;
               
                ds.Tables["emp"].Rows.Add(row);// attach the new row to the ds
                
                int result = da.Update(ds.Tables["emp"]);//update() will reflect the changes to the DB
                if (result == 1)
                {
                    MessageBox.Show("Success ! Recored inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);//use method row.find to find which row wnts to update
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Salary"] = txtSalary.Text;
                    
                   
                    int result = da.Update(ds.Tables["emp"]); //update() will reflect the changes to the DB
                    if (result == 1)
                    {
                        MessageBox.Show("Success ! Recored Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();//delete row
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Success ! Recored Deleted");
                        txtId.Clear();
                        txtName.Clear();
                        txtSalary.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmps();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {

                    txtName.Text = row["Name"].ToString();
                    txtSalary.Text = row["Salary"].ToString();

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
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetAllEmps();
            EmpGridView.DataSource = ds.Tables["emp"];


        }
    }
}


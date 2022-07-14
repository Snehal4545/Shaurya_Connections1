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
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form3()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllStud()
        {
            da = new SqlDataAdapter("select * from Students", con);

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey; // assign PK to the col which in the DataSet
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // emp is a table name given which is in the DataSet
            da.Fill(ds, "Stud");// Fill method fire the select qry
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["Stud"].NewRow();
                row["Name"] = txtName.Text;
                row["Percentage"] = txtPercentage.Text;

                ds.Tables["Stud"].Rows.Add(row);// attach the new row to the ds

                int result = da.Update(ds.Tables["Stud"]);//update() will reflect the changes to the DB
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
                ds = GetAllStud();
                DataRow row = ds.Tables["Stud"].Rows.Find(txtId.Text);//use method row.find to find which row wnts to update
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Percentage"] = txtPercentage.Text;


                    int result = da.Update(ds.Tables["Stud"]); //update() will reflect the changes to the DB
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
                ds = GetAllStud();
                DataRow row = ds.Tables["Stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();//delete row
                    int result = da.Update(ds.Tables["Stud"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Success ! Recored Deleted");
                        txtId.Clear();
                        txtName.Clear();
                        txtPercentage.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetAllStud();
            StudGridView.DataSource = ds.Tables["Stud"];

        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllStud();
                DataRow row = ds.Tables["Stud"].Rows.Find(txtId.Text);
                if (row != null)
                {

                    txtName.Text = row["Name"].ToString();
                    txtPercentage.Text = row["Percentage"].ToString();

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
    }
}

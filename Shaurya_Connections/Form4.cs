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
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form4()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllProd()
        {
            da = new SqlDataAdapter("select * from Product", con);

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey; // assign PK to the col which in the DataSet
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            // emp is a table name given which is in the DataSet
            da.Fill(ds, "Prod");// Fill method fire the select qry
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProd();
                DataRow row = ds.Tables["Prod"].NewRow();
                row["Name"] = txtName.Text;
                row["Price"] = txtPrice.Text;

                ds.Tables["Prod"].Rows.Add(row);// attach the new row to the ds

                int result = da.Update(ds.Tables["Prod"]);//update() will reflect the changes to the DB
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
                ds = GetAllProd();
                DataRow row = ds.Tables["Prod"].Rows.Find(txtId.Text);//use method row.find to find which row wnts to update
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Price"] = txtPrice.Text;


                    int result = da.Update(ds.Tables["Prod"]); //update() will reflect the changes to the DB
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
                ds = GetAllProd();
                DataRow row = ds.Tables["Prod"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();//delete row
                    int result = da.Update(ds.Tables["Prod"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Success ! Recored Deleted");
                        txtId.Clear();
                        txtName.Clear();
                        txtPrice.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchId_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllProd();
                DataRow row = ds.Tables["Prod"].Rows.Find(txtId.Text);
                if (row != null)
                {

                    txtName.Text = row["Name"].ToString();
                    txtPrice.Text = row["Price"].ToString();

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

        private void ProdGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ds = GetAllProd();
            ProdGridView.DataSource = ds.Tables["Prod"];
        }
    }
}

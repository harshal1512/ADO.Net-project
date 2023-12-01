using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm;
using Payrole;

namespace PayrollMangSystem
{
   
    public partial class Employee_Details : BaseForm
    {
        string gender;
        DAL dal = new DAL();
        bool recfnd = false;
        public Employee_Details()
        {
            InitializeComponent();
        }

        private void txtempid_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                dal.isPROcall = true;
                dal.ClearParameters();
                dal.AddParameters("action", "select");
                dal.AddParameters("empid", txtempid.Text);
                SqlDataReader rdr = dal.GetReader("sp_emp_details");
                dal.isPROcall = false;
                if (rdr != null && rdr.HasRows)
                {
                    recfnd = true;
                    rdr.Read();
                    txtempname.Text = rdr["emp_name"].ToString();
                    dtpdob.Text = rdr["dob"].ToString();
                    rtxtaddress.Text = rdr["emp_address"].ToString();
                    txtmonsal.Text = rdr["monthly_salary"].ToString();

                }
                else
                {
                    txtempname.Text = "";
                    rdomale.Checked = true;
                    dtpdob.Text = "";
                    rtxtaddress.Text = "";
                    txtmonsal.Text = "";

                }
                rdr.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do u want to save ?", "SaveEmp", MessageBoxButtons.YesNo) == DialogResult.No) ;
                return;

            if (rdomale.Checked == true) 
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }

            //string Query = $"insert into tbl_employee_details values( '{txtempid.Text}','{txtempname.Text}', '{gender}','{dtpdob.Text}', '{rtxtaddress.Text}','{txtmonsal.Text}')";
            dal.isPROcall = true;
            dal.ClearParameters();
            dal.AddParameters("empid", txtempid.Text);
            dal.AddParameters("empname", txtempname.Text);
            dal.AddParameters("gender", gender);
            dal.AddParameters("dob", dtpdob.Text);
            dal.AddParameters("empaddress", rtxtaddress.Text);
            dal.AddParameters("monthlysalary", txtmonsal.Text);
            
            if (!recfnd)
            {
                dal.AddParameters("action", "insert");
                MessageBox.Show("Record save successfully!!");
            }
            else
            {
                dal.AddParameters("action", "update");
                MessageBox.Show("Record updated successfully!!");
            }
            try
            {
                int res = dal.ExecuteQuery("sp_emp_details");
                if (res > 0)
                {
                    recfnd = false;
                    MessageBox.Show("Record save successfully!!");

                }
                dal.isPROcall = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         }

        private void btncancel_Click(object sender, EventArgs e)
        {
            ClearControls(txtempid, Controls);
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do u want to delete ?", "DeleteEmp", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                if (recfnd)
                {
                    dal.isPROcall = true;
                    dal.ClearParameters();
                    dal.AddParameters("action", "delete");
                    dal.AddParameters("empid", txtempid.Text);
                    int res = dal.ExecuteQuery("sp_emp_details");
                    dal.isPROcall = false;

                    if (res > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                        ClearControls(txtempid, Controls);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            new selection().ShowDialog();
        }
    }
}

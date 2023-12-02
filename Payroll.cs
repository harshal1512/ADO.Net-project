using LoginForm;
using PayrollMangSystem;
using System.Data.SqlClient;

namespace Payrole
{
    public partial class Payroll : BaseForm 
    {
        Employee_Details ed = new Employee_Details();   
        DAL dal = new DAL();
        bool recfnd = false;
        public Payroll()
        {
            InitializeComponent();
            cmbmonth.SelectedIndex = 0;
        }
        private void txtempid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                dal.isPROcall = true;
                dal.ClearParameters();
                dal.AddParameters("action", "select2");
                dal.AddParameters("empid", txtempid.Text);
                SqlDataReader rdr = dal.GetReader("sp_emp");
                dal.isPROcall = false;
                if (rdr != null && rdr.HasRows)
                {
                    recfnd = true;
                    rdr.Read();
                    txtempname.Text = rdr["emp_name"].ToString();
                    txtdepart.Text = rdr["department_name"].ToString();
                    txtdesign.Text = rdr["designation"].ToString();
                    txtmonsal.Text = rdr["monthly_salary"].ToString();
                }
                else
                {
                    txtempname.Text = "";
                    txtdepart.Text = "";
                    txtdesign.Text = "";
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
            if (MessageBox.Show("Do you want to save?", "Save Payroll",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            dal.isPROcall = true;
            dal.ClearParameters();
            dal.AddParameters("empid", txtempid.Text);
            dal.AddParameters("empname", txtempname.Text);
            dal.AddParameters("departmentname", txtdepart.Text);
            dal.AddParameters("designation", txtdesign.Text);
            dal.AddParameters("monthlysalary", txtmonsal.Text);
            dal.AddParameters("month", cmbmonth.Text);
            dal.AddParameters("totalworkingdays", txttworkday.Text);
            dal.AddParameters("paidholidays", txtpholi.Text);
            dal.AddParameters("unpaidholidays", txtunpholi.Text);
            dal.AddParameters("totalattendance", txttattend.Text);      
            dal.AddParameters("currentmonsal", txtcsal.Text);

            if (recfnd)
            {
                dal.AddParameters("action", "insert2");
            }

            try
            {
                int res = dal.ExecuteQuery("sp_emp");
                if (res > 0)
                {
                    recfnd = false;
                    MessageBox.Show("Record Saved Successfully");
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

       

        private void txttattend_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtcsal.Text = ((Class2.CInt(txtmonsal.Text) / Class2.CInt(txttworkday.Text)) *
                Class2.CInt(txttattend.Text)).ToString();
        }

        private void txtunpholi_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txttattend.Text = (Class2.CInt(txttworkday.Text) - Class2.CInt(txtunpholi.Text)).ToString();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            new selection().ShowDialog();
        }
    }
}
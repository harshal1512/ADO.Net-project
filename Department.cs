using LoginForm;
using Payrole;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace employeepayroll_project
{
    public partial class DEPARTMENT : Form
    {
        DAL dal = new DAL();
        bool recfnd = false;
        public DEPARTMENT()
        {
            InitializeComponent();
            cmbdept.SelectedIndex = 0;
        }
        private void Control_Enter(object sender, EventArgs e)
        {
            Control cltr = (Control)sender;
            cltr.BackColor = Color.Yellow;
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            Control cltr = (Control)sender;
            cltr.BackColor = Color.White;
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ct = (Control)sender;

            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}");
            }
            if (e.KeyChar == 27)
            {
                SendKeys.Send("+{TAB}");
            }
            if (ct.Tag != null && ct.Tag.ToString() == "int")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b'))
                {
                    e.KeyChar = (char)0;
                    //  MessageBox.Show("invalid student id");
                }

            }
            else if (ct.Tag != null && ct.Tag.ToString() == "string")
            {
                string validstr = "abcdefghijklmnopqrstuvwxyz \b.@/";
                if (!validstr.Contains(e.KeyChar.ToString().ToLower()))
                {
                    e.KeyChar = (char)0;
                    // MessageBox.Show("Name should contain only letters");
                }
                if (ct.Text.Split(' ').Length > 2 && e.KeyChar == ' ')
                {
                    e.KeyChar = (char)0;
                }

                if (ct.Text.Length > 0 && ct.Text.Substring(ct.Text.Length - 1) == " " && e.KeyChar == ' ')
                {
                    e.KeyChar = (char)0;
                }
            }
        }
        private void ClearControl(Control FocusControl, Control.ControlCollection ctrl, bool ClearFocusControl = true)
        {
            string FocusControlValue = "";
            if (!ClearFocusControl)
                FocusControlValue = FocusControl.Text;

            foreach (Control item in ctrl)
            {
                if (item.Tag != null)
                    item.Text = "";

                if (item.GetType().Name == "ComboBox")
                {
                    ComboBox cb = (ComboBox)item;
                    cb.SelectedIndex = 0;
                }
            }

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            ClearControl(txtempid, Controls);
        }

        private void txtempid_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dal.isPROcall = true;
            dal.ClearParameters();
            dal.AddParameters("action", "select1");
            dal.AddParameters("empid", txtempid.Text);
            SqlDataReader rdr = dal.GetReader("sp_emp");
            dal.isPROcall = false;
            if (rdr != null && rdr.HasRows)
            {
                rdr.Read();
                txtempname.Text = rdr["emp_name"].ToString();
            }
            else
            {
                txtempname.Text = "";
                cmbdept.SelectedIndex = 0;
                txtdesg.Text = "";
            }
            rdr.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do u want to save?", "saveemp", MessageBoxButtons.YesNo) == DialogResult.No) 
            return;
            dal.isPROcall = true;
            dal.ClearParameters();
            dal.AddParameters("departmentname",cmbdept.Text);
            dal.AddParameters("designation", txtdesg.Text);
            
            dal.AddParameters("action", "update1");

            try
            {
                int res = dal.ExecuteQuery("sp_emp");
                if (res > 0)
                {
                    recfnd = false;
                    MessageBox.Show("Record saved successfully");
                }
                dal.isPROcall = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bth_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            new selection().ShowDialog();
        }
    }
}
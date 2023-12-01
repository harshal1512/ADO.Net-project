using employeepayroll_project;
using Payrole;
using PayrollMangSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class selection : Form
    {
        public selection()
        {
            InitializeComponent();
        }

        private void btn_empdetails_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Employee_Details().ShowDialog();
        }

        private void btn_payroll_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Payroll().ShowDialog();
        }

        private void btn_dept_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DEPARTMENT().ShowDialog();
        }
    }
}

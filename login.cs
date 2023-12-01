using System.Runtime.Intrinsics.Arm;
using System.Data.SqlClient;

namespace LoginForm
{
    public partial class LoginForm : BaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if(txt_userid.Text=="admin" && txt_password.Text=="admin@123")
            {
                this.Hide();
                new selection().ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter valid Userid and Password");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ClearControls(txt_userid,Controls);
            txt_userid.Focus();
        }


    }
}
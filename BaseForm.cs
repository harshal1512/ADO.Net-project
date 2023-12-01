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
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }
        public void Control_Enter(object sender, EventArgs e)
        {
            Control ctlr = (Control)sender;
            ctlr.BackColor = Color.Cyan;
        }

        public void Control_Leave(object sender, EventArgs e)
        {
            Control ctlr = (Control)sender;
            ctlr.BackColor = Color.White;
        }
        public void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctrl = (Control)sender;
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{TAB}"); // navigate the form using enter 
            }
            else if (e.KeyChar == 27)
            {
                SendKeys.Send("+{TAB}"); // navigate the form using escape
            }

            if (ctrl.Tag != null && ctrl.Tag.ToString() == "int")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b'))
                {
                    e.KeyChar = (char)0;
                }
            }
            else if (ctrl.Tag != null && ctrl.Tag.ToString() == "string")
            {
                string validstring = "abcdefghijklmnopqrstuvwxyz \b.@/-";

                if (!(validstring.Contains(e.KeyChar.ToString().ToLower())))
                {
                    e.KeyChar = (char)0;
                }
                if (ctrl.Text.Split(' ').Length > 2 && e.KeyChar == ' ')
                {
                    e.KeyChar = (char)0;
                }
                if (ctrl.Text.Length > 0 && ctrl.Text.Substring
                    (ctrl.Text.Length - 1) == " " && e.KeyChar == ' ')
                {
                    e.KeyChar = (char)0;
                }
            }
            else if (ctrl.Tag != null && ctrl.Tag.ToString() == "double")
            {
                if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b'
                 || e.KeyChar == '.'))
                {
                    e.KeyChar = (char)0;
                }
                if (ctrl.Text.Contains('.') && e.KeyChar == '.')
                {
                    e.KeyChar = (char)0;
                }
                int dotpos = (ctrl.Text.IndexOf('.'));
                if (dotpos >= 0 && e.KeyChar != '\b')
                {
                    if (ctrl.Text.Substring(dotpos).Length > 2)
                    {
                        e.KeyChar = (char)0;
                    }
                }
            }
        }
        public void ClearControls(Control FocusControl, Control.ControlCollection ctrl,
            bool CLearFocudControl = true)
        {
            string FocusControlValue = "";

            if (!CLearFocudControl)
            {
                FocusControlValue = FocusControl.Text;
            }
            foreach (Control item in ctrl)
            {
                if (item.Tag != null)
                {
                    item.Text = "";
                }
                if (item.GetType().Name == "ComboBox")
                {
                    ComboBox cmb = (ComboBox)item;
                    cmb.SelectedIndex = 0;
                }
                if (item.GetType().Name == "CheckBox")
                {
                    CheckBox chk = (CheckBox)item;
                    chk.Checked = false;
                }
                if(item.GetType().Name == "RadioButton")
                {
                    RadioButton rdo = (RadioButton)item;
                    rdo.Checked = false;
                }
            }
            if (!CLearFocudControl)
            {
                FocusControl.Text = FocusControlValue;
            }
            FocusControl.Focus();
        }
    }
}

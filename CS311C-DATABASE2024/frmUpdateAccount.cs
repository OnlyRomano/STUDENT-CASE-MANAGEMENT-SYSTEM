using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS311C_DATABASE2024
{
    public partial class frmUpdateAccount : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, editusername, editpassword, edittype, editstatus;
        private int errorcount;

        Class1 updateaccount = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        private void validateForm()
        {
            errorProvider1.Clear();
            errorcount = 0;
            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                errorProvider1.SetError(txtpassword, "password is empty");
                errorcount++;
            }

            if (txtpassword.TextLength < 6)
            {
                errorProvider1.SetError(txtpassword, "password must be at least 6 characters");
                errorcount++;
            }

            if (cmbusertype.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbusertype, "Select Usertype");
                errorcount++;
            }

            if (cmbstatus.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbstatus, "Select Status");
                errorcount++;
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validateForm();

            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Update this account?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        updateaccount.executeSQL("UPDATE tblaccounts SET password = '" + txtpassword.Text + "', usertype = '" + cmbusertype.Text.ToUpper() + "', status = '" + cmbstatus.Text.ToUpper() 
                            + "' WHERE username = '" + txtusername.Text + "'");
                        if (updateaccount.rowAffected > 0)
                        {
                            updateaccount.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Update','Accounts Management', '" + txtusername.Text + "', '" + username + "')");
                            MessageBox.Show("Account Updated", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error on Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmUpdateAccount(string editusername, string editpassword, string edittype, string editstatus, string username)
        {
            InitializeComponent();
            this.username = username;
            this.editusername = editusername;
            this.editpassword = editpassword; 
            this.edittype = edittype;
            this.editstatus = editstatus;
            panel1.MouseDown += new MouseEventHandler(Form_MouseDown);
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void frmUpdateAccount_Load(object sender, EventArgs e)
        {
            txtusername.Text = editusername;
            txtpassword.Text = editpassword;

            if (edittype == "ADMINISTRATOR")
            {
                    cmbusertype.SelectedIndex = 0;
            }
            else if (edittype == "BRANCH ADMINISTRATOR")
            {
                cmbusertype.SelectedIndex = 1;
            }
            else
            {
                cmbusertype.SelectedIndex = 2;
            }

            if (editstatus == "ACTIVE")
            {
                cmbstatus.SelectedIndex = 0;
            }
            else
            {
                cmbstatus.SelectedIndex = 1;
            }
        }

        private void cbshow_CheckedChanged(object sender, EventArgs e)
        {
            if (cbshow.Checked == true)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '*';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            txtpassword.Clear();
            cmbusertype.SelectedIndex = -1;
            cmbstatus.SelectedIndex = -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CS311C_DATABASE2024
{
    public partial class frmNewAccount : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;
        private int errorcount;

        Class1 newaccount = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        public frmNewAccount(string username)
        {
            InitializeComponent();
            this.username = username;
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

        private void validationForm()
        {
            errorProvider1.Clear();
            errorcount = 0;
            if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.SetError(txtusername, "username is empty");
                errorcount++;
            }
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
            try
            {
                DataTable dt = newaccount.GetData("SELECT * FROM tblaccounts WHERE username = '" + txtusername.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtusername, "Username already in use");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Validating on existing account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validationForm();
            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this account?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        newaccount.executeSQL("INSERT INTO tblaccounts (username, password, usertype, status, createdby, datecreated) VALUES ('" + txtusername.Text + "', '" + txtpassword.Text + "', '"
                            + cmbusertype.Text.ToUpper() + "', 'ACTIVE', '" + username + "', '" + DateTime.Now.ToShortDateString() + "')");
                        if (newaccount.rowAffected > 0)
                        {
                            newaccount.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + 
                                "', 'Add','Accounts Management', '" + txtusername.Text + "', '"  + username + "')" );
                            MessageBox.Show("New Account Added", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtusername.Clear();
            txtpassword.Clear();
            errorProvider1.Clear();
            cmbusertype.SelectedIndex = -1;
            txtusername.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();    
        }

        private void frmNewAccount_Load(object sender, EventArgs e)
        {
           
        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CS311C_DATABASE2024
{
    public partial class frmNewViolation : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string code;
        private int errorcount;

        Class1 newviolation = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        public frmNewViolation(string code)
        {
            InitializeComponent();
            this.code = code;
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

            if (string.IsNullOrEmpty(txtcode.Text))
            {
                errorProvider1.SetError(txtcode, "Code is empty");
                errorcount++;
            }
            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                errorProvider1.SetError(txtdescription, "Description is empty");
                errorcount++;
            }
            try
            {
                DataTable dt = newviolation.GetData("SELECT * FROM tblviolation WHERE code = '" + txtcode.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtcode, "Code already in use");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Validating on existing Violation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validationForm();
            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this Violation?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        newviolation.executeSQL("INSERT INTO tblviolation (code, description, status, createdby, datecreated) VALUES ('" + txtcode.Text + "', '" + txtdescription.Text + 
                            "', 'ACTIVE', '" + code + "', '" + DateTime.Now.ToShortDateString() + "')");
                        if (newviolation.rowAffected > 0)
                        {
                            newviolation.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Add','Violation Management', '" + txtcode.Text + "', '" + code + "')");
                            MessageBox.Show("New Violation Added", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtcode.Clear();
            txtdescription.Clear();
            errorProvider1.Clear();
            txtcode.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

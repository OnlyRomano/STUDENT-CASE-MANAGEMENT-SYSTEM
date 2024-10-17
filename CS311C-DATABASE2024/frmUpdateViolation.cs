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

namespace CS311C_DATABASE2024
{
    public partial class frmUpdateViolation : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, editcode, editdescription, edittype, editstatus;
        private int errorcount;

        Class1 updateViolation = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        public frmUpdateViolation(string editcode, string editdescription,string edittype, string editstatus, string username)
        {
            InitializeComponent();
            this.editcode = editcode;
            this.editdescription = editdescription;
            this.edittype = edittype;
            this.editstatus = editstatus;
            this.username = username;
            panel1.MouseDown += new MouseEventHandler(Form_MouseDown);
        }

        private void frmUpdateViolation_Load(object sender, EventArgs e)
        {
            txtcode.Text = editcode;
            txtdescription.Text = editdescription;

            if (edittype == "MINOR OFFENSE")
            {
                cmbtype.SelectedIndex = 0;
            }
            else
            {
                cmbtype.SelectedIndex = 1;
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

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtdescription.Clear();
            errorProvider1.Clear();
            cmbstatus.SelectedIndex = -1;
            cmbtype.SelectedIndex = -1;
            txtdescription.Focus();
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

            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                errorProvider1.SetError(txtdescription, "Description is empty");
                errorcount++;
            }
            if (cmbtype.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbtype, "Select Violation Type");
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
            validationForm();

            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Update this Violation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        updateViolation.executeSQL("UPDATE tblviolation SET code = '" + txtcode.Text + "', description = '" + txtdescription.Text + "', violation_type = '" + cmbtype.Text.ToUpper() + "', status = '" + cmbstatus.Text.ToUpper()
                            + "' WHERE code = '" + txtcode.Text + "'");
                        if (updateViolation.rowAffected > 0)
                        {
                            updateViolation.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Update','Violation Management', '" + txtcode.Text + "', '" + username + "')");
                            MessageBox.Show("Violation Updated", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}

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
    public partial class frmNewStrand : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string strandCode;
        private int errorcount;

        Class1 newstrand = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        public frmNewStrand(string strandCode)
        {
            InitializeComponent();
            this.strandCode = strandCode;
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

            if (string.IsNullOrEmpty(txtstrand_code.Text))
            {
                errorProvider1.SetError(txtstrand_code, "Strand Code is empty");
                errorcount++;
            }
            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                errorProvider1.SetError(txtdescription, "Description is empty");
                errorcount++;
            }
            try
            {
                DataTable dt = newstrand.GetData("SELECT * FROM tblstrands WHERE strand_code = '" + txtstrand_code.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtstrand_code, "Strand Code already in use");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Validating on existing Strand", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validationForm();
            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this Strand?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        newstrand.executeSQL("INSERT INTO tblstrands (strand_code, description, datecreated, createdby) VALUES ('" + txtstrand_code.Text + "', '" + txtdescription.Text + "', '"
                             + DateTime.Now.ToShortDateString() + "', '" + strandCode + "')");
                        if (newstrand.rowAffected > 0)
                        {
                            newstrand.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Add','Strand Management', '" + txtstrand_code.Text + "', '" + strandCode + "')");
                            MessageBox.Show("New Strand Added", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtstrand_code.Clear();
            txtdescription.Clear();
            errorProvider1.Clear();
            txtstrand_code.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

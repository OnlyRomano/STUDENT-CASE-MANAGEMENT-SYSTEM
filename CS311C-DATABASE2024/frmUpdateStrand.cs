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
    public partial class frmUpdateStrand : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, editstrand_code, editdescription;
        private int errorcount;

        Class1 updatestrand = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        public frmUpdateStrand(string editstrand_code, string editdescription, string username)
        {
            InitializeComponent();
            this.username = username;
            this.editstrand_code = editstrand_code;
            this.editdescription = editdescription;
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

        private void frmUpdateStrand_Load(object sender, EventArgs e)
        {
            txtstrand_code.Text = editstrand_code;
            txtdescription.Text = editdescription;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtdescription.Clear();
            errorProvider1.Clear();
        }

        private void validateForm()
        {
            errorProvider1.Clear();
            errorcount = 0;

            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                errorProvider1.SetError(txtdescription, "Description is empty");
                errorcount++;
            }
        }

        public event EventHandler StrandUpdate;

        private void btnsave_Click(object sender, EventArgs e)
        {
            validateForm();

            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Update this Strand?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        updatestrand.executeSQL("UPDATE tblstrands SET strand_code = '" + txtstrand_code.Text + "', description = '" + txtdescription.Text + "' WHERE strand_code = '" + txtstrand_code.Text + "'");
                        if (updatestrand.rowAffected > 0)
                        {
                            updatestrand.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Update','Strand Management', '" + txtstrand_code.Text + "', '" + username + "')");
                            MessageBox.Show("Strand Updated", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StrandUpdate?.Invoke(this, EventArgs.Empty);
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

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
    public partial class frmViolation : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;
        public frmViolation(string username)
        {
            InitializeComponent();
            this.username = username;
            panel2.MouseDown += new MouseEventHandler(Form_MouseDown);
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

        Class1 violation = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        private void frmViolation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cs311c2024DataSet6.tblviolation' table. You can move, or remove it, as needed.
            this.tblviolationTableAdapter.Fill(this.cs311c2024DataSet6.tblviolation);
            try
            {
                DataTable dt = violation.GetData("SELECT code, description, violation_type, status, createdby, datecreated FROM tblviolation WHERE code <> '" + username + "' ORDER BY code");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Violation Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = violation.GetData("SELECT code, description, violation_type, status, createdby, datecreated FROM tblviolation WHERE code <> '" + username +
                    "'AND (code LIKE '%" + txtsearch.Text + "%' OR description LIKE '%" + txtsearch.Text + "%' OR violation_type LIKE '%" + txtsearch.Text + "%') ORDER BY code");

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewViolation newViolation = new frmNewViolation(username);
            newViolation.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editcode = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string editdescription = dataGridView1.Rows[row].Cells[1].Value.ToString();
            string edittype = dataGridView1.Rows[row].Cells[2].Value.ToString();
            string editstatus = dataGridView1.Rows[row].Cells[3].Value.ToString();

            frmUpdateViolation updateViolation = new frmUpdateViolation(editcode, editdescription, edittype, editstatus, username);
            updateViolation.Show();
        }

        private int row;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = (int)e.RowIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Datagrid cellclick", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmViolation_Load(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string selectedViolation = dataGridView1.Rows[row].Cells[0].Value.ToString();
                try
                {
                    violation.executeSQL("DELETE FROM tblviolation WHERE code = '" + selectedViolation+ "'");
                    if (violation.rowAffected > 0)
                    {
                        violation.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                            "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete','Violation Management', '" + selectedViolation + "', '" + username + "')");
                        MessageBox.Show("Violation Deleted", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

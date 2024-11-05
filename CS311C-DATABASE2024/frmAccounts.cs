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
    public partial class frmAccounts : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;
        private Timer autorefresh;

        public frmAccounts(string username)
        {
            InitializeComponent();
            this.username = username;
            panel2.MouseDown += new MouseEventHandler(Form_MouseDown);
            panel1.MouseDown += new MouseEventHandler(Form_MouseDown);

            autorefresh = new Timer();
            autorefresh.Interval = 5000;
            autorefresh.Tick += AutoRefresh_Tick;
            autorefresh.Start();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        Class1 accounts = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        private void frmAccounts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cs311c2024DataSet.tblaccounts' table. You can move, or remove it, as needed.
            this.tblaccountsTableAdapter.Fill(this.cs311c2024DataSet.tblaccounts);
            try
            {
                DataTable dt = accounts.GetData("SELECT username, password, usertype, status, createdby, datecreated FROM tblaccounts WHERE username <> '" + username + "' ORDER BY username");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Accounts Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = accounts.GetData("SELECT username, password, usertype, status, createdby, datecreated FROM tblaccounts WHERE username <> '" + username +
                    "'AND (username LIKE '%" + txtsearch.Text + "%' OR usertype LIKE '%" + txtsearch.Text + "%') ORDER BY username");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewAccount newaccountform = new frmNewAccount(username);
            newaccountform.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmAccounts_Load(sender, e);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string selectedUser = dataGridView1.Rows[row].Cells[0].Value.ToString();
                try
                {
                    accounts.executeSQL("DELETE FROM tblaccounts WHERE username = '" + selectedUser + "'");
                    if (accounts.rowAffected > 0)
                    {
                        accounts.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                            "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete','Accounts Management', '" + selectedUser + "', '" + username + "')");
                        MessageBox.Show("Account Deleted", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                string editusername = dataGridView1.Rows[selectedIndex].Cells[0].Value.ToString();
                string editpassword = dataGridView1.Rows[selectedIndex].Cells[1].Value.ToString();
                string edittype = dataGridView1.Rows[selectedIndex].Cells[2].Value.ToString();
                string editstatus = dataGridView1.Rows[selectedIndex].Cells[3].Value.ToString();

                frmUpdateAccount updateaccountfrm = new frmUpdateAccount(editusername, editpassword, edittype, editstatus, username);
                updateaccountfrm.Show();
            }
            else
            {
                MessageBox.Show("Please select an account to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AutoRefresh_Tick(object sender, EventArgs e)
        {
            frmAccounts_Load(sender, e);
        }
    }
}
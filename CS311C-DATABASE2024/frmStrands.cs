using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS311C_DATABASE2024
{
    public partial class frmStrands : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;

        private Timer autorefresh;
        public frmStrands(string username)
        {
            InitializeComponent();
            this.username = username;
            panel2.MouseDown += new MouseEventHandler(Form_MouseDown);

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

        Class1 strand = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        private void frmStrands_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cs311c2024DataSet1.tblstrands' table. You can move, or remove it, as needed.
            this.tblstrandsTableAdapter.Fill(this.cs311c2024DataSet1.tblstrands);
            try
            {
                DataTable dt = strand.GetData("SELECT strand_code, description, datecreated, createdby FROM tblstrands WHERE strand_code <> '" + username + "' ORDER BY strand_code");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Strand Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = strand.GetData("SELECT strand_code, description, datecreated, createdby FROM tblstrands WHERE strand_code <> '" + username +
                    "'AND (strand_code LIKE '%" + txtsearch.Text + "%' OR description LIKE '%" + txtsearch.Text + "%') ORDER BY strand_code");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmStrands_Load(sender, e);
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
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete this course?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string selectedStrand = dataGridView1.Rows[row].Cells[0].Value.ToString();
                try
                {
                    strand.executeSQL("DELETE FROM tblstrands WHERE strand_code = '" + selectedStrand + "'");
                    if (strand.rowAffected > 0)
                    {
                        strand.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                            "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete','Strand Management', '" + selectedStrand + "', '" + username + "')");
                        MessageBox.Show("Strand Deleted", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewStrand strandnewfrm = new frmNewStrand(username);
            strandnewfrm.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editstrand_code = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string editdescription = dataGridView1.Rows[row].Cells[1].Value.ToString();

            frmUpdateStrand updatestrandfrm = new frmUpdateStrand(editstrand_code, editdescription, username);
            updatestrandfrm.Show();
        }

        private void AutoRefresh_Tick(object sender, EventArgs e)
        {
            frmStrands_Load(sender, e);
        }
    }
}

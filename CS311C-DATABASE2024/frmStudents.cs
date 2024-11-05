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
    public partial class frmStudents : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;
        private Timer autorefresh;
        public frmStudents(string username)
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

        Class1 students = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        private void frmStudents_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cs311c2024DataSet3.tblstudents' table. You can move, or remove it, as needed.
            this.tblstudentsTableAdapter.Fill(this.cs311c2024DataSet3.tblstudents);
            try
            {
                DataTable dt = students.GetData("SELECT studentID, lastname, firstname, middlename, Level, strand_course, createdby, datecreated FROM tblstudents WHERE studentID <> '" + username + "' ORDER BY studentID");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Students Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = students.GetData("SELECT studentID, lastname, firstname, middlename, level, strand_course, createdby, datecreated FROM tblstudents WHERE studentID <> '" + username +
                    "'AND (studentID LIKE '%" + txtsearch.Text + "%' OR lastname LIKE '%" + txtsearch.Text + "%' OR strand_course LIKE '%" + txtsearch.Text + "%') ORDER BY studentID");

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewStudent newstudentfrm = new frmNewStudent(username);
            newstudentfrm.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmStudents_Load(sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string updatestudentID = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string updatelastname = dataGridView1.Rows[row].Cells[1].Value.ToString();
            string updatefirstname = dataGridView1.Rows[row].Cells[2].Value.ToString();
            string updatedmiddlename = dataGridView1.Rows[row].Cells[3].Value.ToString();
            string updatelevel = dataGridView1.Rows[row].Cells[4].Value.ToString();
            string updatestrand_course = dataGridView1.Rows[row].Cells[5].Value.ToString();

            frmUpdateStudent updatestudentfrm = new frmUpdateStudent(updatestudentID, updatelastname, updatefirstname, updatedmiddlename, updatelevel, updatestrand_course, username);
            updatestudentfrm.Show();
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
            DialogResult dr = MessageBox.Show("Are you sure you want to Delete this Student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string selectedStudent = dataGridView1.Rows[row].Cells[0].Value.ToString();
                try
                {
                    students.executeSQL("DELETE FROM tblstudents WHERE studentID = '" + selectedStudent + "'");
                    if (students.rowAffected > 0)
                    {
                        students.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                            "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete','Students Management', '" + selectedStudent + "', '" + username + "')");
                        MessageBox.Show("Student Deleted", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void AutoRefresh_Tick(object sender, EventArgs e)
        {
            frmStudents_Load(sender, e);
        }
    }
}

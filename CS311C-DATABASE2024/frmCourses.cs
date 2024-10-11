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
    public partial class frmCourses : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username;
        public frmCourses(string username)
        {
            InitializeComponent();
            this.username = username;
            panel2.MouseDown += new MouseEventHandler(Form_MouseDown);
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        Class1 course = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        private void frmCourses_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cs311c2024DataSet2.tblcourses' table. You can move, or remove it, as needed.
            this.tblcoursesTableAdapter.Fill(this.cs311c2024DataSet2.tblcourses);
            try
            {
                DataTable dt = course.GetData("SELECT course_code, description, datecreated, createdby FROM tblcourses WHERE course_code <> '" + username + "' ORDER BY course_code");
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
                DataTable dt = course.GetData("SELECT course_code, description, datecreated, createdby FROM tblcourses WHERE course_code <> '" + username +
                    "'AND (course_code LIKE '%" + txtsearch.Text + "%' OR description LIKE '%" + txtsearch.Text + "%') ORDER BY course_code");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on  Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewCourse newcoursefrm = new frmNewCourse(username);
            newcoursefrm.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            frmCourses_Load(sender, e);
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
                string selectedCourse = dataGridView1.Rows[row].Cells[0].Value.ToString();
                try
                {
                    course.executeSQL("DELETE FROM tblcourses WHERE course_code = '" + selectedCourse + "'");
                    if (course.rowAffected > 0)
                    {
                        course.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                            "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete','Courses Management', '" + selectedCourse + "', '" + username + "')");
                        MessageBox.Show("Course Deleted", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string editcourse_code = dataGridView1.Rows[row].Cells[0].Value.ToString();
            string editdescription = dataGridView1.Rows[row].Cells[1].Value.ToString();

            frmUpdateCourse updatecoursefrm = new frmUpdateCourse(editcourse_code, editdescription, username);
            updatecoursefrm.Show();
        }
    }
}

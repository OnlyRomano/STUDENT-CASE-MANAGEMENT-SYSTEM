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
    public partial class frmNewCourse : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string courseCode;
        private int errorcount;

        Class1 newcourse = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        public frmNewCourse(string courseCode)
        {
            InitializeComponent();
            this.courseCode = courseCode;
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

            if (string.IsNullOrEmpty(txtcourse_code.Text))
            {
                errorProvider1.SetError(txtcourse_code, "Course Code is empty");
                errorcount++;
            }
            if (string.IsNullOrEmpty(txtdescription.Text))
            {
                errorProvider1.SetError(txtdescription, "Description is empty");
                errorcount++;
            }
            try
            {
                DataTable dt = newcourse.GetData("SELECT * FROM tblcourses WHERE course_code = '" + txtcourse_code.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtcourse_code, "Course Code already in use");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Validating on existing Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validationForm();
            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this Course?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        newcourse.executeSQL("INSERT INTO tblcourses (course_code, description, datecreated, createdby) VALUES ('" + txtcourse_code.Text + "', '" + txtdescription.Text + "', '"
                             + DateTime.Now.ToShortDateString() + "', '" + courseCode + "')");
                        if (newcourse.rowAffected > 0)
                        {
                            newcourse.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Add','Courses Management', '" + txtcourse_code.Text + "', '" + courseCode + "')");
                            MessageBox.Show("New Course Added", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtcourse_code.Clear();
            txtdescription.Clear();
            errorProvider1.Clear();
            txtcourse_code.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewCourse_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class frmNewStudent : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string studentID;
        private int errorcount;
        Class1 newstudent = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        public frmNewStudent(string studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
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
            if (string.IsNullOrEmpty(txtstudentID.Text))
            {
                errorProvider1.SetError(txtstudentID, "studentID is empty");
                errorcount++;
            }
            if (string.IsNullOrEmpty(txtlastname.Text))
            {
                errorProvider1.SetError(txtlastname, "last name is empty");
                errorcount++;
            }

            if (string.IsNullOrEmpty(txtfirstname.Text))
            {
                errorProvider1.SetError(txtfirstname, "first name is empty");
                errorcount++;
            }

            if (cmblevel.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmblevel, "Select Level");
                errorcount++;
            }

            if (cmblevel.Text == "Senior High School" || cmblevel.Text == "College")
            {
                if (cmbstrandandcourse.SelectedIndex < 0)
                {
                    errorProvider1.SetError(cmbstrandandcourse, "Select Strand or Course");
                    errorcount++;
                }
            }

            try
            {
                DataTable dt = newstudent.GetData("SELECT * FROM tblstudents WHERE studentID = '" + txtstudentID.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtstudentID, "StudentID is already in use");
                    errorcount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Validating on existing student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetStrands()
        {
            try
            {
                // Only selecting strandcode
                return newstudent.GetData("SELECT strand_code, description FROM tblstrands");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error fetching strands", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private DataTable GetCourses()
        {
            try
            {
                // Only selecting coursecode
                return newstudent.GetData("SELECT course_code, description FROM tblcourses");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error fetching courses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void cmblevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbstrandandcourse.DataSource = null;  
            cmbstrandandcourse.SelectedIndex = -1;  
            cmbstrandandcourse.Enabled = false;

            if (cmblevel.SelectedItem != null)
            {
                string selectedLevel = cmblevel.SelectedItem.ToString();

                if (selectedLevel == "Elementary" || selectedLevel == "Junior High School")
                {
                    cmbstrandandcourse.Enabled = false;
                    cmbstrandandcourse.Items.Clear(); // Clear items
                    cmbstrandandcourse.Items.Add("N/A"); // Add "N/A"
                    cmbstrandandcourse.SelectedIndex = 0; // Select "N/A"
                }
                else if (selectedLevel == "Senior High School")
                {
                    DataTable dtStrands = GetStrands();
                    if (dtStrands != null && dtStrands.Rows.Count > 0 && dtStrands.Columns.Contains("strand_code") && dtStrands.Columns.Contains("description"))
                    {
                        cmbstrandandcourse.Enabled = true;
                        cmbstrandandcourse.DisplayMember = "description";  
                        cmbstrandandcourse.ValueMember = "strand_code";  
                        cmbstrandandcourse.DataSource = dtStrands;  
                        cmbstrandandcourse.SelectedIndex = -1;  
                    }
                    else
                    {
                        cmbstrandandcourse.Enabled = false; 
                        cmbstrandandcourse.DataSource = null; 
                        cmbstrandandcourse.Items.Clear(); 
                        cmbstrandandcourse.Items.Add("No strands found"); 
                        cmbstrandandcourse.SelectedIndex = 0; 
                    }
                }
                else if (selectedLevel == "College")
                {
                    DataTable dtCourses = GetCourses();
                    if (dtCourses != null && dtCourses.Rows.Count > 0 && dtCourses.Columns.Contains("course_code") && dtCourses.Columns.Contains("description"))
                    {
                        cmbstrandandcourse.Enabled = true;
                        cmbstrandandcourse.DisplayMember = "description"; 
                        cmbstrandandcourse.ValueMember = "course_code";  
                        cmbstrandandcourse.DataSource = dtCourses;  
                        cmbstrandandcourse.SelectedIndex = -1;  
                    }
                    else
                    {
                        cmbstrandandcourse.Enabled = false; 
                        cmbstrandandcourse.DataSource = null;     
                        cmbstrandandcourse.Items.Clear(); 
                        cmbstrandandcourse.Items.Add("No courses found");
                        cmbstrandandcourse.SelectedIndex = 0; 
                    }
                }
            }
        }

        public event EventHandler StudentAdd;

        private void btnsave_Click(object sender, EventArgs e)
        {
            validationForm();
            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this Student?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string strandCourseCode = (cmbstrandandcourse.SelectedIndex < 0 || cmbstrandandcourse.Text == "N/A") ? "N/A" : cmbstrandandcourse.SelectedValue.ToString();
                    try
                    {
                        newstudent.executeSQL("INSERT INTO tblstudents (studentID, lastname, firstname, middlename, level, strand_course, createdby, datecreated) VALUES ('" + txtstudentID.Text + "', '" + txtlastname.Text + "', '" + txtfirstname.Text + 
                            "', '" + txtmiddlename.Text + "', '" + cmblevel.Text.ToUpper() + "', '" + strandCourseCode.ToUpper() + "', '" + studentID + "', '" + DateTime.Now.ToShortDateString() + "')");
                        if (newstudent.rowAffected > 0)
                        {
                            newstudent.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Add','Students Management', '" + txtstudentID.Text + "', '" + studentID + "')");
                            MessageBox.Show("New Student Added", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StudentAdd?.Invoke(this, EventArgs.Empty);
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmblevel.SelectedIndexChanged -= cmblevel_SelectedIndexChanged;
            txtstudentID.Clear();
            txtlastname.Clear();
            txtfirstname.Clear();
            txtmiddlename.Clear();
            cmblevel.SelectedIndex = -1;
            cmbstrandandcourse.Enabled = false;
            cmbstrandandcourse.SelectedIndex = -1;
            errorProvider1.Clear();
            txtstudentID.Focus();
            cmblevel.SelectedIndexChanged += cmblevel_SelectedIndexChanged;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

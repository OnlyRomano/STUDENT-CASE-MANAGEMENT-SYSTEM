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
    public partial class frmUpdateStudent : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, updatestudentID, updatelastname, updatefirstname, updatemiddlname, updatelevel, updatestrand_course;

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtlastname.Clear();
            txtfirstname.Clear();
            txtmiddlename.Clear();
            cmblevel.SelectedIndex = -1;
            cmbstrandandcourse.SelectedIndex = -1;
            errorProvider1.Clear();
            txtlastname.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable GetStrands()
        {
            try
            {
                // Only selecting strandcode
                return updatestudent.GetData("SELECT strand_code, description FROM tblstrands");
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
                return updatestudent.GetData("SELECT course_code, description FROM tblcourses");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error fetching courses", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void frmUpdateStudent_Load(object sender, EventArgs e)
        {
            txtstudentID.Text = updatestudentID;
            txtlastname.Text = updatelastname;
            txtfirstname.Text = updatefirstname;
            txtmiddlename.Text = updatemiddlname;
            cmblevel.SelectedItem = updatelevel;
            cmblevel_SelectedIndexChanged(sender, e);
            if (cmbstrandandcourse.DataSource != null)
            {
                cmbstrandandcourse.SelectedValue = updatestrand_course;
            }
        }

        private void cmblevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbstrandandcourse.DataSource = null; 
            cmbstrandandcourse.SelectedIndex = -1;
            cmbstrandandcourse.Enabled = false; 
            cmbstrandandcourse.Items.Clear();

            if (cmblevel.SelectedItem != null)
            {
                string selectedLevel = cmblevel.SelectedItem.ToString();

                if (selectedLevel == "ELEMENTARY" || selectedLevel == "JUNIOR HIGH SCHOOL")
                {
                    cmbstrandandcourse.Enabled = false;
                    cmbstrandandcourse.Items.Add("N/A");
                    cmbstrandandcourse.SelectedIndex = 0;
                }
                else if (selectedLevel == "SENIOR HIGH SCHOOL")
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
                else if (selectedLevel == "COLLEGE")
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

        private int errorcount;
        Class1 updatestudent = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");

        public frmUpdateStudent(string updatestudentID, string updatelastname, string updatefirstname, string updatemiddlename, string updatelevel, string updatestrand_course, string username)
        {
            InitializeComponent();
            this.username = username;
            this.updatestudentID = updatestudentID;
            this.updatelastname = updatelastname;
            this.updatefirstname = updatefirstname;
            this.updatemiddlname = updatemiddlename;
            this.updatelevel = updatelevel;
            this.updatestrand_course = updatestrand_course;
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

        private void validateForm()
        {
            errorProvider1.Clear();
            errorcount = 0;

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
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validateForm();

            if (errorcount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Update this student?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string strandCourseCode = (cmbstrandandcourse.SelectedIndex < 0) ? "N/A" : cmbstrandandcourse.SelectedValue.ToString();

                    try
                    {
                        updatestudent.executeSQL("UPDATE tblstudents SET lastname = '" + txtlastname.Text + "', firstname = '" + txtfirstname.Text + "', middlename = '" + txtmiddlename.Text
                            + "', level = '" + cmblevel.Text.ToUpper() + "', strand_course = '" + strandCourseCode.ToUpper() + "' WHERE studentID = '" + txtstudentID.Text + "'");
                        if (updatestudent.rowAffected > 0)
                        {
                            updatestudent.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() +
                                "', 'Update','Students Management', '" + txtstudentID.Text + "', '" + username + "')");
                            MessageBox.Show("Student Updated", "Massage", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

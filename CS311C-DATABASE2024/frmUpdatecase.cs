﻿using CS311C_DATABASE2024;
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

namespace CS311_DATABASE_2024
{
    public partial class frmUpdatecase : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, caseID, studentID, lastname, firstname, middlename, level, strandcourse, vcode, description, vcount,status, action, schoolyear, concernlevel, disciplinary;

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtaction_TextChanged(object sender, EventArgs e)
        {

        }

        Class1 updatecase = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        private int errorCount;
        public frmUpdatecase(string username, string caseID,string studentID,string lastname,string firstname, string middlename, string level, string strandcourse, string vcode
            ,string description,string vcount,string status, string action, string schoolyear, string concernlevel, string disciplinary)
        {
            InitializeComponent();
            this.username = username;
            this.caseID = caseID;
            this.studentID = studentID;
            this.lastname = lastname;
            this.firstname = firstname;
            this.middlename = middlename;
            this.level = level;
            this.strandcourse = strandcourse;
            this.vcode = vcode;
            this.description = description;
            this.vcount = vcount;
            this.status = status;
            this.action = action;
            this.schoolyear = schoolyear;
            this.concernlevel = concernlevel;
            this.disciplinary = disciplinary;
            this.Load += frmUpdatecase_Load;  // Subscribing to the Load event
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
            errorCount = 0;
            if (cmbstatus.SelectedIndex < 1)
            {
                errorProvider1.SetError(cmbstatus, "Select status to resolved");
                errorCount++;
            }
            if (string.IsNullOrEmpty(txtschoolyear.Text))
            {
                errorProvider1.SetError(txtschoolyear, "School Year is Empty");
            }

            if (cmbconcern.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbconcern, "Select Concern Level");
                errorCount++;
            }

            if (string.IsNullOrEmpty(txtdesciplinary.Text))
            {
                errorProvider1.SetError(txtdesciplinary, "Disciplinary Action is Empty");
            }
        }

       private void validataAction()
        {
            errorProvider1.Clear();
            errorCount = 0;
            if (string.IsNullOrEmpty(txtaction.Text))
            {
                errorProvider1.SetError(txtaction, "description is empty");
                errorCount++;
            }
        }

        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if cmbstatus is selected and has a valid value
            if (cmbstatus.SelectedItem != null)
            {
                // Check if the selected value is "ONGOING" or "RESOLVED"
                if (cmbstatus.SelectedItem.ToString().ToUpper() == "ONGOING")
                {
                    txtaction.Enabled = false;  // Disable txtaction if status is "ONGOING"
                    txtaction.Clear();  // Optionally clear the action text
                }
                else
                {
                    txtaction.Enabled = true;  // Enable txtaction for other statuses
                }
            }
        }

        private void frmUpdatecase_Load(object sender, EventArgs e)
        {
            LoadViolationIDs();

            // Set the status value
            txtcaseid.Text = caseID;
            txtstudentid.Text = studentID;
            txtlastname.Text = lastname;
            txtfirstname.Text = firstname;
            txtmiddlename.Text = middlename;
            txtlevel.Text = level;
            txtstrandcourse.Text = strandcourse;

            cmbviolationid.SelectedItem = vcode;

            txtviolationdescription.Text = description;
            cmbviolationcount.SelectedItem = vcount;
            cmbstatus.SelectedItem = status;

            // Set the action text value
            txtaction.Text = action;
            txtschoolyear.Text = schoolyear;
            txtdesciplinary.Text = disciplinary;

            // Disable txtaction if the status is "ONGOING"
            if (status.ToUpper() == "ONGOING")
            {
                txtaction.Enabled = false;
                txtaction.Clear();  // Optionally clear the action text
            }
            else
            {
                txtaction.Enabled = true;
            }

            if (concernlevel == "PREPECT OF DISCIPLINE")
            {
                cmbconcern.SelectedIndex = 0;
            }
            else if (concernlevel == "BRANCH OSA")
            {
                cmbconcern.SelectedIndex = 1;
            }
            else if (concernlevel == "DEAN OF STUDENT AFFAIRS")
            {
                cmbconcern.SelectedIndex = 2;
            }
            else
            {
                cmbconcern.SelectedIndex = 3;
            }
        }
        private void LoadViolationIDs()
        {
            // Clear existing items
            cmbviolationid.Items.Clear();

            // Fetch violation IDs from the database
            string query = "SELECT code FROM tblviolation";  // Adjust the query if needed
            DataTable violationData = updatecase.GetData(query);

            // Populate ComboBox with violation IDs
            foreach (DataRow row in violationData.Rows)
            {
                cmbviolationid.Items.Add(row["code"].ToString());
            }

            // Optionally select the first item or leave it blank
            // cmbviolationid.SelectedIndex = -1; // Uncomment if you want to leave it unselected initially
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmbstatus.SelectedIndex = 0;
            txtaction.Clear();
            errorProvider1.Clear();
            txtschoolyear.Clear();
            cmbconcern.SelectedIndex = -1;
            txtdesciplinary.Clear();
        }

        public event EventHandler CaseUpdate;

        private void btnsave_Click(object sender, EventArgs e)
        {
            validateForm();  // Validate the form input
            if (cmbstatus.SelectedItem != null && cmbstatus.SelectedItem.ToString().ToUpper() == "RESOLVED")
            {
                validataAction();  // Validate action only if status is "RESOLVED"
            }

            if (errorCount == 0)  // If there are no validation errors
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to update this case?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // Update the case in the database using the provided status and action
                        updatecase.executeSQL("UPDATE tblcases SET status = '" + cmbstatus.Text.ToUpper() + "', action = '" + txtaction.Text.ToUpper() + "', SchoolYear = '" + txtschoolyear.Text + "', concernLevel = '" + cmbconcern.Text.ToUpper() + "', disciplinary = '" + txtdesciplinary.Text
                        + "' WHERE caseID = '" + caseID + "'");
                        // Check if the update was successful (i.e., rows were affected)
                        if (updatecase.rowAffected > 0)
                        {
                            // Log the update action into the tbllogs table
                            updatecase.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) " +
                                "VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Update', 'Case Management', '" + caseID + "', '" + username + "')");

                            MessageBox.Show("Case Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CaseUpdate?.Invoke(this, EventArgs.Empty);
                            this.Close();  // Close the form after successful update
                        }
                        else
                        {
                            MessageBox.Show("No changes were made to the case.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error updating case", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}

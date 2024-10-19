using CS311C_DATABASE2024;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS311_DATABASE_2024
{
    public partial class frmUpdatecase : Form
    {
        private string username, caseID, studentID, lastname, firstname, middlename, level, strandcourse, vcode, description, vcount,status, action;

        private void cmbviolationcount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtstrandcourse_TextChanged(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Class1 updatecase = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        private int errorCount;
        public frmUpdatecase(string username, string caseID,string studentID,string lastname,string firstname, string middlename, string level, string strandcourse, string vcode
            ,string description,string vcount,string status, string action)
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
            this.Load += frmUpdatecase_Load;  // Subscribing to the Load event
        }
        private void validateForm()
        {
            errorProvider1.Clear();
            errorCount = 0;
            if (cmbstatus.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbstatus, "Select status");
                errorCount++;
            }
            if (string.IsNullOrEmpty(txtaction.Text))
            {
                errorProvider1.SetError(txtaction, "Password is empty");
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
            cmbstatus.SelectedIndex = -1;
            txtaction.Clear();
        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            {
                validateForm();  // Validate the form input
                if (errorCount == 0)  // If there are no validation errors
                {
                    DialogResult dr = MessageBox.Show("Are you sure you want to update this case?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            // Update the case in the database using the provided status and action
                            updatecase.executeSQL("UPDATE tblcases SET status = '" + cmbstatus.Text.ToUpper() + "', action = '" + txtaction.Text.ToUpper()
                            + "' WHERE caseID = '" + caseID + "'");
                            // Check if the update was successful (i.e., rows were affected)
                            if (updatecase.rowAffected > 0)
                            {
                                // Log the update action into the tbllogs table
                                updatecase.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) " +
                                    "VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Update', 'Case Management', '" + caseID + "', '" + username + "')");

                                MessageBox.Show("Case Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}

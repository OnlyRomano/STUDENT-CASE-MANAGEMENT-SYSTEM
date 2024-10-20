using CS311C_DATABASE2024;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CS311_DATABASE_2024
{
    public partial class frmNewcase : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private string username, studentID, lastname, firstname, middlename, level, strandcourse;

        Class1 newcase = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        private int errorCount;
        private bool isLoadingViolations = false; // To track whether violations are being loaded

        private DataGridView dgvCases;  // To store reference to DataGridView from frmCases

        public frmNewcase(string username, string studentID, string lastname, string firstname,
                          string middlename, string level, string strandcourse, DataGridView dgvCases)
        {
            InitializeComponent();
            this.username = username;
            this.studentID = studentID;
            this.lastname = lastname;
            this.firstname = firstname;
            this.middlename = middlename;
            this.level = level;
            this.strandcourse = strandcourse;
            this.dgvCases = dgvCases;  // Store reference to DataGridView
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmbviolationid.SelectedIndex = -1;
            txtviolationdescription.Clear();
            cmbviolationcount.SelectedIndex = -1; // Clear the violation count when clearing form
        }

        private void validateForm()
        {
            errorProvider1.Clear();
            errorCount = 0;
            if (cmbviolationid.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbviolationid, "Select violation ID");
                errorCount++;
            }
            if (cmbviolationcount.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbviolationcount, "Violation Count is empty");
                errorCount++;
            }
            if (string.IsNullOrEmpty(txtviolationdescription.Text))
            {
                errorProvider1.SetError(txtviolationdescription, "Violation description is empty");
                errorCount++;
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            validateForm();
            if (errorCount == 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to add this case?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        // Get the violation type
                        string vtype = GetViolationType(cmbviolationid.Text);

                        // Insert new case into tblcases
                        newcase.executeSQL("INSERT INTO tblcases (caseID, studentID, violationID, status, violation_count, createdby, datecreated) VALUES ('" + txtcaseid.Text
                            + "', '" + txtstudentid.Text + "', '" + cmbviolationid.Text + "', 'ONGOING', '" + cmbviolationcount.Text.ToUpper() + "', '"
                            + username + "', '" + DateTime.Now.ToShortDateString() + "')");

                        if (newcase.rowAffected > 0)
                        {
                            newcase.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() +
                                "', '" + DateTime.Now.ToShortTimeString() + "', 'Add', 'Cases Management', '" + txtcaseid.Text + "', '" + username + "')");
                            MessageBox.Show("New case added successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error on saving new case", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Function to load violations into cmbviolationid
        private void LoadViolations()
        {
            try
            {
                isLoadingViolations = true;  // Set flag to true to avoid triggering the SelectedIndexChanged event

                // Query to get violation codes and descriptions
                string query = "SELECT code, description FROM tblviolation WHERE status = 'active'";
                DataTable violationData = newcase.GetData(query);

                // Check if data is available
                if (violationData.Rows.Count > 0)
                {
                    cmbviolationid.DataSource = violationData;
                    cmbviolationid.DisplayMember = "code";  // Display vcode in ComboBox
                    cmbviolationid.ValueMember = "description";  // Store description as ValueMember
                    cmbviolationid.SelectedIndex = -1;  // Set to no selection by default
                }
                else
                {
                    MessageBox.Show("No active violations found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading violations: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoadingViolations = false;  // Reset the flag after loading violations
            }
        }

        private int CountOccurrencesInGrid(string vcode)
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvCases.Rows)
            {
                // Ensure that the DataGridView has the correct column name for vcode
                if (row.Cells["violationID"].Value != null && row.Cells["violationID"].Value.ToString() == vcode)
                {
                    count++;
                }
            }
            return count;
        }

        private void cmbviolationid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingViolations && cmbviolationid.SelectedIndex >= 0)
            {
                DataRowView selectedRow = (DataRowView)cmbviolationid.SelectedItem;
                if (selectedRow != null && selectedRow.Row.Table.Columns.Contains("code"))
                {
                    string vcode = selectedRow["code"].ToString();

                    // Fetch violation description
                    txtviolationdescription.Text = selectedRow["description"].ToString();

                    // Check how many times this vcode exists in the student's current cases
                    int violationCount = CountOccurrencesInGrid(vcode);

                    // Set cmbviolationcount based on the occurrence count
                    if (violationCount == 0)
                    {
                        cmbviolationcount.SelectedItem = "FIRST OFFENSE";  // First offense
                    }
                    else if (violationCount == 1)
                    {
                        cmbviolationcount.SelectedItem = "SECOND OFFENSE";  // Second offense
                    }
                    else
                    {
                        cmbviolationcount.SelectedItem = "REPEAT OFFENSE";  // Repeat offense
                    }
                }
                else
                {
                    MessageBox.Show("Error: Violation code or description not found.");
                }
            }
        }
        // Function to get the violation description
        private string GetViolationDescription(string code)
        {
            string description = "";
            try
            {
                string query = $"SELECT description FROM tblviolation WHERE code = '{code}'";
                DataTable descData = newcase.GetData(query);

                if (descData.Rows.Count > 0 && descData.Rows[0]["description"] != DBNull.Value)
                {
                    description = descData.Rows[0]["description"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving violation description: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return description;
        }
        // Function to get the violation type
        private string GetViolationType(string code)
        {
            string vtype = "";
            try
            {
                string query = $"SELECT violation_type FROM tblviolation WHERE code = '{code}'";
                DataTable vtypeData = newcase.GetData(query);

                if (vtypeData.Rows.Count > 0 && vtypeData.Rows[0]["violation_type"] != DBNull.Value)
                {
                    vtype = vtypeData.Rows[0]["violation_type"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving violation type: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return vtype;
        }

        private void frmNewcase_Load(object sender, EventArgs e)
        {
            // Automatically generate the case ID when the form loads
            txtcaseid.Text = $"CASE-{DateTime.Now:yyyyMMddHHmmss}";

            // Display the student data in the respective text boxes
            txtstudentid.Text = studentID;
            txtlastname.Text = lastname;
            txtfirstname.Text = firstname;
            txtmiddlename.Text = middlename;
            txtlevel.Text = level;
            txtstrandcourse.Text = strandcourse;

            // Load violations into the ComboBox
            LoadViolations();
        }
    }
}

using CS311C_DATABASE2024;
using System;
using System.Data;
using System.Windows.Forms;


namespace CS311_DATABASE_2024
{
    public partial class frmCases : Form
    {
        Class1 cases = new Class1("127.0.0.1", "cs311c2024", "jhunmark", "romano");
        private string username;

        public frmCases(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void ClearStudentInformation()
        {
            txtfirstname.Clear();
            txtlastname.Clear();
            txtmiddlename.Clear();
            txtlevel.Clear();
            txtstrandcourse.Clear();
            txtsearch.Clear();
            dataGridView1.DataSource = null;  // Clear the DataGridView
        }

        private DataTable GetStudentData(string studentNumber)
        {
            string query = "SELECT lastname, firstname, middlename, level, strand_course FROM tblstudents WHERE studentID = '" + studentNumber + "'";
            return cases.GetData(query);
        }

        private void frmCases_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            // Clear the student information textboxes
            ClearStudentInformation();

            // Optionally, you can also clear the search textbox
            txtsearch.Clear();
        }

        private int row;
        private void btnadd_Click(object sender, EventArgs e)
        {
            // Ensure that a student is searched and student information is populated
            if (!string.IsNullOrEmpty(txtsearch.Text) &&
                !string.IsNullOrEmpty(txtlastname.Text) &&
                !string.IsNullOrEmpty(txtfirstname.Text) &&
                dataGridView1.Rows.Count > 0) // Check if there are rows in the DataGridView
            {
                string studentID = txtsearch.Text;
                string lastname = txtlastname.Text;
                string firstname = txtfirstname.Text;
                string middlename = txtmiddlename.Text;
                string level = txtlevel.Text;
                string strandcourse = txtstrandcourse.Text;

                // Pass DataGridView reference to frmNewcase
                frmNewcase newCaseForm = new frmNewcase(username, studentID, lastname, firstname, middlename, level, strandcourse, dataGridView1);
                newCaseForm.Show();
            }
            else
            {
                MessageBox.Show("Please search for a student and ensure the student information is populated before adding a case.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            // Ensure that a student is searched first
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                // Ensure that a student is searched first
                if (!string.IsNullOrEmpty(txtsearch.Text))
                {
                    // Check if the student information fields are populated
                    if (!string.IsNullOrEmpty(txtlastname.Text) && !string.IsNullOrEmpty(txtfirstname.Text))
                    {
                        // Check if there are any rows in the DataGridView
                        if (dataGridView1.CurrentRow != null) // Check if a case is selected
                        {
                            string caseID = dataGridView1.CurrentRow.Cells["caseID"].Value.ToString();
                            string studentID = txtsearch.Text;
                            string lastname = txtlastname.Text;
                            string firstname = txtfirstname.Text;
                            string middlename = txtmiddlename.Text;
                            string level = txtlevel.Text;
                            string strandcourse = txtstrandcourse.Text;
                            string vcode = dataGridView1.CurrentRow.Cells["violationID"].Value.ToString();
                            string description = dataGridView1.CurrentRow.Cells["description"].Value.ToString();
                            string vcount = dataGridView1.CurrentRow.Cells["violation_count"].Value.ToString();
                            string status = dataGridView1.CurrentRow.Cells["status"].Value.ToString();
                            string action = dataGridView1.CurrentRow.Cells["action"].Value.ToString();

                            // Pass DataGridView reference to frmUpdatecase
                            frmUpdatecase updateCaseForm = new frmUpdatecase(username, caseID, studentID, lastname, firstname, middlename, level, strandcourse, vcode, description, vcount, status, action);
                            updateCaseForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("No cases available for this student.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please ensure the student information is populated before updating a case.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please search for a student before updating a case.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string studentNumber = txtsearch.Text.Trim();

            if (!string.IsNullOrEmpty(studentNumber))
            {
                // Clear the DataGridView before loading data for the searched student
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                DataTable studentData = GetStudentData(studentNumber);

                if (studentData.Rows.Count > 0)
                {
                    txtlastname.Text = studentData.Rows[0]["lastname"].ToString();
                    txtfirstname.Text = studentData.Rows[0]["firstname"].ToString();
                    txtmiddlename.Text = studentData.Rows[0]["middlename"].ToString();
                    txtlevel.Text = studentData.Rows[0]["level"].ToString();
                    txtstrandcourse.Text = studentData.Rows[0]["strand_course"].ToString();

                    // Now load the cases for the searched student with updated table structure
                    string query = "SELECT caseID, violationID, status, violation_count, action, createdby, datecreated FROM tblcases WHERE studentID = '" + studentNumber + "'";
                    DataTable caseData = cases.GetData(query);

                    // Now add description and vtype from tblviolations based on vcode
                    DataTable resultData = new DataTable();
                    resultData.Columns.Add("caseID");
                    resultData.Columns.Add("violationID");
                    resultData.Columns.Add("description");
                    resultData.Columns.Add("violation_type");
                    resultData.Columns.Add("violation_count");
                    resultData.Columns.Add("status");
                    resultData.Columns.Add("action");
                    resultData.Columns.Add("date");
                    foreach (DataRow row in caseData.Rows)
                    {
                        string vcode = row["violationID"].ToString();
                        string violationQuery = "SELECT description, violation_type FROM tblviolation WHERE code = '" + vcode + "'";
                        DataTable violationData = cases.GetData(violationQuery);

                        string description = violationData.Rows.Count > 0 ? violationData.Rows[0]["description"].ToString() : string.Empty;
                        string vtype = violationData.Rows.Count > 0 ? violationData.Rows[0]["violation_type"].ToString() : string.Empty;

                        resultData.Rows.Add(row["caseID"], vcode, description, vtype, row["violation_count"], row["status"], row["action"], row["datecreated"]);
                    }

                    dataGridView1.DataSource = resultData;
                }
            }
            else
            {
                ClearStudentInformation();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
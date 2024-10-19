namespace CS311C_DATABASE2024
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menutransition = new System.Windows.Forms.Timer(this.components);
            this.sidebartimer = new System.Windows.Forms.Timer(this.components);
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnmenu = new System.Windows.Forms.Button();
            this.menucontainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnaccount = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnstudents = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnstrand = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btncourse = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnviolation = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btncase = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnabout = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnreports = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnlogout = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.sidebar.SuspendLayout();
            this.panel11.SuspendLayout();
            this.menucontainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1649, 52);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CS311C_DATABASE2024.Properties.Resources.arellano_university_logo_D0C35BB9A2_seeklogo_com;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 45);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(62, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(429, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Arellano University Incident Report Management System";
            // 
            // menutransition
            // 
            this.menutransition.Interval = 10;
            this.menutransition.Tick += new System.EventHandler(this.menutransition_Tick);
            // 
            // sidebartimer
            // 
            this.sidebartimer.Interval = 10;
            this.sidebartimer.Tick += new System.EventHandler(this.sidebartimer_Tick);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.sidebar.Controls.Add(this.panel11);
            this.sidebar.Controls.Add(this.menucontainer);
            this.sidebar.Controls.Add(this.panel8);
            this.sidebar.Controls.Add(this.panel9);
            this.sidebar.Controls.Add(this.panel10);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 52);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.sidebar.Size = new System.Drawing.Size(214, 647);
            this.sidebar.TabIndex = 9;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.btnmenu);
            this.panel11.Location = new System.Drawing.Point(3, 13);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(211, 51);
            this.panel11.TabIndex = 7;
            // 
            // btnmenu
            // 
            this.btnmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnmenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnmenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmenu.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmenu.ForeColor = System.Drawing.Color.White;
            this.btnmenu.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_menu_bar_30;
            this.btnmenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnmenu.Location = new System.Drawing.Point(-14, -10);
            this.btnmenu.Name = "btnmenu";
            this.btnmenu.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnmenu.Size = new System.Drawing.Size(257, 68);
            this.btnmenu.TabIndex = 2;
            this.btnmenu.Text = "            Menu";
            this.btnmenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnmenu.UseVisualStyleBackColor = false;
            this.btnmenu.Click += new System.EventHandler(this.btnmenu_Click);
            // 
            // menucontainer
            // 
            this.menucontainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.menucontainer.Controls.Add(this.panel2);
            this.menucontainer.Controls.Add(this.panel3);
            this.menucontainer.Controls.Add(this.panel4);
            this.menucontainer.Controls.Add(this.panel6);
            this.menucontainer.Controls.Add(this.panel5);
            this.menucontainer.Controls.Add(this.panel7);
            this.menucontainer.Controls.Add(this.panel12);
            this.menucontainer.Location = new System.Drawing.Point(3, 70);
            this.menucontainer.Name = "menucontainer";
            this.menucontainer.Size = new System.Drawing.Size(243, 51);
            this.menucontainer.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnMaintenance);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(228, 51);
            this.panel2.TabIndex = 3;
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnMaintenance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaintenance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnMaintenance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaintenance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaintenance.ForeColor = System.Drawing.Color.White;
            this.btnMaintenance.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_maintenance_30;
            this.btnMaintenance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaintenance.Location = new System.Drawing.Point(-14, -10);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnMaintenance.Size = new System.Drawing.Size(257, 68);
            this.btnMaintenance.TabIndex = 2;
            this.btnMaintenance.Text = "            Maintenance";
            this.btnMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaintenance.UseVisualStyleBackColor = false;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click_1);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnaccount);
            this.panel3.Location = new System.Drawing.Point(3, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(228, 51);
            this.panel3.TabIndex = 4;
            // 
            // btnaccount
            // 
            this.btnaccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnaccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnaccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaccount.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaccount.ForeColor = System.Drawing.Color.White;
            this.btnaccount.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_account_30;
            this.btnaccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnaccount.Location = new System.Drawing.Point(-14, -10);
            this.btnaccount.Name = "btnaccount";
            this.btnaccount.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnaccount.Size = new System.Drawing.Size(257, 68);
            this.btnaccount.TabIndex = 2;
            this.btnaccount.Text = "            Accounts";
            this.btnaccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnaccount.UseVisualStyleBackColor = false;
            this.btnaccount.Click += new System.EventHandler(this.btnaccount_Click_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnstudents);
            this.panel4.Location = new System.Drawing.Point(3, 117);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(228, 51);
            this.panel4.TabIndex = 5;
            // 
            // btnstudents
            // 
            this.btnstudents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnstudents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnstudents.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnstudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstudents.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstudents.ForeColor = System.Drawing.Color.White;
            this.btnstudents.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_student_30;
            this.btnstudents.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstudents.Location = new System.Drawing.Point(-14, -10);
            this.btnstudents.Name = "btnstudents";
            this.btnstudents.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnstudents.Size = new System.Drawing.Size(257, 68);
            this.btnstudents.TabIndex = 2;
            this.btnstudents.Text = "            Students";
            this.btnstudents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstudents.UseVisualStyleBackColor = false;
            this.btnstudents.Click += new System.EventHandler(this.btnstudents_Click_1);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnstrand);
            this.panel6.Location = new System.Drawing.Point(3, 174);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(228, 51);
            this.panel6.TabIndex = 6;
            // 
            // btnstrand
            // 
            this.btnstrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnstrand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnstrand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnstrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstrand.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstrand.ForeColor = System.Drawing.Color.White;
            this.btnstrand.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_school_30;
            this.btnstrand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstrand.Location = new System.Drawing.Point(-14, -10);
            this.btnstrand.Name = "btnstrand";
            this.btnstrand.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnstrand.Size = new System.Drawing.Size(257, 68);
            this.btnstrand.TabIndex = 2;
            this.btnstrand.Text = "            Strand";
            this.btnstrand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnstrand.UseVisualStyleBackColor = false;
            this.btnstrand.Click += new System.EventHandler(this.btnstrand_Click_1);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btncourse);
            this.panel5.Location = new System.Drawing.Point(3, 231);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(228, 51);
            this.panel5.TabIndex = 6;
            // 
            // btncourse
            // 
            this.btncourse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btncourse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncourse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btncourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncourse.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncourse.ForeColor = System.Drawing.Color.White;
            this.btncourse.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_course_assign_30;
            this.btncourse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncourse.Location = new System.Drawing.Point(-14, -10);
            this.btncourse.Name = "btncourse";
            this.btncourse.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btncourse.Size = new System.Drawing.Size(257, 68);
            this.btncourse.TabIndex = 2;
            this.btncourse.Text = "            Course";
            this.btncourse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncourse.UseVisualStyleBackColor = false;
            this.btncourse.Click += new System.EventHandler(this.btncourse_Click_1);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnviolation);
            this.panel7.Location = new System.Drawing.Point(3, 288);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(228, 51);
            this.panel7.TabIndex = 6;
            // 
            // btnviolation
            // 
            this.btnviolation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnviolation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnviolation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnviolation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnviolation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnviolation.ForeColor = System.Drawing.Color.White;
            this.btnviolation.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_warning_30;
            this.btnviolation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnviolation.Location = new System.Drawing.Point(-14, -10);
            this.btnviolation.Name = "btnviolation";
            this.btnviolation.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnviolation.Size = new System.Drawing.Size(257, 68);
            this.btnviolation.TabIndex = 2;
            this.btnviolation.Text = "            Violations";
            this.btnviolation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnviolation.UseVisualStyleBackColor = false;
            this.btnviolation.Click += new System.EventHandler(this.btnviolation_Click_1);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btncase);
            this.panel12.Location = new System.Drawing.Point(3, 345);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(228, 51);
            this.panel12.TabIndex = 7;
            // 
            // btncase
            // 
            this.btncase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btncase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btncase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncase.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncase.ForeColor = System.Drawing.Color.White;
            this.btncase.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_folder_30;
            this.btncase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncase.Location = new System.Drawing.Point(-14, -10);
            this.btncase.Name = "btncase";
            this.btncase.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btncase.Size = new System.Drawing.Size(257, 68);
            this.btncase.TabIndex = 2;
            this.btncase.Text = "            Case";
            this.btncase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncase.UseVisualStyleBackColor = false;
            this.btncase.Click += new System.EventHandler(this.btncase_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnabout);
            this.panel8.Location = new System.Drawing.Point(3, 127);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(243, 51);
            this.panel8.TabIndex = 4;
            // 
            // btnabout
            // 
            this.btnabout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnabout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnabout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnabout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnabout.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnabout.ForeColor = System.Drawing.Color.White;
            this.btnabout.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_about_30;
            this.btnabout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnabout.Location = new System.Drawing.Point(-14, -10);
            this.btnabout.Name = "btnabout";
            this.btnabout.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnabout.Size = new System.Drawing.Size(257, 68);
            this.btnabout.TabIndex = 2;
            this.btnabout.Text = "            About";
            this.btnabout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnabout.UseVisualStyleBackColor = false;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnreports);
            this.panel9.Location = new System.Drawing.Point(3, 184);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(243, 51);
            this.panel9.TabIndex = 5;
            // 
            // btnreports
            // 
            this.btnreports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnreports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnreports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnreports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreports.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreports.ForeColor = System.Drawing.Color.White;
            this.btnreports.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_report_30;
            this.btnreports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnreports.Location = new System.Drawing.Point(-14, -10);
            this.btnreports.Name = "btnreports";
            this.btnreports.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnreports.Size = new System.Drawing.Size(257, 68);
            this.btnreports.TabIndex = 2;
            this.btnreports.Text = "            Reports";
            this.btnreports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnreports.UseVisualStyleBackColor = false;
            this.btnreports.Click += new System.EventHandler(this.btnreports_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnlogout);
            this.panel10.Location = new System.Drawing.Point(3, 241);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(243, 51);
            this.panel10.TabIndex = 6;
            // 
            // btnlogout
            // 
            this.btnlogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.btnlogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(108)))), ((int)(((byte)(132)))));
            this.btnlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogout.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogout.ForeColor = System.Drawing.Color.White;
            this.btnlogout.Image = global::CS311C_DATABASE2024.Properties.Resources.icons8_logout_30;
            this.btnlogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogout.Location = new System.Drawing.Point(-14, -10);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnlogout.Size = new System.Drawing.Size(242, 68);
            this.btnlogout.TabIndex = 2;
            this.btnlogout.Text = "            Logout";
            this.btnlogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogout.UseVisualStyleBackColor = false;
            this.btnlogout.Click += new System.EventHandler(this.btnlogout_Click_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(67)))), ((int)(((byte)(83)))));
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(214, 676);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1435, 23);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(141, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(141, 18);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1649, 699);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "Incident Report Management System - Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.menucontainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer menutransition;
        private System.Windows.Forms.Timer sidebartimer;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnmenu;
        private System.Windows.Forms.FlowLayoutPanel menucontainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMaintenance;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnaccount;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnstudents;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnstrand;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btncourse;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnviolation;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnreports;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnabout;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Button btncase;
    }
}
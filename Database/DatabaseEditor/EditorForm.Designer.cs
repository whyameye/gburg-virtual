namespace DatabaseEditor
{
    partial class EditorForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.poiTab = new System.Windows.Forms.TabPage();
            this.pSaveButton = new System.Windows.Forms.Button();
            this.pSelect = new System.Windows.Forms.ListView();
            this.pGroup = new System.Windows.Forms.GroupBox();
            this.pIconCombo = new System.Windows.Forms.ComboBox();
            this.pIconBox = new System.Windows.Forms.PictureBox();
            this.pDeptList = new System.Windows.Forms.CheckedListBox();
            this.pYField = new System.Windows.Forms.NumericUpDown();
            this.pXField = new System.Windows.Forms.NumericUpDown();
            this.pNameField = new System.Windows.Forms.TextBox();
            this.pIdField = new System.Windows.Forms.TextBox();
            this.pDescField = new System.Windows.Forms.TextBox();
            this.pDescLabel = new System.Windows.Forms.Label();
            this.pYLabel = new System.Windows.Forms.Label();
            this.pXLabel = new System.Windows.Forms.Label();
            this.pLocationLabel = new System.Windows.Forms.Label();
            this.pNameLabel = new System.Windows.Forms.Label();
            this.pImageLabel = new System.Windows.Forms.Label();
            this.pDeptLabel = new System.Windows.Forms.Label();
            this.pIdLabel = new System.Windows.Forms.Label();
            this.pSelectLabel = new System.Windows.Forms.Label();
            this.pRemoveButton = new System.Windows.Forms.Button();
            this.pAddButton = new System.Windows.Forms.Button();
            this.deptTab = new System.Windows.Forms.TabPage();
            this.dSaveButton = new System.Windows.Forms.Button();
            this.dSelect = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dMediaList = new System.Windows.Forms.CheckedListBox();
            this.dNameField = new System.Windows.Forms.TextBox();
            this.dIdField = new System.Windows.Forms.TextBox();
            this.dDescField = new System.Windows.Forms.TextBox();
            this.dDescLabel = new System.Windows.Forms.Label();
            this.dNameLabel = new System.Windows.Forms.Label();
            this.dTextLabel = new System.Windows.Forms.Label();
            this.dIdLabel = new System.Windows.Forms.Label();
            this.dSelectLabel = new System.Windows.Forms.Label();
            this.dRemoveButton = new System.Windows.Forms.Button();
            this.dAddButton = new System.Windows.Forms.Button();
            this.mediaTab = new System.Windows.Forms.TabPage();
            this.mSaveButton = new System.Windows.Forms.Button();
            this.mSelect = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mTypeCombo = new System.Windows.Forms.ComboBox();
            this.mPreviewField = new System.Windows.Forms.TextBox();
            this.mPreviewBox = new System.Windows.Forms.PictureBox();
            this.mFileCombo = new System.Windows.Forms.ComboBox();
            this.mNameField = new System.Windows.Forms.TextBox();
            this.mIdField = new System.Windows.Forms.TextBox();
            this.mDescField = new System.Windows.Forms.TextBox();
            this.mFileLabel = new System.Windows.Forms.Label();
            this.mDescLabel = new System.Windows.Forms.Label();
            this.mNameLabel = new System.Windows.Forms.Label();
            this.mPreviewLabel = new System.Windows.Forms.Label();
            this.mTypeLabel = new System.Windows.Forms.Label();
            this.mIdLabel = new System.Windows.Forms.Label();
            this.mSelectLabel = new System.Windows.Forms.Label();
            this.mRemoveButton = new System.Windows.Forms.Button();
            this.mAddButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.poiTab.SuspendLayout();
            this.pGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pYField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pXField)).BeginInit();
            this.deptTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mediaTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.poiTab);
            this.tabControl.Controls.Add(this.deptTab);
            this.tabControl.Controls.Add(this.mediaTab);
            this.tabControl.Location = new System.Drawing.Point(1, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(715, 451);
            this.tabControl.TabIndex = 0;
            // 
            // poiTab
            // 
            this.poiTab.Controls.Add(this.pSaveButton);
            this.poiTab.Controls.Add(this.pSelect);
            this.poiTab.Controls.Add(this.pGroup);
            this.poiTab.Controls.Add(this.pSelectLabel);
            this.poiTab.Controls.Add(this.pRemoveButton);
            this.poiTab.Controls.Add(this.pAddButton);
            this.poiTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.poiTab.Location = new System.Drawing.Point(4, 22);
            this.poiTab.Name = "poiTab";
            this.poiTab.Padding = new System.Windows.Forms.Padding(3);
            this.poiTab.Size = new System.Drawing.Size(707, 425);
            this.poiTab.TabIndex = 0;
            this.poiTab.Text = "Points of Interest";
            this.poiTab.UseVisualStyleBackColor = true;
            // 
            // pSaveButton
            // 
            this.pSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pSaveButton.Location = new System.Drawing.Point(460, 2);
            this.pSaveButton.Name = "pSaveButton";
            this.pSaveButton.Size = new System.Drawing.Size(233, 25);
            this.pSaveButton.TabIndex = 6;
            this.pSaveButton.Text = "Save Current PoI";
            this.pSaveButton.UseVisualStyleBackColor = true;
            this.pSaveButton.Click += new System.EventHandler(this.pSaveButton_Click);
            // 
            // pSelect
            // 
            this.pSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pSelect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.pSelect.Location = new System.Drawing.Point(21, 49);
            this.pSelect.MultiSelect = false;
            this.pSelect.Name = "pSelect";
            this.pSelect.Size = new System.Drawing.Size(173, 327);
            this.pSelect.TabIndex = 1;
            this.pSelect.UseCompatibleStateImageBehavior = false;
            this.pSelect.View = System.Windows.Forms.View.Details;
            // 
            // pGroup
            // 
            this.pGroup.Controls.Add(this.pIconCombo);
            this.pGroup.Controls.Add(this.pIconBox);
            this.pGroup.Controls.Add(this.pDeptList);
            this.pGroup.Controls.Add(this.pYField);
            this.pGroup.Controls.Add(this.pXField);
            this.pGroup.Controls.Add(this.pNameField);
            this.pGroup.Controls.Add(this.pIdField);
            this.pGroup.Controls.Add(this.pDescField);
            this.pGroup.Controls.Add(this.pDescLabel);
            this.pGroup.Controls.Add(this.pYLabel);
            this.pGroup.Controls.Add(this.pXLabel);
            this.pGroup.Controls.Add(this.pLocationLabel);
            this.pGroup.Controls.Add(this.pNameLabel);
            this.pGroup.Controls.Add(this.pImageLabel);
            this.pGroup.Controls.Add(this.pDeptLabel);
            this.pGroup.Controls.Add(this.pIdLabel);
            this.pGroup.Location = new System.Drawing.Point(216, 22);
            this.pGroup.Name = "pGroup";
            this.pGroup.Size = new System.Drawing.Size(480, 392);
            this.pGroup.TabIndex = 5;
            this.pGroup.TabStop = false;
            this.pGroup.Text = "Current PoI Data";
            // 
            // pIconCombo
            // 
            this.pIconCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pIconCombo.FormattingEnabled = true;
            this.pIconCombo.Location = new System.Drawing.Point(311, 198);
            this.pIconCombo.Name = "pIconCombo";
            this.pIconCombo.Size = new System.Drawing.Size(158, 24);
            this.pIconCombo.TabIndex = 6;
            // 
            // pIconBox
            // 
            this.pIconBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pIconBox.Location = new System.Drawing.Point(275, 232);
            this.pIconBox.Name = "pIconBox";
            this.pIconBox.Size = new System.Drawing.Size(154, 154);
            this.pIconBox.TabIndex = 5;
            this.pIconBox.TabStop = false;
            // 
            // pDeptList
            // 
            this.pDeptList.CheckOnClick = true;
            this.pDeptList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pDeptList.FormattingEnabled = true;
            this.pDeptList.Location = new System.Drawing.Point(244, 46);
            this.pDeptList.Name = "pDeptList";
            this.pDeptList.Size = new System.Drawing.Size(226, 140);
            this.pDeptList.TabIndex = 4;
            this.pDeptList.TabStop = false;
            // 
            // pYField
            // 
            this.pYField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pYField.Location = new System.Drawing.Point(149, 164);
            this.pYField.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.pYField.Name = "pYField";
            this.pYField.Size = new System.Drawing.Size(72, 22);
            this.pYField.TabIndex = 2;
            this.pYField.ThousandsSeparator = true;
            // 
            // pXField
            // 
            this.pXField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pXField.Location = new System.Drawing.Point(38, 164);
            this.pXField.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.pXField.Name = "pXField";
            this.pXField.Size = new System.Drawing.Size(70, 22);
            this.pXField.TabIndex = 1;
            this.pXField.ThousandsSeparator = true;
            // 
            // pNameField
            // 
            this.pNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pNameField.Location = new System.Drawing.Point(20, 103);
            this.pNameField.Name = "pNameField";
            this.pNameField.Size = new System.Drawing.Size(201, 22);
            this.pNameField.TabIndex = 0;
            // 
            // pIdField
            // 
            this.pIdField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pIdField.Location = new System.Drawing.Point(20, 50);
            this.pIdField.Name = "pIdField";
            this.pIdField.ReadOnly = true;
            this.pIdField.Size = new System.Drawing.Size(201, 22);
            this.pIdField.TabIndex = 0;
            this.pIdField.TabStop = false;
            // 
            // pDescField
            // 
            this.pDescField.AcceptsReturn = true;
            this.pDescField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pDescField.Location = new System.Drawing.Point(20, 218);
            this.pDescField.Multiline = true;
            this.pDescField.Name = "pDescField";
            this.pDescField.Size = new System.Drawing.Size(201, 157);
            this.pDescField.TabIndex = 3;
            // 
            // pDescLabel
            // 
            this.pDescLabel.AutoSize = true;
            this.pDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pDescLabel.Location = new System.Drawing.Point(10, 191);
            this.pDescLabel.Name = "pDescLabel";
            this.pDescLabel.Size = new System.Drawing.Size(76, 16);
            this.pDescLabel.TabIndex = 0;
            this.pDescLabel.Text = "Description";
            // 
            // pYLabel
            // 
            this.pYLabel.AutoSize = true;
            this.pYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pYLabel.Location = new System.Drawing.Point(127, 163);
            this.pYLabel.Name = "pYLabel";
            this.pYLabel.Size = new System.Drawing.Size(15, 16);
            this.pYLabel.TabIndex = 0;
            this.pYLabel.Text = "y";
            // 
            // pXLabel
            // 
            this.pXLabel.AutoSize = true;
            this.pXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pXLabel.Location = new System.Drawing.Point(16, 163);
            this.pXLabel.Name = "pXLabel";
            this.pXLabel.Size = new System.Drawing.Size(14, 16);
            this.pXLabel.TabIndex = 0;
            this.pXLabel.Text = "x";
            // 
            // pLocationLabel
            // 
            this.pLocationLabel.AutoSize = true;
            this.pLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLocationLabel.Location = new System.Drawing.Point(10, 138);
            this.pLocationLabel.Name = "pLocationLabel";
            this.pLocationLabel.Size = new System.Drawing.Size(59, 16);
            this.pLocationLabel.TabIndex = 0;
            this.pLocationLabel.Text = "Location";
            // 
            // pNameLabel
            // 
            this.pNameLabel.AutoSize = true;
            this.pNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pNameLabel.Location = new System.Drawing.Point(10, 80);
            this.pNameLabel.Name = "pNameLabel";
            this.pNameLabel.Size = new System.Drawing.Size(45, 16);
            this.pNameLabel.TabIndex = 0;
            this.pNameLabel.Text = "Name";
            // 
            // pImageLabel
            // 
            this.pImageLabel.AutoSize = true;
            this.pImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pImageLabel.Location = new System.Drawing.Point(240, 202);
            this.pImageLabel.Name = "pImageLabel";
            this.pImageLabel.Size = new System.Drawing.Size(46, 16);
            this.pImageLabel.TabIndex = 0;
            this.pImageLabel.Text = "Image";
            // 
            // pDeptLabel
            // 
            this.pDeptLabel.AutoSize = true;
            this.pDeptLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pDeptLabel.Location = new System.Drawing.Point(240, 18);
            this.pDeptLabel.Name = "pDeptLabel";
            this.pDeptLabel.Size = new System.Drawing.Size(85, 16);
            this.pDeptLabel.TabIndex = 0;
            this.pDeptLabel.Text = "Departments";
            // 
            // pIdLabel
            // 
            this.pIdLabel.AutoSize = true;
            this.pIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pIdLabel.Location = new System.Drawing.Point(10, 27);
            this.pIdLabel.Name = "pIdLabel";
            this.pIdLabel.Size = new System.Drawing.Size(72, 16);
            this.pIdLabel.TabIndex = 0;
            this.pIdLabel.Text = "ID Number";
            // 
            // pSelectLabel
            // 
            this.pSelectLabel.AutoSize = true;
            this.pSelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pSelectLabel.Location = new System.Drawing.Point(17, 28);
            this.pSelectLabel.Name = "pSelectLabel";
            this.pSelectLabel.Size = new System.Drawing.Size(139, 20);
            this.pSelectLabel.TabIndex = 4;
            this.pSelectLabel.Text = "Point of Interest";
            // 
            // pRemoveButton
            // 
            this.pRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pRemoveButton.Location = new System.Drawing.Point(117, 384);
            this.pRemoveButton.Name = "pRemoveButton";
            this.pRemoveButton.Size = new System.Drawing.Size(77, 24);
            this.pRemoveButton.TabIndex = 3;
            this.pRemoveButton.Text = "Remove";
            this.pRemoveButton.UseVisualStyleBackColor = true;
            this.pRemoveButton.Click += new System.EventHandler(this.pRemoveButton_Click);
            // 
            // pAddButton
            // 
            this.pAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pAddButton.Location = new System.Drawing.Point(21, 384);
            this.pAddButton.Name = "pAddButton";
            this.pAddButton.Size = new System.Drawing.Size(77, 24);
            this.pAddButton.TabIndex = 2;
            this.pAddButton.Text = "Add";
            this.pAddButton.UseVisualStyleBackColor = true;
            this.pAddButton.Click += new System.EventHandler(this.pAddButton_Click);
            // 
            // deptTab
            // 
            this.deptTab.Controls.Add(this.dSaveButton);
            this.deptTab.Controls.Add(this.dSelect);
            this.deptTab.Controls.Add(this.groupBox1);
            this.deptTab.Controls.Add(this.dSelectLabel);
            this.deptTab.Controls.Add(this.dRemoveButton);
            this.deptTab.Controls.Add(this.dAddButton);
            this.deptTab.Location = new System.Drawing.Point(4, 22);
            this.deptTab.Name = "deptTab";
            this.deptTab.Padding = new System.Windows.Forms.Padding(3);
            this.deptTab.Size = new System.Drawing.Size(707, 425);
            this.deptTab.TabIndex = 1;
            this.deptTab.Text = "Departments";
            this.deptTab.UseVisualStyleBackColor = true;
            // 
            // dSaveButton
            // 
            this.dSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dSaveButton.Location = new System.Drawing.Point(460, 2);
            this.dSaveButton.Name = "dSaveButton";
            this.dSaveButton.Size = new System.Drawing.Size(233, 25);
            this.dSaveButton.TabIndex = 12;
            this.dSaveButton.Text = "Save Current Department";
            this.dSaveButton.UseVisualStyleBackColor = true;
            this.dSaveButton.Click += new System.EventHandler(this.dSaveButton_Click);
            // 
            // dSelect
            // 
            this.dSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dSelect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dSelect.HideSelection = false;
            this.dSelect.Location = new System.Drawing.Point(21, 49);
            this.dSelect.MultiSelect = false;
            this.dSelect.Name = "dSelect";
            this.dSelect.Size = new System.Drawing.Size(173, 327);
            this.dSelect.TabIndex = 7;
            this.dSelect.UseCompatibleStateImageBehavior = false;
            this.dSelect.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dMediaList);
            this.groupBox1.Controls.Add(this.dNameField);
            this.groupBox1.Controls.Add(this.dIdField);
            this.groupBox1.Controls.Add(this.dDescField);
            this.groupBox1.Controls.Add(this.dDescLabel);
            this.groupBox1.Controls.Add(this.dNameLabel);
            this.groupBox1.Controls.Add(this.dTextLabel);
            this.groupBox1.Controls.Add(this.dIdLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(216, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 392);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Department Data";
            // 
            // dMediaList
            // 
            this.dMediaList.CheckOnClick = true;
            this.dMediaList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dMediaList.FormattingEnabled = true;
            this.dMediaList.Location = new System.Drawing.Point(244, 39);
            this.dMediaList.Name = "dMediaList";
            this.dMediaList.Size = new System.Drawing.Size(226, 344);
            this.dMediaList.TabIndex = 4;
            this.dMediaList.TabStop = false;
            // 
            // dNameField
            // 
            this.dNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dNameField.Location = new System.Drawing.Point(20, 103);
            this.dNameField.Name = "dNameField";
            this.dNameField.Size = new System.Drawing.Size(201, 22);
            this.dNameField.TabIndex = 0;
            // 
            // dIdField
            // 
            this.dIdField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dIdField.Location = new System.Drawing.Point(20, 50);
            this.dIdField.Name = "dIdField";
            this.dIdField.ReadOnly = true;
            this.dIdField.Size = new System.Drawing.Size(201, 22);
            this.dIdField.TabIndex = 0;
            this.dIdField.TabStop = false;
            // 
            // dDescField
            // 
            this.dDescField.AcceptsReturn = true;
            this.dDescField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dDescField.Location = new System.Drawing.Point(20, 162);
            this.dDescField.Multiline = true;
            this.dDescField.Name = "dDescField";
            this.dDescField.Size = new System.Drawing.Size(201, 221);
            this.dDescField.TabIndex = 3;
            // 
            // dDescLabel
            // 
            this.dDescLabel.AutoSize = true;
            this.dDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dDescLabel.Location = new System.Drawing.Point(10, 137);
            this.dDescLabel.Name = "dDescLabel";
            this.dDescLabel.Size = new System.Drawing.Size(76, 16);
            this.dDescLabel.TabIndex = 0;
            this.dDescLabel.Text = "Description";
            // 
            // dNameLabel
            // 
            this.dNameLabel.AutoSize = true;
            this.dNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dNameLabel.Location = new System.Drawing.Point(10, 80);
            this.dNameLabel.Name = "dNameLabel";
            this.dNameLabel.Size = new System.Drawing.Size(45, 16);
            this.dNameLabel.TabIndex = 0;
            this.dNameLabel.Text = "Name";
            // 
            // dTextLabel
            // 
            this.dTextLabel.AutoSize = true;
            this.dTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTextLabel.Location = new System.Drawing.Point(240, 23);
            this.dTextLabel.Name = "dTextLabel";
            this.dTextLabel.Size = new System.Drawing.Size(149, 16);
            this.dTextLabel.TabIndex = 0;
            this.dTextLabel.Text = "Associated Media Files";
            // 
            // dIdLabel
            // 
            this.dIdLabel.AutoSize = true;
            this.dIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dIdLabel.Location = new System.Drawing.Point(10, 27);
            this.dIdLabel.Name = "dIdLabel";
            this.dIdLabel.Size = new System.Drawing.Size(72, 16);
            this.dIdLabel.TabIndex = 0;
            this.dIdLabel.Text = "ID Number";
            // 
            // dSelectLabel
            // 
            this.dSelectLabel.AutoSize = true;
            this.dSelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dSelectLabel.Location = new System.Drawing.Point(17, 28);
            this.dSelectLabel.Name = "dSelectLabel";
            this.dSelectLabel.Size = new System.Drawing.Size(104, 20);
            this.dSelectLabel.TabIndex = 10;
            this.dSelectLabel.Text = "Department";
            // 
            // dRemoveButton
            // 
            this.dRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dRemoveButton.Location = new System.Drawing.Point(117, 384);
            this.dRemoveButton.Name = "dRemoveButton";
            this.dRemoveButton.Size = new System.Drawing.Size(77, 24);
            this.dRemoveButton.TabIndex = 9;
            this.dRemoveButton.Text = "Remove";
            this.dRemoveButton.UseVisualStyleBackColor = true;
            this.dRemoveButton.Click += new System.EventHandler(this.dRemoveButton_Click);
            // 
            // dAddButton
            // 
            this.dAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dAddButton.Location = new System.Drawing.Point(21, 384);
            this.dAddButton.Name = "dAddButton";
            this.dAddButton.Size = new System.Drawing.Size(77, 24);
            this.dAddButton.TabIndex = 8;
            this.dAddButton.Text = "Add";
            this.dAddButton.UseVisualStyleBackColor = true;
            this.dAddButton.Click += new System.EventHandler(this.dAddButton_Click);
            // 
            // mediaTab
            // 
            this.mediaTab.Controls.Add(this.mSaveButton);
            this.mediaTab.Controls.Add(this.mSelect);
            this.mediaTab.Controls.Add(this.groupBox2);
            this.mediaTab.Controls.Add(this.mSelectLabel);
            this.mediaTab.Controls.Add(this.mRemoveButton);
            this.mediaTab.Controls.Add(this.mAddButton);
            this.mediaTab.Location = new System.Drawing.Point(4, 22);
            this.mediaTab.Name = "mediaTab";
            this.mediaTab.Padding = new System.Windows.Forms.Padding(3);
            this.mediaTab.Size = new System.Drawing.Size(707, 425);
            this.mediaTab.TabIndex = 2;
            this.mediaTab.Text = "Media Files";
            this.mediaTab.UseVisualStyleBackColor = true;
            // 
            // mSaveButton
            // 
            this.mSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mSaveButton.Location = new System.Drawing.Point(460, 2);
            this.mSaveButton.Name = "mSaveButton";
            this.mSaveButton.Size = new System.Drawing.Size(233, 25);
            this.mSaveButton.TabIndex = 7;
            this.mSaveButton.Text = "Save Current Media File";
            this.mSaveButton.UseVisualStyleBackColor = true;
            this.mSaveButton.Click += new System.EventHandler(this.mSaveButton_Click);
            // 
            // mSelect
            // 
            this.mSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mSelect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.mSelect.HideSelection = false;
            this.mSelect.Location = new System.Drawing.Point(21, 49);
            this.mSelect.MultiSelect = false;
            this.mSelect.Name = "mSelect";
            this.mSelect.Size = new System.Drawing.Size(173, 327);
            this.mSelect.TabIndex = 1;
            this.mSelect.UseCompatibleStateImageBehavior = false;
            this.mSelect.View = System.Windows.Forms.View.Details;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mTypeCombo);
            this.groupBox2.Controls.Add(this.mPreviewField);
            this.groupBox2.Controls.Add(this.mPreviewBox);
            this.groupBox2.Controls.Add(this.mFileCombo);
            this.groupBox2.Controls.Add(this.mNameField);
            this.groupBox2.Controls.Add(this.mIdField);
            this.groupBox2.Controls.Add(this.mDescField);
            this.groupBox2.Controls.Add(this.mFileLabel);
            this.groupBox2.Controls.Add(this.mDescLabel);
            this.groupBox2.Controls.Add(this.mNameLabel);
            this.groupBox2.Controls.Add(this.mPreviewLabel);
            this.groupBox2.Controls.Add(this.mTypeLabel);
            this.groupBox2.Controls.Add(this.mIdLabel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(216, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 392);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Media File Data";
            // 
            // mTypeCombo
            // 
            this.mTypeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTypeCombo.FormattingEnabled = true;
            this.mTypeCombo.Items.AddRange(new object[] {
            "Text",
            "Image",
            "Video"});
            this.mTypeCombo.Location = new System.Drawing.Point(125, 50);
            this.mTypeCombo.Name = "mTypeCombo";
            this.mTypeCombo.Size = new System.Drawing.Size(89, 24);
            this.mTypeCombo.TabIndex = 7;
            // 
            // mPreviewField
            // 
            this.mPreviewField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPreviewField.Location = new System.Drawing.Point(239, 42);
            this.mPreviewField.Multiline = true;
            this.mPreviewField.Name = "mPreviewField";
            this.mPreviewField.ReadOnly = true;
            this.mPreviewField.Size = new System.Drawing.Size(235, 158);
            this.mPreviewField.TabIndex = 6;
            // 
            // mPreviewBox
            // 
            this.mPreviewBox.Location = new System.Drawing.Point(239, 42);
            this.mPreviewBox.Name = "mPreviewBox";
            this.mPreviewBox.Size = new System.Drawing.Size(235, 158);
            this.mPreviewBox.TabIndex = 5;
            this.mPreviewBox.TabStop = false;
            // 
            // mFileCombo
            // 
            this.mFileCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mFileCombo.FormattingEnabled = true;
            this.mFileCombo.Location = new System.Drawing.Point(20, 154);
            this.mFileCombo.Name = "mFileCombo";
            this.mFileCombo.Size = new System.Drawing.Size(201, 24);
            this.mFileCombo.TabIndex = 5;
            // 
            // mNameField
            // 
            this.mNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mNameField.Location = new System.Drawing.Point(20, 103);
            this.mNameField.Name = "mNameField";
            this.mNameField.Size = new System.Drawing.Size(201, 22);
            this.mNameField.TabIndex = 4;
            // 
            // mIdField
            // 
            this.mIdField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mIdField.Location = new System.Drawing.Point(20, 50);
            this.mIdField.Name = "mIdField";
            this.mIdField.ReadOnly = true;
            this.mIdField.Size = new System.Drawing.Size(86, 22);
            this.mIdField.TabIndex = 0;
            this.mIdField.TabStop = false;
            // 
            // mDescField
            // 
            this.mDescField.AcceptsReturn = true;
            this.mDescField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mDescField.Location = new System.Drawing.Point(20, 206);
            this.mDescField.Multiline = true;
            this.mDescField.Name = "mDescField";
            this.mDescField.Size = new System.Drawing.Size(454, 177);
            this.mDescField.TabIndex = 6;
            // 
            // mFileLabel
            // 
            this.mFileLabel.AutoSize = true;
            this.mFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mFileLabel.Location = new System.Drawing.Point(10, 135);
            this.mFileLabel.Name = "mFileLabel";
            this.mFileLabel.Size = new System.Drawing.Size(70, 16);
            this.mFileLabel.TabIndex = 0;
            this.mFileLabel.Text = "File Name";
            // 
            // mDescLabel
            // 
            this.mDescLabel.AutoSize = true;
            this.mDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mDescLabel.Location = new System.Drawing.Point(10, 185);
            this.mDescLabel.Name = "mDescLabel";
            this.mDescLabel.Size = new System.Drawing.Size(76, 16);
            this.mDescLabel.TabIndex = 0;
            this.mDescLabel.Text = "Description";
            // 
            // mNameLabel
            // 
            this.mNameLabel.AutoSize = true;
            this.mNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mNameLabel.Location = new System.Drawing.Point(10, 80);
            this.mNameLabel.Name = "mNameLabel";
            this.mNameLabel.Size = new System.Drawing.Size(45, 16);
            this.mNameLabel.TabIndex = 0;
            this.mNameLabel.Text = "Name";
            // 
            // mPreviewLabel
            // 
            this.mPreviewLabel.AutoSize = true;
            this.mPreviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPreviewLabel.Location = new System.Drawing.Point(240, 23);
            this.mPreviewLabel.Name = "mPreviewLabel";
            this.mPreviewLabel.Size = new System.Drawing.Size(81, 16);
            this.mPreviewLabel.TabIndex = 0;
            this.mPreviewLabel.Text = "File Preview";
            // 
            // mTypeLabel
            // 
            this.mTypeLabel.AutoSize = true;
            this.mTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTypeLabel.Location = new System.Drawing.Point(122, 27);
            this.mTypeLabel.Name = "mTypeLabel";
            this.mTypeLabel.Size = new System.Drawing.Size(81, 16);
            this.mTypeLabel.TabIndex = 0;
            this.mTypeLabel.Text = "Media Type";
            // 
            // mIdLabel
            // 
            this.mIdLabel.AutoSize = true;
            this.mIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mIdLabel.Location = new System.Drawing.Point(10, 27);
            this.mIdLabel.Name = "mIdLabel";
            this.mIdLabel.Size = new System.Drawing.Size(72, 16);
            this.mIdLabel.TabIndex = 0;
            this.mIdLabel.Text = "ID Number";
            // 
            // mSelectLabel
            // 
            this.mSelectLabel.AutoSize = true;
            this.mSelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mSelectLabel.Location = new System.Drawing.Point(17, 28);
            this.mSelectLabel.Name = "mSelectLabel";
            this.mSelectLabel.Size = new System.Drawing.Size(91, 20);
            this.mSelectLabel.TabIndex = 16;
            this.mSelectLabel.Text = "Media File";
            // 
            // mRemoveButton
            // 
            this.mRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mRemoveButton.Location = new System.Drawing.Point(117, 384);
            this.mRemoveButton.Name = "mRemoveButton";
            this.mRemoveButton.Size = new System.Drawing.Size(77, 24);
            this.mRemoveButton.TabIndex = 3;
            this.mRemoveButton.Text = "Remove";
            this.mRemoveButton.UseVisualStyleBackColor = true;
            this.mRemoveButton.Click += new System.EventHandler(this.mRemoveButton_Click);
            // 
            // mAddButton
            // 
            this.mAddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAddButton.Location = new System.Drawing.Point(21, 384);
            this.mAddButton.Name = "mAddButton";
            this.mAddButton.Size = new System.Drawing.Size(77, 24);
            this.mAddButton.TabIndex = 2;
            this.mAddButton.Text = "Add";
            this.mAddButton.UseVisualStyleBackColor = true;
            this.mAddButton.Click += new System.EventHandler(this.mAddButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 448);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "EditorForm";
            this.Text = "GVirtual Database Editor";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.tabControl.ResumeLayout(false);
            this.poiTab.ResumeLayout(false);
            this.poiTab.PerformLayout();
            this.pGroup.ResumeLayout(false);
            this.pGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pIconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pYField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pXField)).EndInit();
            this.deptTab.ResumeLayout(false);
            this.deptTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mediaTab.ResumeLayout(false);
            this.mediaTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPreviewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage poiTab;
        private System.Windows.Forms.TabPage deptTab;
        private System.Windows.Forms.TabPage mediaTab;
        private System.Windows.Forms.GroupBox pGroup;
        private System.Windows.Forms.Label pSelectLabel;
        private System.Windows.Forms.Button pRemoveButton;
        private System.Windows.Forms.Button pAddButton;
        private System.Windows.Forms.Label pLocationLabel;
        private System.Windows.Forms.Label pNameLabel;
        private System.Windows.Forms.Label pIdLabel;
        private System.Windows.Forms.TextBox pDescField;
        private System.Windows.Forms.Label pDescLabel;
        private System.Windows.Forms.PictureBox pIconBox;
        private System.Windows.Forms.CheckedListBox pDeptList;
        private System.Windows.Forms.NumericUpDown pYField;
        private System.Windows.Forms.NumericUpDown pXField;
        private System.Windows.Forms.TextBox pNameField;
        private System.Windows.Forms.TextBox pIdField;
        private System.Windows.Forms.Label pYLabel;
        private System.Windows.Forms.Label pXLabel;
        private System.Windows.Forms.Label pImageLabel;
        private System.Windows.Forms.Label pDeptLabel;
        private System.Windows.Forms.ListView pSelect;
        private System.Windows.Forms.Button pSaveButton;
        private System.Windows.Forms.ComboBox pIconCombo;
        private System.Windows.Forms.Button dSaveButton;
        private System.Windows.Forms.ListView dSelect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox dMediaList;
        private System.Windows.Forms.TextBox dNameField;
        private System.Windows.Forms.TextBox dIdField;
        private System.Windows.Forms.TextBox dDescField;
        private System.Windows.Forms.Label dDescLabel;
        private System.Windows.Forms.Label dNameLabel;
        private System.Windows.Forms.Label dTextLabel;
        private System.Windows.Forms.Label dIdLabel;
        private System.Windows.Forms.Label dSelectLabel;
        private System.Windows.Forms.Button dRemoveButton;
        private System.Windows.Forms.Button dAddButton;
        private System.Windows.Forms.Button mSaveButton;
        private System.Windows.Forms.ListView mSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox mNameField;
        private System.Windows.Forms.TextBox mIdField;
        private System.Windows.Forms.TextBox mDescField;
        private System.Windows.Forms.Label mDescLabel;
        private System.Windows.Forms.Label mNameLabel;
        private System.Windows.Forms.Label mPreviewLabel;
        private System.Windows.Forms.Label mIdLabel;
        private System.Windows.Forms.Label mSelectLabel;
        private System.Windows.Forms.Button mRemoveButton;
        private System.Windows.Forms.Button mAddButton;
        private System.Windows.Forms.ComboBox mFileCombo;
        private System.Windows.Forms.Label mFileLabel;
        private System.Windows.Forms.TextBox mPreviewField;
        private System.Windows.Forms.PictureBox mPreviewBox;
        private System.Windows.Forms.Label mTypeLabel;
        private System.Windows.Forms.ComboBox mTypeCombo;
    }
}
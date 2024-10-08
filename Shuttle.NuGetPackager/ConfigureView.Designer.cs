﻿namespace Shuttle.NuGetPackager
{
    partial class ConfigureView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureView));
            this.PackageName = new System.Windows.Forms.TextBox();
            this.ExplicitPackageName = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Authors = new System.Windows.Forms.TextBox();
            this.Owners = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Tags = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LicenseLabel = new System.Windows.Forms.Label();
            this.LicenseType = new System.Windows.Forms.ComboBox();
            this.License = new System.Windows.Forms.TextBox();
            this.IconPath = new System.Windows.Forms.TextBox();
            this.HasIcon = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.RequireLicenseAcceptance = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.RepositoryUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ProjectUrl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ReadmePath = new System.Windows.Forms.TextBox();
            this.HasReadme = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TargetFrameworkUnified = new System.Windows.Forms.RadioButton();
            this.TargetFrameworkStandard = new System.Windows.Forms.RadioButton();
            this.TargetFrameworkBoth = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PackageName
            // 
            this.PackageName.Enabled = false;
            this.PackageName.Location = new System.Drawing.Point(16, 32);
            this.PackageName.Name = "PackageName";
            this.PackageName.Size = new System.Drawing.Size(240, 20);
            this.PackageName.TabIndex = 1;
            // 
            // ExplicitPackageName
            // 
            this.ExplicitPackageName.AutoSize = true;
            this.ExplicitPackageName.Location = new System.Drawing.Point(16, 16);
            this.ExplicitPackageName.Name = "ExplicitPackageName";
            this.ExplicitPackageName.Size = new System.Drawing.Size(142, 17);
            this.ExplicitPackageName.TabIndex = 0;
            this.ExplicitPackageName.Text = "Explicit Package Name?";
            this.ExplicitPackageName.UseVisualStyleBackColor = true;
            this.ExplicitPackageName.CheckedChanged += new System.EventHandler(this.ExplicitPackageName_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Authors";
            // 
            // Authors
            // 
            this.Authors.Location = new System.Drawing.Point(16, 176);
            this.Authors.Name = "Authors";
            this.Authors.Size = new System.Drawing.Size(240, 20);
            this.Authors.TabIndex = 5;
            // 
            // Owners
            // 
            this.Owners.Location = new System.Drawing.Point(16, 224);
            this.Owners.Name = "Owners";
            this.Owners.Size = new System.Drawing.Size(240, 20);
            this.Owners.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Owners";
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(16, 80);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(240, 68);
            this.Description.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description";
            // 
            // Tags
            // 
            this.Tags.Location = new System.Drawing.Point(272, 272);
            this.Tags.Name = "Tags";
            this.Tags.Size = new System.Drawing.Size(240, 20);
            this.Tags.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(272, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Tags";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "License Type";
            // 
            // LicenseLabel
            // 
            this.LicenseLabel.AutoSize = true;
            this.LicenseLabel.Location = new System.Drawing.Point(272, 64);
            this.LicenseLabel.Name = "LicenseLabel";
            this.LicenseLabel.Size = new System.Drawing.Size(44, 13);
            this.LicenseLabel.TabIndex = 14;
            this.LicenseLabel.Text = "License";
            // 
            // LicenseType
            // 
            this.LicenseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LicenseType.FormattingEnabled = true;
            this.LicenseType.Items.AddRange(new object[] {
            "None",
            "Expression",
            "File"});
            this.LicenseType.Location = new System.Drawing.Point(272, 32);
            this.LicenseType.Name = "LicenseType";
            this.LicenseType.Size = new System.Drawing.Size(240, 21);
            this.LicenseType.TabIndex = 13;
            this.LicenseType.SelectedIndexChanged += new System.EventHandler(this.LicenseType_SelectedIndexChanged);
            // 
            // License
            // 
            this.License.Enabled = false;
            this.License.Location = new System.Drawing.Point(272, 80);
            this.License.Name = "License";
            this.License.Size = new System.Drawing.Size(240, 20);
            this.License.TabIndex = 15;
            // 
            // IconPath
            // 
            this.IconPath.Enabled = false;
            this.IconPath.Location = new System.Drawing.Point(272, 176);
            this.IconPath.Name = "IconPath";
            this.IconPath.Size = new System.Drawing.Size(240, 20);
            this.IconPath.TabIndex = 19;
            // 
            // HasIcon
            // 
            this.HasIcon.AutoSize = true;
            this.HasIcon.Location = new System.Drawing.Point(272, 158);
            this.HasIcon.Name = "HasIcon";
            this.HasIcon.Size = new System.Drawing.Size(75, 17);
            this.HasIcon.TabIndex = 17;
            this.HasIcon.Text = "Has Icon?";
            this.HasIcon.UseVisualStyleBackColor = true;
            this.HasIcon.CheckedChanged += new System.EventHandler(this.HasIcon_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(432, 397);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 32);
            this.Cancel.TabIndex = 27;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(336, 397);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(80, 32);
            this.Ok.TabIndex = 26;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // RequireLicenseAcceptance
            // 
            this.RequireLicenseAcceptance.AutoSize = true;
            this.RequireLicenseAcceptance.Location = new System.Drawing.Point(272, 111);
            this.RequireLicenseAcceptance.Name = "RequireLicenseAcceptance";
            this.RequireLicenseAcceptance.Size = new System.Drawing.Size(165, 17);
            this.RequireLicenseAcceptance.TabIndex = 16;
            this.RequireLicenseAcceptance.Text = "Require license acceptance?";
            this.RequireLicenseAcceptance.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(464, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "File Path";
            // 
            // RepositoryUrl
            // 
            this.RepositoryUrl.Location = new System.Drawing.Point(16, 272);
            this.RepositoryUrl.Name = "RepositoryUrl";
            this.RepositoryUrl.Size = new System.Drawing.Size(240, 20);
            this.RepositoryUrl.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Repository Url";
            // 
            // ProjectUrl
            // 
            this.ProjectUrl.Location = new System.Drawing.Point(16, 316);
            this.ProjectUrl.Name = "ProjectUrl";
            this.ProjectUrl.Size = new System.Drawing.Size(240, 20);
            this.ProjectUrl.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 300);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Project Url";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(464, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "File Path";
            // 
            // ReadmePath
            // 
            this.ReadmePath.Enabled = false;
            this.ReadmePath.Location = new System.Drawing.Point(272, 224);
            this.ReadmePath.Name = "ReadmePath";
            this.ReadmePath.Size = new System.Drawing.Size(240, 20);
            this.ReadmePath.TabIndex = 22;
            // 
            // HasReadme
            // 
            this.HasReadme.AutoSize = true;
            this.HasReadme.Location = new System.Drawing.Point(272, 206);
            this.HasReadme.Name = "HasReadme";
            this.HasReadme.Size = new System.Drawing.Size(117, 17);
            this.HasReadme.TabIndex = 20;
            this.HasReadme.Text = "Has README.md?";
            this.HasReadme.UseVisualStyleBackColor = true;
            this.HasReadme.CheckedChanged += new System.EventHandler(this.HasReadme_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TargetFrameworkBoth);
            this.groupBox1.Controls.Add(this.TargetFrameworkStandard);
            this.groupBox1.Controls.Add(this.TargetFrameworkUnified);
            this.groupBox1.Location = new System.Drawing.Point(272, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 91);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Framework";
            // 
            // TargetFrameworkUnified
            // 
            this.TargetFrameworkUnified.AutoSize = true;
            this.TargetFrameworkUnified.Checked = true;
            this.TargetFrameworkUnified.Location = new System.Drawing.Point(6, 19);
            this.TargetFrameworkUnified.Name = "TargetFrameworkUnified";
            this.TargetFrameworkUnified.Size = new System.Drawing.Size(116, 17);
            this.TargetFrameworkUnified.TabIndex = 0;
            this.TargetFrameworkUnified.TabStop = true;
            this.TargetFrameworkUnified.Text = "Unified (.NET 6.0+)";
            this.TargetFrameworkUnified.UseVisualStyleBackColor = true;
            // 
            // TargetFrameworkStandard
            // 
            this.TargetFrameworkStandard.AutoSize = true;
            this.TargetFrameworkStandard.Location = new System.Drawing.Point(6, 42);
            this.TargetFrameworkStandard.Name = "TargetFrameworkStandard";
            this.TargetFrameworkStandard.Size = new System.Drawing.Size(92, 17);
            this.TargetFrameworkStandard.TabIndex = 1;
            this.TargetFrameworkStandard.Text = "Standard (2.1)";
            this.TargetFrameworkStandard.UseVisualStyleBackColor = true;
            // 
            // TargetFrameworkBoth
            // 
            this.TargetFrameworkBoth.AutoSize = true;
            this.TargetFrameworkBoth.Location = new System.Drawing.Point(6, 65);
            this.TargetFrameworkBoth.Name = "TargetFrameworkBoth";
            this.TargetFrameworkBoth.Size = new System.Drawing.Size(47, 17);
            this.TargetFrameworkBoth.TabIndex = 2;
            this.TargetFrameworkBoth.Text = "Both";
            this.TargetFrameworkBoth.UseVisualStyleBackColor = true;
            // 
            // ConfigureView
            // 
            this.AcceptButton = this.Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(529, 435);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ReadmePath);
            this.Controls.Add(this.HasReadme);
            this.Controls.Add(this.ProjectUrl);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RepositoryUrl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RequireLicenseAcceptance);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.IconPath);
            this.Controls.Add(this.HasIcon);
            this.Controls.Add(this.License);
            this.Controls.Add(this.LicenseType);
            this.Controls.Add(this.LicenseLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Tags);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Owners);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Authors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PackageName);
            this.Controls.Add(this.ExplicitPackageName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure NuGet Project";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LicenseLabel;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        public System.Windows.Forms.TextBox PackageName;
        public System.Windows.Forms.CheckBox ExplicitPackageName;
        public System.Windows.Forms.TextBox Authors;
        public System.Windows.Forms.TextBox Owners;
        public System.Windows.Forms.TextBox Description;
        public System.Windows.Forms.TextBox Tags;
        public System.Windows.Forms.ComboBox LicenseType;
        public System.Windows.Forms.TextBox License;
        public System.Windows.Forms.TextBox IconPath;
        public System.Windows.Forms.CheckBox HasIcon;
        public System.Windows.Forms.CheckBox RequireLicenseAcceptance;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox ProjectUrl;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox RepositoryUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox ReadmePath;
        public System.Windows.Forms.CheckBox HasReadme;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton TargetFrameworkBoth;
        private System.Windows.Forms.RadioButton TargetFrameworkStandard;
        private System.Windows.Forms.RadioButton TargetFrameworkUnified;
    }
}
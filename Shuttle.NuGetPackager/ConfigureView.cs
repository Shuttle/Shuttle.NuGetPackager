using System;
using System.Linq;
using System.Windows.Forms;

namespace Shuttle.NuGetPackager
{
    public partial class ConfigureView : Form
    {
        private const string Required = "Required";

        public ConfigureView()
        {
            InitializeComponent();
        }

        private void ExplicitPackageName_CheckedChanged(object sender, EventArgs e)
        {
            PackageName.Enabled = ExplicitPackageName.Checked;
        }

        private void LicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            License.Enabled = LicenseType.SelectedIndex > 0;

            switch (LicenseType.SelectedText)
            {
                case "Expression":
                {
                    LicenseLabel.Text = @"License Expression";
                    break;
                }
                case "File":
                {
                    LicenseLabel.Text = @"License File Path";
                    break;
                }
            }
        }

        private void HasIcon_CheckedChanged(object sender, EventArgs e)
        {
            IconPath.Enabled = HasIcon.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            ErrorProvider.SetError(PackageName,
                ExplicitPackageName.Checked && string.IsNullOrWhiteSpace(PackageName.Text)
                    ? Required
                    : string.Empty);

            ErrorProvider.SetError(Description,
                string.IsNullOrWhiteSpace(Description.Text)
                    ? Required
                    : string.Empty);

            ErrorProvider.SetError(Authors,
                string.IsNullOrWhiteSpace(Authors.Text)
                    ? Required
                    : string.Empty);

            ErrorProvider.SetError(License,
                LicenseType.SelectedIndex > 0 && string.IsNullOrWhiteSpace(License.Text)
                    ? Required
                    : string.Empty);

            ErrorProvider.SetError(IconPath,
                HasIcon.Checked && string.IsNullOrWhiteSpace(IconPath.Text)
                    ? Required
                    : string.Empty);

            ErrorProvider.SetError(ReadmePath,
                HasReadme.Checked && string.IsNullOrWhiteSpace(ReadmePath.Text)
                    ? Required
                    : string.Empty);

            if (HasErrors())
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }

        public bool IsTargetFrameworkUnified => TargetFrameworkUnified.Checked;
        public bool IsTargetFrameworkStandard => TargetFrameworkStandard.Checked;
        public bool IsTargetFrameworkBoth => TargetFrameworkUnified.Checked && TargetFrameworkStandard.Checked;

        private bool HasErrors()
        {
            return ErrorProvider.ContainerControl.Controls.Cast<Control>().Any(c => ErrorProvider.GetError(c) != string.Empty);
        }

        private void HasReadme_CheckedChanged(object sender, EventArgs e)
        {
            ReadmePath.Enabled = HasReadme.Checked;
        }
    }
}
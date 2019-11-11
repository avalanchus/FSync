using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using FSyncLib;

namespace FSyncManager
{
    public partial class Settings : Form
    {
        private readonly Configuration _configuration;

        public string Exclusions
        {
            get => tbxExlusions.Text;
            set => tbxExlusions.Text = value;
        }

        public string Extensions
        {
            get => tbxExtensions.Text;
            set => tbxExtensions.Text = value;
        }

        public string Period
        {
            get => nudRefreshPeriod.Value.ToString(CultureInfo.InvariantCulture);
            set
            {
                Int32.TryParse(value, out var period);
                nudRefreshPeriod.Value = period;
            }
        }

        public string NumberFilesToCopy
        {
            get => nudNumberFilesToCopy.Value.ToString(CultureInfo.InvariantCulture);
            set
            {
                Int32.TryParse(value, out var numberFilesToCopy);
                nudNumberFilesToCopy.Value = numberFilesToCopy;
            }
        }

        public Measure Measure
        {
            get => Enum.TryParse(cbMeasure.SelectedText, out Measure measure) ? measure : Measure.s;
            set => cbMeasure.SelectedIndex = cbMeasure.FindStringExact(value.ToString());
        }

        public OperationType OperationType
        {
            get => Enum.TryParse(cbMeasure.SelectedText, out OperationType operationType) ? operationType : OperationType.Copy;
            set => cbOperationType.SelectedIndex = cbOperationType.FindStringExact(value.ToString());
        }

        public bool WriteToLog
        {
            get => chbWriteToLog.Checked;
            set => chbWriteToLog.Checked = value;
        }

        public string SourceFolder
        {
            get => tbxSourceFolder.Text;
            set => tbxSourceFolder.Text = value;
        }

        public string DestFolder
        {
            get => tbxDestFolder.Text;
            set => tbxDestFolder.Text = value;
        }

        private void btnSourceFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbxSourceFolder.Text))
                sourceFolderBrowserDialog.SelectedPath = tbxSourceFolder.Text;
            if (sourceFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbxSourceFolder.Text = sourceFolderBrowserDialog.SelectedPath;
            }
        }

        private void btnDestFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(tbxDestFolder.Text))
                destFolderBrowserDialog.SelectedPath = tbxDestFolder.Text;
            if (destFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbxDestFolder.Text = destFolderBrowserDialog.SelectedPath;
            }
        }

        private bool SetConfig(string name, string value)
        {
            var isDiffer = value != _configuration.AppSettings.Settings[name].Value;
            if (isDiffer)
            {
                _configuration.AppSettings.Settings.Remove(name);
                _configuration.AppSettings.Settings.Add(name, value);
            }
            return isDiffer;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            bool isDiffer = false;
            isDiffer |= SetConfig(nameof(Exclusions), Exclusions);
            isDiffer |= SetConfig(nameof(Extensions), Extensions);
            isDiffer |= SetConfig(nameof(Period), Period);
            isDiffer |= SetConfig(nameof(Measure), Measure.ToString());
            isDiffer |= SetConfig(nameof(OperationType), OperationType.ToString());
            isDiffer |= SetConfig(nameof(NumberFilesToCopy), NumberFilesToCopy);
            isDiffer |= SetConfig(nameof(WriteToLog), WriteToLog.ToString());
            isDiffer |= SetConfig(nameof(SourceFolder), SourceFolder);
            isDiffer |= SetConfig(nameof(DestFolder), DestFolder);
            if (isDiffer)
                _configuration.Save(ConfigurationSaveMode.Modified);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;            
        }

        public Settings()
        {
            InitializeComponent();
            _configuration = FSync.GetConfiguration();
            Exclusions = _configuration.AppSettings.Settings[nameof(Exclusions)].Value;
            Extensions = _configuration.AppSettings.Settings[nameof(Extensions)].Value;
            Period = _configuration.AppSettings.Settings[nameof(Period)].Value;
            Measure = _configuration.AppSettings.Settings[nameof(Measure)].Value.ToMeasure();
            OperationType = _configuration.AppSettings.Settings[nameof(OperationType)].Value.ToOperationType();
            NumberFilesToCopy = _configuration.AppSettings.Settings[nameof(NumberFilesToCopy)].Value;
            WriteToLog = Convert.ToBoolean(_configuration.AppSettings.Settings[nameof(WriteToLog)].Value);
            SourceFolder = _configuration.AppSettings.Settings[nameof(SourceFolder)].Value;
            DestFolder = _configuration.AppSettings.Settings[nameof(DestFolder)].Value;
        }
    }
}

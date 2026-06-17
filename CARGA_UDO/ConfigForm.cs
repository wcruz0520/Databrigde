using System;
using System.Configuration;
using System.Windows.Forms;

namespace CARGA_UDO
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            cmbTipoServidor.Items.Clear();
            cmbTipoServidor.Items.Add(new ComboBoxItem("SQL Server 2012", "dst_MSSQL2012"));
            cmbTipoServidor.Items.Add(new ComboBoxItem("SQL Server 2014", "dst_MSSQL2014"));
            cmbTipoServidor.Items.Add(new ComboBoxItem("SQL Server 2016", "dst_MSSQL2016"));
            cmbTipoServidor.Items.Add(new ComboBoxItem("SQL Server 2017", "dst_MSSQL2017"));
            cmbTipoServidor.Items.Add(new ComboBoxItem("SQL Server 2019", "dst_MSSQL2019"));
            cmbTipoServidor.Items.Add(new ComboBoxItem("SAP HANA", "dst_HANADB"));
            cmbTipoServidor.DisplayMember = "Text";
            cmbTipoServidor.ValueMember = "Value";

            txtServidor.Text = SapConnectionConfig.GetValue(SapConnectionConfig.ServerKey);
            txtServidorLicencia.Text = SapConnectionConfig.GetValue(SapConnectionConfig.LicenseServerKey);
            txtBaseCompania.Text = SapConnectionConfig.GetValue(SapConnectionConfig.CompanyDbKey);
            txtUsuarioBD.Text = SapConnectionConfig.GetValue(SapConnectionConfig.DbUserKey);
            txtClaveBD.Text = SapConnectionConfig.GetValue(SapConnectionConfig.DbPasswordKey);
            txtUsuarioSAP.Text = SapConnectionConfig.GetValue(SapConnectionConfig.SapUserKey);
            txtClaveSAP.Text = SapConnectionConfig.GetValue(SapConnectionConfig.SapPasswordKey);
            SelectServerType(SapConnectionConfig.GetValue(SapConnectionConfig.DbServerTypeKey));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            SapConnectionConfig.Save(
                txtServidor.Text.Trim(),
                txtServidorLicencia.Text.Trim(),
                txtBaseCompania.Text.Trim(),
                GetSelectedServerType(),
                txtUsuarioBD.Text.Trim(),
                txtClaveBD.Text,
                txtUsuarioSAP.Text.Trim(),
                txtClaveSAP.Text);

            MessageBox.Show("Configuración guardada correctamente.", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtServidor.Text) ||
                string.IsNullOrWhiteSpace(txtServidorLicencia.Text) ||
                string.IsNullOrWhiteSpace(txtBaseCompania.Text) ||
                cmbTipoServidor.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtUsuarioBD.Text) ||
                string.IsNullOrWhiteSpace(txtUsuarioSAP.Text))
            {
                MessageBox.Show("Complete servidor, servidor de licencias, compañía, tipo de base de datos, usuario BD y usuario SAP.",
                    "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string GetSelectedServerType()
        {
            return (cmbTipoServidor.SelectedItem as ComboBoxItem)?.Value ?? string.Empty;
        }

        private void SelectServerType(string value)
        {
            for (int i = 0; i < cmbTipoServidor.Items.Count; i++)
            {
                if ((cmbTipoServidor.Items[i] as ComboBoxItem)?.Value == value)
                {
                    cmbTipoServidor.SelectedIndex = i;
                    return;
                }
            }

            if (cmbTipoServidor.Items.Count > 0)
                cmbTipoServidor.SelectedIndex = 0;
        }

        private class ComboBoxItem
        {
            public ComboBoxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public string Text { get; }
            public string Value { get; }
        }
    }
}

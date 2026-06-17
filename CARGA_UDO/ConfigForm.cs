using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CARGA_UDO
{
    public partial class ConfigForm : Form
    {
        private readonly List<SapConnectionProfile> profiles = new List<SapConnectionProfile>();
        private string activeProfileId;
        private bool loadingProfile;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            CargarTiposServidor();
            profiles.Clear();
            profiles.AddRange(SapConnectionConfig.GetProfiles());
            activeProfileId = SapConnectionConfig.GetActiveProfile()?.Id;

            if (profiles.Count == 0)
                profiles.Add(SapConnectionConfig.CreateNewProfile("Conexión SAP 1"));

            RefrescarListaConexiones(activeProfileId ?? profiles[0].Id);
        }

        private void CargarTiposServidor()
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
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!GuardarPerfilSeleccionado())
                return;

            SapConnectionProfile selected = GetSelectedProfile();
            SapConnectionConfig.SaveProfiles(profiles, selected?.Id);
            MessageBox.Show("Conexiones guardadas correctamente.", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //DialogResult = DialogResult.OK;
            //Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAgregarConexion_Click(object sender, EventArgs e)
        {
            if (!GuardarPerfilSeleccionado())
                return;

            SapConnectionProfile profile = SapConnectionConfig.CreateNewProfile($"Conexión SAP {profiles.Count + 1}");
            profiles.Add(profile);
            RefrescarListaConexiones(profile.Id);
            txtNombreConexion.Focus();
            txtNombreConexion.SelectAll();
        }

        private void btnEliminarConexion_Click(object sender, EventArgs e)
        {
            SapConnectionProfile selected = GetSelectedProfile();
            if (selected == null)
                return;

            if (MessageBox.Show($"¿Eliminar la conexión '{selected.DisplayName}'?", "Eliminar conexión", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            profiles.Remove(selected);
            if (profiles.Count == 0)
                profiles.Add(SapConnectionConfig.CreateNewProfile("Conexión SAP 1"));

            RefrescarListaConexiones(profiles[0].Id);
        }

        private void lstConexiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingProfile)
                return;

            CargarPerfil(GetSelectedProfile());
        }

        private bool GuardarPerfilSeleccionado()
        {
            SapConnectionProfile selected = GetSelectedProfile();
            if (selected == null)
                return true;

            if (!ValidarCampos())
                return false;

            selected.Name = txtNombreConexion.Text.Trim();
            selected.Server = txtServidor.Text.Trim();
            selected.LicenseServer = txtServidorLicencia.Text.Trim();
            selected.CompanyDb = txtBaseCompania.Text.Trim();
            selected.DbServerType = GetSelectedServerType();
            selected.DbUser = txtUsuarioBD.Text.Trim();
            selected.DbPassword = txtClaveBD.Text;
            selected.SapUser = txtUsuarioSAP.Text.Trim();
            selected.SapPassword = txtClaveSAP.Text;
            RefrescarListaConexiones(selected.Id);
            return true;
        }

        private void RefrescarListaConexiones(string selectedId)
        {
            loadingProfile = true;
            lstConexiones.DataSource = null;
            lstConexiones.DataSource = profiles.ToList();
            lstConexiones.DisplayMember = "DisplayName";
            loadingProfile = false;

            SapConnectionProfile selected = profiles.FirstOrDefault(profile => profile.Id == selectedId) ?? profiles.FirstOrDefault();
            lstConexiones.SelectedItem = selected;
            CargarPerfil(selected);
        }

        private void CargarPerfil(SapConnectionProfile profile)
        {
            loadingProfile = true;
            txtNombreConexion.Text = profile?.Name ?? string.Empty;
            txtServidor.Text = profile?.Server ?? string.Empty;
            txtServidorLicencia.Text = profile?.LicenseServer ?? string.Empty;
            txtBaseCompania.Text = profile?.CompanyDb ?? string.Empty;
            txtUsuarioBD.Text = profile?.DbUser ?? string.Empty;
            txtClaveBD.Text = profile?.DbPassword ?? string.Empty;
            txtUsuarioSAP.Text = profile?.SapUser ?? string.Empty;
            txtClaveSAP.Text = profile?.SapPassword ?? string.Empty;
            SelectServerType(profile?.DbServerType);
            loadingProfile = false;
        }

        private SapConnectionProfile GetSelectedProfile()
        {
            return lstConexiones.SelectedItem as SapConnectionProfile;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreConexion.Text) ||
                string.IsNullOrWhiteSpace(txtServidor.Text) ||
                string.IsNullOrWhiteSpace(txtServidorLicencia.Text) ||
                string.IsNullOrWhiteSpace(txtBaseCompania.Text) ||
                cmbTipoServidor.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtUsuarioBD.Text) ||
                string.IsNullOrWhiteSpace(txtUsuarioSAP.Text))
            {
                MessageBox.Show("Complete nombre, servidor, servidor de licencias, compañía, tipo de base de datos, usuario BD y usuario SAP.",
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

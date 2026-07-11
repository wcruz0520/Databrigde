using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace CARGA_UDO
{
    public partial class ConfigForm : Form
    {
        private readonly List<SapConnectionProfile> profiles = new List<SapConnectionProfile>();
        private string activeProfileId;
        private bool loadingProfile;

        public bool ConnectionsChanged { get; private set; }

        public ConfigForm()
        {
            InitializeComponent();
            AplicarDisenoProfesional();
        }

        private void AplicarDisenoProfesional()
        {
            Color colorPrimario = SystemColors.ActiveCaption;
            Color colorTexto = Color.FromArgb(45, 55, 72);
            Color colorFondo = Color.White;

            BackColor = colorFondo;
            Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);

            lstConexiones.BackColor = Color.FromArgb(248, 251, 255);
            lstConexiones.BorderStyle = BorderStyle.FixedSingle;
            lstConexiones.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);

            EstilizarBoton(btnAgregarConexion, colorPrimario, Color.Black);
            EstilizarBoton(btnEliminarConexion, Color.Gainsboro, colorTexto);
            EstilizarBoton(btnGuardar, colorPrimario, Color.Black);
            EstilizarBoton(btnCancelar, Color.Gainsboro, colorTexto);

            foreach (Control control in Controls)
            {
                Label label = control as Label;
                TextBox textBox = control as TextBox;
                ComboBox comboBox = control as ComboBox;
                CheckBox checkBox = control as CheckBox;

                if (label != null)
                {
                    label.ForeColor = colorTexto;
                    label.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                else if (textBox != null)
                {
                    textBox.BackColor = colorFondo;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                else if (comboBox != null)
                {
                    comboBox.BackColor = colorFondo;
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                else if (checkBox != null)
                {
                    checkBox.ForeColor = colorTexto;
                    checkBox.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
            }

            nudVersionSAP.BorderStyle = BorderStyle.FixedSingle;
            nudVersionSAP.BackColor = colorFondo;
            nudVersionSAP.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void EstilizarBoton(Button boton, Color fondo, Color texto)
        {
            boton.BackColor = fondo;
            boton.ForeColor = texto;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = Color.FromArgb(180, 195, 210);
            boton.FlatAppearance.BorderSize = 1;
            boton.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            boton.UseVisualStyleBackColor = false;
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            CargarTiposServidor();
            CargarMediosConexion();
            profiles.Clear();
            profiles.AddRange(SapConnectionConfig.GetProfiles());
            activeProfileId = SapConnectionConfig.GetActiveProfile()?.Id;

            if (profiles.Count == 0)
                profiles.Add(SapConnectionConfig.CreateNewProfile("Conexión SAP 1"));

            RefrescarListaConexiones(activeProfileId ?? profiles[0].Id);
        }

        private void CargarMediosConexion()
        {
            cmbMedioConexion.Items.Clear();
            cmbMedioConexion.Items.Add(new ComboBoxItem("DI API", SapConnectionMethod.DiApi.ToString()));
            cmbMedioConexion.Items.Add(new ComboBoxItem("Service Layer", SapConnectionMethod.ServiceLayer.ToString()));
            cmbMedioConexion.DisplayMember = "Text";
            cmbMedioConexion.ValueMember = "Value";
            cmbMedioConexion.SelectedIndex = 0;
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
            ConnectionsChanged = true;
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
            selected.Version = Convert.ToInt32(nudVersionSAP.Value);
            selected.UseTrusted = chckUseTrusted.Checked;
            selected.SLDServer = txtServidorSLD.Text.Trim();
            selected.LicenseServer = txtServidorLicencia.Text.Trim();
            selected.CompanyDb = txtBaseCompania.Text.Trim();
            selected.DbServerType = GetSelectedServerType();
            selected.DbUser = txtUsuarioBD.Text.Trim();
            selected.DbPassword = txtClaveBD.Text;
            selected.SapUser = txtUsuarioSAP.Text.Trim();
            selected.SapPassword = txtClaveSAP.Text;
            selected.ConnectionMethod = GetSelectedConnectionMethod();
            selected.ServiceLayerUrl = txtServiceLayerUrl.Text.Trim();
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
            nudVersionSAP.Value = profile?.Version > 0 ? profile.Version : 10;
            chckUseTrusted.Checked = profile?.UseTrusted == true;
            txtServidorSLD.Text = profile?.SLDServer ?? string.Empty;
            txtServidorLicencia.Text = profile?.LicenseServer ?? string.Empty;
            txtBaseCompania.Text = profile?.CompanyDb ?? string.Empty;
            txtUsuarioBD.Text = profile?.DbUser ?? string.Empty;
            txtClaveBD.Text = profile?.DbPassword ?? string.Empty;
            txtUsuarioSAP.Text = profile?.SapUser ?? string.Empty;
            txtClaveSAP.Text = profile?.SapPassword ?? string.Empty;
            txtServiceLayerUrl.Text = profile?.ServiceLayerUrl ?? string.Empty;
            SelectConnectionMethod(profile?.ConnectionMethod ?? SapConnectionMethod.DiApi);
            SelectServerType(profile?.DbServerType);
            loadingProfile = false;
            ActualizarCamposPorMedioConexion();
        }

        private SapConnectionProfile GetSelectedProfile()
        {
            return lstConexiones.SelectedItem as SapConnectionProfile;
        }

        private bool ValidarCampos()
        {
            SapConnectionMethod method = GetSelectedConnectionMethod();

            if (string.IsNullOrWhiteSpace(txtNombreConexion.Text) ||
                string.IsNullOrWhiteSpace(txtBaseCompania.Text) ||
                string.IsNullOrWhiteSpace(txtUsuarioSAP.Text))
            {
                MessageBox.Show("Complete nombre, compañía y usuario SAP.",
                    "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (method == SapConnectionMethod.ServiceLayer)
            {
                if (string.IsNullOrWhiteSpace(txtServiceLayerUrl.Text))
                {
                    MessageBox.Show("Para Service Layer complete la URL del servicio.",
                        "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }

            if (string.IsNullOrWhiteSpace(txtServidor.Text) || cmbTipoServidor.SelectedItem == null)
            {
                MessageBox.Show("Para DI API complete servidor SAP y tipo de base de datos.",
                    "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudVersionSAP.Value < 10)
            {
                if (string.IsNullOrWhiteSpace(txtServidorLicencia.Text) ||
                    (!chckUseTrusted.Checked && string.IsNullOrWhiteSpace(txtUsuarioBD.Text)))
                {
                    MessageBox.Show("Para SAP menor a versión 10 complete servidor de licencias y, si no usa trusted, usuario BD.",
                        "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (string.IsNullOrWhiteSpace(txtServidorSLD.Text))
            {
                MessageBox.Show("Para SAP versión 10 o superior complete el servidor SLD.",
                    "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private void cmbMedioConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadingProfile)
                return;

            ActualizarCamposPorMedioConexion();
        }

        private void ActualizarCamposPorMedioConexion()
        {
            bool esServiceLayer = GetSelectedConnectionMethod() == SapConnectionMethod.ServiceLayer;

            lblServiceLayerUrl.Visible = esServiceLayer;
            txtServiceLayerUrl.Visible = esServiceLayer;

            lblServidor.Visible = !esServiceLayer;
            txtServidor.Visible = !esServiceLayer;
            label2.Visible = !esServiceLayer;
            nudVersionSAP.Visible = !esServiceLayer;
            lblServidorLicencia.Visible = !esServiceLayer;
            txtServidorLicencia.Visible = !esServiceLayer;
            label1.Visible = !esServiceLayer;
            txtServidorSLD.Visible = !esServiceLayer;
            chckUseTrusted.Visible = !esServiceLayer;
            lblTipoServidor.Visible = !esServiceLayer;
            cmbTipoServidor.Visible = !esServiceLayer;
            lblUsuarioBD.Visible = !esServiceLayer;
            txtUsuarioBD.Visible = !esServiceLayer;
            lblClaveBD.Visible = !esServiceLayer;
            txtClaveBD.Visible = !esServiceLayer;

            if (esServiceLayer)
                OrganizarCamposServiceLayer();
            else
                OrganizarCamposDiApi();
        }

        private void OrganizarCamposServiceLayer()
        {
            SetTop(lblNombreConexion, txtNombreConexion, 55);
            SetTop(lblBaseCompania, txtBaseCompania, 89);
            SetTop(lblUsuarioSAP, txtUsuarioSAP, 123);
            SetTop(lblClaveSAP, txtClaveSAP, 157);
            SetTop(lblServiceLayerUrl, txtServiceLayerUrl, 191);
            //btnGuardar.Top = 235;
            //btnCancelar.Top = 235;
            //ClientSize = new System.Drawing.Size(ClientSize.Width, 291);
        }

        private void OrganizarCamposDiApi()
        {
            SetTop(lblNombreConexion, txtNombreConexion, 55);
            SetTop(label2, nudVersionSAP, 89);
            SetTop(lblServidor, txtServidor, 123);
            SetTop(lblServidorLicencia, txtServidorLicencia, 163);
            SetTop(label1, txtServidorSLD, 208);
            chckUseTrusted.Top = 208;
            SetTop(lblBaseCompania, txtBaseCompania, 247);
            SetTop(lblTipoServidor, cmbTipoServidor, 287);
            SetTop(lblUsuarioBD, txtUsuarioBD, 327);
            SetTop(lblClaveBD, txtClaveBD, 367);
            SetTop(lblUsuarioSAP, txtUsuarioSAP, 407);
            SetTop(lblClaveSAP, txtClaveSAP, 447);
            //btnGuardar.Top = 522;
            //btnCancelar.Top = 522;
            //ClientSize = new System.Drawing.Size(ClientSize.Width, 588);
        }

        private void SetTop(Control label, Control input, int top)
        {
            label.Top = top + 3;
            input.Top = top;
        }

        private SapConnectionMethod GetSelectedConnectionMethod()
        {
            string value = (cmbMedioConexion.SelectedItem as ComboBoxItem)?.Value;
            return Enum.TryParse(value, out SapConnectionMethod method) ? method : SapConnectionMethod.DiApi;
        }

        private void SelectConnectionMethod(SapConnectionMethod method)
        {
            for (int i = 0; i < cmbMedioConexion.Items.Count; i++)
            {
                if ((cmbMedioConexion.Items[i] as ComboBoxItem)?.Value == method.ToString())
                {
                    cmbMedioConexion.SelectedIndex = i;
                    return;
                }
            }

            if (cmbMedioConexion.Items.Count > 0)
                cmbMedioConexion.SelectedIndex = 0;
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

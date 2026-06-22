namespace CARGA_UDO
{
    partial class ConfigForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lstConexiones = new System.Windows.Forms.ListBox();
            this.btnAgregarConexion = new System.Windows.Forms.Button();
            this.btnEliminarConexion = new System.Windows.Forms.Button();
            this.lblNombreConexion = new System.Windows.Forms.Label();
            this.txtNombreConexion = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidorLicencia = new System.Windows.Forms.Label();
            this.txtServidorLicencia = new System.Windows.Forms.TextBox();
            this.lblBaseCompania = new System.Windows.Forms.Label();
            this.txtBaseCompania = new System.Windows.Forms.TextBox();
            this.lblTipoServidor = new System.Windows.Forms.Label();
            this.cmbTipoServidor = new System.Windows.Forms.ComboBox();
            this.lblUsuarioBD = new System.Windows.Forms.Label();
            this.txtUsuarioBD = new System.Windows.Forms.TextBox();
            this.lblClaveBD = new System.Windows.Forms.Label();
            this.txtClaveBD = new System.Windows.Forms.TextBox();
            this.lblUsuarioSAP = new System.Windows.Forms.Label();
            this.txtUsuarioSAP = new System.Windows.Forms.TextBox();
            this.lblClaveSAP = new System.Windows.Forms.Label();
            this.txtClaveSAP = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtServidorSLD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chckUseTrusted = new System.Windows.Forms.CheckBox();
            this.nudVersionSAP = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMedioConexion = new System.Windows.Forms.Label();
            this.cmbMedioConexion = new System.Windows.Forms.ComboBox();
            this.lblServiceLayerUrl = new System.Windows.Forms.Label();
            this.txtServiceLayerUrl = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersionSAP)).BeginInit();
            this.SuspendLayout();
            //
            // lstConexiones
            //
            this.lstConexiones.FormattingEnabled = true;
            this.lstConexiones.ItemHeight = 20;
            this.lstConexiones.Location = new System.Drawing.Point(24, 22);
            this.lstConexiones.Name = "lstConexiones";
            this.lstConexiones.Size = new System.Drawing.Size(224, 384);
            this.lstConexiones.TabIndex = 0;
            this.lstConexiones.SelectedIndexChanged += new System.EventHandler(this.lstConexiones_SelectedIndexChanged);
            //
            // btnAgregarConexion
            //
            this.btnAgregarConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.btnAgregarConexion.Location = new System.Drawing.Point(24, 420);
            this.btnAgregarConexion.Name = "btnAgregarConexion";
            this.btnAgregarConexion.Size = new System.Drawing.Size(104, 36);
            this.btnAgregarConexion.TabIndex = 1;
            this.btnAgregarConexion.Text = "Agregar";
            this.btnAgregarConexion.UseVisualStyleBackColor = true;
            this.btnAgregarConexion.Click += new System.EventHandler(this.btnAgregarConexion_Click);
            //
            // btnEliminarConexion
            //
            this.btnEliminarConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.btnEliminarConexion.Location = new System.Drawing.Point(144, 420);
            this.btnEliminarConexion.Name = "btnEliminarConexion";
            this.btnEliminarConexion.Size = new System.Drawing.Size(104, 36);
            this.btnEliminarConexion.TabIndex = 2;
            this.btnEliminarConexion.Text = "Eliminar";
            this.btnEliminarConexion.UseVisualStyleBackColor = true;
            this.btnEliminarConexion.Click += new System.EventHandler(this.btnEliminarConexion_Click);
            //
            // lblNombreConexion
            //
            this.lblNombreConexion.AutoSize = true;
            this.lblNombreConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblNombreConexion.Location = new System.Drawing.Point(278, 58);
            this.lblNombreConexion.Name = "lblNombreConexion";
            this.lblNombreConexion.Size = new System.Drawing.Size(58, 17);
            this.lblNombreConexion.TabIndex = 3;
            this.lblNombreConexion.Text = "Nombre";
            //
            // txtNombreConexion
            //
            this.txtNombreConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNombreConexion.Location = new System.Drawing.Point(450, 55);
            this.txtNombreConexion.Name = "txtNombreConexion";
            this.txtNombreConexion.Size = new System.Drawing.Size(306, 26);
            this.txtNombreConexion.TabIndex = 4;
            //
            // lblServidor
            //
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblServidor.Location = new System.Drawing.Point(278, 126);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(92, 17);
            this.lblServidor.TabIndex = 5;
            this.lblServidor.Text = "Servidor SAP";
            //
            // txtServidor
            //
            this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtServidor.Location = new System.Drawing.Point(450, 123);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(306, 26);
            this.txtServidor.TabIndex = 6;
            //
            // lblServidorLicencia
            //
            this.lblServidorLicencia.AutoSize = true;
            this.lblServidorLicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblServidorLicencia.Location = new System.Drawing.Point(278, 166);
            this.lblServidorLicencia.Name = "lblServidorLicencia";
            this.lblServidorLicencia.Size = new System.Drawing.Size(119, 17);
            this.lblServidorLicencia.TabIndex = 7;
            this.lblServidorLicencia.Text = "Servidor licencias";
            //
            // txtServidorLicencia
            //
            this.txtServidorLicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtServidorLicencia.Location = new System.Drawing.Point(450, 163);
            this.txtServidorLicencia.Name = "txtServidorLicencia";
            this.txtServidorLicencia.Size = new System.Drawing.Size(306, 26);
            this.txtServidorLicencia.TabIndex = 8;
            //
            // lblBaseCompania
            //
            this.lblBaseCompania.AutoSize = true;
            this.lblBaseCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblBaseCompania.Location = new System.Drawing.Point(278, 250);
            this.lblBaseCompania.Name = "lblBaseCompania";
            this.lblBaseCompania.Size = new System.Drawing.Size(105, 17);
            this.lblBaseCompania.TabIndex = 9;
            this.lblBaseCompania.Text = "Base compañía";
            //
            // txtBaseCompania
            //
            this.txtBaseCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtBaseCompania.Location = new System.Drawing.Point(450, 247);
            this.txtBaseCompania.Name = "txtBaseCompania";
            this.txtBaseCompania.Size = new System.Drawing.Size(306, 26);
            this.txtBaseCompania.TabIndex = 10;
            //
            // lblTipoServidor
            //
            this.lblTipoServidor.AutoSize = true;
            this.lblTipoServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblTipoServidor.Location = new System.Drawing.Point(278, 290);
            this.lblTipoServidor.Name = "lblTipoServidor";
            this.lblTipoServidor.Size = new System.Drawing.Size(110, 17);
            this.lblTipoServidor.TabIndex = 11;
            this.lblTipoServidor.Text = "Tipo base datos";
            //
            // cmbTipoServidor
            //
            this.cmbTipoServidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbTipoServidor.FormattingEnabled = true;
            this.cmbTipoServidor.Location = new System.Drawing.Point(450, 287);
            this.cmbTipoServidor.Name = "cmbTipoServidor";
            this.cmbTipoServidor.Size = new System.Drawing.Size(306, 28);
            this.cmbTipoServidor.TabIndex = 12;
            //
            // lblUsuarioBD
            //
            this.lblUsuarioBD.AutoSize = true;
            this.lblUsuarioBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblUsuarioBD.Location = new System.Drawing.Point(278, 330);
            this.lblUsuarioBD.Name = "lblUsuarioBD";
            this.lblUsuarioBD.Size = new System.Drawing.Size(80, 17);
            this.lblUsuarioBD.TabIndex = 13;
            this.lblUsuarioBD.Text = "Usuario BD";
            //
            // txtUsuarioBD
            //
            this.txtUsuarioBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUsuarioBD.Location = new System.Drawing.Point(450, 327);
            this.txtUsuarioBD.Name = "txtUsuarioBD";
            this.txtUsuarioBD.Size = new System.Drawing.Size(306, 26);
            this.txtUsuarioBD.TabIndex = 14;
            //
            // lblClaveBD
            //
            this.lblClaveBD.AutoSize = true;
            this.lblClaveBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblClaveBD.Location = new System.Drawing.Point(278, 370);
            this.lblClaveBD.Name = "lblClaveBD";
            this.lblClaveBD.Size = new System.Drawing.Size(66, 17);
            this.lblClaveBD.TabIndex = 15;
            this.lblClaveBD.Text = "Clave BD";
            //
            // txtClaveBD
            //
            this.txtClaveBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtClaveBD.Location = new System.Drawing.Point(450, 367);
            this.txtClaveBD.Name = "txtClaveBD";
            this.txtClaveBD.PasswordChar = '*';
            this.txtClaveBD.Size = new System.Drawing.Size(306, 26);
            this.txtClaveBD.TabIndex = 16;
            //
            // lblUsuarioSAP
            //
            this.lblUsuarioSAP.AutoSize = true;
            this.lblUsuarioSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblUsuarioSAP.Location = new System.Drawing.Point(278, 410);
            this.lblUsuarioSAP.Name = "lblUsuarioSAP";
            this.lblUsuarioSAP.Size = new System.Drawing.Size(88, 17);
            this.lblUsuarioSAP.TabIndex = 17;
            this.lblUsuarioSAP.Text = "Usuario SAP";
            //
            // txtUsuarioSAP
            //
            this.txtUsuarioSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUsuarioSAP.Location = new System.Drawing.Point(450, 407);
            this.txtUsuarioSAP.Name = "txtUsuarioSAP";
            this.txtUsuarioSAP.Size = new System.Drawing.Size(306, 26);
            this.txtUsuarioSAP.TabIndex = 18;
            //
            // lblClaveSAP
            //
            this.lblClaveSAP.AutoSize = true;
            this.lblClaveSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblClaveSAP.Location = new System.Drawing.Point(278, 450);
            this.lblClaveSAP.Name = "lblClaveSAP";
            this.lblClaveSAP.Size = new System.Drawing.Size(74, 17);
            this.lblClaveSAP.TabIndex = 19;
            this.lblClaveSAP.Text = "Clave SAP";
            //
            // txtClaveSAP
            //
            this.txtClaveSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtClaveSAP.Location = new System.Drawing.Point(450, 447);
            this.txtClaveSAP.Name = "txtClaveSAP";
            this.txtClaveSAP.PasswordChar = '*';
            this.txtClaveSAP.Size = new System.Drawing.Size(306, 26);
            this.txtClaveSAP.TabIndex = 20;
            //
            // btnGuardar
            //
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.btnGuardar.Location = new System.Drawing.Point(519, 522);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 36);
            this.btnGuardar.TabIndex = 21;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.btnCancelar.Location = new System.Drawing.Point(651, 522);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 36);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // txtServidorSLD
            //
            this.txtServidorSLD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtServidorSLD.Location = new System.Drawing.Point(545, 208);
            this.txtServidorSLD.Name = "txtServidorSLD";
            this.txtServidorSLD.Size = new System.Drawing.Size(211, 26);
            this.txtServidorSLD.TabIndex = 24;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label1.Location = new System.Drawing.Point(278, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Servidor SLD";
            //
            // chckUseTrusted
            //
            this.chckUseTrusted.AutoSize = true;
            this.chckUseTrusted.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckUseTrusted.Location = new System.Drawing.Point(450, 208);
            this.chckUseTrusted.Name = "chckUseTrusted";
            this.chckUseTrusted.Size = new System.Drawing.Size(83, 21);
            this.chckUseTrusted.TabIndex = 25;
            this.chckUseTrusted.Text = "Trusted";
            this.chckUseTrusted.UseVisualStyleBackColor = true;
            //
            // nudVersionSAP
            //
            this.nudVersionSAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudVersionSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudVersionSAP.Location = new System.Drawing.Point(450, 89);
            this.nudVersionSAP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVersionSAP.Name = "nudVersionSAP";
            this.nudVersionSAP.Size = new System.Drawing.Size(72, 23);
            this.nudVersionSAP.TabIndex = 26;
            this.nudVersionSAP.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label2.Location = new System.Drawing.Point(278, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Version SAP";
            //
            // lblMedioConexion
            //
            this.lblMedioConexion.AutoSize = true;
            this.lblMedioConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblMedioConexion.Location = new System.Drawing.Point(278, 24);
            this.lblMedioConexion.Name = "lblMedioConexion";
            this.lblMedioConexion.Size = new System.Drawing.Size(121, 17);
            this.lblMedioConexion.TabIndex = 28;
            this.lblMedioConexion.Text = "Medio conexión";
            //
            // cmbMedioConexion
            //
            this.cmbMedioConexion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedioConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cmbMedioConexion.FormattingEnabled = true;
            this.cmbMedioConexion.Location = new System.Drawing.Point(450, 21);
            this.cmbMedioConexion.Name = "cmbMedioConexion";
            this.cmbMedioConexion.Size = new System.Drawing.Size(306, 28);
            this.cmbMedioConexion.TabIndex = 29;
            this.cmbMedioConexion.SelectedIndexChanged += new System.EventHandler(this.cmbMedioConexion_SelectedIndexChanged);
            //
            // lblServiceLayerUrl
            //
            this.lblServiceLayerUrl.AutoSize = true;
            this.lblServiceLayerUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblServiceLayerUrl.Location = new System.Drawing.Point(278, 486);
            this.lblServiceLayerUrl.Name = "lblServiceLayerUrl";
            this.lblServiceLayerUrl.Size = new System.Drawing.Size(117, 17);
            this.lblServiceLayerUrl.TabIndex = 30;
            this.lblServiceLayerUrl.Text = "URL Service Layer";
            //
            // txtServiceLayerUrl
            //
            this.txtServiceLayerUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtServiceLayerUrl.Location = new System.Drawing.Point(450, 483);
            this.txtServiceLayerUrl.Name = "txtServiceLayerUrl";
            this.txtServiceLayerUrl.Size = new System.Drawing.Size(306, 26);
            this.txtServiceLayerUrl.TabIndex = 31;
            //
            // ConfigForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 588);
            this.Controls.Add(this.txtServiceLayerUrl);
            this.Controls.Add(this.lblServiceLayerUrl);
            this.Controls.Add(this.cmbMedioConexion);
            this.Controls.Add(this.lblMedioConexion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudVersionSAP);
            this.Controls.Add(this.chckUseTrusted);
            this.Controls.Add(this.txtServidorSLD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtClaveSAP);
            this.Controls.Add(this.lblClaveSAP);
            this.Controls.Add(this.txtUsuarioSAP);
            this.Controls.Add(this.lblUsuarioSAP);
            this.Controls.Add(this.txtClaveBD);
            this.Controls.Add(this.lblClaveBD);
            this.Controls.Add(this.txtUsuarioBD);
            this.Controls.Add(this.lblUsuarioBD);
            this.Controls.Add(this.cmbTipoServidor);
            this.Controls.Add(this.lblTipoServidor);
            this.Controls.Add(this.txtBaseCompania);
            this.Controls.Add(this.lblBaseCompania);
            this.Controls.Add(this.txtServidorLicencia);
            this.Controls.Add(this.lblServidorLicencia);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.txtNombreConexion);
            this.Controls.Add(this.lblNombreConexion);
            this.Controls.Add(this.btnEliminarConexion);
            this.Controls.Add(this.btnAgregarConexion);
            this.Controls.Add(this.lstConexiones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de conexiones SAP";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVersionSAP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstConexiones;
        private System.Windows.Forms.Button btnAgregarConexion;
        private System.Windows.Forms.Button btnEliminarConexion;
        private System.Windows.Forms.Label lblNombreConexion;
        private System.Windows.Forms.TextBox txtNombreConexion;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblServidorLicencia;
        private System.Windows.Forms.TextBox txtServidorLicencia;
        private System.Windows.Forms.Label lblBaseCompania;
        private System.Windows.Forms.TextBox txtBaseCompania;
        private System.Windows.Forms.Label lblTipoServidor;
        private System.Windows.Forms.ComboBox cmbTipoServidor;
        private System.Windows.Forms.Label lblUsuarioBD;
        private System.Windows.Forms.TextBox txtUsuarioBD;
        private System.Windows.Forms.Label lblClaveBD;
        private System.Windows.Forms.TextBox txtClaveBD;
        private System.Windows.Forms.Label lblUsuarioSAP;
        private System.Windows.Forms.TextBox txtUsuarioSAP;
        private System.Windows.Forms.Label lblClaveSAP;
        private System.Windows.Forms.TextBox txtClaveSAP;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtServidorSLD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chckUseTrusted;
        private System.Windows.Forms.NumericUpDown nudVersionSAP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMedioConexion;
        private System.Windows.Forms.ComboBox cmbMedioConexion;
        private System.Windows.Forms.Label lblServiceLayerUrl;
        private System.Windows.Forms.TextBox txtServiceLayerUrl;
    }
}

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
            this.SuspendLayout();
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServidor.Location = new System.Drawing.Point(24, 24);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(92, 17);
            this.lblServidor.TabIndex = 0;
            this.lblServidor.Text = "Servidor SAP";
            // 
            // txtServidor
            // 
            this.txtServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidor.Location = new System.Drawing.Point(196, 21);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(306, 26);
            this.txtServidor.TabIndex = 1;
            // 
            // lblServidorLicencia
            // 
            this.lblServidorLicencia.AutoSize = true;
            this.lblServidorLicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServidorLicencia.Location = new System.Drawing.Point(24, 64);
            this.lblServidorLicencia.Name = "lblServidorLicencia";
            this.lblServidorLicencia.Size = new System.Drawing.Size(119, 17);
            this.lblServidorLicencia.TabIndex = 2;
            this.lblServidorLicencia.Text = "Servidor licencias";
            // 
            // txtServidorLicencia
            // 
            this.txtServidorLicencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServidorLicencia.Location = new System.Drawing.Point(196, 61);
            this.txtServidorLicencia.Name = "txtServidorLicencia";
            this.txtServidorLicencia.Size = new System.Drawing.Size(306, 26);
            this.txtServidorLicencia.TabIndex = 3;
            // 
            // lblBaseCompania
            // 
            this.lblBaseCompania.AutoSize = true;
            this.lblBaseCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseCompania.Location = new System.Drawing.Point(24, 104);
            this.lblBaseCompania.Name = "lblBaseCompania";
            this.lblBaseCompania.Size = new System.Drawing.Size(105, 17);
            this.lblBaseCompania.TabIndex = 4;
            this.lblBaseCompania.Text = "Base compañía";
            // 
            // txtBaseCompania
            // 
            this.txtBaseCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaseCompania.Location = new System.Drawing.Point(196, 101);
            this.txtBaseCompania.Name = "txtBaseCompania";
            this.txtBaseCompania.Size = new System.Drawing.Size(306, 26);
            this.txtBaseCompania.TabIndex = 5;
            // 
            // lblTipoServidor
            // 
            this.lblTipoServidor.AutoSize = true;
            this.lblTipoServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoServidor.Location = new System.Drawing.Point(24, 144);
            this.lblTipoServidor.Name = "lblTipoServidor";
            this.lblTipoServidor.Size = new System.Drawing.Size(110, 17);
            this.lblTipoServidor.TabIndex = 6;
            this.lblTipoServidor.Text = "Tipo base datos";
            // 
            // cmbTipoServidor
            // 
            this.cmbTipoServidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoServidor.FormattingEnabled = true;
            this.cmbTipoServidor.Location = new System.Drawing.Point(196, 141);
            this.cmbTipoServidor.Name = "cmbTipoServidor";
            this.cmbTipoServidor.Size = new System.Drawing.Size(306, 28);
            this.cmbTipoServidor.TabIndex = 7;
            // 
            // lblUsuarioBD
            // 
            this.lblUsuarioBD.AutoSize = true;
            this.lblUsuarioBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioBD.Location = new System.Drawing.Point(24, 184);
            this.lblUsuarioBD.Name = "lblUsuarioBD";
            this.lblUsuarioBD.Size = new System.Drawing.Size(80, 17);
            this.lblUsuarioBD.TabIndex = 8;
            this.lblUsuarioBD.Text = "Usuario BD";
            // 
            // txtUsuarioBD
            // 
            this.txtUsuarioBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioBD.Location = new System.Drawing.Point(196, 181);
            this.txtUsuarioBD.Name = "txtUsuarioBD";
            this.txtUsuarioBD.Size = new System.Drawing.Size(306, 26);
            this.txtUsuarioBD.TabIndex = 9;
            // 
            // lblClaveBD
            // 
            this.lblClaveBD.AutoSize = true;
            this.lblClaveBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveBD.Location = new System.Drawing.Point(24, 224);
            this.lblClaveBD.Name = "lblClaveBD";
            this.lblClaveBD.Size = new System.Drawing.Size(66, 17);
            this.lblClaveBD.TabIndex = 10;
            this.lblClaveBD.Text = "Clave BD";
            // 
            // txtClaveBD
            // 
            this.txtClaveBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveBD.Location = new System.Drawing.Point(196, 221);
            this.txtClaveBD.Name = "txtClaveBD";
            this.txtClaveBD.PasswordChar = '*';
            this.txtClaveBD.Size = new System.Drawing.Size(306, 26);
            this.txtClaveBD.TabIndex = 11;
            // 
            // lblUsuarioSAP
            // 
            this.lblUsuarioSAP.AutoSize = true;
            this.lblUsuarioSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioSAP.Location = new System.Drawing.Point(24, 264);
            this.lblUsuarioSAP.Name = "lblUsuarioSAP";
            this.lblUsuarioSAP.Size = new System.Drawing.Size(88, 17);
            this.lblUsuarioSAP.TabIndex = 12;
            this.lblUsuarioSAP.Text = "Usuario SAP";
            // 
            // txtUsuarioSAP
            // 
            this.txtUsuarioSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioSAP.Location = new System.Drawing.Point(196, 261);
            this.txtUsuarioSAP.Name = "txtUsuarioSAP";
            this.txtUsuarioSAP.Size = new System.Drawing.Size(306, 26);
            this.txtUsuarioSAP.TabIndex = 13;
            // 
            // lblClaveSAP
            // 
            this.lblClaveSAP.AutoSize = true;
            this.lblClaveSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveSAP.Location = new System.Drawing.Point(24, 304);
            this.lblClaveSAP.Name = "lblClaveSAP";
            this.lblClaveSAP.Size = new System.Drawing.Size(74, 17);
            this.lblClaveSAP.TabIndex = 14;
            this.lblClaveSAP.Text = "Clave SAP";
            // 
            // txtClaveSAP
            // 
            this.txtClaveSAP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveSAP.Location = new System.Drawing.Point(196, 301);
            this.txtClaveSAP.Name = "txtClaveSAP";
            this.txtClaveSAP.PasswordChar = '*';
            this.txtClaveSAP.Size = new System.Drawing.Size(306, 26);
            this.txtClaveSAP.TabIndex = 15;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(265, 358);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 36);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(397, 358);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 36);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 418);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de conexión DI API";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
    }
}

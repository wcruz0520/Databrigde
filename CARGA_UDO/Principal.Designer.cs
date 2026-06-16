
namespace CARGA_UDO
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConectar = new FontAwesome.Sharp.IconButton();
            this.btnProccess = new FontAwesome.Sharp.IconButton();
            this.btnDetener = new FontAwesome.Sharp.IconButton();
            this.dtgRegistros = new System.Windows.Forms.DataGridView();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.cmbTipoObj = new System.Windows.Forms.ComboBox();
            this.grpModoCarga = new System.Windows.Forms.GroupBox();
            this.rdbAgregarActualizar = new System.Windows.Forms.RadioButton();
            this.rdbSoloActualizar = new System.Windows.Forms.RadioButton();
            this.rdbSoloAgregar = new System.Windows.Forms.RadioButton();
            this.prgCarga = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistros)).BeginInit();
            this.grpModoCarga.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConectar.FlatAppearance.BorderSize = 0;
            this.btnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.IconChar = FontAwesome.Sharp.IconChar.PlugCircleXmark;
            this.btnConectar.IconColor = System.Drawing.Color.Red;
            this.btnConectar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConectar.IconSize = 32;
            this.btnConectar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConectar.Location = new System.Drawing.Point(719, 8);
            this.btnConectar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(148, 35);
            this.btnConectar.TabIndex = 0;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnProccess
            // 
            this.btnProccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProccess.BackColor = System.Drawing.Color.Gainsboro;
            this.btnProccess.FlatAppearance.BorderSize = 0;
            this.btnProccess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProccess.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.btnProccess.IconColor = System.Drawing.Color.Green;
            this.btnProccess.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnProccess.IconSize = 32;
            this.btnProccess.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProccess.Location = new System.Drawing.Point(33, 506);
            this.btnProccess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(140, 35);
            this.btnProccess.TabIndex = 1;
            this.btnProccess.Text = "Procesar";
            this.btnProccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProccess.UseVisualStyleBackColor = false;
            this.btnProccess.Click += new System.EventHandler(this.btnProccess_Click);
            // 
            // btnDetener
            // 
            this.btnDetener.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDetener.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDetener.Enabled = false;
            this.btnDetener.FlatAppearance.BorderSize = 0;
            this.btnDetener.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetener.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetener.IconChar = FontAwesome.Sharp.IconChar.Stop;
            this.btnDetener.IconColor = System.Drawing.Color.Red;
            this.btnDetener.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDetener.IconSize = 32;
            this.btnDetener.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetener.Location = new System.Drawing.Point(218, 506);
            this.btnDetener.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(139, 35);
            this.btnDetener.TabIndex = 8;
            this.btnDetener.Text = "Detener";
            this.btnDetener.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetener.UseVisualStyleBackColor = false;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // dtgRegistros
            // 
            this.dtgRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgRegistros.BackgroundColor = System.Drawing.Color.White;
            this.dtgRegistros.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRegistros.Location = new System.Drawing.Point(33, 115);
            this.dtgRegistros.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgRegistros.Name = "dtgRegistros";
            this.dtgRegistros.RowHeadersWidth = 51;
            this.dtgRegistros.RowTemplate.Height = 24;
            this.dtgRegistros.Size = new System.Drawing.Size(835, 355);
            this.dtgRegistros.TabIndex = 2;
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(33, 60);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(51, 40);
            this.btnCargar.TabIndex = 3;
            this.btnCargar.Text = "📁";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.Color.White;
            this.txtTableName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTableName.Enabled = false;
            this.txtTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(6, 27);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(248, 19);
            this.txtTableName.TabIndex = 4;
            // 
            // cmbTipoObj
            // 
            this.cmbTipoObj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoObj.FormattingEnabled = true;
            this.cmbTipoObj.Location = new System.Drawing.Point(265, 20);
            this.cmbTipoObj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTipoObj.Name = "cmbTipoObj";
            this.cmbTipoObj.Size = new System.Drawing.Size(146, 33);
            this.cmbTipoObj.TabIndex = 5;
            // 
            // grpModoCarga
            // 
            this.grpModoCarga.Controls.Add(this.rdbAgregarActualizar);
            this.grpModoCarga.Controls.Add(this.rdbSoloActualizar);
            this.grpModoCarga.Controls.Add(this.rdbSoloAgregar);
            this.grpModoCarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpModoCarga.Location = new System.Drawing.Point(539, 45);
            this.grpModoCarga.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpModoCarga.Name = "grpModoCarga";
            this.grpModoCarga.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpModoCarga.Size = new System.Drawing.Size(328, 62);
            this.grpModoCarga.TabIndex = 9;
            this.grpModoCarga.TabStop = false;
            this.grpModoCarga.Text = "Modo de carga";
            // 
            // rdbAgregarActualizar
            // 
            this.rdbAgregarActualizar.AutoSize = true;
            this.rdbAgregarActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAgregarActualizar.Location = new System.Drawing.Point(228, 22);
            this.rdbAgregarActualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbAgregarActualizar.Name = "rdbAgregarActualizar";
            this.rdbAgregarActualizar.Size = new System.Drawing.Size(76, 21);
            this.rdbAgregarActualizar.TabIndex = 2;
            this.rdbAgregarActualizar.TabStop = true;
            this.rdbAgregarActualizar.Text = "Ambos";
            this.rdbAgregarActualizar.UseVisualStyleBackColor = true;
            // 
            // rdbSoloActualizar
            // 
            this.rdbSoloActualizar.AutoSize = true;
            this.rdbSoloActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSoloActualizar.Location = new System.Drawing.Point(112, 22);
            this.rdbSoloActualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbSoloActualizar.Name = "rdbSoloActualizar";
            this.rdbSoloActualizar.Size = new System.Drawing.Size(95, 21);
            this.rdbSoloActualizar.TabIndex = 1;
            this.rdbSoloActualizar.TabStop = true;
            this.rdbSoloActualizar.Text = "Actualizar";
            this.rdbSoloActualizar.UseVisualStyleBackColor = true;
            // 
            // rdbSoloAgregar
            // 
            this.rdbSoloAgregar.AutoSize = true;
            this.rdbSoloAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSoloAgregar.Location = new System.Drawing.Point(9, 22);
            this.rdbSoloAgregar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdbSoloAgregar.Name = "rdbSoloAgregar";
            this.rdbSoloAgregar.Size = new System.Drawing.Size(84, 21);
            this.rdbSoloAgregar.TabIndex = 0;
            this.rdbSoloAgregar.TabStop = true;
            this.rdbSoloAgregar.Text = "Agregar";
            this.rdbSoloAgregar.UseVisualStyleBackColor = true;
            // 
            // prgCarga
            // 
            this.prgCarga.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prgCarga.Location = new System.Drawing.Point(390, 506);
            this.prgCarga.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prgCarga.Name = "prgCarga";
            this.prgCarga.Size = new System.Drawing.Size(478, 35);
            this.prgCarga.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgCarga.TabIndex = 6;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(390, 474);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 20);
            this.lblEstado.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTableName);
            this.groupBox1.Controls.Add(this.cmbTipoObj);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(103, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 62);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tabla principal";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpModoCarga);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.prgCarga);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.dtgRegistros);
            this.Controls.Add(this.btnProccess);
            this.Controls.Add(this.btnConectar);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistros)).EndInit();
            this.grpModoCarga.ResumeLayout(false);
            this.grpModoCarga.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnConectar;
        private FontAwesome.Sharp.IconButton btnProccess;
        private FontAwesome.Sharp.IconButton btnDetener;
        private System.Windows.Forms.DataGridView dtgRegistros;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.ComboBox cmbTipoObj;
        private System.Windows.Forms.GroupBox grpModoCarga;
        private System.Windows.Forms.RadioButton rdbSoloAgregar;
        private System.Windows.Forms.RadioButton rdbSoloActualizar;
        private System.Windows.Forms.RadioButton rdbAgregarActualizar;
        private System.Windows.Forms.ProgressBar prgCarga;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}


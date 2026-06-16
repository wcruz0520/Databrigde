
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
            this.prgCarga = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistros)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConectar
            // 
            this.btnConectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConectar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConectar.FlatAppearance.BorderSize = 0;
            this.btnConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConectar.IconChar = FontAwesome.Sharp.IconChar.PlugCircleXmark;
            this.btnConectar.IconColor = System.Drawing.Color.Red;
            this.btnConectar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConectar.IconSize = 32;
            this.btnConectar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConectar.Location = new System.Drawing.Point(684, 45);
            this.btnConectar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(183, 46);
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
            this.btnProccess.IconChar = FontAwesome.Sharp.IconChar.Play;
            this.btnProccess.IconColor = System.Drawing.Color.Green;
            this.btnProccess.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnProccess.IconSize = 32;
            this.btnProccess.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProccess.Location = new System.Drawing.Point(33, 488);
            this.btnProccess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(159, 49);
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
            this.btnDetener.IconChar = FontAwesome.Sharp.IconChar.Stop;
            this.btnDetener.IconColor = System.Drawing.Color.Red;
            this.btnDetener.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDetener.IconSize = 32;
            this.btnDetener.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetener.Location = new System.Drawing.Point(198, 488);
            this.btnDetener.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(159, 49);
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
            this.dtgRegistros.Size = new System.Drawing.Size(835, 350);
            this.dtgRegistros.TabIndex = 2;
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(33, 45);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(51, 42);
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
            this.txtTableName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTableName.Location = new System.Drawing.Point(145, 60);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(237, 24);
            this.txtTableName.TabIndex = 4;
            // 
            // cmbTipoObj
            // 
            this.cmbTipoObj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoObj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoObj.FormattingEnabled = true;
            this.cmbTipoObj.Location = new System.Drawing.Point(404, 56);
            this.cmbTipoObj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTipoObj.Name = "cmbTipoObj";
            this.cmbTipoObj.Size = new System.Drawing.Size(186, 33);
            this.cmbTipoObj.TabIndex = 5;
            // 
            // prgCarga
            // 
            this.prgCarga.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prgCarga.Location = new System.Drawing.Point(390, 506);
            this.prgCarga.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prgCarga.Name = "prgCarga";
            this.prgCarga.Size = new System.Drawing.Size(477, 29);
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
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnDetener);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.prgCarga);
            this.Controls.Add(this.cmbTipoObj);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.dtgRegistros);
            this.Controls.Add(this.btnProccess);
            this.Controls.Add(this.btnConectar);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Principal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRegistros)).EndInit();
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
        private System.Windows.Forms.ProgressBar prgCarga;
        private System.Windows.Forms.Label lblEstado;
    }
}


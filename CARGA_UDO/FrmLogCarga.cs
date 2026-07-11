using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CARGA_UDO
{
    public partial class FrmLogCarga : Form
    {
        private List<ResultadoCarga> _logOriginal;
        public FrmLogCarga(List<ResultadoCarga> log)
        {
            InitializeComponent();
            AplicarDisenoProfesional();

            _logOriginal = log;

            cmbFiltro.Items.Add("Todos");
            cmbFiltro.Items.Add("Exitosos");
            cmbFiltro.Items.Add("Fallidos");
            cmbFiltro.SelectedIndex = 0;

            CargarGrid(_logOriginal);

            //dtgLog.DataSource = log.Select(x => new
            //{
            //    Code = x.Code,
            //    Resultado = x.Descripcion
            //}).ToList();
        }

        private void AplicarDisenoProfesional()
        {
            Color colorPrimario = SystemColors.ActiveCaption;
            Color colorTexto = Color.FromArgb(45, 55, 72);

            BackColor = Color.White;
            Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);

            cmbFiltro.BackColor = Color.White;
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.FlatStyle = FlatStyle.Flat;
            cmbFiltro.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Regular, GraphicsUnit.Point, 0);

            dtgLog.AllowUserToAddRows = false;
            dtgLog.AllowUserToDeleteRows = false;
            dtgLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgLog.BackgroundColor = Color.White;
            dtgLog.BorderStyle = BorderStyle.None;
            dtgLog.EnableHeadersVisualStyles = false;
            dtgLog.GridColor = Color.FromArgb(226, 232, 240);
            dtgLog.ColumnHeadersDefaultCellStyle.BackColor = colorPrimario;
            dtgLog.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dtgLog.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtgLog.DefaultCellStyle.BackColor = Color.White;
            dtgLog.DefaultCellStyle.ForeColor = colorTexto;
            dtgLog.DefaultCellStyle.SelectionBackColor = Color.FromArgb(221, 235, 247);
            dtgLog.DefaultCellStyle.SelectionForeColor = Color.Black;
            dtgLog.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 251, 255);
            dtgLog.RowHeadersVisible = false;
            dtgLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CargarGrid(List<ResultadoCarga> lista)
        {
            dtgLog.DataSource = null;
            dtgLog.DataSource = lista.Select(x => new
            {
                Code = x.Code,
                Resultado = x.Descripcion
            }).ToList();
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = cmbFiltro.SelectedItem.ToString();

            if (filtro == "Todos")
                CargarGrid(_logOriginal);

            else if (filtro == "Exitosos")
                CargarGrid(_logOriginal.Where(x => x.Exitoso).ToList());

            else
                CargarGrid(_logOriginal.Where(x => !x.Exitoso).ToList());
        }
    }
}

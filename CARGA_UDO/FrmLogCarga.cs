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

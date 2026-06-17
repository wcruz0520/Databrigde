using CARGA_UDO.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.IO;
using ExcelDataReader;

namespace CARGA_UDO
{
    public partial class Principal : Form
    {
        public string[] strTest = new string[4];
        public string strConnString = "";
        public string sCookie;
        public bool conectarse = true;
        public int ret;
        public string strSQL;
        public bool resultproceso = false;
        public string msg_error = "";
        private bool cancelarProceso = false;

        public SAPbouiCOM.Application rSboApp;
        public SAPbouiCOM.SboGuiApi rSboGui;

        public List<ResultadoCarga> logCarga = new List<ResultadoCarga>();

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            strTest = Environment.GetCommandLineArgs();
            ActualizarEstadoConexion(false);

            if (SapConnectionConfig.IsConfigured())
            {
                ConectarSAP();
            }

            cmbTipoObj.Items.Clear();

            cmbTipoObj.DataSource = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("NO", "Ningún Objeto"),
                new KeyValuePair<string, string>("M", "Datos Maestro"),
                new KeyValuePair<string, string>("ML", "Filas de datos maestros"),
                new KeyValuePair<string, string>("D", "Documentos"),
                new KeyValuePair<string, string>("DL", "Filas de documentos"),
                new KeyValuePair<string, string>("NOAI", "Ningún Objeto con incremento automático")
            };

            cmbTipoObj.DisplayMember = "Value";
            cmbTipoObj.ValueMember = "Key";
            cmbTipoObj.SelectedIndex = 0;
            cmbTipoObj.Enabled = false;

            rdbAgregarActualizar.Checked = true;
        }

        private string ObtenerModoCargaSeleccionado()
        {
            if (rdbSoloAgregar.Checked)
                return "I";

            if (rdbSoloActualizar.Checked)
                return "U";

            return "A";
        }

        private string ObtenerCadenaConexionSAP()
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();

                if (args.Length < 2)
                    return string.Empty; // SAP NO lanzó el addon

                return args[1];
            }
            catch
            {
                return string.Empty;
            }
        }

        private bool ConectarSAP()
        {
            try
            {
                if (!SapConnectionConfig.IsConfigured())
                {
                    MessageBox.Show("Configure la conexión DI API antes de conectar.",
                                    "Configuración requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActualizarEstadoConexion(false);
                    return false;
                }

                Globals.rCompany = new SAPbobsCOM.Company();
                SapConnectionConfig.ApplyToCompany(Globals.rCompany);

                ret = Globals.rCompany.Connect();
                if (ret != 0)
                {
                    Globals.rCompany.GetLastError(out int errorCode, out string errorMsg);
                    Globals.rCompany = null;
                    MessageBox.Show($"Error al conectar DI API ({errorCode}): {errorMsg}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ActualizarEstadoConexion(false);
                    return false;
                }

                ActualizarEstadoConexion(true);
                return true;
            }
            catch (Exception ex)
            {
                Globals.rCompany = null;
                ActualizarEstadoConexion(false);
                MessageBox.Show("Error conectando a SAP por DI API: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ActualizarEstadoConexion(bool conectado)
        {
            if (conectado && Globals.rCompany != null && Globals.rCompany.Connected)
            {
                this.Text = $"Databrigde (conectado a {Globals.rCompany.CompanyDB} - {Globals.rCompany.UserName})";//$"Databrigde {Globals.rCompany.CompanyName.ToString().ToUpper()}";
                this.btnConectar.Text = "";
                this.btnConectar.Enabled = true;
                this.btnConectar.IconColor = Color.Red;
                this.btnConectar.IconChar = IconChar.PlugCircleXmark;
                this.btnProccess.Enabled = true;
                //this.txtBoxBD.Text = $"Compania: {Globals.rCompany.CompanyDB}";
                //this.txtBoxUser.Text = $"Usuario: {Globals.rCompany.UserName}";
                //this.txtBoxBD.Enabled = false;
                //this.txtBoxUser.Enabled = false;
                return;
            }

            this.Text = "Databrigde (desconectado)";
            this.btnConectar.Text = "";
            this.btnConectar.Enabled = true;
            this.btnConectar.IconColor = Color.Green;
            this.btnConectar.IconChar = IconChar.PlugCircleCheck;
            this.btnProccess.Enabled = false;
            //this.txtBoxBD.Text = $"Compania: ";
            //this.txtBoxUser.Text = $"Usuario: ";
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (this.btnConectar.IconChar == IconChar.PlugCircleCheck)
            {
                ConectarSAP();
            }
            else
            {
                try
                {
                    if (Globals.rCompany != null && Globals.rCompany.Connected)
                    {
                        Globals.rCompany.Disconnect();
                        Globals.rCompany = null;
                        rSboApp = null;
                        rSboGui = null;
                    }

                    ActualizarEstadoConexion(false);
                    //this.btnSimular.Enabled = false;

                    //MessageBox.Show("Se ha desconectado de SAP correctamente.",
                    //                "Desconexión",
                    //                MessageBoxButtons.OK,
                    //                MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al desconectarse de SAP: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenExcelAndLoad(dtgRegistros);
        }

        private void OpenExcelAndLoad(DataGridView targetGrid)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccione archivo de Excel";
                ofd.Filter = "Archivos de Excel|*.xlsx;*.xls";
                ofd.Multiselect = false;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    LoadExcelToTabs(ofd.FileName, targetGrid);
                }
            }
        }

        private void LoadExcelToTabs(string excelPath, DataGridView placeholderGrid)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = reader.AsDataSet(conf);

                var primeraHoja = dataSet.Tables[0].TableName;
                if (!string.IsNullOrWhiteSpace(primeraHoja))
                {
                    string tablaSeleccionada = primeraHoja.Trim();

                    if (!ExisteTablaEnBaseDatos(tablaSeleccionada))
                    {
                        string schema = Globals.rCompany?.CompanyDB ?? string.Empty;
                        MessageBox.Show(
                            $"La tabla [{tablaSeleccionada}] no existe en el schema [{schema}].",
                            "Tabla no encontrada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    txtTableName.Text = tablaSeleccionada;
                    IdentificarTipoTablaSeleccionada(txtTableName.Text);
                }

                var tabs = EnsureTabControlFor(placeholderGrid);
                tabs.TabPages.Clear();

                foreach (DataTable table in dataSet.Tables)
                {
                    var page = new TabPage
                    {
                        Text = string.IsNullOrWhiteSpace(table.TableName) ? "Hoja" : table.TableName
                    };

                    var grid = new DataGridView
                    {
                        Dock = DockStyle.Fill,

                        // No editable
                        ReadOnly = true,
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false,
                        AllowUserToResizeRows = false,

                        DataSource = table,
                        BackgroundColor = Color.White,
                        RowHeadersVisible = false,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                        // Contenido más pequeño
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                            WrapMode = DataGridViewTriState.False
                        },

                        // Encabezado en negrita y reducido
                        ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                        {
                            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                            Alignment = DataGridViewContentAlignment.MiddleCenter
                        },

                        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                        ColumnHeadersHeight = 24,
                        RowTemplate = { Height = 22 }
                    };

                    grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grid.MultiSelect = false;
                    grid.EditMode = DataGridViewEditMode.EditProgrammatically;

                    page.Controls.Add(grid);
                    tabs.TabPages.Add(page);
                }
            }
        }

        private TabControl EnsureTabControlFor(DataGridView placeholderGrid)
        {
            var parent = placeholderGrid.Parent;
            var tabName = placeholderGrid.Name + "Tabs";
            var existing = parent.Controls.Find(tabName, false).FirstOrDefault() as TabControl;
            if (existing != null) return existing;

            var tabs = new TabControl
            {
                Name = tabName,
                Bounds = placeholderGrid.Bounds,
                Anchor = placeholderGrid.Anchor,
                //Dock = DockStyle.Fill
            };
            parent.Controls.Add(tabs);
            tabs.BringToFront();
            placeholderGrid.Visible = false;   // ocultamos el grid “placeholder”
            return tabs;
        }

        //private void btnProccess_Click(object sender, EventArgs e)
        //{
        //    var tabs = this.Controls.OfType<TabControl>().FirstOrDefault();
        //    if (tabs == null || tabs.TabPages.Count == 0) return;

        //    string tablaCabecera = txtTableName.Text.Trim();
        //    string tipoObj = cmbTipoObj.SelectedValue.ToString();

        //    var gridCabecera = tabs.TabPages[0].Controls.OfType<DataGridView>().First();
        //    var registrosCabecera = LeerRegistrosDesdeGrid(gridCabecera);

        //    for (int i = 0; i < registrosCabecera.Count; i++)
        //    {
        //        var hijos = new List<(string, List<RegistroTabla>)>();

        //        for (int h = 1; h < tabs.TabPages.Count; h++)
        //        {
        //            var gridHijo = tabs.TabPages[h].Controls.OfType<DataGridView>().First();
        //            var registrosHijo = LeerRegistrosDesdeGrid(gridHijo)
        //                                .Where(x => x.Code == registrosCabecera[i].Code)
        //                                .ToList();

        //            hijos.Add((tabs.TabPages[h].Text, registrosHijo));
        //        }

        //        GuardarCabecera(tablaCabecera, registrosCabecera[i], hijos);
        //    }

        //    MessageBox.Show("Carga finalizada correctamente");
        //}

        //private void btnProccess_Click(object sender, EventArgs e)
        //{
        //    logCarga.Clear();
        //    prgCarga.Value = 0;
        //    lblEstado.Text = "Iniciando carga...";

        //    var tabs = this.Controls.OfType<TabControl>().FirstOrDefault();
        //    if (tabs == null || tabs.TabPages.Count == 0) return;

        //    string tablaCabecera = txtTableName.Text.Trim();

        //    var gridCabecera = tabs.TabPages[0].Controls.OfType<DataGridView>().First();
        //    var registrosCabecera = LeerRegistrosDesdeGrid(gridCabecera);

        //    int total = registrosCabecera.Count;
        //    int procesados = 0;

        //    foreach (var cab in registrosCabecera)
        //    {
        //        try
        //        {
        //            var hijos = new List<(string, List<RegistroTabla>)>();

        //            for (int h = 1; h < tabs.TabPages.Count; h++)
        //            {
        //                var gridHijo = tabs.TabPages[h].Controls.OfType<DataGridView>().First();
        //                var registrosHijo = LeerRegistrosDesdeGrid(gridHijo)
        //                                    .Where(x => x.Code == cab.Code)
        //                                    .ToList();

        //                hijos.Add((tabs.TabPages[h].Text, registrosHijo));
        //            }

        //            bool actualizado = ExisteRegistro(tablaCabecera, cab.Code);

        //            msg_error = "";

        //            GuardarCabecera(tablaCabecera, cab, hijos);

        //            if (resultproceso)
        //            {
        //                logCarga.Add(new ResultadoCarga
        //                {
        //                    Code = cab.Code,
        //                    Exitoso = resultproceso,
        //                    Descripcion = actualizado ? "Actualizado exitosamente" : "Creado exitosamente"
        //                });
        //            }
        //            else
        //            {
        //                logCarga.Add(new ResultadoCarga
        //                {
        //                    Code = cab.Code,
        //                    Exitoso = resultproceso,
        //                    Descripcion = msg_error
        //                });
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            logCarga.Add(new ResultadoCarga
        //            {
        //                Code = cab.Code,
        //                Exitoso = resultproceso,
        //                Descripcion = ex.Message
        //            });
        //        }

        //        procesados++;
        //        int progreso = (int)((procesados / (double)total) * 100);
        //        prgCarga.Value = progreso;
        //        lblEstado.Text = $"Procesando {procesados} de {total}...";
        //        Application.DoEvents();
        //    }

        //    lblEstado.Text = "Carga finalizada";
        //    MostrarPantallaLog();
        //}

        private void btnProccess_Click(object sender, EventArgs e)
        {
            logCarga.Clear();
            prgCarga.Value = 0;
            lblEstado.Text = "Iniciando carga...";
            btnProccess.Enabled = false;
            btnDetener.Enabled = true;
            cancelarProceso = false;
            bool transaccionIniciada = false;
            bool procesoCancelado = false;
            bool hayErrores = false;

            try
            {
                var tabs = this.Controls.OfType<TabControl>().FirstOrDefault();
                if (tabs == null || tabs.TabPages.Count == 0) return;

                string objeto = txtTableName.Text.Trim();                 // UDO Code o tabla (según tu uso)
                string tipoObj = IdentificarTipoTablaSeleccionada(objeto); // Tipo de tabla según OUTB
                string modoCarga = ObtenerModoCargaSeleccionado(); // "I", "U", "A"

                var gridCabecera = tabs.TabPages[0].Controls.OfType<DataGridView>().First();

                // Lee registros según el tipo (Code o DocEntry)
                var registrosCabecera = LeerRegistrosDesdeGrid(gridCabecera, tipoObj);

                int total = registrosCabecera.Count;
                int procesados = 0;

                if (total == 0)
                {
                    lblEstado.Text = "No hay registros para procesar";
                    return;
                }

                transaccionIniciada = IniciarTransaccionSAP();

                foreach (var cab in registrosCabecera)
                {
                    if (cancelarProceso)
                    {
                        procesoCancelado = true;
                        break;
                    }
                    try
                    {
                        var hijos = new List<(string tablaHija, List<RegistroTabla> registros)>();
                        for (int h = 1; h < tabs.TabPages.Count; h++)
                        {
                            var gridHijo = tabs.TabPages[h].Controls.OfType<DataGridView>().First();
                            var registrosHijo = LeerRegistrosDesdeGrid(gridHijo, tipoObj)
                                                .Where(x => x.KeyValue == cab.KeyValue)
                                                .ToList();

                            hijos.Add((tabs.TabPages[h].Text, registrosHijo));
                        }

                        msg_error = "";

                        if (tipoObj == "M")
                        {
                            bool actualizado = ExisteRegistro_Maestro(objeto, cab.KeyValue);
                            if (DebeProcesarRegistro(modoCarga, actualizado, cab.KeyValue))
                            {
                                Guardar_Maestro(objeto, cab, hijos);

                                if (!resultproceso) hayErrores = true;
                                logCarga.Add(new ResultadoCarga
                                {
                                    Code = cab.KeyValue,
                                    Exitoso = resultproceso,
                                    Descripcion = resultproceso ? (actualizado ? "Actualizado exitosamente" : "Creado exitosamente") : msg_error
                                });
                            }
                        }
                        else if (tipoObj == "D")
                        {
                            bool actualizado = ExisteRegistro_Documento(objeto, cab.KeyValue);
                            if (DebeProcesarRegistro(modoCarga, actualizado, cab.KeyValue))
                            {
                                Guardar_Documento(objeto, cab, hijos);

                                if (!resultproceso) hayErrores = true;
                                logCarga.Add(new ResultadoCarga
                                {
                                    Code = cab.KeyValue, // aquí realmente es DocEntry
                                    Exitoso = resultproceso,
                                    Descripcion = resultproceso ? (actualizado ? "Actualizado exitosamente" : "Creado exitosamente") : msg_error
                                });
                            }
                        }
                        else if (tipoObj == "NO" || tipoObj == "NOAI")
                        {
                            bool actualizado = ExisteRegistro_NoObjeto(objeto, cab.KeyValue);
                            if (DebeProcesarRegistro(modoCarga, actualizado, cab.KeyValue))
                            {
                                Guardar_NoObjeto(objeto, cab, tipoObj == "NOAI");  // NO maneja hijos (si los necesitas dime y lo extendemos)

                                if (!resultproceso) hayErrores = true;
                                logCarga.Add(new ResultadoCarga
                                {
                                    Code = cab.KeyValue,
                                    Exitoso = resultproceso,
                                    Descripcion = resultproceso ? (actualizado ? "Actualizado exitosamente" : "Creado exitosamente") : msg_error
                                });
                            }
                        }
                        else
                        {
                            hayErrores = true;
                            logCarga.Add(new ResultadoCarga
                            {
                                Code = cab.KeyValue,
                                Exitoso = false,
                                Descripcion = "El tipo de tabla seleccionado corresponde a líneas y no se puede procesar como cabecera."
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        hayErrores = true;
                        logCarga.Add(new ResultadoCarga
                        {
                            Code = cab.KeyValue,
                            Exitoso = false,
                            Descripcion = ex.Message
                        });
                    }

                    procesados++;
                    int progreso = (int)((procesados / (double)total) * 100);
                    prgCarga.Value = progreso;
                    lblEstado.Text = $"Procesando {procesados} de {total}...";
                    Application.DoEvents();
                }

                if (procesoCancelado /*|| hayErrores*/)
                {
                    if (transaccionIniciada)
                        FinalizarTransaccionSAP(false);
                    transaccionIniciada = false;
                    lblEstado.Text = procesoCancelado
                        ? "Proceso detenido. Se aplicó rollback."
                        : "Carga con errores. Se aplicó rollback.";
                }
                else
                {
                    if (transaccionIniciada)
                        FinalizarTransaccionSAP(true);
                    transaccionIniciada = false;
                    lblEstado.Text = "Carga finalizada. Se aplicó commit.";
                }

                MostrarPantallaLog();
            }
            catch (Exception ex)
            {
                if (transaccionIniciada)
                    FinalizarTransaccionSAP(false);

                lblEstado.Text = "Error en la carga. Se aplicó rollback.";
                MessageBox.Show("Error en el proceso: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProccess.Enabled = true;
                btnDetener.Enabled = false;
                cancelarProceso = false;
            }

        }



        private bool ExisteTablaEnBaseDatos(string tabla)
        {
            if (string.IsNullOrWhiteSpace(tabla) || Globals.rCompany == null || !Globals.rCompany.Connected)
                return false;

            try
            {
                var rs = (SAPbobsCOM.Recordset)Globals.rCompany
                    .GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                bool esHana = Globals.rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                string nombreTabla = tabla.Trim().TrimStart('@').Replace("'", "''");
                string tablaFisica = $"@{nombreTabla}";
                string companyDb = (Globals.rCompany.CompanyDB ?? string.Empty).Replace("'", "''");

                string sql = esHana
                    ? $"SELECT \"TABLE_NAME\" FROM \"SYS\".\"TABLES\" WHERE UPPER(\"SCHEMA_NAME\") = UPPER('{companyDb}') AND UPPER(\"TABLE_NAME\") = UPPER('{tablaFisica}')"
                    : $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE UPPER(TABLE_NAME) = UPPER('{tablaFisica}')";

                rs.DoQuery(sql);
                return !rs.EoF;
            }
            catch (Exception ex)
            {
                msg_error = "No fue posible validar la existencia de la tabla en la base de datos: " + ex.Message;
                return false;
            }
        }

        private string IdentificarTipoTablaSeleccionada(string tabla)
        {
            string tipoObj = ObtenerTipoTablaDesdeOUTB(tabla);

            if (!string.IsNullOrWhiteSpace(tipoObj))
            {
                SeleccionarTipoObjeto(tipoObj);
                return tipoObj;
            }

            SeleccionarTipoObjeto("NO");
            return "NO";
        }

        private string ObtenerTipoTablaDesdeOUTB(string tabla)
        {
            if (string.IsNullOrWhiteSpace(tabla) || Globals.rCompany == null || !Globals.rCompany.Connected)
                return null;

            try
            {
                var rs = (SAPbobsCOM.Recordset)Globals.rCompany
                    .GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                bool esHana = Globals.rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                string tablaSinArroba = tabla.Trim().TrimStart('@').Replace("'", "''");
                string sql = esHana
                    ? $"SELECT \"ObjectType\" FROM \"OUTB\" WHERE UPPER(\"TableName\") = UPPER('{tablaSinArroba}')"
                    : $"SELECT [ObjectType] FROM [OUTB] WHERE UPPER([TableName]) = UPPER('{tablaSinArroba}')";

                rs.DoQuery(sql);
                if (rs.EoF)
                    return null;

                string objectType = rs.Fields.Item("ObjectType").Value?.ToString();
                return MapearTipoTablaOUTB(objectType);
            }
            catch (Exception ex)
            {
                msg_error = "No fue posible identificar el tipo de tabla en OUTB: " + ex.Message;
                return null;
            }
        }

        private string MapearTipoTablaOUTB(string objectType)
        {
            switch ((objectType ?? string.Empty).Trim())
            {
                case "0": return "NO";
                case "1": return "M";
                case "2": return "ML";
                case "3": return "D";
                case "4": return "DL";
                case "5": return "NOAI";
                default: return null;
            }
        }

        private void SeleccionarTipoObjeto(string tipoObj)
        {
            if (cmbTipoObj.DataSource == null || string.IsNullOrWhiteSpace(tipoObj))
                return;

            cmbTipoObj.SelectedValue = tipoObj;
        }

        private bool DebeProcesarRegistro(string modoCarga, bool existe, string keyValue)
        {
            if (modoCarga == "I" && existe)
            {
                logCarga.Add(new ResultadoCarga
                {
                    Code = keyValue,
                    Exitoso = true,
                    Descripcion = "Omitido: el registro ya existe y se seleccionó solo agregar registro"
                });
                return false;
            }

            if (modoCarga == "U" && !existe)
            {
                logCarga.Add(new ResultadoCarga
                {
                    Code = keyValue,
                    Exitoso = true,
                    Descripcion = "Omitido: el registro no existe y se seleccionó solo actualizar registros"
                });
                return false;
            }

            return true;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            cancelarProceso = true;
            btnDetener.Enabled = false;
            lblEstado.Text = "Deteniendo proceso...";
        }

        private bool IniciarTransaccionSAP()
        {
            if (Globals.rCompany != null && Globals.rCompany.Connected && !Globals.rCompany.InTransaction)
            {
                Globals.rCompany.StartTransaction();
                return true;
            }

            return false;
        }

        private void FinalizarTransaccionSAP(bool confirmar)
        {
            if (Globals.rCompany == null || !Globals.rCompany.Connected || !Globals.rCompany.InTransaction)
                return;

            Globals.rCompany.EndTransaction(confirmar
                ? SAPbobsCOM.BoWfTransOpt.wf_Commit
                : SAPbobsCOM.BoWfTransOpt.wf_RollBack);
        }

        private List<RegistroTabla> LeerRegistrosDesdeGrid(DataGridView grid, string tipoObj)
        {
            var lista = new List<RegistroTabla>();

            // Busca columna clave por nombre (recomendado)
            int idxKey = -1;

            if (tipoObj == "D")
                idxKey = FindColumnIndex(grid, "DocEntry");
            else
                idxKey = FindColumnIndex(grid, "Code");

            // Fallback: para tablas con llave manual usa la primera columna.
            // Para tablas con incremento automático, si no viene Code no quitamos
            // ninguna columna de los campos porque SAP asigna la llave al insertar.
            if (idxKey < 0 && tipoObj != "NOAI") idxKey = 0;

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var registro = new RegistroTabla();
                registro.KeyValue = idxKey >= 0
                    ? row.Cells[idxKey].Value?.ToString()
                    : (row.Index + 1).ToString();

                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    if (idxKey >= 0 && i == idxKey) continue;

                    var colName = grid.Columns[i].Name;
                    registro.Campos[colName] = row.Cells[i].Value;
                }

                lista.Add(registro);
            }

            return lista;
        }

        private bool ExisteRegistro(string tabla, string code)
        {
            var rs = (SAPbobsCOM.Recordset)Globals.rCompany
                .GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            bool esHana = Globals.rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB;

            string sql;

            if (esHana)
            {
                sql = $"SELECT \"Code\" FROM \"@{tabla}\" WHERE \"Code\" = '{code.Replace("'", "''")}'";
            }
            else
            {
                sql = $"SELECT [Code] FROM [@{tabla}] WHERE [Code] = '{code.Replace("'", "''")}'";
            }

            rs.DoQuery(sql);

            return !rs.EoF;
        }

        private int FindColumnIndex(DataGridView grid, string columnName)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                var name = grid.Columns[i].Name ?? "";
                if (string.Equals(name, columnName, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        //private void GuardarCabecera(string udoCode, RegistroTabla registro, List<(string tablaHija, List<RegistroTabla> registros)> hijos)
        //{
        //    try
        //    {
        //        var srv = Globals.rCompany.GetCompanyService();
        //        var genSrv = srv.GetGeneralService(udoCode);

        //        SAPbobsCOM.GeneralData data;
        //        bool existe = ExisteRegistro(udoCode, registro.Code);

        //        if (existe)
        //        {
        //            var param = (SAPbobsCOM.GeneralDataParams)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

        //            param.SetProperty("Code", registro.Code);
        //            data = genSrv.GetByParams(param);
        //        }
        //        else
        //        {
        //            data = (SAPbobsCOM.GeneralData)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

        //            data.SetProperty("Code", registro.Code);
        //            //data.SetProperty("Name", registro.Code);
        //        }

        //        foreach (var campo in registro.Campos)
        //        {
        //            if (!string.IsNullOrWhiteSpace(campo.Value.ToString()))
        //                data.SetProperty(campo.Key, campo.Value);
        //        }

        //        foreach (var h in hijos)
        //        {
        //            var child = data.Child(h.tablaHija);

        //            for (int i = child.Count - 1; i >= 0; i--)
        //            {
        //                child.Remove(i);
        //            }

        //            foreach (var r in h.registros)
        //            {
        //                var line = child.Add();

        //                foreach (var c in r.Campos)
        //                {
        //                    if (c.Value != null)
        //                        line.SetProperty(c.Key, c.Value);
        //                }
        //            }
        //        }

        //        if (existe)
        //            genSrv.Update(data);
        //        else
        //            genSrv.Add(data);

        //        resultproceso = true;
        //    }
        //    catch(Exception ex)
        //    {
        //        resultproceso = false;
        //        msg_error = "Error al guardar: " + ex.Message;
        //        //MessageBox.Show("Error al guardar: " + ex.Message,
        //        //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        private void MostrarPantallaLog()
        {
            var frm = new FrmLogCarga(logCarga);
            frm.ShowDialog();
        }

        private bool ExisteRegistro_Maestro(string tablaOudo, string code)
            => ExisteRegistroPorCode(tablaOudo, code);

        private bool ExisteRegistro_NoObjeto(string tabla, string code)
            => ExisteRegistroPorCode(tabla, code);

        private bool ExisteRegistroPorCode(string tabla, string code)
        {
            var rs = (SAPbobsCOM.Recordset)Globals.rCompany
                .GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            bool esHana = Globals.rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB;
            string safe = (code ?? "").Replace("'", "''");

            string sql = esHana
                ? $"SELECT \"Code\" FROM \"@{tabla}\" WHERE \"Code\" = '{safe}'"
                : $"SELECT [Code] FROM [@{tabla}] WHERE [Code] = '{safe}'";

            rs.DoQuery(sql);
            return !rs.EoF;
        }

        private bool ExisteRegistro_Documento(string udoCode, string docEntry)
        {
            int de;
            if (!int.TryParse(docEntry, out de)) return false;

            try
            {
                var srv = Globals.rCompany.GetCompanyService();
                var genSrv = srv.GetGeneralService(udoCode);

                var param = (SAPbobsCOM.GeneralDataParams)
                    genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                // Para UDO Documento la llave es DocEntry
                param.SetProperty("DocEntry", de);

                var data = genSrv.GetByParams(param);
                return data != null;
            }
            catch
            {
                return false;
            }
        }

        private string ObtenerValorCampo(Dictionary<string, object> campos, params string[] nombres)
        {
            foreach (var nombre in nombres)
            {
                var item = campos.FirstOrDefault(x => string.Equals(x.Key, nombre, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrWhiteSpace(item.Key))
                    return item.Value?.ToString();
            }

            return null;
        }

        private void AsignarCamposGeneralData(SAPbobsCOM.GeneralData data, Dictionary<string, object> campos, bool omitirCamposSistemaLinea = false)
        {
            foreach (var campo in campos)
            {
                if (campo.Value == null) continue;

                string nombreCampo = campo.Key ?? "";

                if (omitirCamposSistemaLinea)
                {
                    if (string.Equals(nombreCampo, "LineNum", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(nombreCampo, "LineId", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(nombreCampo, "VisOrder", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(nombreCampo, "Object", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(nombreCampo, "LogInst", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }

                var valor = campo.Value.ToString();
                if (string.IsNullOrWhiteSpace(valor)) continue;

                data.SetProperty(nombreCampo, campo.Value);

            }
        }

        private SAPbobsCOM.GeneralData BuscarLineaExistentePorLineNum(SAPbobsCOM.GeneralDataCollection child, string lineNumTexto)
        {
            if (string.IsNullOrWhiteSpace(lineNumTexto))
                return null;

            int lineNumBuscado;
            if (!int.TryParse(lineNumTexto, out lineNumBuscado))
                return null;

            for (int i = 0; i < child.Count; i++)
            {
                var linea = child.Item(i);

                try
                {
                    object valor = null;

                    try
                    {
                        valor = linea.GetProperty("LineNum");
                    }
                    catch
                    {
                        try
                        {
                            valor = linea.GetProperty("LineId");
                        }
                        catch
                        {
                            valor = null;
                        }
                    }

                    if (valor != null)
                    {
                        int lineNumExistente;
                        if (int.TryParse(valor.ToString(), out lineNumExistente) && lineNumExistente == lineNumBuscado)
                            return linea;
                    }
                }
                catch
                {
                }
            }

            return null;
        }

        private void ProcesarLineasHijasPorLineNum(
            SAPbobsCOM.GeneralData data,
            string tablaHija,
            List<RegistroTabla> registrosHijos)
        {
            var child = data.Child(tablaHija);

            foreach (var registroHijo in registrosHijos)
            {
                string lineNum = ObtenerValorCampo(registroHijo.Campos, "LineNum", "LineId");

                SAPbobsCOM.GeneralData linea;

                if (!string.IsNullOrWhiteSpace(lineNum))
                {
                    linea = BuscarLineaExistentePorLineNum(child, lineNum);

                    if (linea != null)
                    {
                        AsignarCamposGeneralData(linea, registroHijo.Campos, true);
                        continue;
                    }
                }

                linea = child.Add();
                AsignarCamposGeneralData(linea, registroHijo.Campos, true);
            }
        }

        //private void Guardar_Maestro(string udoCode, RegistroTabla registro, List<(string tablaHija, List<RegistroTabla> registros)> hijos)
        //{
        //    try
        //    {
        //        var srv = Globals.rCompany.GetCompanyService();
        //        var genSrv = srv.GetGeneralService(udoCode);

        //        SAPbobsCOM.GeneralData data;
        //        bool existe = ExisteRegistro_Maestro(udoCode, registro.KeyValue);

        //        if (existe)
        //        {
        //            var param = (SAPbobsCOM.GeneralDataParams)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

        //            param.SetProperty("Code", registro.KeyValue);
        //            data = genSrv.GetByParams(param);
        //        }
        //        else
        //        {
        //            data = (SAPbobsCOM.GeneralData)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

        //            data.SetProperty("Code", registro.KeyValue);
        //        }

        //        foreach (var campo in registro.Campos)
        //        {
        //            if (campo.Value != null && !string.IsNullOrWhiteSpace(campo.Value.ToString()))
        //                data.SetProperty(campo.Key, campo.Value);
        //        }

        //        foreach (var h in hijos)
        //        {
        //            var child = data.Child(h.tablaHija);

        //            for (int i = child.Count - 1; i >= 0; i--)
        //                child.Remove(i);

        //            foreach (var r in h.registros)
        //            {
        //                var line = child.Add();
        //                foreach (var c in r.Campos)
        //                    if (c.Value != null) line.SetProperty(c.Key, c.Value);
        //            }
        //        }

        //        if (existe) genSrv.Update(data);
        //        else genSrv.Add(data);

        //        resultproceso = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultproceso = false;
        //        msg_error = "Error al guardar Maestro: " + ex.Message;
        //    }
        //}

        private void Guardar_Maestro(string udoCode, RegistroTabla registro, List<(string tablaHija, List<RegistroTabla> registros)> hijos)
        {
            try
            {
                var srv = Globals.rCompany.GetCompanyService();
                var genSrv = srv.GetGeneralService(udoCode);

                SAPbobsCOM.GeneralData data;
                bool existe = ExisteRegistro_Maestro(udoCode, registro.KeyValue);

                if (existe)
                {
                    var param = (SAPbobsCOM.GeneralDataParams)
                        genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                    param.SetProperty("Code", registro.KeyValue);
                    data = genSrv.GetByParams(param);
                }
                else
                {
                    data = (SAPbobsCOM.GeneralData)
                        genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                    data.SetProperty("Code", registro.KeyValue);
                }

                // Cabecera
                AsignarCamposGeneralData(data, registro.Campos);

                // Hijos: actualizar por LineNum si existe, si no agregar
                foreach (var h in hijos)
                {
                    ProcesarLineasHijasPorLineNum(data, h.tablaHija, h.registros);
                }

                if (existe)
                    genSrv.Update(data);
                else
                    genSrv.Add(data);

                resultproceso = true;
            }
            catch (Exception ex)
            {
                resultproceso = false;
                msg_error = "Error al guardar Maestro: " + ex.Message;
            }
        }

        //private void Guardar_Documento(string udoCode, RegistroTabla cab, List<(string tablaHija, List<RegistroTabla> registros)> hijos)
        //{
        //    try
        //    {
        //        var srv = Globals.rCompany.GetCompanyService();
        //        var genSrv = srv.GetGeneralService(udoCode);

        //        SAPbobsCOM.GeneralData data;

        //        int docEntry;
        //        bool tieneDocEntry = int.TryParse(cab.KeyValue, out docEntry);

        //        bool existe = tieneDocEntry && ExisteRegistro_Documento(udoCode, cab.KeyValue);

        //        if (existe)
        //        {
        //            var param = (SAPbobsCOM.GeneralDataParams)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

        //            param.SetProperty("DocEntry", docEntry);
        //            data = genSrv.GetByParams(param);
        //        }
        //        else
        //        {
        //            data = (SAPbobsCOM.GeneralData)
        //                genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

        //            // IMPORTANTE:
        //            // En muchos UDO Documento, DocEntry lo asigna SAP al Add().
        //            // Si intentas setear DocEntry manual puede fallar.
        //        }

        //        // Cabecera
        //        foreach (var campo in cab.Campos)
        //        {
        //            if (campo.Value != null && !string.IsNullOrWhiteSpace(campo.Value.ToString()))
        //                data.SetProperty(campo.Key, campo.Value);
        //        }

        //        // Líneas (child tables)
        //        foreach (var h in hijos)
        //        {
        //            var child = data.Child(h.tablaHija);

        //            // limpiar líneas existentes (si es update)
        //            for (int i = child.Count - 1; i >= 0; i--)
        //                child.Remove(i);

        //            foreach (var r in h.registros)
        //            {
        //                var line = child.Add();
        //                foreach (var c in r.Campos)
        //                    if (c.Value != null) line.SetProperty(c.Key, c.Value);
        //            }
        //        }

        //        if (existe) genSrv.Update(data);
        //        else genSrv.Add(data);

        //        resultproceso = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultproceso = false;
        //        msg_error = "Error al guardar Documento: " + ex.Message;
        //    }
        //}

        private void Guardar_Documento(string udoCode, RegistroTabla cab, List<(string tablaHija, List<RegistroTabla> registros)> hijos)
        {
            try
            {
                var srv = Globals.rCompany.GetCompanyService();
                var genSrv = srv.GetGeneralService(udoCode);

                SAPbobsCOM.GeneralData data;

                int docEntry;
                bool tieneDocEntry = int.TryParse(cab.KeyValue, out docEntry);
                bool existe = tieneDocEntry && ExisteRegistro_Documento(udoCode, cab.KeyValue);

                if (existe)
                {
                    var param = (SAPbobsCOM.GeneralDataParams)
                        genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);

                    param.SetProperty("DocEntry", docEntry);
                    data = genSrv.GetByParams(param);
                }
                else
                {
                    data = (SAPbobsCOM.GeneralData)
                        genSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);
                }

                // Cabecera
                AsignarCamposGeneralData(data, cab.Campos);

                // Hijos: actualizar por LineNum si existe, si no agregar
                foreach (var h in hijos)
                {
                    ProcesarLineasHijasPorLineNum(data, h.tablaHija, h.registros);
                }

                if (existe)
                    genSrv.Update(data);
                else
                    genSrv.Add(data);

                resultproceso = true;
            }
            catch (Exception ex)
            {
                resultproceso = false;
                msg_error = "Error al guardar Documento: " + ex.Message;
            }
        }

        private bool ExisteCampo(Dictionary<string, object> campos, string nombreCampo)
        {
            return campos.Keys.Any(k => string.Equals(k, nombreCampo, StringComparison.OrdinalIgnoreCase));
        }

        private void Guardar_NoObjeto(string tabla, RegistroTabla reg, bool incrementoAutomatico = false)
        {
            try
            {
                bool esHana = Globals.rCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB;

                bool existe = ExisteRegistro_NoObjeto(tabla, reg.KeyValue);

                // armamos SET y columnas dinámicas
                var campos = new Dictionary<string, object>(reg.Campos);

                // Asegurar Code/Name si aplica (muchas UDT sin objeto igual tienen Code/Name).
                // En tablas sin objeto con incremento automático, SAP asigna Code en altas,
                // por lo que solo lo enviamos si viene explícito en el archivo.
                if (!incrementoAutomatico || ExisteCampo(campos, "Code"))
                {
                    if (!ExisteCampo(campos, "Code")) campos["Code"] = reg.KeyValue;
                    if (!ExisteCampo(campos, "Name")) campos["Name"] = reg.KeyValue;
                }

                string SqlValue(object v)
                {
                    if (v == null) return "NULL";
                    var s = v.ToString().Replace("'", "''");
                    return $"'{s}'";
                }

                string q(string col) => esHana ? $"\"{col}\"" : $"[{col}]";
                string qt(string t) => esHana ? $"\"@{t}\"" : $"[@{t}]";

                var rs = (SAPbobsCOM.Recordset)Globals.rCompany
                    .GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                if (existe)
                {
                    // UPDATE
                    var sets = campos
                        .Where(k => !string.Equals(k.Key, "Code", StringComparison.OrdinalIgnoreCase)) // no tocar PK
                        .Select(k => $"{q(k.Key)} = {SqlValue(k.Value)}");

                    string sql = $"{(esHana ? "UPDATE " : "UPDATE ")}{qt(tabla)} SET {string.Join(", ", sets)} WHERE {q("Code")} = {SqlValue(reg.KeyValue)}";
                    rs.DoQuery(sql);
                }
                else
                {
                    // INSERT
                    var cols = campos.Keys.Select(k => q(k));
                    var vals = campos.Values.Select(v => SqlValue(v));

                    string sql = $"INSERT INTO {qt(tabla)} ({string.Join(", ", cols)}) VALUES ({string.Join(", ", vals)})";
                    rs.DoQuery(sql);
                }

                resultproceso = true;
            }
            catch (Exception ex)
            {
                resultproceso = false;
                msg_error = "Error al guardar No Objeto: " + ex.Message;
            }
        }

        private void MIConfig_Click(object sender, EventArgs e)
        {
            using (var form = new ConfigForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK &&
                    (Globals.rCompany == null || !Globals.rCompany.Connected))
                {
                    ConectarSAP();
                }
            }
        }

    }

    public class RegistroTabla
    {
        // Para Maestro/No-objeto será Code; para Documento será DocEntry
        public string KeyValue { get; set; }

        public Dictionary<string, object> Campos { get; set; } = new Dictionary<string, object>();
    }

    public class ResultadoCarga
    {
        public string Code { get; set; }
        public string Descripcion { get; set; }
        public bool Exitoso { get; set; }
    }

}

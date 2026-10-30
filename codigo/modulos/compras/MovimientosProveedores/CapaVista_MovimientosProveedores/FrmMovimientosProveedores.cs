
using CapaControlador_Componentes;
using CapaVista_Componentes.Componentes;
using System;
using System.Data;
using System.Windows.Forms;

/*
 * ==================================================================
 * Área : Compras
 * Autor : Carlos Andres Arriaza Lara
 * Carné : 0901-23-13862
 * Fecha : 24/10/2026
 * ==================================================================
 * Propósito :
 * Presenta los filtros, los datos del proveedor y la tabla de
 * movimientos con sus importes y estados.
 * ==================================================================
*/

namespace CapaVista_MovimientosProveedores
{
    public partial class FrmMovimientosProveedores : Form
    {
        // Identificador del proveedor seleccionado.
        private int? _IdProveedorSeleccionado;

        // Datos de demostración de movimientos.
        private readonly object[,] _Movimientos =
        {
            {
                new DateTime(2026, 10, 1),
                "Saldo inicial",
                "SAL-001",
                "Saldo inicial del proveedor",
                0.00m, 0.00m, 0.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 2),
                "Factura",
                "FAC-00125",
                "Compra de suministros de oficina",
                2500.00m, 0.00m, 2500.00m,
                "Pendiente"
            },
            {
                new DateTime(2026, 10, 3),
                "Factura",
                "FAC-00128",
                "Compra de equipo informático",
                4800.00m, 0.00m, 7300.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 5),
                "Pago",
                "PAG-00045",
                "Pago parcial mediante transferencia",
                0.00m, 3000.00m, 4300.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 6),
                "Nota de crédito",
                "NC-00018",
                "Descuento por productos defectuosos",
                0.00m, 500.00m, 3800.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 7),
                "Factura",
                "FAC-00134",
                "Compra de materiales de limpieza",
                1200.00m, 0.00m, 5000.00m,
                "Pendiente"
            },
            {
                new DateTime(2026, 10, 8),
                "Pago",
                "PAG-00049",
                "Pago de factura FAC-00128",
                0.00m, 2000.00m, 3000.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 10),
                "Factura",
                "FAC-00140",
                "Compra de repuestos para mantenimiento",
                1750.00m, 0.00m, 4750.00m,
                "Pendiente"
            }
        };

        private readonly DataTable _TablaMovimientos = new DataTable();

        public FrmMovimientosProveedores()
        {
            InitializeComponent();

            // Configuración inicial.
            ComprasBtnConsultar.Enabled = false;

            ClsPresentacionCompras
                .ComprasMetFijarTamanoFormulario(this);

            BackColor = ClsTemaCompras.FondoGeneral;

            // Eventos del selector de proveedores.
            ComprasCboProveedor.SelectedIndexChanged +=
                ComprasCboProveedor_SelectedIndexChanged;

            // Evento del botón Consultar.
            ComprasBtnConsultar.Click +=
                ComprasBtnConsultar_Click;

            // Inicialización de la tabla.
            ComprasProcInicializarTablaMovimientos();
            ComprasProcCargarMovimientos();

            ComprasDgvMovimientos.DataBindingComplete +=
                (Remitente, Evento) =>
                    ComprasProcFormatearMovimientos();

            ComprasProcFormatearMovimientos();
        }

        // ============================================================
        // PROVEEDOR SELECCIONADO
        // ============================================================

        private void ComprasCboProveedor_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            try
            {
                ClsProveedorCompras Proveedor =
                    ComprasCboProveedor
                        .ComprasFuncObtenerProveedorSeleccionado();

                if (Proveedor == null)
                {
                    ComprasProcLimpiarDatosProveedor();
                    return;
                }

                // Guardar el ID para futuras consultas.
                _IdProveedorSeleccionado = Proveedor.IdProveedor;

                // Mostrar los datos del proveedor.
                ComprasLblNombreProveedor.Text =
                    Proveedor.NombreProveedor ?? string.Empty;

                ComprasLblNit.Text =
                    Proveedor.NitProveedor ?? string.Empty;

                ComprasLblTelefono.Text =
                    Proveedor.TelefonoProveedor ?? string.Empty;

                ComprasLblCorreo.Text =
                    Proveedor.CorreoProveedor ?? string.Empty;

                ComprasBtnConsultar.Enabled = true;
            }
            catch (Exception Ex)
            {
                ComprasProcLimpiarDatosProveedor();

                MessageBox.Show(
                    "No se pudieron obtener los datos del proveedor.\n\n"
                    + Ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // LIMPIAR DATOS DEL PROVEEDOR
        // ============================================================

        private void ComprasProcLimpiarDatosProveedor()
        {
            _IdProveedorSeleccionado = null;

            ComprasLblNombreProveedor.Text = string.Empty;
            ComprasLblNit.Text = string.Empty;
            ComprasLblTelefono.Text = string.Empty;
            ComprasLblCorreo.Text = string.Empty;

            ComprasBtnConsultar.Enabled = false;
        }

        // ============================================================
        // BOTÓN CONSULTAR
        // ============================================================

        private void ComprasBtnConsultar_Click(
            object sender, EventArgs e)
        {
            if (!_IdProveedorSeleccionado.HasValue)
            {
                MessageBox.Show(
                    "Selecciona un proveedor antes de consultar.",
                    "Proveedor requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int IdProveedor = _IdProveedorSeleccionado.Value;

            // TODO:
            // Consultar los movimientos reales usando IdProveedor.
            //
            // Actualmente la tabla contiene datos de demostración.
            // No se filtran todavía por proveedor.

            MessageBox.Show(
                "Proveedor seleccionado correctamente.\n"
                + "ID del proveedor: " + IdProveedor + "\n\n"
                + "La consulta de movimientos reales está pendiente.",
                "Consulta de movimientos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ============================================================
        // INICIALIZAR TABLA DE MOVIMIENTOS
        // ============================================================

        private void ComprasProcInicializarTablaMovimientos()
        {
            _TablaMovimientos.Columns.Add(
                "ComprasColFecha", typeof(DateTime));

            _TablaMovimientos.Columns.Add(
                "Tipo de movimiento", typeof(string));

            _TablaMovimientos.Columns.Add(
                "ComprasColDocumento", typeof(string));

            _TablaMovimientos.Columns.Add(
                "Descripción", typeof(string));

            _TablaMovimientos.Columns.Add(
                "ComprasColDebe", typeof(decimal));

            _TablaMovimientos.Columns.Add(
                "ComprasColHaber", typeof(decimal));

            _TablaMovimientos.Columns.Add(
                "ComprasColSaldo", typeof(decimal));

            _TablaMovimientos.Columns.Add(
                "ComprasColEstado", typeof(string));
        }

        // ============================================================
        // FORMATEAR TABLA
        // ============================================================

        private void ComprasProcFormatearMovimientos()
        {
            string[] Campos =
            {
                "ComprasColFecha",
                "ComprasColDocumento",
                "ComprasColDebe",
                "ComprasColHaber",
                "ComprasColSaldo",
                "ComprasColEstado"
            };

            string[] Titulos =
            {
                "Fecha",
                "Documento",
                "Debe",
                "Haber",
                "Saldo",
                "Estado"
            };

            for (int Indice = 0;
                 Indice < Campos.Length;
                 Indice++)
            {
                if (ComprasDgvMovimientos.Columns.Contains(
                    Campos[Indice]))
                {
                    ComprasDgvMovimientos
                        .Columns[Campos[Indice]]
                        .HeaderText = Titulos[Indice];
                }
            }

            if (!ComprasDgvMovimientos.Columns.Contains(
                "ComprasColFecha"))
            {
                return;
            }

            ComprasDgvMovimientos
                .Columns["ComprasColFecha"]
                .DefaultCellStyle.Format = "dd/MM/yyyy";

            foreach (string Campo in new[]
            {
                "ComprasColDebe",
                "ComprasColHaber",
                "ComprasColSaldo"
            })
            {
                if (!ComprasDgvMovimientos.Columns.Contains(Campo))
                    continue;

                var Estilo = ComprasDgvMovimientos
                    .Columns[Campo]
                    .DefaultCellStyle;

                Estilo.Format = "N2";

                Estilo.FormatProvider =
                    System.Globalization.CultureInfo
                        .InvariantCulture;

                Estilo.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }
        }

        // ============================================================
        // CARGAR MOVIMIENTOS DE DEMOSTRACIÓN
        // ============================================================

        private void ComprasProcCargarMovimientos()
        {
            for (int Indice = 0;
                 Indice < _Movimientos.GetLength(0);
                 Indice++)
            {
                _TablaMovimientos.Rows.Add(
                    _Movimientos[Indice, 0],
                    _Movimientos[Indice, 1],
                    _Movimientos[Indice, 2],
                    _Movimientos[Indice, 3],
                    _Movimientos[Indice, 4],
                    _Movimientos[Indice, 5],
                    _Movimientos[Indice, 6],
                    _Movimientos[Indice, 7]
                );
            }

            ComprasDgvMovimientos.AutoGenerateColumns = true;
            ComprasDgvMovimientos.DataSource = _TablaMovimientos;
        }
    }
}

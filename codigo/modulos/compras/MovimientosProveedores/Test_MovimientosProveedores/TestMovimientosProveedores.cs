
using CapaVista_MovimientosProveedores;
using CapaVista_Componentes.Componentes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Reflection;

namespace Test_MovimientosProveedores
{
    [TestClass]
    public sealed class TestMovimientosProveedores
    {
        // Obtener controles privados del formulario.
        private T ObtenerControl<T>(
            FrmMovimientosProveedores formulario,
            string nombre) where T : class
        {
            FieldInfo campo = typeof(FrmMovimientosProveedores)
                .GetField(
                    nombre,
                    BindingFlags.Instance |
                    BindingFlags.NonPublic
                );

            Assert.IsNotNull(
                campo,
                $"No se encontró el campo {nombre}."
            );

            T control = campo.GetValue(formulario) as T;

            Assert.IsNotNull(
                control,
                $"No se pudo obtener el control {nombre}."
            );

            return control;
        }

        // 1. Inicialización del formulario.
        [STATestMethod]
        public void TestInicializacionFormulario()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                Assert.IsNotNull(formulario);

                Assert.AreEqual(
                    "6001 – MovimientosProveedores",
                    formulario.Text
                );
            }
        }

        // 2. Verificar que el botón Consultar esté deshabilitado.
        [STATestMethod]
        public void TestBotonConsultar()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                ClsButtonCompras boton = ObtenerControl<ClsButtonCompras>(
                    formulario,
                    "ComprasBtnConsultar"
                );

                Assert.IsFalse(
                    boton.Enabled,
                    "El botón Consultar debe iniciar deshabilitado."
                );
            }
        }

        // 3. Verificar la carga de movimientos.
        [STATestMethod]
        public void TestCargaMovimientos()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                UsrDataGridViewCompras tabla =
                    ObtenerControl<UsrDataGridViewCompras>(
                        formulario,
                        "ComprasDgvMovimientos"
                    );

                DataTable datos = tabla.DataSource as DataTable;

                Assert.IsNotNull(
                    datos,
                    "La tabla debe tener un DataTable como origen de datos."
                );

                Assert.AreEqual(
                    8,
                    datos.Rows.Count,
                    "Se deben cargar 8 movimientos."
                );
            }
        }

        // 4. Verificar la cantidad de columnas.
        [STATestMethod]
        public void TestCantidadColumnas()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                UsrDataGridViewCompras tabla =
                    ObtenerControl<UsrDataGridViewCompras>(
                        formulario,
                        "ComprasDgvMovimientos"
                    );

                Assert.AreEqual(
                    8,
                    tabla.Columns.Count,
                    "La tabla debe contener 8 columnas."
                );
            }
        }

        // 5. Verificar las columnas de movimientos.
        [STATestMethod]
        public void TestExistenciaColumnas()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                UsrDataGridViewCompras tabla =
                    ObtenerControl<UsrDataGridViewCompras>(
                        formulario,
                        "ComprasDgvMovimientos"
                    );

                string[] columnas =
                {
                    "ComprasColFecha",
                    "Tipo de movimiento",
                    "ComprasColDocumento",
                    "Descripción",
                    "ComprasColDebe",
                    "ComprasColHaber",
                    "ComprasColSaldo",
                    "ComprasColEstado"
                };

                foreach (string columna in columnas)
                {
                    Assert.IsTrue(
                        tabla.Columns.Contains(columna),
                        $"No existe la columna {columna}."
                    );
                }
            }
        }

        // 6. Verificar formato de fecha.
        [STATestMethod]
        public void TestFormatoFecha()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                UsrDataGridViewCompras tabla =
                    ObtenerControl<UsrDataGridViewCompras>(
                        formulario,
                        "ComprasDgvMovimientos"
                    );

                Assert.AreEqual(
                    "dd/MM/yyyy",
                    tabla.Columns["ComprasColFecha"]
                        .DefaultCellStyle.Format
                );
            }
        }

        // 7. Verificar formato monetario.
        [STATestMethod]
        public void TestFormatoImportes()
        {
            using (var formulario = new FrmMovimientosProveedores())
            {
                UsrDataGridViewCompras tabla =
                    ObtenerControl<UsrDataGridViewCompras>(
                        formulario,
                        "ComprasDgvMovimientos"
                    );

                string[] columnas =
                {
                    "ComprasColDebe",
                    "ComprasColHaber",
                    "ComprasColSaldo"
                };

                foreach (string columna in columnas)
                {
                    Assert.AreEqual(
                        "N2",
                        tabla.Columns[columna]
                            .DefaultCellStyle.Format,
                        $"Formato incorrecto en {columna}."
                    );
                }
            }
        }
    }
}

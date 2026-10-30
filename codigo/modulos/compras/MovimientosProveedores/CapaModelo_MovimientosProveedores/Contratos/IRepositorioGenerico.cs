using System.Collections.Generic;
using System.Data;

/*
 * ==================================================================
 * Área : Compras
 * Autor : Carlos Andres Arriaza Lara
 * Carné : 0901-23-13862
 * Fecha : 29/10/2026
 * ==================================================================
 * Propósito :
 *  Define las operaciones comunes para agregar, editar, eliminar y
 *  consultar las entidades administradas por los repositorios.
 * ===================================================================
*/

namespace CapaModelo_MovimientosProveedores.Contratos
{
    public interface IRepositorioGenerico<Entidad> where Entidad : class
    {
        DataTable ComprasFuncListarProveedores();
    }
}

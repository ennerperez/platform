using System.Data;

#if PORTABLE
namespace System.Data
{
    //
    // Resumen:
    //     Describe la versión de un System.Data.DataRow.
    public enum DataRowVersion
    {
        //
        // Resumen:
        //     La fila contiene sus valores originales.
        Original = 256,
        //
        // Resumen:
        //     La fila contiene valores actuales.
        Current = 512,
        //
        // Resumen:
        //     La fila contiene un valor propuesto.
        Proposed = 1024,
        //
        // Resumen:
        //     La versión predeterminada de System.Data.DataRowState.Para un valor de DataRowState
        //     igual a Added, Modified o Deleted, la versión predeterminada es Current.Para
        //     un valor System.Data.DataRowState de Detached, la versión es Proposed.
        Default = 1536
    }

    //
    // Resumen:
    //     Obtiene el estado de un objeto System.Data.DataRow.
    [Flags]
    public enum DataRowState
    {
        //
        // Resumen:
        //     Se ha creado la fila, pero no forma parte de ningún System.Data.DataRowCollection.System.Data.DataRow
        //     se encuentra en este estado inmediatamente después de haber sido creado y antes
        //     de que se agregue a una colección, o bien si se ha quitado de una colección.
        Detached = 1,
        //
        // Resumen:
        //     La fila no ha cambiado desde que se llamó a System.Data.DataRow.AcceptChanges
        //     por última vez.
        Unchanged = 2,
        //
        // Resumen:
        //     La fila se ha agregado a System.Data.DataRowCollection y no se ha llamado a System.Data.DataRow.AcceptChanges.
        Added = 4,
        //
        // Resumen:
        //     La fila se ha eliminado mediante el método System.Data.DataRow.Delete del System.Data.DataRow.
        Deleted = 8,
        //
        // Resumen:
        //     La fila se ha modificado y no se ha llamado a System.Data.DataRow.AcceptChanges.
        Modified = 16
    }
}
#endif

namespace Platform.Model
{
    public class ISynchronizable
    {

        DataRowVersion RowVersion { get; set; }
        DataRowState RowState { get; set; }

    }
}

using AuxiliarAbarrotes.Clases;
using System.Collections.Generic;

namespace AuxiliarAbarrotes
{
    public interface IDatabase
    {
        IList<Interfaces.ICategoria> getCategorias();
        IList<Interfaces.IProducto> getProductos(int categoria_id, string filtro);
        IList<Interfaces.ITicket> GetTickets(bool ultimos50);
        IList<Interfaces.IArticuloTicket> GetArticuloTickets(int ticket_id);
        void UpdatePrecioVenta(string codigo, double monto);
        void UpdatePrecioFinal(string codigo, double monto);
        Configuracion LeerConfiguracion();
        void GrabarConfiguracion(Configuracion configuracion);
        DatosFactura leerDatosFactura();
        void GrabarDatosFactura(DatosFactura datosFactura);
        void AbrirBaseSistema();
        void GrabarNroComprobante(int nroCbte);
    }
}

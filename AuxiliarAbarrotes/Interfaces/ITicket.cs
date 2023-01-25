using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Interfaces
{
    public interface ITicket
    {
        int Id { get; set; }
        string Nombre { get; set; }
        DateTime Fecha { get; set; }
        double Subtotal { get; set; }
        double Total { get; set; }
        double Ganancia { get; set; }
        int Cantidad { get; set; }
    }
}

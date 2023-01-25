using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Interfaces
{
    public interface IArticuloTicket
    {
        int Id { get; set; }
        string Codigo { get; set; }
        string Nombre { get; set; }
        int Cantidad { get; set; }
        double Precio { get; set; }
    }
}

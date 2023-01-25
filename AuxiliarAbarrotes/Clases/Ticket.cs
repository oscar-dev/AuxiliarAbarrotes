using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Clases
{
    public class Ticket : Interfaces.ITicket
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }
        public double Ganancia { get; set; }
        public int Cantidad { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Clases
{
    public class Producto : Interfaces.IProducto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Departamento { get; set; }
        public string Descripcion { get; set; }
        public string TVenta { get; set; }
        public double PCosto { get; set; }
        public double PVenta { get; set; }
        public int Dept { get; set; }
        public double PFinal { get; set; }
    }
}

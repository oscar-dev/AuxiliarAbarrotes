using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Interfaces
{
    public interface IProducto
    {
        int Id { get; set; }
        string Codigo{ get; set; }
        string Departamento { get; set; }
        string Descripcion { get; set; }
        string TVenta { get; set; }
        double PCosto{ get; set; }
        double PVenta { get; set; }
        int Dept { get; set; }
        double PFinal { get; set; }
    }
}

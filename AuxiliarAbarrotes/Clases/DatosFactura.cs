using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Clases
{
    public class DatosFactura
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string IIBB { get;set; }
        public string CondicionIVA { get; set; }
        public string Domicilio1 { get; set; }
        public string Domicilio2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliarAbarrotes.Clases
{
    public class Configuracion
    {
        public string pathDB { get; set; }
        public string impresora { get; set; }
        public string certificado { get; set; }
        public string clave_cert { get; set; }
        public int punto_vta { get; set; }
        public int ultimo_cbte { get; set; }
    }
}

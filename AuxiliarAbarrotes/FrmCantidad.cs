using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuxiliarAbarrotes
{
    public partial class FrmCantidad : Form
    {
        public int Cantidad { get; set; }
        public FrmCantidad()
        {
            InitializeComponent();

            this.Cantidad = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Cantidad = Convert.ToInt32(nudCantidad.Value);

            this.DialogResult = DialogResult.OK;
        }

        private void FrmCantidad_Load(object sender, EventArgs e)
        {
            nudCantidad.Value = this.Cantidad;
        }
    }
}

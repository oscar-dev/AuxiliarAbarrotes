using AuxiliarAbarrotes.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuxiliarAbarrotes
{
    public partial class FrmProductos : Form
    {
        private Interfaces.ICategoria _categoria;
        private string _filtro;
        private IDatabase _db;
        
        public List<Interfaces.IProducto> Productos { get; set; }

        public FrmProductos(IDatabase db, Interfaces.ICategoria categoria, string filtro)
        {
            InitializeComponent();

            _db = db;
            _filtro = filtro;
            _categoria = categoria;
            Productos = new List<Interfaces.IProducto>();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            IList<Interfaces.IProducto> productos = this._db.getProductos(_categoria == null ? -1 : _categoria.Id, _filtro);
            dgvDatos.Rows.Clear();

            foreach (var item in productos)
            {
                int idx = dgvDatos.Rows.Add(item.Departamento);
                dgvDatos.Rows[idx].Cells[1].Value = item.Codigo;
                dgvDatos.Rows[idx].Cells[2].Value = item.Descripcion;
                dgvDatos.Rows[idx].Cells[3].Value = item.PVenta;
                dgvDatos.Rows[idx].Cells[4].Value = item.PFinal;
                dgvDatos.Rows[idx].Tag = item;
            }
        }

        private void btnSeleccionados_Click(object sender, EventArgs e)
        {
            devolverSeleccionados();
        }

        private void devolverSeleccionados()
        {
            this.Productos.Clear();

            for (int i = 0; i < dgvDatos.SelectedRows.Count; i++)
            {
                this.Productos.Add((Interfaces.IProducto)dgvDatos.SelectedRows[i].Tag);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnAgregarTodos_Click(object sender, EventArgs e)
        {
            this.Productos.Clear();

            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                this.Productos.Add((Interfaces.IProducto)dgvDatos.Rows[i].Tag);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void dgvDatos_DoubleClick(object sender, EventArgs e)
        {
            devolverSeleccionados();
        }
    }
}

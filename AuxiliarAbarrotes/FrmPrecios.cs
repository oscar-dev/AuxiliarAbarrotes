using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuxiliarAbarrotes
{
    public partial class FrmPrecios : Form
    {
        private IDatabase _db;
        public FrmPrecios(IDatabase db)
        {
            InitializeComponent();
            
            this._db = db;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPrecios_Resize(object sender, EventArgs e)
        {
            panel2.Left = (panel1.Width / 2) - (panel2.Width / 2);
        }

        private void FrmPrecios_Load(object sender, EventArgs e)
        {
            this.Text = "Modificación de precios";
            /*
            if( !check() )
            {
                this.Close();
            }
            */
            try
            {
                this._db.AbrirBaseSistema();
            } catch
            {
                MessageBox.Show("No se pudo abrir la base de productos.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            cbCategorias.Items.AddRange(this._db.getCategorias().ToArray());

            rbPorcentaje.Checked = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Interfaces.ICategoria categoria = (Interfaces.ICategoria)cbCategorias.SelectedItem;

            IList<Interfaces.IProducto> productos = this._db.getProductos(categoria == null ? -1 : categoria.Id, tbFiltro.Text);

            dgvDatos.Rows.Clear();

            foreach (var item in productos)
            {
                int idx = dgvDatos.Rows.Add(item.Id.ToString());
                dgvDatos.Rows[idx].Cells[0].Value = item.Departamento;
                dgvDatos.Rows[idx].Cells[1].Value = item.Codigo;
                dgvDatos.Rows[idx].Cells[2].Value = item.Descripcion;
                dgvDatos.Rows[idx].Cells[3].Value = item.PVenta;
            }
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            bool porcentaje = rbPorcentaje.Checked;
            double valor = Decimal.ToDouble(nudValor.Value);

            foreach (DataGridViewRow item in dgvDatos.Rows)
            {
                double precio = Double.Parse(item.Cells[3].Value.ToString());

                precio += porcentaje ? (precio * (valor/100.0)) : valor;

                item.Cells[3].Value = precio;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dgvDatos.Rows)
                {
                    //int id = int.Parse(item.Cells[0].Value.ToString());
                    string codigo = item.Cells[1].Value.ToString();

                    double precio = double.Parse(item.Cells[3].Value.ToString());

                    this._db.UpdatePrecioVenta(codigo, precio);

                }

                MessageBox.Show("Los precios se actualizaron correctamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error actualizando precios", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool check()
        {
            bool ret = false;
            try
            {

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AuxiliarAbarrotes");
                if (key == null)
                {
                    return ret;
                }

                object oValue = key.GetValue("key");
                if (oValue == null)
                {
                    key.Close();
                    return ret;
                }
                else
                {
                    byte[] bufferDest = new byte[12];
                    FileSystemInfo info = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

                    DateTime dtInfo = info.CreationTimeUtc;

                    SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                    Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(dtInfo.Ticks.ToString());
                    Byte[] hashDestino = sha1.ComputeHash(textOriginal);

                    bufferDest[0] = hashDestino[0];
                    bufferDest[1] = hashDestino[1];
                    bufferDest[2] = hashDestino[2];
                    bufferDest[3] = hashDestino[3];
                    bufferDest[4] = hashDestino[4];
                    bufferDest[5] = hashDestino[5];
                    bufferDest[6] = 0x93;
                    bufferDest[7] = 0x33;
                    bufferDest[8] = 0x23;
                    bufferDest[9] = 0x45;
                    bufferDest[10] = 0x20;
                    bufferDest[11] = 0x91;

                    Byte[] hash = sha1.ComputeHash(bufferDest);
                    StringBuilder sHash = new StringBuilder();

                    foreach (byte b in hash)
                    {
                        sHash.AppendFormat("{0:x2}", b);
                    }

                    string cadena = sHash.ToString();

                    if (cadena.Equals((string)oValue))
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    key.Close();
                }
            }
            catch
            {
                ret = false;
            }

            return ret;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmPrecios_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this._db != null)
            {
                this._db.CerrarBaseSistemas();
            }

            Cursor.Current = Cursors.Default;
        }
    }
}

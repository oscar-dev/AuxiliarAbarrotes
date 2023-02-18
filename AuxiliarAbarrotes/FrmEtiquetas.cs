using AuxiliarAbarrotes.Clases;
using AuxiliarAbarrotes.Interfaces;
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
    public partial class FrmEtiquetas : Form
    {
        private IDatabase _db;
        public FrmEtiquetas(IDatabase db)
        {
            InitializeComponent();

            this._db = db;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEtiquetas_Load(object sender, EventArgs e)
        {
            this.Text = "Impresión de etiquetas";
            /*
            if( ! check() )
            {
                this.Close();
            }
            */
            try
            {
                this._db.AbrirCopiaBaseSistema();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la base de productos. Error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            cbCategorias.Items.AddRange(this._db.getCategorias().ToArray());
        }

        private void FrmEtiquetas_Resize(object sender, EventArgs e)
        {
            panel2.Left = (panel1.Width / 2) - (panel2.Width / 2);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Interfaces.ICategoria categoria = (Interfaces.ICategoria)cbCategorias.SelectedItem;

            FrmProductos frmProductos = new FrmProductos(this._db, categoria, tbFiltro.Text);
            
            if( frmProductos.ShowDialog() == DialogResult.OK )
            {
                foreach (var item in frmProductos.Productos)
                {
                    int idx = dgvDatos.Rows.Add(item.Departamento);
                    dgvDatos.Rows[idx].Cells[1].Value = item.Codigo;
                    dgvDatos.Rows[idx].Cells[2].Value = item.Descripcion;
                    dgvDatos.Rows[idx].Cells[3].Value = item.PVenta;
                    dgvDatos.Rows[idx].Cells[4].Value = item.PFinal;
                    dgvDatos.Rows[idx].Cells[5].Value = 1;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<Clases.Etiqueta> datos = new List<Clases.Etiqueta>();

            foreach(DataGridViewRow item in dgvDatos.Rows)
            {
                int cantidad = Convert.ToInt32(item.Cells[5].Value);

                for(int i = 0; i < cantidad; i++)
                {
                    datos.Add(new Clases.Etiqueta()
                    {
                        Codigo = item.Cells[1].Value.ToString(),
                        Nombre = item.Cells[2].Value.ToString(),
                        Departamento = item.Cells[0].Value.ToString(),
                        Precio = Double.Parse(item.Cells[4].Value.ToString())
                    });
                }
            }

            FrmRptEtiqueta frmRptEtiqueta = new FrmRptEtiqueta(datos);

            frmRptEtiqueta.ShowDialog();

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

        private void FrmEtiquetas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if( this._db != null )
            {
                this._db.CerrarBaseSistemas();
            }
            Cursor.Current = Cursors.Default;
        }

        private void cambiarCantidad()
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                FrmCantidad frmCantidad = new FrmCantidad();

                frmCantidad.Cantidad = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[5].Value);

                if (frmCantidad.ShowDialog() == DialogResult.OK)
                {
                    dgvDatos.SelectedRows[0].Cells[5].Value = frmCantidad.Cantidad;
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cambiarCantidad();
        }

        private void dgvDatos_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dgvDatos_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    cambiarCantidad();
                    break;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }
    }
}


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AuxiliarAbarrotes
{
    public partial class FrmFacturas : Form
    {
        private IDatabase _db;
        public FrmFacturas(IDatabase db)
        {
            InitializeComponent();

            this._db = db;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            panel1.Left = (splitContainer1.Panel2.Width / 2) - panel1.Width / 2;
        }

        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            this.Text = "Generar Factura";
            /*
            if (!check())
            {
                this.Close();
            }*/
            
            if (!checkConfig())
            {
                this.Close();
                return;
            }

            try
            {
                this._db.AbrirBaseSistema();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la base de productos. Error: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            ckbUltimos50.Checked = true;
            ckbConsumidorFinal.Checked = true;
            tbCUIT.Enabled = false;
            tbCUIT.Text = "";

            buscarTickets(ckbUltimos50.Checked);
        }

        private void ckbUltimos50_CheckedChanged(object sender, EventArgs e)
        {
            buscarTickets(ckbUltimos50.Checked);
        }

        private void buscarTickets(bool ultimos50)
        {
            IList<Interfaces.ITicket> tickets = this._db.GetTickets(ultimos50);

            dgvDatos.Rows.Clear();

            foreach(Interfaces.ITicket ticket in tickets)
            {
                int idx = dgvDatos.Rows.Add(ticket.Id.ToString());
                dgvDatos.Rows[idx].Cells[1].Value = ticket.Nombre;
                dgvDatos.Rows[idx].Cells[2].Value = ticket.Fecha.ToString("dd/MM/yyyy hh:mm:ss");
                dgvDatos.Rows[idx].Cells[3].Value = ticket.Cantidad.ToString();
                dgvDatos.Rows[idx].Cells[4].Value = ticket.Total.ToString();
                dgvDatos.Rows[idx].Tag = ticket;
            }
        }
        private void dgvDatos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                lbListaArticulos.Items.Clear();
                lbListaArticulos.Tag = null;

                IList<Interfaces.IArticuloTicket> articuloTickets = this._db.GetArticuloTickets(Int32.Parse(dgvDatos.SelectedRows[0].Cells[0].Value.ToString()));

                foreach( var articuloTicket in articuloTickets)
                {
                    int idx = lbListaArticulos.Items.Add(articuloTicket.Codigo + " - " + articuloTicket.Nombre + " - " + articuloTicket.Cantidad.ToString());
                }
                lbListaArticulos.Tag = articuloTickets;
            }
        }

        private void ckbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            tbCUIT.Enabled = !ckbConsumidorFinal.Checked;

            if (!tbCUIT.Enabled) tbCUIT.Text = "";
        }

        private bool checkConfig()
        {
            Clases.Configuracion config = this._db.LeerConfiguracion();
            Clases.DatosFactura datosFactura = this._db.leerDatosFactura();

            if( datosFactura.CUIT.Trim().Length <= 0 )
            {
                MessageBox.Show("Falta configurar CUIT", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (config.certificado.Trim().Length <= 0)
            {
                MessageBox.Show("Falta indicar el certificado a utilizar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (config.clave_cert.Trim().Length <= 0)
            {
                MessageBox.Show("Falta indicar el certificado a utilizar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                Interfaces.ITicket ticket = (Interfaces.ITicket)dgvDatos.SelectedRows[0].Tag;
                Clases.Configuracion config = this._db.LeerConfiguracion();
                Clases.DatosFactura datosFactura = this._db.leerDatosFactura();

                AFIP.FacturaElectronica facturaElectronica = new AFIP.FacturaElectronica();
                
                facturaElectronica.CbteTipo = 6;
                facturaElectronica.PtoVenta = config.punto_vta;
                facturaElectronica.ClaveCertificado = config.clave_cert;
                facturaElectronica.PathCertificado = config.certificado;
                facturaElectronica.Cuit = long.Parse(datosFactura.CUIT);
                //facturaElectronica.Fecha = ticket.Fecha;
                facturaElectronica.Fecha = DateTime.Now;
                facturaElectronica.Monto = ticket.Total;
                facturaElectronica.NroCbte = config.ultimo_cbte + 1;

                if( facturaElectronica.Facturar() )
                {
                    this._db.GrabarNroComprobante(facturaElectronica.NroCbte);

                    Clases.Factura factura = new Clases.Factura();
                    factura.Fecha = facturaElectronica.Fecha;
                    factura.TipoCbte = facturaElectronica.CbteTipo;
                    factura.PuntoVenta = facturaElectronica.PtoVenta;
                    factura.NumeroCbte = facturaElectronica.NroCbte;
                    factura.Impresora = config.impresora;
                    factura.RazonSocial = datosFactura.RazonSocial;
                    factura.CUIT = datosFactura.CUIT;
                    factura.CondicionOtros = datosFactura.IIBB;
                    factura.CondicionIVA = datosFactura.CondicionIVA;
                    factura.Direccion1 = datosFactura.Domicilio1;
                    factura.Direccion2 = datosFactura.Domicilio2;
                    factura.CAE = facturaElectronica.CAE;
                    factura.VtoCAE = facturaElectronica.FechaCbte;

                    factura.ticket = ticket;
                    factura.articuloTickets = (List<Interfaces.IArticuloTicket>)lbListaArticulos.Tag;

                    factura.Generar();
                } else
                {
                    MessageBox.Show("Error facturando: [" + facturaElectronica.ErrorMessage + "]", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void FrmFacturas_FormClosed(object sender, FormClosedEventArgs e)
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

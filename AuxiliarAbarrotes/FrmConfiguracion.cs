using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AuxiliarAbarrotes
{
    public partial class FrmConfiguracion : Form
    {
        private IDatabase _db;
        public FrmConfiguracion(IDatabase db)
        {
            InitializeComponent();

            this._db = db;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmConfiguracion_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.Width / 2) - (panel1.Width / 2);
        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            this.Text = "Configuración";

            cbImpresora.Items.Clear();
            
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbImpresora.Items.Add(printer);
            }

            loadValues();
        }
        private void btnPathDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivos de bases de datos (*.fdb)|*.fdb|Todos los archivos (*.*)|*.*";
            dialog.Title = "Seleccione el archivo de base de datos...";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbPathDB.Text = dialog.FileName;
            }
        }
        private void btnPathCert_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Certificados (*.pfx)|*.pfx|Todos los archivos (*.*)|*.*";
            dialog.Title = "Seleccione el certificado...";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbPathCert.Text = dialog.FileName;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if( cbImpresora.SelectedItem == null )
            {
                MessageBox.Show("Debe seleccionar una impresora válida", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                Clases.Configuracion config = new Clases.Configuracion();

                config.pathDB = tbPathDB.Text;
                config.certificado = tbPathCert.Text;
                config.clave_cert = tbClaveCert.Text;
                config.impresora = cbImpresora.SelectedItem.ToString();

                int punto_vta = 0;
                if (int.TryParse(nudPtoVta.Value.ToString(), out punto_vta))
                {
                    config.punto_vta = punto_vta;
                }
                else
                {
                    config.punto_vta = 1;
                }

                int ultimo_cbte = 0;
                if (int.TryParse(tbUltimoCbte.Text, out ultimo_cbte))
                {
                    config.ultimo_cbte = ultimo_cbte;
                }
                else
                {
                    config.ultimo_cbte = 1;
                }

                this._db.GrabarConfiguracion(config);

                Clases.DatosFactura datosFactura = new Clases.DatosFactura();

                datosFactura.RazonSocial = tbRazonSocial.Text;
                datosFactura.CUIT = tbCUIT.Text;
                datosFactura.IIBB = tbIIBB.Text;
                datosFactura.Domicilio1 = tbDomicilio1.Text;
                datosFactura.Domicilio2 = tbDomicilio2.Text;
                datosFactura.CondicionIVA = tbCondicionIVA.Text;

                this._db.GrabarDatosFactura(datosFactura);

                MessageBox.Show("La configuración se grabó correctamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch
            {
                MessageBox.Show("Error grabando configuración", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadValues()
        {
            Clases.Configuracion config = this._db.LeerConfiguracion();

            tbPathDB.Text = config.pathDB;
            cbImpresora.SelectedItem = config.impresora;
            tbPathCert.Text = config.certificado;
            tbClaveCert.Text = config.clave_cert;
            nudPtoVta.Value = config.punto_vta;
            tbUltimoCbte.Text = config.ultimo_cbte.ToString();

            Clases.DatosFactura datosFactura = this._db.leerDatosFactura();

            tbRazonSocial.Text = datosFactura.RazonSocial;
            tbCUIT.Text = datosFactura.CUIT;
            tbIIBB.Text = datosFactura.IIBB;
            tbDomicilio1.Text = datosFactura.Domicilio1;
            tbDomicilio2.Text = datosFactura.Domicilio2;
            tbCondicionIVA.Text = datosFactura.CondicionIVA;

        }
        private void btnGetAfip_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnGetAfip.Enabled = false;

            if (tbClaveCert.Text.Trim().Length <= 0)
            {
                MessageBox.Show("No se ingresó ninguna clave", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

            if (tbCUIT.Text.Trim().Length <= 0)
            {
                MessageBox.Show("No se ingresó ningun CUIT", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

            if( tbPathCert.Text.Trim().Length <= 0 )
            {
                MessageBox.Show("No se ingresó ningun certificado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

            AFIP.FacturaElectronica fe = new AFIP.FacturaElectronica();

            fe.Cuit = long.Parse(tbCUIT.Text);
            fe.PathCertificado = tbPathCert.Text;
            fe.ClaveCertificado = tbClaveCert.Text;

            int ultimoCbte = fe.GetUltimoCbte(1, 6);

            tbUltimoCbte.Text = ultimoCbte.ToString();
            
            btnGetAfip.Enabled = true;

            Cursor.Current = Cursors.Default;
        }
    }
}

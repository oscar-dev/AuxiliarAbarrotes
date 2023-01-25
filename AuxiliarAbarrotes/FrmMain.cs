using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;

namespace AuxiliarAbarrotes
{
    public partial class FrmMain : Form
    {
        private IDatabase _database;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void tsbPrecios_Click(object sender, EventArgs e)
        {
            FrmPrecios frmPrecios = new FrmPrecios(this._database);

            frmPrecios.ShowDialog();
        }

        private void tsbEtiquetas_Click(object sender, EventArgs e)
        {
            FrmEtiquetas frmEtiquetas = new FrmEtiquetas(this._database);

            frmEtiquetas.ShowDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
           /*if( !check() )
           {
                FrmLicencia frmLicencia = new FrmLicencia();

                frmLicencia.ShowDialog();

                this.Close();

                return;
            }
           */
            this._database = new Database(ConfigurationManager.AppSettings.Get("dbpath"),
                                            ConfigurationManager.AppSettings.Get("dbname"));
        }

        private void tsbFacturas_Click(object sender, EventArgs e)
        {
            FrmFacturas frmFacturas = new FrmFacturas(this._database);

            frmFacturas.ShowDialog();
        }

        private void tsbConfig_Click(object sender, EventArgs e)
        {
            FrmConfiguracion frmConfiguracion = new FrmConfiguracion(this._database);

            frmConfiguracion.ShowDialog();
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
    }
}

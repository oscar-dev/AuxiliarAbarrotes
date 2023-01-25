using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AuxiliarAbarrotes
{
    public partial class FrmLicencia : Form
    {
        public FrmLicencia()
        {
            InitializeComponent();

            btnSalir.Tag = false;

            tbClave1.TextChanged += TbClave1_TextChanged;
            tbClave2.TextChanged += TbClave1_TextChanged;
            tbClave3.TextChanged += TbClave1_TextChanged;
            tbClave4.TextChanged += TbClave1_TextChanged;
            tbClave5.TextChanged += TbClave1_TextChanged;
            tbClave6.TextChanged += TbClave1_TextChanged;

        }

        private void TbClave1_TextChanged(object sender, EventArgs e)
        {
            if( tbClave1.Text.Trim().Length > 0 ||
                tbClave2.Text.Trim().Length > 0 ||
                tbClave3.Text.Trim().Length > 0 ||
                tbClave4.Text.Trim().Length > 0 ||
                tbClave5.Text.Trim().Length > 0 ||
                tbClave6.Text.Trim().Length > 0 )
            {
                btnSalir.Tag = true;
                btnSalir.Text = "&Registrar";
            }
            else
            {
                btnSalir.Tag = false;
                btnSalir.Text = "&Salir";
            }
        }

        private void FrmLicencia_Load(object sender, EventArgs e)
        {
            GenerarKey();
        }

        private void GenerarKey()
        {
            FileSystemInfo info = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            DateTime dtInfo = info.CreationTimeUtc;

            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(dtInfo.Ticks.ToString());
            Byte[] hash = sha1.ComputeHash(textOriginal);

            StringBuilder cadena = new StringBuilder();

            cadena.AppendFormat("{0:X2}", hash[0]);
            tbKey1.Text = cadena.ToString();
            cadena.Clear();

            cadena.AppendFormat("{0:X2}", hash[1]);
            tbKey2.Text = cadena.ToString();
            cadena.Clear();

            cadena.AppendFormat("{0:X2}", hash[2]);
            tbKey3.Text = cadena.ToString();
            cadena.Clear();
            
            cadena.AppendFormat("{0:X2}", hash[3]);
            tbKey4.Text = cadena.ToString();
            cadena.Clear();

            cadena.AppendFormat("{0:X2}", hash[4]);
            tbKey5.Text = cadena.ToString();
            cadena.Clear();

            cadena.AppendFormat("{0:X2}", hash[5]);
            tbKey6.Text = cadena.ToString();
            cadena.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[6];
            byte[] bufferDest = new byte[12];

            if ( (bool)btnSalir.Tag)
            {
                try
                {
                    buffer[0] = byte.Parse(tbClave1.Text, System.Globalization.NumberStyles.HexNumber);
                    buffer[1] = byte.Parse(tbClave2.Text, System.Globalization.NumberStyles.HexNumber);
                    buffer[2] = byte.Parse(tbClave3.Text, System.Globalization.NumberStyles.HexNumber);
                    buffer[3] = byte.Parse(tbClave4.Text, System.Globalization.NumberStyles.HexNumber);
                    buffer[4] = byte.Parse(tbClave5.Text, System.Globalization.NumberStyles.HexNumber);
                    buffer[5] = byte.Parse(tbClave6.Text, System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    MessageBox.Show("Clave de entrada incorrecta");
                    tbClave1.Focus();
                }
            
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

                if( hash[0] == buffer[0] &&
                    hash[1] == buffer[1] &&
                    hash[2] == buffer[2] &&
                    hash[3] == buffer[3] &&
                    hash[4] == buffer[4] &&
                    hash[5] == buffer[5] )
                {
                    hashDestino = sha1.ComputeHash(bufferDest);
                    StringBuilder sHash = new StringBuilder();

                    foreach( byte b in hashDestino)
                    {
                        sHash.AppendFormat("{0:x2}", b);
                    }

                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AuxiliarAbarrotes", true);

                    if( key == null )
                    {
                        key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AuxiliarAbarrotes", true);
                    }

                    key.SetValue("key", sHash.ToString());
                
                    MessageBox.Show("El software se registro con exito. Vuelva a ingresar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Licencia no valida", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void tbClave1_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbClave1.Text.Length >= 2 ) tbClave2.Focus();
        }

        private void tbClave2_TextChanged(object sender, EventArgs e)
        {
            if (tbClave2.Text.Length >= 2) tbClave3.Focus();
        }

        private void tbClave3_TextChanged(object sender, EventArgs e)
        {
            if (tbClave3.Text.Length >= 2) tbClave4.Focus();
        }

        private void tbClave4_TextChanged(object sender, EventArgs e)
        {
            if (tbClave4.Text.Length >= 2 ) tbClave5.Focus();
        }

        private void tbClave5_TextChanged(object sender, EventArgs e)
        {
            if (tbClave5.Text.Length >= 2 ) tbClave6.Focus();
        }
    }
}

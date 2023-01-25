using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using Newtonsoft.Json;
using System.Collections;

namespace AuxiliarAbarrotes.Clases
{
    public class Factura
    {
        public int PuntoVenta { get; set; }
        public long NumeroCbte { get; set; }
        public int TipoCbte { get; set; }
        public string RazonSocial { get; set; }
        public DateTime Fecha { get; set; }
        public string CUIT { get; set; }
        public string CondicionIVA { get; set; }
        public string CondicionOtros { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string CUITCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CondicionCliente { get; set; }
        public bool ConsumidorFinal { get; set; }
        public string CAE { get; set; }
        public string VtoCAE { get; set; }
        public string Impresora { get; set; }
        public Interfaces.ITicket ticket { get; set; }
        public List<Interfaces.IArticuloTicket> articuloTickets { get; set; }
        
        class QRDataFiscal
        {
            public short ver;
            public string fecha;
            public long cuit;
            public int ptoVta;
            public short tipoCmp;
            public long nroCmp;
            public long importe;
            public string moneda;
            public long ctz;
            public short tipoDocRec;
            public long nroDocRec;
            public string tipoCodAut;
            public long codAut;
        };
        public void Generar()
        {
            PrintDocument pd = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();

            ps.PrinterName = this.Impresora;

            pd.PrinterSettings = ps;

            pd.DocumentName = "ticket";

            pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintPage);

            pd.Print();
        }
        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            float entrelinea = 6;
            //Get the Graphics object  
            Graphics g = e.Graphics;

            SolidBrush brush = new SolidBrush(Color.Black);

            //Create a font Arial with size 16  
            Font font = new Font("Arial", 14);

            float x = 20;
            float y = 20;
            RectangleF rect = new RectangleF(x, y, 200, 20);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(this.RazonSocial, font, brush, rect, sf);
            y += 18;
            y += entrelinea;
            rect.Y = y;
            rect.Height = 12;
            font = new Font("Arial", 10);
            g.DrawString("CUIT: " + this.CUIT, font, brush, rect, sf);
            y += 12;
            y += entrelinea;
            font = new Font("Arial", 10);
            rect.Y = y;
            g.DrawString(this.Direccion1, font, brush, rect, sf);
            y += 12;
            y += entrelinea;
            rect.Y = y;
            g.DrawString(this.Direccion2, font, brush, rect, sf);
            y += 12;
            y += entrelinea;
            rect.Y = y;
            g.DrawString(this.CondicionIVA, font, brush, rect, sf);
            y += 12;
            y += entrelinea;
            rect.Y = y;
            g.DrawString("IIBB: " + this.CondicionOtros, font, brush, rect, sf);

            y += 12;
            y += entrelinea;
            Pen pen = new Pen(Color.Black, 1);
            g.DrawLine(pen, x, y, x + 200, y);

            Font fontBig = new Font("Arial", 24, FontStyle.Bold);
            y += entrelinea;
            rect.Y = y;
            rect.Height = 26;
            g.DrawString(getNombreTipoCbte(), fontBig, brush, rect, sf);

            y += fontBig.Size;
            y += entrelinea * 2;
            rect.Y = y;
            rect.Height = 16;
            g.DrawString(this.PuntoVenta.ToString("D4") + " - " + this.NumeroCbte.ToString("D8"), font, brush, rect, sf);

            y += entrelinea * 3;
            g.DrawLine(pen, x, y, x + 200, y);

            y += entrelinea * 2;
            rect.Y = y;
            g.DrawString("Consumidor Final", font, brush, rect, sf);

            y += entrelinea * 3;
            g.DrawLine(pen, x, y, x + 200, y);

            Font fontMini = new Font("Arial", 6);

            y += entrelinea * 2;
            //g.DrawString("Cant.     Precio    Articulo    Importe", fontMini, brush, x, y);

            g.DrawString("Cant.", fontMini, brush, x, y);
            g.DrawString("   Precio", fontMini, brush, x + 22, y);
            g.DrawString("Articulo", fontMini, brush, x + 68, y);
            g.DrawString("   Importe", fontMini, brush, x + 160, y);

            foreach (var articulo in this.articuloTickets)
            {
                y += entrelinea + font.Size;

                string[] lineas = calculaLineasTickets(g, articulo.Nombre, font, 165);

                g.DrawString(String.Format("{0}", articulo.Cantidad).PadLeft(7), fontMini, brush, x, y);
                g.DrawString(String.Format("{0:N2}", articulo.Precio).PadLeft(9), fontMini, brush, x +22, y);
                g.DrawString(lineas[0].PadRight(20), fontMini, brush, x + 68, y);
                g.DrawString(String.Format("{0:N2}", (articulo.Precio * articulo.Cantidad)).PadLeft(9), fontMini, brush, x + 160, y);

                /*g.DrawString(String.Format("{0:N2}", articulo.Cantidad) + "   " +
                                    String.Format("{0:N2}", articulo.Precio) + "   " +
                                    lineas[0].PadRight(20) +
                                    String.Format("{0:N2}", (articulo.Precio * articulo.Cantidad)), fontMini, brush, x, y);*/

                if( lineas.Length > 1)
                {
                    for(int i=1; i < lineas.Length; i++ )
                    {
                        y += entrelinea + font.Size;
                        g.DrawString(lineas[i].PadRight(20), fontMini, brush, x + 68, y);
                    }
                }
            }

            y += entrelinea * 3;
            g.DrawLine(pen, x, y, x + 200, y);

            fontBig = new Font("Arial", 20);
            y += entrelinea*2;
            rect.Y = y;
            rect.Height = 24;
            g.DrawString("Total: " + String.Format("{0:N2}", ticket.Total), fontBig, brush, rect, sf);

            y += fontBig.Size;
            y += entrelinea * 2;
            g.DrawLine(pen, x, y, x + 200, y);

            y += entrelinea;
            rect.Y = y;
            rect.Height = 16;
            g.DrawString("CAE: " + this.CAE + "    Fecha: " + this.VtoCAE, fontMini, brush, rect, sf);

            y += entrelinea*2;

            QRDataFiscal qrData = new QRDataFiscal();
            qrData.ver = 1;
            qrData.fecha = this.Fecha.ToString("Y-M-d");
            qrData.cuit = long.Parse(this.CUIT);
            qrData.ptoVta = this.PuntoVenta;
            qrData.tipoCmp = (short)this.TipoCbte;
            qrData.nroCmp = this.NumeroCbte;
            qrData.importe = (long)ticket.Total * 100;
            qrData.moneda = "PES";
            qrData.ctz = 1;
            qrData.tipoDocRec = 0;
            qrData.nroDocRec = 0;
            qrData.tipoCodAut = "E";
            qrData.codAut = long.Parse(this.CAE);

            string strQR = "https://www.afip.gob.ar/fe/qr/?p=" + Base64Encode(JsonConvert.SerializeObject(qrData));

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(strQR, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            g.DrawImage(qrCodeImage, new Rectangle( (int)x, (int)y, 200, 200));
        }

        private string[] calculaLineasTickets( Graphics g, string texto, Font font, int wSize)
        {
            List<String> lineas = new List<String>();
            string buffer = "";
            int ultimoEspacio = -1;

            for (int i = 0; i < texto.Length; i++)
            {
                buffer += texto[i];

                ultimoEspacio = texto[i] == ' ' ? i : ultimoEspacio;

                SizeF s = g.MeasureString(buffer, font);

                if (s.Width > wSize)
                {
                    if (ultimoEspacio > -1)
                    {
                        buffer = buffer.Remove(buffer.Length - (i - ultimoEspacio));
                        i = ultimoEspacio;
                        ultimoEspacio = -1;
                    }

                    lineas.Add(buffer.Trim());
                    buffer = "";
                }
            }

            if (buffer.Length > 0) lineas.Add(buffer.Trim());

            return lineas.ToArray();
        }

        private string getNombreTipoCbte()
        {
            string nombre = "";
            switch(this.TipoCbte)
            {
                case 6:
                    nombre = "B"; 
                    break;
            }

            return nombre;
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}

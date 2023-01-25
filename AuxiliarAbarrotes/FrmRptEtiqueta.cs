using Microsoft.Reporting.WinForms;
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
    public partial class FrmRptEtiqueta : Form
    {
        public FrmRptEtiqueta(IList<Clases.Etiqueta> etiquetas)
        {
            InitializeComponent();

            ReportDataSource reportDataSource = new ReportDataSource("Items", etiquetas);

            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
        }

        private void FrmRptEtiqueta_Load(object sender, EventArgs e)
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            this.reportViewer1.RefreshReport();
        }
    }
}

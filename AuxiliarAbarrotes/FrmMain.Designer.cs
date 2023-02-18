namespace AuxiliarAbarrotes
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPrecios = new System.Windows.Forms.ToolStripButton();
            this.tsbEtiquetas = new System.Windows.Forms.ToolStripButton();
            this.tsbFacturas = new System.Windows.Forms.ToolStripButton();
            this.tsbConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrecios,
            this.tsbEtiquetas,
            this.tsbFacturas,
            this.tsbConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(642, 73);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPrecios
            // 
            this.tsbPrecios.AutoSize = false;
            this.tsbPrecios.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbPrecios.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrecios.Image")));
            this.tsbPrecios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPrecios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrecios.Name = "tsbPrecios";
            this.tsbPrecios.Size = new System.Drawing.Size(80, 70);
            this.tsbPrecios.Text = "Precios";
            this.tsbPrecios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPrecios.ToolTipText = "Precios";
            this.tsbPrecios.Click += new System.EventHandler(this.tsbPrecios_Click);
            // 
            // tsbEtiquetas
            // 
            this.tsbEtiquetas.AutoSize = false;
            this.tsbEtiquetas.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbEtiquetas.Image = ((System.Drawing.Image)(resources.GetObject("tsbEtiquetas.Image")));
            this.tsbEtiquetas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEtiquetas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEtiquetas.Name = "tsbEtiquetas";
            this.tsbEtiquetas.Size = new System.Drawing.Size(80, 70);
            this.tsbEtiquetas.Text = "Etiquetas";
            this.tsbEtiquetas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEtiquetas.ToolTipText = "Precios";
            this.tsbEtiquetas.Click += new System.EventHandler(this.tsbEtiquetas_Click);
            // 
            // tsbFacturas
            // 
            this.tsbFacturas.AutoSize = false;
            this.tsbFacturas.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbFacturas.Image = ((System.Drawing.Image)(resources.GetObject("tsbFacturas.Image")));
            this.tsbFacturas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFacturas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFacturas.Name = "tsbFacturas";
            this.tsbFacturas.Size = new System.Drawing.Size(80, 70);
            this.tsbFacturas.Text = "Facturas";
            this.tsbFacturas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFacturas.ToolTipText = "Precios";
            this.tsbFacturas.Click += new System.EventHandler(this.tsbFacturas_Click);
            // 
            // tsbConfig
            // 
            this.tsbConfig.AutoSize = false;
            this.tsbConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsbConfig.Image")));
            this.tsbConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbConfig.Name = "tsbConfig";
            this.tsbConfig.Size = new System.Drawing.Size(80, 70);
            this.tsbConfig.Text = "Configuración";
            this.tsbConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbConfig.ToolTipText = "Precios";
            this.tsbConfig.Click += new System.EventHandler(this.tsbConfig_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 279);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auxiliar Abarrotes";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPrecios;
        private System.Windows.Forms.ToolStripButton tsbEtiquetas;
        private System.Windows.Forms.ToolStripButton tsbFacturas;
        private System.Windows.Forms.ToolStripButton tsbConfig;
    }
}


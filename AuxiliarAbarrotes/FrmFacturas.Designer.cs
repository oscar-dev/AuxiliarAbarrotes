namespace AuxiliarAbarrotes
{
    partial class FrmFacturas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturas));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creado_en = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCAE = new System.Windows.Forms.TextBox();
            this.lbListaArticulos = new System.Windows.Forms.ListBox();
            this.ckbConsumidorFinal = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCUIT = new System.Windows.Forms.TextBox();
            this.ckbUltimos50 = new System.Windows.Forms.CheckBox();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(813, 53);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(120, 50);
            this.toolStripButton2.Text = "&Salir";
            this.toolStripButton2.ToolTipText = "Precios";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDatos);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(813, 405);
            this.splitContainer1.SplitterDistance = 570;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nombre,
            this.creado_en,
            this.cantidad,
            this.total});
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatos.Location = new System.Drawing.Point(0, 0);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.ShowEditingIcon = false;
            this.dgvDatos.ShowRowErrors = false;
            this.dgvDatos.Size = new System.Drawing.Size(570, 405);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellEnter);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 160;
            // 
            // creado_en
            // 
            this.creado_en.HeaderText = "Fecha Creac.";
            this.creado_en.Name = "creado_en";
            this.creado_en.ReadOnly = true;
            this.creado_en.Width = 120;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "Cant. Productos";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 120;
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            this.total.Width = 120;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFacturar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbCAE);
            this.panel1.Controls.Add(this.lbListaArticulos);
            this.panel1.Controls.Add(this.ckbConsumidorFinal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbCUIT);
            this.panel1.Controls.Add(this.ckbUltimos50);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 402);
            this.panel1.TabIndex = 1;
            // 
            // btnFacturar
            // 
            this.btnFacturar.Location = new System.Drawing.Point(28, 280);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(176, 33);
            this.btnFacturar.TabIndex = 8;
            this.btnFacturar.Text = "&Facturar";
            this.btnFacturar.UseVisualStyleBackColor = true;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "CAE";
            // 
            // tbCAE
            // 
            this.tbCAE.Location = new System.Drawing.Point(13, 341);
            this.tbCAE.Name = "tbCAE";
            this.tbCAE.ReadOnly = true;
            this.tbCAE.Size = new System.Drawing.Size(207, 21);
            this.tbCAE.TabIndex = 6;
            // 
            // lbListaArticulos
            // 
            this.lbListaArticulos.FormattingEnabled = true;
            this.lbListaArticulos.Location = new System.Drawing.Point(13, 114);
            this.lbListaArticulos.Name = "lbListaArticulos";
            this.lbListaArticulos.Size = new System.Drawing.Size(207, 160);
            this.lbListaArticulos.TabIndex = 5;
            // 
            // ckbConsumidorFinal
            // 
            this.ckbConsumidorFinal.AutoSize = true;
            this.ckbConsumidorFinal.Location = new System.Drawing.Point(59, 91);
            this.ckbConsumidorFinal.Name = "ckbConsumidorFinal";
            this.ckbConsumidorFinal.Size = new System.Drawing.Size(108, 17);
            this.ckbConsumidorFinal.TabIndex = 4;
            this.ckbConsumidorFinal.Text = "Consumidor Final";
            this.ckbConsumidorFinal.UseVisualStyleBackColor = true;
            this.ckbConsumidorFinal.CheckedChanged += new System.EventHandler(this.ckbConsumidorFinal_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "CUIT";
            // 
            // tbCUIT
            // 
            this.tbCUIT.Location = new System.Drawing.Point(13, 64);
            this.tbCUIT.Name = "tbCUIT";
            this.tbCUIT.Size = new System.Drawing.Size(207, 21);
            this.tbCUIT.TabIndex = 2;
            // 
            // ckbUltimos50
            // 
            this.ckbUltimos50.AutoSize = true;
            this.ckbUltimos50.Location = new System.Drawing.Point(42, 20);
            this.ckbUltimos50.Name = "ckbUltimos50";
            this.ckbUltimos50.Size = new System.Drawing.Size(148, 17);
            this.ckbUltimos50.TabIndex = 1;
            this.ckbUltimos50.Text = "Mostrar ultimos 50 tickets";
            this.ckbUltimos50.UseVisualStyleBackColor = true;
            this.ckbUltimos50.CheckedChanged += new System.EventHandler(this.ckbUltimos50_CheckedChanged);
            // 
            // FrmFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 458);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmFacturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFacturas";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFacturas_FormClosed);
            this.Load += new System.EventHandler(this.FrmFacturas_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn creado_en;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbUltimos50;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCAE;
        private System.Windows.Forms.ListBox lbListaArticulos;
        private System.Windows.Forms.CheckBox ckbConsumidorFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCUIT;
        private System.Windows.Forms.Button btnFacturar;
    }
}